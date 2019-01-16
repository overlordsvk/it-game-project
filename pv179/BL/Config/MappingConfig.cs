using AutoMapper;
using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using Game.DAL.Entities;
using Game.DAL.Entity.Entities;
using Game.Infrastructure.Query;

namespace BL.Config
{
    public static class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Account, AccountDto>();
            config.CreateMap<AccountDto, Account>();

            config.CreateMap<Account, AccountCreateDto>().ReverseMap();

            config.CreateMap<Fight, FightDto>();
            config.CreateMap<FightDto, Fight>().ForMember(dest => dest.Attacker, opt => opt.Ignore())
                                                .ForMember(dest => dest.Defender, opt => opt.Ignore())
                                                .ForMember(dest => dest.DefenderArmor, opt => opt.Ignore())
                                                .ForMember(dest => dest.DefenderWeapon, opt => opt.Ignore())
                                                .ForMember(dest => dest.AttackerWeapon, opt => opt.Ignore())
                                                .ForMember(dest => dest.AttackerArmor, opt => opt.Ignore());

            config.CreateMap<Group, GroupDto>();
            config.CreateMap<GroupDto, Group>().ForMember(dest => dest.Members, opt => opt.Ignore())
                                                .ForMember(dest => dest.Wall, opt => opt.Ignore());

            config.CreateMap<GroupPost, GroupPostDto>();
            config.CreateMap<GroupPostDto, GroupPost>().ForMember(dest => dest.Author, opt => opt.Ignore())
                                                        .ForMember(dest => dest.Group, opt => opt.Ignore());

            config.CreateMap<Character, CharacterDto>();
            config.CreateMap<CharacterDto, Character>().ForMember(dest => dest.Account, opt => opt.Ignore())
                                                        .ForMember(dest => dest.AttackerFights, opt => opt.Ignore())
                                                        .ForMember(dest => dest.DefenderFights, opt => opt.Ignore())
                                                        .ForMember(dest => dest.Items, opt => opt.Ignore())
                                                        .ForMember(dest => dest.Group, opt => opt.Ignore())
                                                        .ForMember(dest => dest.ReceiverChats, opt => opt.Ignore())
                                                        .ForMember(dest => dest.SenderChats, opt => opt.Ignore());

            config.CreateMap<Chat, ChatDto>();
            config.CreateMap<ChatDto, Chat>().ForMember(dest => dest.Messages, opt => opt.Ignore())
                                            .ForMember(dest => dest.Receiver, opt => opt.Ignore())
                                            .ForMember(dest => dest.Sender, opt => opt.Ignore());

            config.CreateMap<Item, ItemDto>();
            config.CreateMap<ItemDto, Item>().ForMember(dest => dest.Owner, opt => opt.Ignore());

            config.CreateMap<Message, MessageDto>();
            config.CreateMap<MessageDto, Message>().ForMember(dest => dest.Author, opt => opt.Ignore())
                                                    .ForMember(dest => dest.Chat, opt => opt.Ignore());

            config.CreateMap<QueryResult<Account>, QueryResultDto<AccountDto, AccountFilterDto>>();
            config.CreateMap<QueryResult<Fight>, QueryResultDto<FightDto, FightFilterDto>>();
            config.CreateMap<QueryResult<Group>, QueryResultDto<GroupDto, GroupFilterDto>>();
            config.CreateMap<QueryResult<Item>, QueryResultDto<ItemDto, ItemFilterDto>>();
            config.CreateMap<QueryResult<Character>, QueryResultDto<CharacterDto, CharacterFilterDto>>();
            config.CreateMap<QueryResult<Message>, QueryResultDto<MessageDto, MessageFilterDto>>();
            config.CreateMap<QueryResult<Chat>, QueryResultDto<ChatDto, ChatFilterDto>>();
            config.CreateMap<QueryResult<GroupPost>, QueryResultDto<GroupPostDto, GroupFilterDto>>();
        }
    }
}