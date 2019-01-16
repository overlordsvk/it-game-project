using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using Game.DAL.Entity.Entities;
using Game.Infrastructure.Query;
using Game.Infrastructure.Query.Predicates;
using Game.Infrastructure.Query.Predicates.Operators;
using System.Collections.Generic;

namespace BL.QueryObject
{
    public class ChatQueryObject : QueryObjectBase<ChatDto, Chat, ChatFilterDto, IQuery<Chat>>
    {
        public ChatQueryObject(IMapper mapper, IQuery<Chat> query) : base(mapper, query)
        {
        }

        protected override IQuery<Chat> ApplyWhereClause(IQuery<Chat> query, ChatFilterDto filter)
        {
            var predicate = new CompositePredicate(new List<IPredicate>
            {
                new SimplePredicate(nameof(Chat.ReceiverId), ValueComparingOperator.Equal, filter.CharacterId),
                new SimplePredicate(nameof(Chat.SenderId), ValueComparingOperator.Equal, filter.CharacterId)
            }, LogicalOperator.OR);
            return !filter.CharacterId.HasValue
                ? query
                : query.Where(predicate);
        }
    }
}