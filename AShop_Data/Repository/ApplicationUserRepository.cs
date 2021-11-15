using System;
using System.Linq;
using AShop_Data.Repository.IRepository;
using AShop_Models;

namespace AShop_Data.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly AshopDB _context;
        public ApplicationUserRepository(AshopDB context): base(context)
        {
            _context = context;
        }
       
    }
}
