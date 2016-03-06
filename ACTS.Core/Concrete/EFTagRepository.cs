using ACTS.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.Core.Concrete
{
    public class EFTagRepository : ITagRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<string> GetAll()
        {
            var tagsCollection = context.Posts.Select(p => p.CombinedTags).ToList();
            return string.Join(",", tagsCollection).Split(',').Distinct();
        }

        public string Get(string tag)
        {
            var posts = context.Posts.Where(post => post.CombinedTags.Contains(tag)).ToList();
            posts = posts.Where(post =>
                   post.Tags.Contains(tag, StringComparer.CurrentCultureIgnoreCase))
                   .ToList();

            if (!posts.Any())
            {
                throw new KeyNotFoundException($"The tag \"{tag}\" does not exist.");
            }

            return tag.ToLower();
        }

        public void Edit(string existingTag, string newTag)
        {
            var posts = context.Posts.Where(post => post.CombinedTags.Contains(existingTag)).ToList();

            posts = posts.Where(post =>
                    post.Tags.Contains(existingTag, StringComparer.CurrentCultureIgnoreCase))
                    .ToList();

            if (!posts.Any())
            {
                throw new KeyNotFoundException($"The tag \"{existingTag}\" does not exist.");
            }

            foreach (var post in posts)
            {
                post.Tags.Remove(existingTag);
                post.Tags.Add(newTag);
            }

            context.SaveChanges();
        }

        public void Delete(string tag)
        {
            var posts = context.Posts.Where(post => post.CombinedTags.Contains(tag))
                    .ToList();

            posts = posts.Where(post =>
                post.Tags.Contains(tag, StringComparer.CurrentCultureIgnoreCase))
                .ToList();

            if (!posts.Any())
            {
                throw new KeyNotFoundException($"The tag \"{tag}\" does not exist.");
            }

            foreach (var post in posts)
            {
                post.Tags.Remove(tag);
            }

            context.SaveChanges();
        }
    }
}
