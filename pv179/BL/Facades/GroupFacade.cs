using BL.DTO;
using BL.DTO.Common;
using BL.DTO.Filters;
using BL.Facades.Common;
using BL.Services.Characters;
using BL.Services.GroupPosts;
using BL.Services.Groups;
using Game.Infrastructure.UnitOfWork;
using System;
using System.Threading.Tasks;

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

        public async Task<Guid> CreateGroup(Guid groupFounder, GroupDto group, bool isAdmin = false)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (isAdmin) 
                {
                    var emptyGroup = _groupService.Create(group);
                    await uow.Commit();
                    return emptyGroup;
                }

                var founder = await _characterService.GetAsync(groupFounder);
                if (founder == null)
                {
                    return Guid.Empty;
                }
                if (founder.Group != null)
                {
                    return Guid.Empty;
                }

                founder.IsGroupAdmin = true;
                var groupId = _groupService.Create(group);
                founder.GroupId = groupId;
                await _characterService.Update(founder);
                await uow.Commit();
                return groupId;
            }
        }

        public async Task RemoveGroup(Guid groupId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var group = await _groupService.GetAsync(groupId);
                if (group == null)
                    return;
                foreach (var member in group.Members)
                {
                    member.Group = null;
                    member.IsGroupAdmin = false;
                    await _characterService.Update(member);
                }
                _groupService.Delete(groupId);
                await uow.Commit();
            }
        }

        public async Task Edit(GroupDto group)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
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

        public async Task<GroupDto> GetGroupAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _groupService.GetAsync(id);
            }
        }

        public async Task<QueryResultDto<GroupDto, GroupFilterDto>> GetGroupsByFilterAsync(GroupFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _groupService.ListGroupsAsync(filter);
            }
        }

        public async Task<int> AddToGroup(Guid characterId, Guid groupId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var group = await _groupService.GetAsync(groupId);
                var character = await _characterService.GetAsync(characterId);
                if (group == null || character == null)
                    return -1;
                if (character.Group != null)
                    return -2;
                group.Members.Add(character);
                character.Group = group;
                character.GroupId = group.Id;
                await _characterService.Update(character);
                await _groupService.Update(group);
                await uow.Commit();
                return 1;
            }
        }

        public async Task<bool> RemoveFromGroup(Guid characterId, Guid groupId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var group = await _groupService.GetAsync(groupId);
                var character = await _characterService.GetAsync(characterId);
                if (group == null || character == null)
                    return false;
                group.Members.Remove(character);
                character.Group = null;
                character.GroupId = null;
                character.IsGroupAdmin = false;
                await _characterService.Update(character);
                await _groupService.Update(group);
                if (group.Members.Count <= 1) 
                {
                    _groupService.Delete(groupId);
                }
                await uow.Commit();
                return true;
            }
        }

        public async Task<QueryResultDto<GroupPostDto, GroupPostFilterDto>> GetGroupPostsAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var posts = await _groupPostService.ListGroupPostsAsync(new GroupPostFilterDto { GroupId = id, PageSize = 20, SortAscending = true, SortCriteria = "Timestamp" });
                return posts;
            }
        }

        public async Task CreatePost(GroupPostDto groupPost)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var post = _groupPostService.Create(groupPost);
                await uow.Commit();
            }
        }

        public async void EditPost(GroupPostDto groupPost)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                await _groupPostService.Update(groupPost);
                await uow.Commit();
            }
        }

        public async void DeletePost(Guid groupPostId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                _groupPostService.Delete(groupPostId);
                await uow.Commit();
            }
        }
    }
}