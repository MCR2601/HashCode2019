using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2019_Reiterer
{
    class Image
    {
        public int Index;
        public int Size;
        public TagCollection Tags;

        public Image(string Line,int i)
        {
            Index = i;
            string[] parts = Line.Split(' ');

            Size = parts[0] == "V" ? 1 : 2; // vertical size 1; horizontal size 2

            List<Tag> tags = new List<Tag>();

            int tagcount = int.Parse(parts[1]);

            for (int x = 2; x < tagcount + 2; x++)
            {
                tags.Add(new Tag(parts[x]));
            }
            Tags = new TagCollection(tags);

        }

        public override string ToString()
        {
            return "--"+ Index +"_" + Size + ": " + Tags.ToString();
        }

    }
}
