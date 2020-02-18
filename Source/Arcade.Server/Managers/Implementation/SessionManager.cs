using BusinessEntities;
using Common.Core;
using Common.Faults;
using DataAccess;
using Facade.Managers;
using Facade.Repositories;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Managers.Implementation
{
    public class SessionManager : ManagerBase, ISessionManager
    {
        public SessionManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task<SessionDto> Create(SessionUploadDto session, CancellationToken token = default)
        {
            var currentItem = await GetCurrent(session.ComputerId);
            if (currentItem != null)
                throw new FaultException(FaultType.BadRequest, "A session is currently open on the computer, try adding the cleint to queue");
            var toAdd = Mapper.Map<Session>(session);
            toAdd.StartDate = DateTime.UtcNow;
            toAdd.PaymentId = await CreatePayment(session.PaymentDue);
            return Mapper.Map<SessionDto>(await ServiceProvider.GetService<ISessionRepository>().InsertAsync(toAdd, token));
        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task EndSession(string id)
        {
            var session = await GetCurrent(id);
            session.EndDate = DateTime.UtcNow;
            await ServiceProvider.GetService<ISessionRepository>().UpdateAsync(session);
        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task<SessionDto> AddToQueue(SessionUploadDto session, CancellationToken token = default)
        {
            var currentItem = await GetCurrent(session.ComputerId);
            if (currentItem == null)
                throw new FaultException(FaultType.BadRequest, "There are no active sessions on this computer, try starting a new session");
            var toAdd = Mapper.Map<Session>(session);
            toAdd.StartDate = DateTime.MinValue;
            toAdd.QueueDate = DateTime.UtcNow;
            var latestNumber = (await ServiceProvider.GetService<ArcadeContext>().QueueNumberStorage.OrderBy(p => p.Id).ToListAsync()).Last().LatestNumber;
            toAdd.QueueNumber = (latestNumber + 1) == 1000? 101: latestNumber + 1;
            toAdd.PaymentId = await CreatePayment(session.PaymentDue);
            await ServiceProvider.GetService<ArcadeContext>().InsertWithInt32IdentityAsync(new QueueNumberStorage { LatestNumber = toAdd.QueueNumber });
            var sessionDto = await ServiceProvider.GetService<ISessionRepository>().InsertAsync(toAdd, token);
            return Mapper.Map<SessionDto>(await ServiceProvider.GetService<ISessionRepository>().LoadWith(c => c.Computer).LoadWith(c => c.Computer.Type).FindByIDAsync(sessionDto.Id));
        }

        public async Task<List<ComputerQueueDto>> GetAllComputers()
        {
            var computers = await ServiceProvider.GetService<IComputerRepository>().LoadWith(c => c.Type).GetAllAsync();
            var res = new List<ComputerQueueDto>();
            foreach (var computer in computers)
            {
                var dto = Mapper.Map<ComputerQueueDto>(computer);
                var queue = await GetQueue(computer.Id);
                var current = await GetCurrent(computer.Id);
                dto.Queue = Mapper.Map<List<SessionDto>>(queue);
                dto.Current = Mapper.Map<SessionDto>(current);

                res.Add(dto);
            }
            return res;
        }

        public async Task<ComputerQueueDto> GetComputerById(string computerId)
        {
            var computer = await ServiceProvider.GetService<IComputerRepository>().LoadWith(c => c.Type).FindByIDAsync(computerId);
            var dto = Mapper.Map<ComputerQueueDto>(computer);
            var queue = await GetQueue(computer.Id);
            var current = await GetCurrent(computerId);
            dto.Queue = Mapper.Map<List<SessionDto>>(queue);
            dto.Current = Mapper.Map<SessionDto>(current);
            return dto;
        }

        public async Task<ComputerQueueDto> GetFullComputerById(string computerId)
        {
            var computer = await ServiceProvider.GetService<IComputerRepository>().LoadWith(c => c.Type).FindByIDAsync(computerId);
            var dto = Mapper.Map<ComputerQueueDto>(computer);
            var queue = await GetQueue(computer.Id);
            var current = await GetCurrent(computerId);
            if(current == null && queue.Count > 0)
            {
                var firstInQueue = queue.OrderBy(s => s.QueueDate).ThenBy(s => s.QueueNumber).First();
                firstInQueue.StartDate = DateTime.UtcNow;
                await ServiceProvider.GetService<ISessionRepository>().UpdateAsync(firstInQueue);
                queue = await GetQueue(computer.Id);
                current = await GetCurrent(computerId);
            }
            dto.Queue = Mapper.Map<List<SessionDto>>(queue);
            dto.Current = Mapper.Map<SessionDto>(current);
            return dto;
        }

        private async Task<Session> GetCurrent(string computerId)
        {
            return (await ServiceProvider.GetService<ISessionRepository>().GetComputerSessionsAsync(computerId))
                            .Where(s => s.State == BusinessEntities.SessionState.Current)
                            .SingleOrDefault();
        }

        private async Task<List<Session>> GetQueue(string computerId)
        {
            return (await ServiceProvider.GetService<ISessionRepository>().GetComputerSessionsAsync(computerId))
                            .Where(s => s.State == BusinessEntities.SessionState.InQueue)
                            .ToList();
        }

        private async Task<string> CreatePayment(float amount)
        {
            Payment payment = new Payment {
                Amount = amount,
                Date = DateTime.UtcNow,
                EmployeeId = GetCurrentUserId()
            };
            return (await ServiceProvider.GetService<IPaymentRepository>().InsertAsync(payment)).Id;
        }
    }
}
