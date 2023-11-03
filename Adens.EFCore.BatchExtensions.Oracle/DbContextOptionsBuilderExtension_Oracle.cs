using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
#if (!NET7_0_OR_GREATER)
using Adens.EFCore.BatchExtensions.Oracle.Internal;
#endif
using Adens.EFCore.BatchExtensions_NET7;

namespace Adens.EFCore.BatchExtensions.Oracle
{
    public static class DbContextOptionsBuilderExtension_Oracle
    {
        public static DbContextOptionsBuilder UseBatchEF_Oracle(this DbContextOptionsBuilder optBuilder)
        {
#if (NET7_0_OR_GREATER)
            throw ExceptionHelpers.CreateBatchNotSupportException_InEF7();
#else
            optBuilder.ReplaceService<IQuerySqlGeneratorFactory, AdensQuerySqlGeneratorFactory_Oracle>();
            return optBuilder;
#endif
        }
    }
}
