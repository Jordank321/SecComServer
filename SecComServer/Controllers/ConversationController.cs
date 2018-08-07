using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecComServer.Data;

namespace SecComServer.Controllers
{
    [Produces("application/json")]
    [Route("Conversation")]
    [Authorize]
    public class ConversationController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ConversationController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("All")]
        public IActionResult All()
        {
            var username = Username;
            var convos = _context.Conversations
                .Include(c => c.FirstUser)
                .Include(c => c.SecondUser)
                .Where(c => c.FirstUser.UserName == username
                            || c.SecondUser.UserName == username);
            return Ok(convos.Select(convo => new ConversationResponse(convo)));
        }

        [HttpGet]
        [Route("{requestedConvo}")]
        public async Task<IActionResult> GetConversation(string requestedConvo)
        {
            if (int.TryParse(requestedConvo, out var convoId)) return await GetConversation(convoId);

            var user = requestedConvo;
            var convo = await _context.FindConvo(Username, user);
            if (!await _context.ConvoReady(convo)) return Forbid();

            return Ok(new ConversationResponse(convo));
        }


        public async Task<IActionResult> GetConversation(int convoId)
        {
            var convo = await _context.FindConvo(convoId);
            if (!await _context.ConvoReady(convo)) return Forbid();

            return Ok(new ConversationResponse(convo));
        }

        [HttpPost]
        [Route("{userToAdd}")]
        public async Task<IActionResult> NewConversation(string userToAdd)
        {
            var otherUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userToAdd);

            if (otherUser == null) return BadRequest();

            var username = Username;
            var convo = await _context.FindConvo(userToAdd, username);

            if (convo != null) return await AcceptExistingConversation(convo);

            convo = new Conversation
            {
                FirstUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username),
                SecondUser = otherUser,
                FirstUserAccepted = true
            };

            _context.Conversations.Add(convo);
            await _context.SaveChangesAsync();
            return Created($"/Conversation/{convo.ConversationId}", new ConversationResponse(convo));
        }

        private async Task<IActionResult> AcceptExistingConversation(Conversation conversation)
        {
            if (Username == conversation.FirstUser.UserName)
            {
                if (conversation.FirstUserAccepted) return StatusCode(302);

                conversation.FirstUserAccepted = true;
            }
            else
            {
                if (conversation.SecondUserAccepted) return StatusCode(302);

                conversation.SecondUserAccepted = true;
            }

            await _context.SaveChangesAsync();
            return Created($"/Conversation/{conversation.ConversationId}", new ConversationResponse(conversation));
        }
    }
}