using System;
using System.Collections.Generic;
using System.Linq;
using AShop_Data.Repository.IRepository;
using AShop_Models;
using AShop_Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AShop_Data.Repository
{
    public class InquiryDetailsRepository : Repository<InquiryDetails>, IInquiryDetailsRepository
    {
        private readonly AshopDB _context;
        public InquiryDetailsRepository(AshopDB context): base(context)
        {
            _context = context;
        }

        public void Update(InquiryDetails obj)
        {
            _context.InquiryDetails.Update(obj);

        }
    }
}
