using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cricketBackend.Model
{
    public class Like
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
