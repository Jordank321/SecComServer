using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecComServer.Data;

namespace SecComServer.Controllers
{
    [Produces("application/json")]
    [Route("Messages")]
    [Authorize]
    public class MessagesController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{convoId}")]
        public async Task<IActionResult> GetConversationMessages(int convoId)
        {
            var convo = await _context.FindConvo(convoId);
            if (!await _context.ConvoReady(convo)) return Forbid();
            return Ok(GetConversationMessages(convo));
        }

        [HttpGet]
        [Route("{user}")]
        public async Task<IActionResult> GetConversationMessages(string user)
        {
            var username = Username;
            var convo = await _context.FindConvo(username, user);
            if (!await _context.ConvoReady(convo)) return Forbid();

            return Ok(GetConversationMessages(convo));
        }

        public IQueryable<EncryptedMessageResponse> GetConversationMessages(Conversation convo)
        {
            var convoId = convo.ConversationId;
            return _context.EncryptedMessages.Where(m => m.Conversation.ConversationId == convoId).Select(m => new EncryptedMessageResponse(m));
        }

        [HttpPost]
        [Route("{convoId}")]
        public async Task<IActionResult> AddConversationMessage(int convoId, [FromBody]MessageRequest encryptedMessageRequest)
        {
            var convo = await _context.FindConvo(convoId);
            if (!await _context.ConvoReady(convo)) return Forbid();

            var message = new EncryptedMessage
            {
                Conversation = convo,
                FirstUser = convo.FirstUser.UserName,
                SecondUser = convo.SecondUser.UserName,
                Sent = DateTime.UtcNow,
                Content = encryptedMessageRequest.EncryptedMessage
            };

            _context.EncryptedMessages.Add(message);
            await _context.SaveChangesAsync();

            return Created($"/Messages/{convoId}/{message.EncryptedMessageId}", new EncryptedMessageResponse(message));
        }

        public class MessageRequest
        {
            public string EncryptedMessage { get; set; }
        }
    }
}