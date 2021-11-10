using BL.DTO.Common;

namespace BL.DTO.Filters
{
    public class AccountFilterDto : FilterDtoBase
    {
        public string Email { get; set; }
        public string Username { get; set; }
    }
}