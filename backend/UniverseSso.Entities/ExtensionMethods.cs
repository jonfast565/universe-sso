using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace UniverseSso.Entities
{
    public static class ExtensionMethods
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
