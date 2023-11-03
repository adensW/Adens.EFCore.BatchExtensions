#if (!NET7_0_OR_GREATER)
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Oracle.EntityFrameworkCore.Infrastructure.Internal;

namespace Adens.EFCore.BatchExtensions.Oracle.Internal
{
    class AdensQuerySqlGeneratorFactory_Oracle : IQuerySqlGeneratorFactory
    {
        private ISqlGenerationHelper _sqlGenerationHelper;
        private readonly QuerySqlGeneratorDependencies _dependencies;

        private readonly IOracleOptions _oracleOptions;

        public AdensQuerySqlGeneratorFactory_Oracle(QuerySqlGeneratorDependencies dependencies,
            ISqlGenerationHelper sqlGenerationHelper, IOracleOptions oracleOptions)
        {
            this._dependencies = dependencies;
            this._sqlGenerationHelper = sqlGenerationHelper;
            this._oracleOptions = oracleOptions;
        }
        public QuerySqlGenerator Create()
        {
            return new AdensQuerySqlGenerator_Oracle(this._dependencies, this._oracleOptions, this._sqlGenerationHelper);
        }
    }
}
#endif