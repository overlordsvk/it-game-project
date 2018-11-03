using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using Game.DAL.Entities;
using Game.Infrastructure.Query;
using Game.Infrastructure.Query.Predicates;
using Game.Infrastructure.Query.Predicates.Operators;

namespace BL.QueryObject
{
    public class MessageQueryObject : QueryObjectBase<MessageDto, Message, MessageFilterDto, IQuery<Message>>
    {
        public MessageQueryObject(IMapper mapper, IQuery<Message> query) : base(mapper, query)
        {
        }

        protected override IQuery<Message> ApplyWhereClause(IQuery<Message> query, MessageFilterDto filter)
        {
            return !filter.ChatId.HasValue
                ? query
                : query.Where(new SimplePredicate(nameof(Message.ChatId),
                    ValueComparingOperator.Equal,
                    filter.ChatId.Value));
        }
    }
}
