using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Enums;

namespace _01_DataAccessLayer.Repository
{
    public class QueryOptions<T> where T : class
    {
        public Expression<Func<T, bool>>? Filter { get; set; }
        public Expression<Func<T, object>>? OrderBy { get; set; }
        public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public Expression<Func<T, object>>[] Includes { get; set; }
    }
}
