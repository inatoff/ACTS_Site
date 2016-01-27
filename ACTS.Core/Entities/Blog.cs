using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Entities
{
    public class Blog
    {
        [ForeignKey(nameof(Teacher))]
        [Key]
        public int TeacherId { get; set; }
        
        public Teacher Teacher { get; set; }

        public List<Post> Posts { get; set; }
    }
}
