using System;
using System.Linq;
using AShop_Data.Repository.IRepository;
using AShop_Models;

namespace AShop_Data.Repository
{
    public class ApplicationTypeRepository : Repository<ApplicationType>, IApplicationTypeRepository
    {
        private readonly AshopDB _context;
        public ApplicationTypeRepository(AshopDB context): base(context)
        {
            _context = context;
        }
        public void Update(ApplicationType obj)
        {
            var objFromDb = base.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
            }
        }
    }
}
