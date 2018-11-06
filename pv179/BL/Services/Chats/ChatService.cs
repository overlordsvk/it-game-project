using AutoMapper;
using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using BL.QueryObject;
using BL.Services.Common;
using Game.DAL.Entity.Entities;
using Game.Infrastructure;
using Game.Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Chats
{
    public class ChatService : CrudQueryServiceBase<Chat, ChatDto, ChatFilterDto>, IChatService
    {
        public ChatService(IMapper mapper, IRepository<Chat> repository, QueryObjectBase<ChatDto, Chat, ChatFilterDto, IQuery<Chat>> query) : base(mapper, repository, query)
        {
        }

        public async Task<QueryResultDto<ChatDto, ChatFilterDto>> ListChatsAsync(ChatFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }

        public void RemoveReferencesToCharacterAsync(int characterId)
        {
            var chats = ListChatsAsync(new ChatFilterDto { CharacterId = characterId}).Result;
            foreach (var chat in chats.Items)
            {
                if (chat.ReceiverId == characterId)
                    chat.ReceiverId = null;
                if (chat.SenderId == characterId)
                    chat.SenderId = null;
                Update(chat).Wait();
            }
        }

        protected override async Task<Chat> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Chat.Messages), nameof(Chat.Receiver), nameof(Chat.Sender));
        }

    }
}
