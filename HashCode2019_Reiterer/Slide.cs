using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2019_Reiterer
{
    class Slide
    {
        public List<Image> Images;

        public TagCollection Tags => Images.Aggregate(new TagCollection(new List<Tag>()), (a, b) => a + b.Tags);

        public Slide(List<Image> images,Image i)
        {
            Images = new List<Image>(images);
            Images.Add(i);
        }
        public Slide(List<Image> images)
        {
            Images = images;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-Slide Start");
            foreach (var item in Images)
            {
                sb.AppendLine(item.ToString());
            }
            
            return sb.ToString();
        }
    }
}
