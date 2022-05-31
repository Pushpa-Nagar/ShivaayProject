using ProductManagementAPI.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShivaayProductDBContext DbContext;
        public UnitOfWork(ShivaayProductDBContext dbContext)
        {
            DbContext = dbContext;
        }

        #region Implementation

        /// <summary>
        /// Method to save changes in the database
        /// </summary>
        public void Commit()
        {
            try
            {
                DbContext.SaveChanges();
            }
            catch
            {
                throw;
            }

        }

        #endregion

    }
}
