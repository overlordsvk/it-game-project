using BL.DTO;
using BL.DTO.Filters;
using BL.Facades;
using GameWebMVC.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GameWebMVC.Controllers
{
    [Authorize(Roles = "HasCharacter")]
    public class MessagingController : Controller
    {
        #region Constants

        public const int PageSize = 10;

        #endregion Constants

        public MessagingFacade MessagingFacade { get; set; }
        public CharacterFacade CharacterFacade { get; set; }

        public async Task<ActionResult> MailBox(int page = 1)
        {
            var character = await CharacterFacade.GetCharacterById(Guid.Parse(User.Identity.Name));
            var filter = new ChatFilterDto { CharacterId = character.Id, PageSize = PageSize, RequestedPageNumber = page, SortCriteria = nameof(ChatDto.LastMessageTimestamp) };
            var chats = await MessagingFacade.GetChatsByFilterAsync(filter);

            // Paging
            ViewBag.RequestedPageNumber = chats.RequestedPageNumber;
            ViewBag.PageCount = (int)Math.Ceiling((double)chats.TotalItemsCount / (double)PageSize);
            // Paging END

            return View(chats.Items.Distinct().ToList());
        }

        public ActionResult NewChat()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> NewChat(ChatCreate chat)
        {
            if (ModelState.IsValid)
            {
                var receiver = await CharacterFacade.GetCharacterAccordingToNameAsync(chat.ReceiverName);
                if (receiver == null)
                {
                    ModelState.AddModelError("", "Daný charakter neexistuje");
                    return View(chat);
                }
                var id = Guid.Parse(User.Identity.Name);

                var chatDto = new ChatDto
                {
                    SenderId = id,
                    ReceiverId = receiver.Id,
                    Subject = chat.Subject,
                };
                var message = await MessagingFacade.CreateChat(chatDto);
                if (message != Guid.Empty)
                {
                    return RedirectToAction("Mailbox");
                }
            }
            return View();
        }

        public async Task<ActionResult> Chat(Guid id)
        {
            var chat = await MessagingFacade.GetChatById(id);
            var characterId = Guid.Parse(User.Identity.Name);
            if (characterId != chat.ReceiverId && characterId != chat.SenderId)
            {
                return RedirectToAction("NotAuthorized", "Error");
            }
            if (chat == null)
            {
                return RedirectToAction("Mailbox");
            }
            chat.Messages = chat.Messages.OrderBy(m => m.Timestamp).ToList();
            return View(chat);
        }

        public async Task<ActionResult> Reply(Guid chatId)
        {
            var chat = await MessagingFacade.GetChatById(chatId);
            var characterId = Guid.Parse(User.Identity.Name);
            if (characterId != chat.ReceiverId && characterId != chat.SenderId)
            {
                return RedirectToAction("NotAuthorized", "Error");
            }

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
                return RedirectToAction("Chat", new { id = message.ChatId });
            }
            catch
            {
                return RedirectToAction("Chat", new { id = message.ChatId });
            }
        }
    }
}