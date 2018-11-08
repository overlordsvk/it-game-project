using System;

namespace Game.Infrastructure
{
    public interface IEntity
    {
        Guid Id { get; set; }

        string TableName { get; }
    }
}
