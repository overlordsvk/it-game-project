using BL.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO.Filters
{
    public class AccountFilterDto : FilterDtoBase
    {
        public string Email { get; set; }
        public string Usermane { get; set; }

    }
}
