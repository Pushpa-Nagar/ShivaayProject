using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Extensions
{
    public static class DataTypeExtensions
    {
        public static List<T> GetCurrentPageList<T>(this IEnumerable<T> allRecordsList, int pageSize, int skipRecods)
        {
            List<T> currentPageRecordList = null;
            if (allRecordsList != null)
            {
                if (pageSize > 0)
                {
                    if (skipRecods > 1)
                    {
                        currentPageRecordList = allRecordsList.Skip(skipRecods).Take(pageSize).ToList();
                    }
                    else
                    {
                        currentPageRecordList = allRecordsList.Take(pageSize).ToList();
                    }
                }
                else
                {
                    currentPageRecordList = allRecordsList.ToList();
                }
            }
            if (currentPageRecordList == null)
                currentPageRecordList = new List<T>();

            return currentPageRecordList;
        }
    }
}

