using Microsoft.EntityFrameworkCore;

namespace ChatService.Persistence
{
    internal class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }
        
        public DbSet<ChatMessageEntity> ChatMessages { get; set; }
    }
}
