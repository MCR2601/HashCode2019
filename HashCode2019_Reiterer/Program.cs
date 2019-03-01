using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRenderingFramework.Debug;

namespace HashCode2019_Reiterer
{
    class Program
    {
        public static string Input = "b_lovely_landscapes.txt";

        static void Main(string[] args)
        {
            ClassicDebugger debugger = new ClassicDebugger();

            debugger.UpdateTime = 1000;
            

            List<Image> images = File.ReadAllLines(Input).Skip(1).Select((x,i) => new Image(x,i)).ToList();

            SlideShow show = new SlideShow(
                new List<Slide>()
                {
                    new Slide(new List<Image>()
                    {
                        new Image("H 3 cat beach sun",0)
                    }),
                    new Slide(new List<Image>()
                    {
                        new Image("H 2 garden cat",3)
                    }),
                    new Slide(new List<Image>()
                    {
                        new Image("V 2 garden selfie",1),
                        new Image("V 2 selfie smile",2)
                    })
                });


            SlideShowGenerator gen = new SlideShowGenerator(images);

            debugger.Watcher.Add(gen.ImagesRemaining);

            debugger.Activate();

            //gen.RandomSearch();
            //gen.RandomSequence();
            gen.RandomWebbing();

            debugger.Abort();
            System.Threading.Thread.Sleep(2000);

            Console.WriteLine(Judge.JudgeSlideShow(gen.BestSoFar.Key));

            //Console.WriteLine(gen.BestSoFar.Key.ToString());
            
            //Console.ReadLine();
            Console.WriteLine(Judge.JudgeSlideShow(gen.BestSoFar.Key));
            Console.WriteLine(gen.AllCollections().Count);
            Console.WriteLine(gen.Images.Sum(x=>x.Tags.Tags.Count()));

            StreamWriter sw = new StreamWriter("output"+Input);
            sw.Write(gen.BestSoFar.Key.GetOutputVersion());
            sw.Close();

            Console.ReadLine();
        }
    }
}
