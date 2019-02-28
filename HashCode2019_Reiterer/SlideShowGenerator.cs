using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRenderingFramework.Debug;

namespace HashCode2019_Reiterer
{
    class SlideShowGenerator
    {
        public List<Image> Images;

        public Dictionary<SlideShow, int> DoneShows = new Dictionary<SlideShow, int>();

        public KeyValuePair<SlideShow, int> BestSoFar;


        public Holder<int> ImagesRemaining;

        public SlideShowGenerator(List<Image> images)
        {
            Images = images;
            ImagesRemaining = new Holder<int>("Best score", 0, 30);
        }

        public void Reset()
        {
            BestSoFar = new KeyValuePair<SlideShow, int>(null, int.MaxValue);
            DoneShows = new Dictionary<SlideShow, int>();
        }

        public void QuickGenerate()
        {
            Queue<Image> newImage = new Queue<Image>( Images.OrderByDescending(x => x.Size).ToList());

            List<Image> currSlide = new List<Image>();

            List<Slide> Slides = new List<Slide>();

            bool earlyEnd = false;
            do
            {
                ImagesRemaining.SetValue(newImage.Count);

                if (currSlide.Count + newImage.First().Size > 2)
                {
                    earlyEnd = true;
                }
                else
                {
                    currSlide.Add(newImage.Peek());
                    newImage.Dequeue();
                }
                if (currSlide.Sum(x => x.Size) == 2 || earlyEnd)
                {
                    // form slide 
                    Slides.Add(new Slide(currSlide));
                    currSlide = new List<Image>();
                    earlyEnd = false;
                }
            }
            while (newImage.Count != 0);

            SlideShow show = new SlideShow(Slides);
            BestSoFar = new KeyValuePair<SlideShow, int>(show, 0);
        }

        public List<Tag> AllCollections()
        {
            return Images.SelectMany(x => x.Tags.Tags, (a, b) => b).Distinct(new TagComparere()).ToList();
        }

        public void RandomSearch()
        {
            // number of checked Images is either (n ... remaining images)
            // -) n * 0.05
            // -) (50 * n + 10000) ^ 0.5
            // the lower value is chosen

            List<Image> AllImages = Images;
            List<Image> Vertical = Images.Where(x => x.Size == 1).ToList();
            List<Image> Horizontal = Images.Where(x => x.Size == 2).ToList();

            List<Image> currSlide = new List<Image>();

            Image quick = AllImages.First(x => x.Size == 2);

            List<Slide> Slides = new List<Slide>();
            AllImages.Remove(quick);
            Horizontal.Remove(quick);

            Slides.Add(new Slide(new List<Image>() { quick }));
            
            bool earlyEnd = false;
            while (AllImages.Count!=0)
            {
                ImagesRemaining.SetValue(AllImages.Count());

                Image nextImage;

                // find next image
                if (AllImages.Count() <3000)
                { // when below 1 % search through everybody

                    nextImage = AllImages.AsParallel().OrderByDescending(x => Judge.SlideToSlideScore(Slides.Last(), new Slide(currSlide,x))).First();

                }
                else
                {
                    int searchCount = (int)Math.Min(AllImages.Count() * 0.05, Math.Pow(50 * AllImages.Count() + 10000, 0.45));
                    if (searchCount == 0)
                    {
                        searchCount = AllImages.Count();
                    }
                    nextImage = AllImages.GetRandom(searchCount).AsParallel().OrderByDescending(x => Judge.SlideToSlideScore(Slides.Last(), new Slide(currSlide,x ))).First();
                    //int something = (Judge.SlideToSlideScore(Slides.Last(), new Slide(currSlide, nextImage)));
                    //int somethingelse = something;
                }

                // check if next image would fit
                if (currSlide.Count== 0 || nextImage.Size == 1)
                {
                    currSlide.Add(nextImage);
                    AllImages.Remove(nextImage);
                    if (nextImage.Size == 1)
                    {
                        Vertical.Remove(nextImage);
                    }
                    else
                    {
                        Horizontal.Remove(nextImage);
                    }
                }

                if (currSlide.Sum(x=>x.Size)==2|| (currSlide.Sum(y=>y.Size)==1 && nextImage.Size==2))
                {
                    // end the slide
                    Slide s = new Slide(currSlide);
                    Slides.Add(s);
                    currSlide = new List<Image>();
                }
            }
            BestSoFar = new KeyValuePair<SlideShow, int>(new SlideShow(Slides),0);
        }

        public void RandomSequence()
        {
            while (!Console.KeyAvailable)
            {
                ImagesRemaining.SetValue(BestSoFar.Value);
                List<Slide> newPermut = Images.AsParallel().OrderBy(x => Extender.rng.Next(Images.Count)).Select(y=>new Slide(new List<Image>() { y })).ToList();

                SlideShow s = new SlideShow(newPermut);
                int score = Judge.JudgeSlideShow(s);
                if (BestSoFar.Value<score)
                {
                    BestSoFar = new KeyValuePair<SlideShow, int>(s, score);
                }
            }
        }

    }
}
