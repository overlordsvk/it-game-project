﻿using BL.DTO;
using BL.Facades.Common;
using BL.Services.Characters;
using BL.Services.Chats;
using BL.Services.Messages;
using Game.Infrastructure.UnitOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class MessagingFacade : FacadeBase
    {
        private readonly ICharacterService _characterService;
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;


        public MessagingFacade(IUnitOfWorkProvider unitOfWorkProvider, ICharacterService characterService, IChatService chatService, IMessageService messageService) : base(unitOfWorkProvider)
        {
            _characterService = characterService;
            _chatService = chatService;
            _messageService = messageService;
        }


        public async Task<Guid> CreateChat(ChatDto chat)
        {
            using(var uow = UnitOfWorkProvider.Create())
            {
                if (!chat.SenderId.HasValue || !chat.ReceiverId.HasValue || string.IsNullOrWhiteSpace(chat.Subject))
                {
                    return Guid.Empty;
                }
                var sender = await _characterService.GetAsync(chat.SenderId.Value, withIncludes: false);
                var receiver = await _characterService.GetAsync(chat.ReceiverId.Value, withIncludes: false);
                if (sender == null || receiver == null)
                {
                    return Guid.Empty;
                }
                var chatId = _chatService.Create(chat);
                await uow.Commit();
                return chatId;
            }
        }

        public async Task<Guid> SendMessage(MessageDto message)
        {
            using(var uow = UnitOfWorkProvider.Create())
            {
                if (!message.AuthorId.HasValue)
                {
                    return Guid.Empty;
                }
                var sender = await _characterService.GetAsync(message.AuthorId.Value, withIncludes: false);
                var chat = await _chatService.GetAsync(message.ChatId, withIncludes: false);
                if(sender == null || chat == null)
                {
                    return Guid.Empty;
                }
                message.Timestamp = DateTime.Now;
                chat.LastMessageTimestamp = message.Timestamp;
                await _chatService.Update(chat);
                var messageId = _messageService.Create(message);
                await uow.Commit();
                return messageId;
            }
        }
    }



}