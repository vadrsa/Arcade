using BusinessEntities;
using Common.DataAccess;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using System.Linq;

namespace Repositories.LinqToDB
{
    public class AcradeDB : DataConnection
    {
        private static MappingSchema mappingSchema;

        public AcradeDB() : base("Default")
        {

            if (mappingSchema == null)
                mappingSchema = InitContextMappings(this.MappingSchema);
            FluentMappingBuilder mb = MappingSchema.GetFluentMappingBuilder();

            MappingSchema.EntityDescriptorCreatedCallback = (mappingSchema, entityDescriptor) =>
            {
                if (!entityDescriptor.TypeAccessor.Type.IsAbstract)
                {
                    if (entityDescriptor.TypeAccessor.Type.IsSubclassOf(typeof(IDEntityBase<int>)) || entityDescriptor.TypeAccessor.Type.IsSubclassOf(typeof(IDEntityBase<string>)))
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
                .HasPrimaryKey(x => x.ID)
                .HasIdentity(x => x.ID);

            ms.GetFluentMappingBuilder()
                .Entity<IDEntityBase<string>>()
                .HasPrimaryKey(x => x.ID);
            return ms;

        }

        public ITable<User> Users => GetTable<User>();
        public ITable<Role> Roles => GetTable<Role>();
        public ITable<Image> Images => GetTable<Image>();

    }
}