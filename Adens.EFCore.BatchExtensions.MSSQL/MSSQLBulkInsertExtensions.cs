﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Adens.EFCore.BatchExtensions.Extensions;
using Adens.EFCore.BatchExtensions.Internal;

namespace System.Linq
{
    public static class MSSQLBulkInsertExtensions
    {
        public static async Task BulkInsertAsync<TEntity>(this DbContext dbCtx,
            IEnumerable<TEntity> items, SqlTransaction externalTransaction = null, SqlBulkCopyOptions copyOptions = SqlBulkCopyOptions.Default, CancellationToken cancellationToken = default, int? bulkCopyTimeoutInSecond=null) where TEntity : class
        {
            var conn = dbCtx.Database.GetDbConnection();
            await conn.OpenIfNeededAsync(cancellationToken);
            DataTable dataTable = BulkInsertUtils.BuildDataTable(dbCtx,dbCtx.Set<TEntity>(), items);
            using (SqlBulkCopy bulkCopy = BuildSqlBulkCopy<TEntity>((SqlConnection)conn, dbCtx,externalTransaction,copyOptions))
            {                
                if(bulkCopyTimeoutInSecond!=null)
                {
                    bulkCopy.BulkCopyTimeout = bulkCopyTimeoutInSecond.Value;
                }
                await bulkCopy.WriteToServerAsync(dataTable, cancellationToken);
            }
        }
        public static async Task BulkInsertAsync<TEntity>(this DbSet<TEntity> dbset,
           IEnumerable<TEntity> items, SqlTransaction externalTransaction = null, SqlBulkCopyOptions copyOptions = SqlBulkCopyOptions.Default, CancellationToken cancellationToken = default, int? bulkCopyTimeoutInSecond = null) where TEntity : class
        {
            var dbCtx = dbset.GetDbContext();
            var conn = dbCtx.Database.GetDbConnection();
            await conn.OpenIfNeededAsync(cancellationToken);
            DataTable dataTable = BulkInsertUtils.BuildDataTable(dbCtx, dbCtx.Set<TEntity>(), items);
            using (SqlBulkCopy bulkCopy = BuildSqlBulkCopy<TEntity>((SqlConnection)conn, dbCtx, externalTransaction, copyOptions))
            {
                if (bulkCopyTimeoutInSecond != null)
                {
                    bulkCopy.BulkCopyTimeout = bulkCopyTimeoutInSecond.Value;
                }
                await bulkCopy.WriteToServerAsync(dataTable, cancellationToken);
            }
        }
        private static SqlBulkCopy BuildSqlBulkCopy<TEntity>(SqlConnection conn,DbContext dbCtx,SqlTransaction externalTransaction, SqlBulkCopyOptions copyOptions) where TEntity : class
        {
            SqlBulkCopy bulkCopy = new SqlBulkCopy(conn,copyOptions,externalTransaction);
            var dbSet = dbCtx.Set<TEntity>();
            var entityType = dbSet.EntityType;
            var dbProps = BulkInsertUtils.ParseDbProps<TEntity>(dbCtx,entityType);
            bulkCopy.DestinationTableName = entityType.GetSchemaQualifiedTableName();//Schema may be used
            foreach (var dbProp in dbProps)
            {
                string columnName = dbProp.ColumnName;
                bulkCopy.ColumnMappings.Add(columnName, columnName);
            }
            return bulkCopy;
        }

        public static void BulkInsert<TEntity>(this DbContext dbCtx,
            IEnumerable<TEntity> items, SqlTransaction externalTransaction = null, SqlBulkCopyOptions copyOptions = SqlBulkCopyOptions.Default,int ? bulkCopyTimeoutInSecond = null) where TEntity : class
        {            
            var conn = dbCtx.Database.GetDbConnection();
            conn.OpenIfNeeded();
            DataTable dataTable = BulkInsertUtils.BuildDataTable(dbCtx, dbCtx.Set<TEntity>(), items);
            using (SqlBulkCopy bulkCopy = BuildSqlBulkCopy<TEntity>((SqlConnection)conn, dbCtx,externalTransaction,copyOptions))
            {
                if (bulkCopyTimeoutInSecond != null)
                {
                    bulkCopy.BulkCopyTimeout = bulkCopyTimeoutInSecond.Value;
                }
                bulkCopy.WriteToServer(dataTable);
            }
        }
        public static void BulkInsert<TEntity>(this DbSet<TEntity> dbset,
           IEnumerable<TEntity> items, SqlTransaction externalTransaction = null, SqlBulkCopyOptions copyOptions = SqlBulkCopyOptions.Default, int? bulkCopyTimeoutInSecond = null) where TEntity : class
        {
            var dbCtx = dbset.GetDbContext();
            var conn = dbCtx.Database.GetDbConnection();
            conn.OpenIfNeeded();
            DataTable dataTable = BulkInsertUtils.BuildDataTable(dbCtx, dbCtx.Set<TEntity>(), items);
            using (SqlBulkCopy bulkCopy = BuildSqlBulkCopy<TEntity>((SqlConnection)conn, dbCtx, externalTransaction, copyOptions))
            {
                if (bulkCopyTimeoutInSecond != null)
                {
                    bulkCopy.BulkCopyTimeout = bulkCopyTimeoutInSecond.Value;
                }
                bulkCopy.WriteToServer(dataTable);
            }
        }
    }
}
