using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SecComServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<EncryptedMessage> EncryptedMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public async Task<bool> ConvoReady(Conversation convo)
        {
            if (convo == null) return false;
            return convo.FirstUserAccepted && convo.SecondUserAccepted;
        }

        public Task<Conversation> FindConvo(int id)
        {
            return Conversations
                .Include(c => c.FirstUser)
                .Include(c => c.SecondUser)
                .FirstOrDefaultAsync(c => c.ConversationId == id);
        }

        public Task<Conversation> FindConvo(string user1, string user2)
        {
            var convo = Conversations
                .Include(c => c.FirstUser)
                .Include(c => c.SecondUser)
                .SingleOrDefaultAsync(c =>
                    c.FirstUser.UserName == user1 && c.SecondUser.UserName == user2
                    || c.SecondUser.UserName == user1 && c.FirstUser.UserName == user2);
            return convo;
        }
    }
}
