using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2019_Reiterer
{
    class WebNode
    {
        public Image image;
        public int Size => image.Size;

        public bool Used;

        public List<WebNode> TargetNode;

        public WebNode(Image image)
        {
            this.image = image;
            Used = false;
            TargetNode = new List<WebNode>();
        }

        public void AddConnections(List<WebNode> nodes)
        {
            TargetNode = nodes;
        }

        public WebNode GetNextImage(int requiredSize, Slide previous, List<Image> currImages)
        {
            List<WebNode> viable = TargetNode.Where(x =>!x.Used && x.Size <= requiredSize).ToList();

            if (viable.Count==0)
            {
                return null;
            }


            WebNode Best = viable.OrderByDescending(x => Judge.SlideToSlideScore(previous, new Slide(currImages, x.image))).ToList().GetRandom(1).First();

            return Best;
        }



    }
}
