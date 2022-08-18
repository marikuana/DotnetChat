using DotnetChat.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetChat.Data
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Group>();
            modelBuilder.Entity<Private>();
        }
    }
}
