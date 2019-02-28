using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2019_Reiterer
{
    static class Judge
    {
        public static int JudgeSlideShow(SlideShow show)
        {
            int score = 0;

            for (int i = 0; i < show.Slides.Count - 1; i++)
            {
                Slide Curr = show.Slides[i];
                Slide Next = show.Slides[i + 1];

                // in common
                int common = (Curr.Tags.Intersect(Next.Tags)).Count;
                // unique in curr
                int uniqueCurr = (Curr.Tags - Next.Tags).Count;
                // unique in next
                int uniqueNext = (Next.Tags - Curr.Tags).Count;

                int MinValue = Math.Min(common, Math.Min(uniqueCurr, uniqueNext));


                score += MinValue;


            }

            return score;
        }

        public static int SlideToSlideScore(Slide a, Slide b)
        {
            // in common
            int common = (a.Tags.Intersect(b.Tags)).Count;
            // unique in curr
            int uniqueCurr = (a.Tags - b.Tags).Count;
            // unique in next
            int uniqueNext = (b.Tags - a.Tags).Count;

            int MinValue = Math.Min(common, Math.Min(uniqueCurr, uniqueNext));
            return MinValue;
        }

    }
}
