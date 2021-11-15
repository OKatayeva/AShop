using System;
using System.Linq;
using AShop_Data.Repository.IRepository;
using AShop_Models;

namespace AShop_Data.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly AshopDB _context;
        public BrandRepository(AshopDB context): base(context)
        {
            _context = context;
        }
        public void Update(Brand obj)
        {
            var objFromDb = base.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.BrandName = obj.BrandName;
            }
        }
    }
}
