using System;
using System.Linq;
using AShop_Data.Repository.IRepository;
using AShop_Models;

namespace AShop_Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AshopDB _context;
        public CategoryRepository(AshopDB context): base(context)
        {
            _context = context;
        }
        public void Update(Category obj)
        {
            var objFromDb = base.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.DisplayOrder = obj.DisplayOrder;
            }
        }
    }
}
