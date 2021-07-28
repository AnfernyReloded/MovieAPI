using System;
using System.Collections.Generic;

#nullable disable

namespace MovieWebAPI.Models
{
    public partial class Movies
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public double? Runtime { get; set; }
    }
}
