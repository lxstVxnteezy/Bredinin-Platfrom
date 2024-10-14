using FluentMigrator.Runner.VersionTableInfo;

namespace Bredinin.TestProject.Service.DataContext.Migration.Metadata
{
    [VersionTableMetaData]
    public class VersionTableMetaData : DefaultVersionTableMetaData
    {
        public override string TableName => "_migration_metadata";
    }
}
