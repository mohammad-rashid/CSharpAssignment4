using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Async
{
    class Program
    {
        static void Main(string[] args)
        {
            var loadLinesTask = Task.Run(async () =>
            {

                using (var stream = new StreamReader(
                    File.OpenRead("A.txt")))
                {
                    var lines = new List<string>();
                    string line;
                   
                    while ((line = await stream.ReadLineAsync()) != null)
                    {
                        lines.Add(line);
                  
                    }
                    return lines;
                }

            });
            loadLinesTask.ContinueWith(async t =>
            {
                using (var outputFile = new StreamWriter(File.OpenWrite("B.txt")))
                {
                    foreach (var line in loadLinesTask.Result)
                    {
                        await outputFile.WriteLineAsync(line);
                    }
                    
                }
            });
        }
        
    }
}
