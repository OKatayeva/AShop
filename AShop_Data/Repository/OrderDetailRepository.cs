using System;
using System.Collections.Generic;
using System.Linq;
using AShop_Data.Repository.IRepository;
using AShop_Models;
using AShop_Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AShop_Data.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly AshopDB _context;
        public OrderDetailRepository(AshopDB context): base(context)
        {
            _context = context;
        }

        public void Update(OrderDetail obj)
        {
            _context.OrderDetail.Update(obj);

        }
    }
}
