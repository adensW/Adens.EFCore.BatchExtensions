﻿using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Adens.EFCore.BatchExtensions_NET7;

namespace Microsoft.EntityFrameworkCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBatchEF_MSSQL(this IServiceCollection services)
        {
#if (NET7_0_OR_GREATER)
            throw ExceptionHelpers.CreateBatchNotSupportException_InEF7();
#else
            return services.AddScoped<IQuerySqlGeneratorFactory, Adens.EFCore.BatchExtensions.MSSQL.Internal.AdensQuerySqlGeneratorFactory_MSSQL>();
#endif
        }
    }
}
