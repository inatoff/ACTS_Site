using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }

        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }

        public Teacher Author { get; set; }

        public List<Post> Posts { get; set; }
    }
}
