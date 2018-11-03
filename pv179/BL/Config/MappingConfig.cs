using AutoMapper;
using BL.DTO;
using Game.DAL.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO.Common;
using BL.DTO.Filters;
using Game.Infrastructure.Query;

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
            config.CreateMap<QueryResult<Account>, QueryResultDto<AccountDto, AccountFilterDto>>();
            config.CreateMap<QueryResult<Fight>, QueryResultDto<FightDto, FightFilterDto>>();
            config.CreateMap<QueryResult<Group>, QueryResultDto<GroupDto, GroupFilterDto>>();
            config.CreateMap<QueryResult<Item>, QueryResultDto<ItemDto, ItemFilterDto>>();
            config.CreateMap<QueryResult<Character>, QueryResultDto<CharacterDto, CharacterFilterDto>>();
            config.CreateMap<QueryResult<Chat>, QueryResultDto<MessageDto, MessageFilterDto>>();
            //config.CreateMap<Group, GroupInfoDto>.ForMember(groupDto => )
        }
    }
}
