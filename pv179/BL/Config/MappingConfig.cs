using AutoMapper;
using BL.DTO;
using Game.DAL.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Config
{
    public static class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Account, AccountDto>().ReverseMap();
            config.CreateMap<Account, AccountCreateDto>().ReverseMap();
            config.CreateMap<Fight, FightDto>().ReverseMap();
            config.CreateMap<Group, GroupDto>().ReverseMap();
            //config.CreateMap<Group, GroupInfoDto>.ForMember(groupDto => )
        }
    }
}
