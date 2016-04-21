using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace com.github.dergash.SM421LogParser
{
    public class FeedersInfoParser
    {
        public FeedersInfo ParseLogFile(string pathToLogFile)
        {
            var result = new FeedersInfo();
            using (FileStream fs = File.OpenRead(pathToLogFile))
            {
                string[] logFileLines = File.ReadAllLines(pathToLogFile, System.Text.Encoding.GetEncoding(1251));
                if (IsSM421RawLog(logFileLines) == false)
                {
                    String ErrMsg = "Input file " + pathToLogFile + " has incorrect format. ";
                    ErrMsg += "Correct log identified by 'Power Time' text on first row. ";
                    ErrMsg += "Second row is also a possibility if first row contains VERSION label";
                    throw new FormatException(ErrMsg);
                }
                FeederRecordType? currentSection = null;
                foreach (String line in logFileLines)
                {
                    var lineType = GetRecordType(line);
                    if(lineType != FeederRecordType.Record)
                    {
                        currentSection = lineType;
                        continue;
                    }
                    if (currentSection == null || currentSection == FeederRecordType.NozzleSection)
                    {
                        continue;
                    }
                    else
                    {
                        var record = FeederRecord.FromLogLine(line);
                        if (record != null && currentSection.HasValue)
                        {
                            result.AppendRecord(currentSection.Value, record);
                        }
                    }
                }
            }
            return result;
        }

        private FeederRecordType? GetRecordType(String line)
        {
            switch (line)
            {
                case "[Tape Feeder]":
                    return FeederRecordType.TapeSection;
                case "[Stick Feeder]":
                    return FeederRecordType.StickSection;
                case "[Tray Feeder]":
                    return FeederRecordType.TraySection;
                case "[Nozzle]":
                    return FeederRecordType.NozzleSection;
                default:
                    return FeederRecordType.Record;
            }
        }

        private Boolean IsSM421RawLog(String[] InputLines)
        {
            if (InputLines.Length < 2) return false;
            if (InputLines[0].StartsWith("VERSION"))
            {
                return InputLines[1].StartsWith("Power Time");
            }
            else
            {
                return InputLines[0].StartsWith("Power Time");
            }
        }
    }
}
