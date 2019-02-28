using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2019_Reiterer
{
    class TagCollection
    {
        static int TooBig = 500;
        public List<Tag> Tags;

        public int Count => Tags.Count();

        public TagCollection(List<Tag> tags)
        {
            Tags = tags;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in Tags)
            {
                sb.Append(item.ToString() + " ");
            }
            return sb.ToString();
        }

        public static TagCollection operator +(TagCollection a, TagCollection b)
        {   
            return new TagCollection(a.Tags.Union(b.Tags, new TagComparere()).ToList());
        }
        // remove tags from a which are also in b
        public static TagCollection operator -(TagCollection a, TagCollection b)
        {
            return new TagCollection(a.Tags.Where(x=>!b.Tags.Any(y=> y == x)).ToList());
        }
        public TagCollection Intersect(TagCollection t)
        {
            return new TagCollection(Tags.Intersect(t.Tags,new TagComparere()).ToList());
        }
    }
}
