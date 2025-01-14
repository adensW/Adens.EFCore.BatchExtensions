﻿using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Adens.EFCore.BatchExtensions.Internal
{
    public interface IAdensQuerySqlGenerator
    {
		public bool IsForBatchEF { get; set; }

		public List<string> ProjectionSQL
		{
			get;
		}

		/// <summary>
		/// the where clause
		/// </summary>
		public string PredicateSQL
		{
			get;
			set;
		}

		public IRelationalCommand GetCommand(SelectExpression selectExpression);

		public IRelationalCommandBuilder P_Sql { get; }

		public Expression Visit(Expression node);

		public void P_GenerateSetOperation(SetOperationBase setOperation);

		public void P_GenerateTop(SelectExpression selectExpression);

		public void P_GeneratePseudoFromClause();

		public void P_GenerateOrderings(SelectExpression selectExpression);

		public void P_GenerateLimitOffset(SelectExpression selectExpression);

		public string P_AliasSeparator { get; }
	}
}
