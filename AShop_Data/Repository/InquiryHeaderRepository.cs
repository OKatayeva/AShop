using System;
using System.Collections.Generic;
using System.Linq;
using AShop_Data.Repository.IRepository;
using AShop_Models;
using AShop_Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AShop_Data.Repository
{
    public class InquiryHeaderRepository : Repository<InquiryHeader>, IInquiryHeaderRepository
    {
        private readonly AshopDB _context;
        public InquiryHeaderRepository(AshopDB context): base(context)
        {
            _context = context;
        }

        public void Update(InquiryHeader obj)
        {
            _context.InquiryHeader.Update(obj);

        }
    }
}
