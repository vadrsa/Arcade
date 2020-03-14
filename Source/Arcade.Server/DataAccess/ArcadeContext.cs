using BusinessEntities;
using Common.DataAccess;
using Common.Faults;
using LinqToDB;
using LinqToDB.Identity;
using LinqToDB.Mapping;
using System.Linq;

namespace DataAccess
{
    public class ArcadeContext : IdentityDataConnection<User, Role, string>
    {
        private static MappingSchema mappingSchema;

        public ArcadeContext() : base("Default")
        {

            if (mappingSchema == null)
                mappingSchema = InitContextMappings(this.MappingSchema);
            FluentMappingBuilder mb = MappingSchema.GetFluentMappingBuilder();

            MappingSchema.EntityDescriptorCreatedCallback = (mappingSchema, entityDescriptor) =>
            {
                if (!entityDescriptor.TypeAccessor.Type.IsAbstract)
                {
                    if (entityDescriptor.TypeAccessor.Type.IsSubclassOf(typeof(IDEntityBase<>)))
                    {
                        var idCol = entityDescriptor.Columns.Where(c => c.MemberName == "ID").FirstOrDefault();
                        if (idCol.MemberName == idCol.ColumnName)
                            idCol.ColumnName = entityDescriptor.TypeAccessor.Type.Name + "ID";

                    }

                }
            };
        }

        private static MappingSchema InitContextMappings(MappingSchema ms)
        {
            ms.GetFluentMappingBuilder()
                .Entity<IDEntityBase<int>>()
                .HasPrimaryKey(x => x.Id)
                .HasIdentity(x => x.Id);

            ms.GetFluentMappingBuilder()
                .Entity<IDEntityBase<string>>()
                .HasPrimaryKey(x => x.Id);
            return ms;

        }

        public ITable<Fault> Faults => GetTable<Fault>();
        public ITable<Employee> Employees => GetTable<Employee>();
        public ITable<Game> Games => GetTable<Game>();
        public ITable<Image> Images => GetTable<Image>();
        public ITable<SystemSetting> SystemSettings => GetTable<SystemSetting>();
        public ITable<ComputerType> ComputerTypes => GetTable<ComputerType>();
        public ITable<Computer> Computers => GetTable<Computer>();
        public ITable<Payment> Payments => GetTable<Payment>();
        public ITable<Session> Sessions => GetTable<Session>();
        public ITable<QueueNumberStorage> QueueNumberStorage => GetTable<QueueNumberStorage>();
        public ITable<EmployeeActivity> EmployeeActivity => GetTable<EmployeeActivity>();
        
    }
}
