using System;
using System.Collections.Generic;
using AShop_Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AShop_Data.Repository.IRepository
{
    public interface IInquiryHeaderRepository : IRepository<InquiryHeader>
    {
        void Update(InquiryHeader obj);
        
    }
}
