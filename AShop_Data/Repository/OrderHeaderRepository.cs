using System;
using System.Collections.Generic;
using System.Linq;
using AShop_Data.Repository.IRepository;
using AShop_Models;
using AShop_Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AShop_Data.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly AshopDB _context;
        public OrderHeaderRepository(AshopDB context): base(context)
        {
            _context = context;
        }

        public void Update(OrderHeader obj)
        {
            _context.OrderHeader.Update(obj);

        }
    }
}
