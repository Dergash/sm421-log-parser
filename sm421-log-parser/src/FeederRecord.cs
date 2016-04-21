using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.github.dergash.SM421LogParser
{
    public class FeederRecord
    {
        const UInt16 minFieldsAmount = 8; // Количество полей в одной лог-записи (разделенных запятой)
        public String FeederName;
        public String FeederId;
        public String Name;
        public UInt32 PickupAmount;
        public UInt32 PickupMissedAmount;
        public UInt32 PlacedAmount;
        public UInt32 DumpedAmount;
        public UInt32 Unknown1;
        public UInt32 PartNG;

        public static FeederRecord FromLogLine(String logLine)
        {
            var result = new FeederRecord();
            try
            {
                if (String.IsNullOrWhiteSpace(logLine)) return null;
                string[] fields = logLine.Split(',');
                if (fields[(int)FeederRecordField.FeederId] == "0")
                {
                    return null;
                }
                if (fields.Length == 8)
                {
                    result.FeederName = fields[(int)FeederRecordField.FeederName];
                    result.Name = fields[(int)FeederRecordField.Name - 1];
                    result.PickupAmount = TryParse(fields[(int)FeederRecordField.PickupAmount - 1]);
                    result.PickupMissedAmount = TryParse(fields[(int)FeederRecordField.PickupMissedAmount - 1]);
                    result.PlacedAmount = TryParse(fields[(int)FeederRecordField.PlacedAmount - 1]);
                    result.DumpedAmount = TryParse(fields[(int)FeederRecordField.DumpedAmount - 1]);
                    result.Unknown1 = TryParse(fields[(int)FeederRecordField.Unknown1 - 1]);
                    result.PartNG = TryParse(fields[(int)FeederRecordField.PartNG - 1]);
                }
                else if (fields.Length == 9)
                {
                    result.FeederName = fields[(int)FeederRecordField.FeederName];
                    result.Name = fields[(int)FeederRecordField.Name];
                    result.PickupAmount = TryParse(fields[(int)FeederRecordField.PickupAmount]);
                    result.PickupMissedAmount = TryParse(fields[(int)FeederRecordField.PickupMissedAmount]);
                    result.PlacedAmount = TryParse(fields[(int)FeederRecordField.PlacedAmount]);
                    result.DumpedAmount = TryParse(fields[(int)FeederRecordField.DumpedAmount]);
                    result.Unknown1 = TryParse(fields[(int)FeederRecordField.Unknown1]);
                    result.PartNG = TryParse(fields[(int)FeederRecordField.PartNG]);
                }
                else throw new FormatException();
            }
            catch(Exception e)
            {
                throw new FormatException("Fail to parse log record: " + logLine + "; 8 or 9 fields expected");
            }
            return result;
        }

        public override String ToString()
        {
            var result = "";
            result += "FeederName: " + FeederName + ", ";
         //   result += "FeederId: " + FeederId + ", ";
            result += "Name: " + Name + ", ";
            result += "PickupAmount: " + PickupAmount + ", ";
            result += "PickupMissedAmount: " + PickupMissedAmount + ", ";
            result += "PlacedAmount: " + PlacedAmount + ", ";
            result += "DumpedAmount: " + DumpedAmount + ", ";
         //   result += "Unknown1: " + Unknown1 + ", ";
         //   result += "PartNG: " + PartNG + " ";
            return result;
        }

        private static UInt32 TryParse(String input)
        {
            UInt32 result = 0;
            UInt32.TryParse(input, out result);
            return result;
        }
    }
}
