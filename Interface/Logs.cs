using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public static class Logs
    {
        public static void Log(string text)
        {
            using (TextWriter tw = File.AppendText(Database.Paths.Logs))
            {
                tw.WriteLine($"Time: {DateTime.Now}");
                tw.WriteLine(text);
            }
        }

    }
}
