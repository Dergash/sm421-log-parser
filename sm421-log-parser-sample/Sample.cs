using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace com.github.dergash.SM421LogParser
{
    class Sample
    {
        static void Main(string[] args)
        {
            string FilePath = @"..\..\..\log-samples\sample-1.log";
            if(File.Exists(FilePath) == false)
            {
                Console.WriteLine("File " + Path.GetFullPath(FilePath) + " not found");
                return;
            }
            var parser = new FeedersInfoParser();
            var recordsInfo = parser.ParseLogFile(FilePath);
            Console.WriteLine("[Tape Feeder]");
            foreach(var record in recordsInfo.TapeRecords)
            {
                if (record.PickupAmount == 0) continue;
                Console.WriteLine(record.ToString());
            }
            Console.WriteLine("[Stick Feeder]");
            foreach (var record in recordsInfo.StickRecords)
            {
                if (record.PickupAmount == 0) continue;
                Console.WriteLine(record.ToString());
            }
            Console.WriteLine("[Tray Feeder]");
            foreach (var record in recordsInfo.TrayRecords)
            {
                if (record.PickupAmount == 0) continue;
                Console.WriteLine(record.ToString());
            }
        }
    }
}
