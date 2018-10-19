using DynamicRepository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DAL.Entity.Entities
{
    public interface IEntity : IIdentifiableEntity
    {
        new int Id { get; set; }

        string TableName { get; }
    }
}
