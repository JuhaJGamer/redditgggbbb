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
            var webAgent = new BotWebAgent("Good_Good_GB_BB", "newpass", "tLNL8oa6HZkPZA", "99QLr7S3F2CjzJDGA6zvpTVwx5M", "no://no.no");
            //This will check if the access token is about to expire before each request and automatically request a new one for you
            //"false" means that it will NOT load the logged in user profile so reddit.User will be null
            var reddit = new Reddit(webAgent, false);
            var subreddit = reddit.GetSubreddit("/r/all");
            subreddit.Subscribe();
            while (true)
            {
                foreach (var post in subreddit.New.Take(25))
                {
                    if (post.Title == "What is my karma?")
                    {
                        // Note: This is an example. Bots are not permitted to cast votes automatically.
                        post.Upvote();
                        var comment = post.Comment(string.Format("You have {0} link karma!", post.Author.LinkKarma));
                        comment.Distinguish(VotableThing.DistinguishType.Moderator);
                    }
                    foreach(var comm in post.Comments)
                    {
                        Console.Write("Comment author:{0}", comm.AuthorName);
                        if(comm.AuthorName == "Good_GoodBot_BadBot")
                        {
                            foreach(Comment c in comm.Comments)
                            {
                                if (c.Body.ToLower() == "good bot" || c.Body.ToLower() == "good bot.")
                                {
                                    times++;
                                    var comment = c.Reply(string.Format("You are the {0}^th person calling /u/Good_GoodBot_BadBot a good bot. HE definetly is a little less awesome!", times));
                                    writef(times);                              
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(10);
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
