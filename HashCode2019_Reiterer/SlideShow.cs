using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2019_Reiterer
{
    class SlideShow
    {
        public List<Slide> Slides;
        public List<Image> UsedImages;

        public SlideShow(List<Slide> slides)
        {
            Slides = slides;
            UsedImages = slides.SelectMany(x => x.Images, (a, b) => b).ToList();
        }
        public string GetOutputVersion()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Slides.Count().ToString());

            foreach (var item in Slides)
            {
                string line = "";
                foreach (var i in item.Images)
                {
                    line += " " + i.Index;
                }
                line = line.Trim(' ');
                sb.AppendLine(line);
            }

            return sb.ToString();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SlideShow Start:");
            foreach (var item in Slides)
            {
                sb.AppendLine(item.ToString());
            }
            sb.AppendLine("-------------");
            return sb.ToString();
        }
    }
}
