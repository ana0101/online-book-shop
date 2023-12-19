﻿using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OnlineBookShop.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }
}
