using RedditSharp;
using RedditSharp.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace goodgoodgoodbotbadbot
{
    class Program
    {
        static void Main(string[] args)
        {
            int times;
            //File shit
            if (File.Exists("times.t"))
            {
                StreamReader fr = File.OpenText("times.t");
                times = int.Parse(fr.ReadLine());
                fr.Close();
            }
            else
            {
                times = 0;
            }

            //Reddit code
            var webAgent = new BotWebAgent("Good_Good_GB_BB", "newpass", "1zqbIrqwygfySg", "2oHOvib5wBKMmaHEpW9C4QU3Q7M", "localhost:8080");
            //This will check if the access token is about to expire before each request and automatically request a new one for you
            //"false" means that it will NOT load the logged in user profile so reddit.User will be null
            var reddit = new Reddit(webAgent, true);
            var subreddit = reddit.RSlashAll;
            while (true)
            {
                foreach (var c in subreddit.Comments)
                {
                    try
                    {
                        Console.Write("Comment author:{0}", c.AuthorName);
                        Console.Write(" || Parent author:{0}\n", ((Comment)reddit.GetThingByFullname(c.ParentId)).AuthorName);
                        if (((Comment)reddit.GetThingByFullname(c.ParentId)).AuthorName == "Good_GoodBot_BadBot")
                        {
                            if (c.Body.ToLower() == "good bot" || c.Body.ToLower() == "good bot.")
                            {
                                times++;
                                var comment = c.Reply(string.Format("You are the {0}^th person calling /u/Good_GoodBot_BadBot a good bot. HE definetly is a little less awesome!", times));
                                Console.WriteLine("Commented: {0}", times);
                                writef(times);
                            }
                        }
                    }
                    catch (InvalidCastException)
                    {
                        Console.WriteLine(" || Not a comment");
                    }
                }
                Thread.Sleep(500);
            }
        }

        static void writef(int times)
        {
            File.Delete("times.t");
            var f = File.CreateText("times.t");
            f.WriteLine(times);
        }
    }
}
