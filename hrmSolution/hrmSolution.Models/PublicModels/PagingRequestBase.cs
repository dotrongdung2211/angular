using System;
using System.Collections.Generic;
using System.Text;

namespace hrmSolution.Models.PublicModels
{
    public class PagingRequestBase
    {
        public string KeyWord { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

    }
}
