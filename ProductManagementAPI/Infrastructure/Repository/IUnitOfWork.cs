using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        #region Declaration
        /// <summary>
        /// Commit method to save changes in the database
        /// </summary>
        void Commit();

        #endregion
    }
}