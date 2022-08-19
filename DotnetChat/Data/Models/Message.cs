﻿using DotnetChat.Enums;

namespace DotnetChat.Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        public User Author { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public MessageDelete Delete { get; set; }
        public Message? MessageReply { get; set; }
        public Chat Chat { get; set; }
    }
}