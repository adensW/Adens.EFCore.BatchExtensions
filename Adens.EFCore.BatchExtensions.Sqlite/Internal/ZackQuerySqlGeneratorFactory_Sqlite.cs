#if (!NET7_0_OR_GREATER)
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace Adens.EFCore.BatchExtensions.Sqlite.Internal
{
    class AdensQuerySqlGeneratorFactory_Sqlite : IQuerySqlGeneratorFactory
    {
        private QuerySqlGeneratorDependencies dependencies;
        private ISqlGenerationHelper _sqlGenerationHelper;

        public AdensQuerySqlGeneratorFactory_Sqlite(QuerySqlGeneratorDependencies dependencies,
            ISqlGenerationHelper sqlGenerationHelper)
        {
            this.dependencies = dependencies;
            this._sqlGenerationHelper = sqlGenerationHelper;
        }
        public QuerySqlGenerator Create()
        {
            return new AdensQuerySqlGenerator_Sqlite(this.dependencies, this._sqlGenerationHelper);
        }
    }
}
#endif