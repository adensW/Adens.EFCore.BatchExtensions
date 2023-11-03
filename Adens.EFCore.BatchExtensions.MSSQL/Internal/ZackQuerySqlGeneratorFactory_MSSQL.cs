#if (!NET7_0_OR_GREATER)
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace Adens.EFCore.BatchExtensions.MSSQL.Internal
{
    class AdensQuerySqlGeneratorFactory_MSSQL : IQuerySqlGeneratorFactory
    {
        private QuerySqlGeneratorDependencies dependencies;

        private ISqlGenerationHelper _sqlGenerationHelper;

        public AdensQuerySqlGeneratorFactory_MSSQL(QuerySqlGeneratorDependencies dependencies, ISqlGenerationHelper sqlGenerationHelper)
        {
            this.dependencies = dependencies;
            this._sqlGenerationHelper = sqlGenerationHelper;
        }
        public QuerySqlGenerator Create()
        {
            return new AdensQuerySqlGenerator_MSSQL(this.dependencies, this._sqlGenerationHelper);
        }
    }
}
#endif