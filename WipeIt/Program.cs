using RedditSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RedditSharp.Things;

namespace WipeIt
{
    class Program
    {
        static DateTime threshold = DateTime.Now.AddMonths(-2);

        static void Main(string[] args)
        {
            var userName = args[0];
            var password = args[1];

            var reddit = new Reddit(userName, password, true);
            Console.WriteLine("Processing");
            int processed, internalSkip, globalSkip = 0;
            do
            {
                processed = 0;
                internalSkip = 0;
                foreach (var comment in reddit.User.Comments.Skip(globalSkip).Take(25))
                {

                    if (IsCommentOld(comment))
                    {
                        Console.WriteLine(comment.Created);
                        comment.Del();
                        processed++;
                    }
                    else
                    {
                        internalSkip++;

                    }
                }
                Console.WriteLine("Processed a total of " + processed);
                globalSkip += internalSkip;
            } while (processed > 0 || internalSkip == 25);

            Console.WriteLine("Done Processing");
            Console.ReadKey();
        }

        private static bool IsCommentOld(Comment comment)
        {
            return comment.Created < threshold;
        }
    }
}
