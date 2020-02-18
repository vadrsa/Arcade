using SharedEntities;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Threading.Tasks;
using Facade.Repositories;
using Common.Faults;
using BusinessEntities;
using System.Linq;
using Facade.Managers;

namespace Managers.Implementation
{
    public class SystemSettingManager : ManagerBase, ISystemSettingManager
    {
        public SystemSettingManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task SetSetting(SystemSettingDto settingDto)
        {
            if (settingDto.Setting == SystemSettingType.Undefined)
                throw new FaultException(FaultType.BadRequest, "Couldn't find system setting 'Undefined'");
            var old = await ServiceProvider.GetService<ISystemSettingRepository>().FindByIDAsync((int)settingDto.Setting);
            Type settingSystemType = Type.GetType(old.Type);
            object objectOfCorrectType = null;
            try
            {
                objectOfCorrectType = Convert.ChangeType(settingDto.Value, settingSystemType);
            }
            catch(Exception ex)
            {
                throw new FaultException(FaultType.BadRequest, $"Setting value was in an incorrect format. Couldn't cast to {settingSystemType.Name}");
            }
            var setting = new BusinessEntities.SystemSetting {
                Id = (int)settingDto.Setting,
                Name = old.Name,
                Value = objectOfCorrectType.ToString(),
                Type = old.Type
            };
            await ServiceProvider.GetService<ISystemSettingRepository>().UpdateAsync(setting);
        }

        public async Task<SystemSettingDto> GetSetting(int id)
        {
            return GetDto(await ServiceProvider.GetService<ISystemSettingRepository>().FindByIDAsync(id));
        }

        public async Task<List<SystemSettingDto>> GetAllSettings()
        {
            return (await ServiceProvider.GetService<ISystemSettingRepository>().GetAllAsync()).Select(s => GetDto(s)).ToList();
        }

        private SystemSettingDto GetDto(SystemSetting setting)
        {
            return new SystemSettingDto
            {
                Setting = (SystemSettingType)setting.Id,
                Name = setting.Name,
                Value = setting.Value
            };
        }
    }
}
