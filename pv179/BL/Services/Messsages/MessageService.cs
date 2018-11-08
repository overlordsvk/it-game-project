using AutoMapper;
using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using BL.QueryObject;
using BL.Services.Common;
using Game.DAL.Entities;
using Game.Infrastructure;
using Game.Infrastructure.Query;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services.Messages
{
    public class MessageService : CrudQueryServiceBase<Message, MessageDto, MessageFilterDto>, IMessageService
    {
        public MessageService(IMapper mapper, IRepository<Message> repository, QueryObjectBase<MessageDto, Message, MessageFilterDto, IQuery<Message>> query) : base(mapper, repository, query)
        {
        }

        public async Task<QueryResultDto<MessageDto, MessageFilterDto>> ListMessagesAsync(MessageFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }

        protected override async Task<Message> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Message.Author), nameof(Message.Chat));
        }
    }
}
