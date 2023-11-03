using Microsoft.EntityFrameworkCore;
using System;

namespace Adens.EFCore.BatchExtensions.InMemory
{
    internal static class InMemoryHelper
    {
        public static void AssertIsInMemory(this DbContext ctx)
        {
            if(!ctx.Database.IsInMemory())
            {
                throw new InvalidOperationException("only supported on InMemory");
            }
        }
    }
}
