using System;
using AShop_Models;

namespace AShop_Data.Repository.IRepository
{
    public interface IApplicationTypeRepository : IRepository<ApplicationType>
    {
        void Update(ApplicationType obj);
    }
}
