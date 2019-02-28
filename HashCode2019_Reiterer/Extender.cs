using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2019_Reiterer
{
    static class Extender
    {
        public static Random rng = new Random();
        public static List<T> GetRandom<T>(this List<T> list,int count)
        {
            List<int> indexes = new List<int>();
            List<T> searched = new List<T>();

            for (int i = 0; i < count; i++)
            {
                int next = rng.Next(list.Count());
                if (!indexes.Contains(i))
                {
                    indexes.Add(next);
                    searched.Add(list[i]);
                }
            }
            return searched;
        }


    }
}
