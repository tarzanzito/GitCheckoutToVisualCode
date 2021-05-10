using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candal.Configuration
{
    public class AppArgs
    {
        public void Mount(string[] args)
        {
            var elements = new System.Collections.Generic.Dictionary<string, string>();

            foreach(string item in args)
            {
                System.Console.WriteLine(item);

                string action = null;
                string data = null;

                string char1 = item.Substring(1, 1);
                string char2 = item.Substring(2, 1);

                if (char2 == "-")
                    action = item.Substring(2, item.Length);
                else if (char1 == "-")
                    action = item.Substring(2, item.Length);
                else
                    data = item;

                

            }

        }
    }
}
