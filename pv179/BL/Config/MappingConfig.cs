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
using Game.DAL.Entities;
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
            config.CreateMap<GroupPost, GroupPostDto>().ReverseMap();
            config.CreateMap<Character, CharacterDto>().ReverseMap();
            config.CreateMap<Chat, ChatDto>().ReverseMap();
            config.CreateMap<Item, ItemDto>().ReverseMap();
            config.CreateMap<Message, MessageDto>().ReverseMap();
            config.CreateMap<QueryResult<Account>, QueryResultDto<AccountDto, AccountFilterDto>>();
            config.CreateMap<QueryResult<Fight>, QueryResultDto<FightDto, FightFilterDto>>();
            config.CreateMap<QueryResult<Group>, QueryResultDto<GroupDto, GroupFilterDto>>();
            config.CreateMap<QueryResult<Item>, QueryResultDto<ItemDto, ItemFilterDto>>();
            config.CreateMap<QueryResult<Character>, QueryResultDto<CharacterDto, CharacterFilterDto>>();
            config.CreateMap<QueryResult<Message>, QueryResultDto<MessageDto, MessageFilterDto>>();
            config.CreateMap<QueryResult<Chat>, QueryResultDto<ChatDto, CharacterFilterDto>>();
            config.CreateMap<QueryResult<GroupPost>, QueryResultDto<GroupPostDto, GroupFilterDto>>();
        }
    }
}
