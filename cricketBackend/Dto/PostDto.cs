using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cricketBackend.Dto
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
