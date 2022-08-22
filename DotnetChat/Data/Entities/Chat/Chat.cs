﻿namespace DotnetChat.Data.Models
{
    public abstract class Chat
    {
        public int Id { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<Message> Messages { get; set; }

        public Chat()
        {
            Members = new List<User>();
            Messages = new List<Message>();
        }

        public virtual string GetName()
        {
            return string.Join(",", Members.Select(s => s.Login));
        }
    }
}