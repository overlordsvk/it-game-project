﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using BL.Facades.Common;
using BL.Services.Characters;
using BL.Services.GroupPosts;
using BL.Services.Groups;
using Game.Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class GroupFacade : FacadeBase
    {
        private readonly IGroupService _groupService;
        private readonly IGroupPostService _groupPostService;
        private readonly ICharacterService _characterService;

        public GroupFacade(IUnitOfWorkProvider unitOfWorkProvider, IGroupService groupService, IGroupPostService groupPostService, ICharacterService characterService) : base(unitOfWorkProvider)
        {
            _groupService = groupService;
            _groupPostService = groupPostService;
            _characterService = characterService;
        }

        public async Task<int> CreateGroup(int groupFounder,string name, string description, string imagePath)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var founder = _characterService.GetAsync(groupFounder).Result;
                if (founder == null)
                {
                    return -1;
                }
                if (founder.Group != null)
                {
                    return -2;
                }
                founder.Group = new GroupDto
                {
                    Name = name,
                    Description = description,
                    Picture = imagePath,
                    Members = new List<CharacterDto> {founder}
                };
                var groupId = _groupService.Create(founder.Group);
                await _characterService.Update(founder);
                await uow.Commit();
                return groupId;
            }
        }

        public async void RemoveGroup(int groupId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var group =_groupService.GetAsync(groupId).Result;
                if (group == null)
                    return;
                foreach (var member in group.Members)
                {
                    member.Group = null;
                    await _characterService.Update(member);
                }
                _groupService.Delete(groupId);
                await uow.Commit();
            }
        }

        public async void EditDescription(int groupId, string newDescription)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var group =_groupService.GetAsync(groupId).Result;
                if (group == null)
                    return;
                group.Description = newDescription;
                await _groupService.Update(group);
                await uow.Commit();
            }
        }

        public async Task<QueryResultDto<GroupDto, GroupFilterDto>> GetAllGroupsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _groupService.ListAllAsync();
            }
        }

        public async Task<QueryResultDto<GroupDto, GroupFilterDto>> GetGroupsByFilterAsync(GroupFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _groupService.ListFightsAsync(filter);
            }
        }

        public async Task<int> AddToGroup(int characterId, int groupId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var group =_groupService.GetAsync(groupId).Result;
                var character = _characterService.GetAsync(characterId).Result;
                if (group == null || character == null)
                    return -1;
                if (character.Group != null)
                    return -2;
                group.Members.Add(character);
                character.Group = group;
                await _characterService.Update(character);
                await _groupService.Update(group);
                await uow.Commit();
                return 1;
            }
        }

        public async Task<int> RemoveFromGroup(int characterId, int groupId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var group =_groupService.GetAsync(groupId).Result;
                var character = _characterService.GetAsync(characterId).Result;
                if (group == null || character == null)
                    return -1;
                if (character.Group != null)
                    return -2;
                if (!group.Members.Contains(character))
                {
                    return -3;
                }
                group.Members.Remove(character);
                character.Group = null;
                await _characterService.Update(character);
                await _groupService.Update(group);
                await uow.Commit();
                return 1;
            }
        }


        //post in clan forum
        //get posts of clan
        //remove post in clan
        //edit post in clan
    }
}