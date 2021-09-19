using System;
using System.Collections.Generic;

namespace AShop_Models.ViewModels
{
    public class InquiryVM
    {
        public InquiryHeader InquiryHeader { get; set; }

        public IEnumerable<InquiryDetails> InquiryDetails { get; set; }

    }
}
