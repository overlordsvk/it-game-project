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
        string TableName { get; }
    }
}
