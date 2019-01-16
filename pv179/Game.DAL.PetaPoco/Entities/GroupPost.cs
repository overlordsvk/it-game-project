using AsyncPoco;
using Game.Infrastructure;
using System;

namespace Game.DAL.PetaPoco.Entities
{
    [TableName(TableNames.GroupPostTable)]
    [PrimaryKey("Id", autoIncrement = false)]
    public class GroupPost : IEntity
    {
        public Guid Id { get; set; }

        public Guid? CharacterId { get; set; }

        [Ignore]
        public Character Author { get; set; }

        public string Text { get; set; }

        public Guid GroupId { get; set; }

        [Ignore]
        public Group Group { get; set; }

        public DateTime Timestamp { get; set; }

        [Ignore]
        public string TableName { get; } = TableNames.GroupPostTable;
    }
}