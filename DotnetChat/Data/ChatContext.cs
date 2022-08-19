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

            InitData(modelBuilder);
        }

        private void InitData(ModelBuilder modelBuilder)
        {
            User[] users = new User[]
            {
                new User { Id = 1, Login = "Marikuana", Password = "123" },
                new User { Id = 2, Login = "Bob", Password = "123" },
                new User { Id = 3, Login = "Tom", Password = "123" },
            };

            Group groupChat = new Group { Id = 1, Name = "Test Group" };
            Private privateChat = new Private { Id = 2 };

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Group>().HasData(groupChat);
            modelBuilder.Entity<Private>().HasData(privateChat);

            modelBuilder.Entity("ChatUser").HasData(new[]
            {
                new { ChatsId = 1, MembersId = 1 },
                new { ChatsId = 1, MembersId = 2 },
                new { ChatsId = 1, MembersId = 3 },

                new { ChatsId = 2, MembersId = 1 },
                new { ChatsId = 2, MembersId = 2 },
            });
        }

        
    }
}
