using BL.DTO;
using BL.Facades;
using BL.Services.Chats;
using GameWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GameWebMVC.Controllers
{
    [Authorize(Roles = "HasCharacter")]
    public class MessagingController : Controller
    {
        public MessagingFacade MessagingFacade { get; set; }
        public CharacterFacade CharacterFacade { get; set; }


        public async Task<ActionResult> MailBox()
        {
            var character = await CharacterFacade.GetCharacterById(Guid.Parse(User.Identity.Name));
            var chats = new List<ChatDto>();
            chats.AddRange(character.SenderChats);
            chats.AddRange(character.ReceiverChats);
            chats = chats.OrderByDescending(x => x.LastMessageTimestamp).ToList();
            return View(chats);
        }

        public ActionResult NewChat()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> NewChat(ChatCreate chat)
        {
            try
            {
                var receiver = await CharacterFacade.GetCharacterAccordingToNameAsync(chat.ReceiverName);
                if (receiver == null)
                {
                    return View(chat);
                }
                var id = Guid.Parse(User.Identity.Name);

                var chatDto = new ChatDto
                {
                    SenderId = id,
                    ReceiverId = receiver.Id,
                    Subject = chat.Subject,

                };
                await MessagingFacade.CreateChat(chatDto);

                return RedirectToAction("Mailbox");
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> Chat(Guid id)
        {
            var chat = await MessagingFacade.GetChatById(id);

            if (chat == null)
            {
                return RedirectToAction("Mailbox");
            }
            chat.Messages = chat.Messages.OrderByDescending(m => m.Timestamp).ToList();
            return View(chat);

        }

        public ActionResult Reply(Guid chatId)
        {
            var message = new MessageDto
            {
                ChatId = chatId,
                AuthorId = Guid.Parse(User.Identity.Name),
            };
            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Reply(MessageDto message)
        {
            try
            {
                await MessagingFacade.SendMessage(message);
                return RedirectToAction("Chat", new {id = message.ChatId });
            }
            catch
            {
                return View();
            }
        }
    }
}
