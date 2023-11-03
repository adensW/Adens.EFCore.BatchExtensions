﻿#if (!NET7_0_OR_GREATER)
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Adens.EFCore.BatchExtensions.Internal;

namespace Adens.EFCore.BatchExtensions.Npgsql.Internal
{
    class AdensQuerySqlGenerator_Npgsql : NpgsqlQuerySqlGenerator, IAdensQuerySqlGenerator
    {
		/// <summary>
		/// columns of the select statement
		/// </summary>
		private List<string> _projectionSQL = new List<string>();

		/// <summary>
		/// if IsForSingleTable=true, AdensQuerySqlGenerator will change the default behavior to capture PredicateSQL and so on.
		/// if IsForSingleTable=false, AdensQuerySqlGenerator will use all the implementations of base class
		/// </summary>
		public bool IsForBatchEF { get; set; }

		public List<string> ProjectionSQL
		{
			get
			{
				return this._projectionSQL;
			}
		}

		/// <summary>
		/// the where clause
		/// </summary>
		public string PredicateSQL
		{
			get;
			set;
		}

		private ISqlGenerationHelper _sqlGenerationHelper;
		public AdensQuerySqlGenerator_Npgsql(QuerySqlGeneratorDependencies dependencies, ISqlGenerationHelper sqlGenerationHelper, bool reverseNullOrderingEnabled, Version postgresVersion)
			: base(dependencies,reverseNullOrderingEnabled,postgresVersion)
		{
			this._sqlGenerationHelper = sqlGenerationHelper;
			this.IsForBatchEF = false;
		}


		protected override Expression VisitSelect(SelectExpression selectExpression)
		{
			if (!IsForBatchEF)
			{
				return base.VisitSelect(selectExpression);
			}
			return SqlGeneratorUtils.VisitSelect(this, this._sqlGenerationHelper, selectExpression);
		}

		protected override Expression VisitColumn(ColumnExpression columnExpression)
		{
			if (IsForBatchEF)
			{
				Sql.Append(_sqlGenerationHelper.DelimitIdentifier(columnExpression.Name));
				return columnExpression;
			}
			else
			{
				return base.VisitColumn(columnExpression);
			}
		}

		protected override Expression VisitTable(TableExpression tableExpression)
		{
			if (IsForBatchEF)
			{
				Sql.Append(_sqlGenerationHelper.DelimitIdentifier(tableExpression.Name));
				return tableExpression;
			}
			else
			{
				return base.VisitTable(tableExpression);
			}
		}

		public IRelationalCommandBuilder P_Sql => this.Sql;
		public string P_AliasSeparator => this.AliasSeparator;


		public void P_GenerateSetOperation(SetOperationBase setOperation)
		{
			this.GenerateSetOperation(setOperation);
		}

		public void P_GenerateTop(SelectExpression selectExpression)
		{
			this.GenerateTop(selectExpression);

		}

		public void P_GeneratePseudoFromClause()
		{
			this.GeneratePseudoFromClause();

		}

		public void P_GenerateOrderings(SelectExpression selectExpression)
		{
			this.GenerateOrderings(selectExpression);
		}

		public void P_GenerateLimitOffset(SelectExpression selectExpression)
		{
			this.GenerateLimitOffset(selectExpression);
		}
	}
}
#endif