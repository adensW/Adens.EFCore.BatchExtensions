using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
#if (!NET7_0_OR_GREATER)
using Adens.EFCore.BatchExtensions.Oracle.Internal;
#endif
using Adens.EFCore.BatchExtensions_NET7;

namespace Microsoft.EntityFrameworkCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBatchEF_Oracle(this IServiceCollection services)
        {
#if (NET7_0_OR_GREATER)
            throw ExceptionHelpers.CreateBatchNotSupportException_InEF7();
#else
            return services.AddScoped<IQuerySqlGeneratorFactory, AdensQuerySqlGeneratorFactory_Oracle>();
#endif
        }
    }
}
