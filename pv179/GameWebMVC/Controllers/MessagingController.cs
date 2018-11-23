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
    public class MessagingController : Controller
    {
        public MessagingFacade messagingFacade { get; set; }
        public CharacterFacade characterFacade { get; set; }
        // GET: Messaging
        public async Task<ActionResult> Index()
        {
            //messagingFacade.

            return View();
        }

        // GET: Messaging/Details/5
        public async Task<ActionResult> MailBox()
        {
            var id = Session["accountId"] as Guid?;
            var character = await characterFacade.GetCharacterById(id.Value);
            var chats = new List<ChatDto>();
            chats.AddRange(character.SenderChats);
            chats.AddRange(character.ReceiverChats);
            return View(chats);
        }

        // GET: Messaging/Create
        public ActionResult NewChat()
        {
            return View();
        }

        // POST: Messaging/Create
        [HttpPost]
        public async Task<ActionResult> NewChat(ChatCreate chat)
        {
            try
            {
                var receiver = await characterFacade.GetCharacterAccordingToNameAsync(chat.ReceiverName);
                if (receiver == null)
                {
                    return View(chat);
                }
                var id = Session["accountId"] as Guid?;

                var chatDto = new ChatDto
                {
                    SenderId = id,
                    ReceiverId = receiver.Id,
                    Subject = chat.Subject,

                };
                await messagingFacade.CreateChat(chatDto);

                return RedirectToAction("Mailbox");
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> Chat(Guid id)
        {
            var chat = await messagingFacade.GetChatById(id);
            if (chat == null)
            {
                return RedirectToAction("Mailbox");
            }
            return View(chat.Messages);

        }
    }
}
