using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using BL.QueryObject;
using BL.Services.Common;
using Game.DAL.Entity.Entities;
using Game.Infrastructure;
using Game.Infrastructure.Query;

namespace BL.Services.Fights
{
    public class FightService : CrudQueryServiceBase<Fight, FightDto, FightFilterDto>, IFightService
    {
        public FightService(IMapper mapper, IRepository<Fight> repository, QueryObjectBase<FightDto, Fight, FightFilterDto, IQuery<Fight>> query) : base(mapper, repository, query)
        {
        }

        protected override async Task<Fight> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Fight.Attacker), nameof(Fight.AttackerWeapon), nameof(Fight.AttackerArmor), nameof(Fight.Defender), nameof(Fight.DefenderWeapon), nameof(Fight.DefenderArmor));
        }

        public async Task<QueryResultDto<FightDto, FightFilterDto>> ListFightsAsync(FightFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }

        //public void RemoveFightCharacterConnections(int characterId)
        //{
        //    var attackerFights = ListFightsAsync(new FightFilterDto { AttackerId = characterId }).Result.Items;
        //    var defenderFights = ListFightsAsync(new FightFilterDto { DefenderId = characterId }).Result.Items;

        //    foreach (var a in attackerFights)
        //    {
        //        a.AttackerId = null;
        //        Update(a).Wait();
        //    }

        //    foreach (var d in defenderFights)
        //    {
        //        d.DefenderId = null;
        //        Update(d).Wait();
        //    }
            
        //}

    }
}
