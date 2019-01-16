using AutoMapper;

namespace BL.Services.Common
{
    public abstract class ServiceBase
    {
        protected readonly IMapper Mapper;

        protected ServiceBase(IMapper mapper)
        {
            this.Mapper = mapper;
        }
    }
}