using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace com.github.dergash.SM421LogParser
{
    public class FeedersInfo
    {
        public IList<FeederRecord> TapeRecords;
        public IList<FeederRecord> StickRecords;
        public IList<FeederRecord> TrayRecords;

        public FeedersInfo()
        {
            this.TapeRecords = new List<FeederRecord>();
            this.StickRecords = new List<FeederRecord>();
            this.TrayRecords = new List<FeederRecord>();
        }

        public void AppendRecord(FeederRecordType recordType, FeederRecord record)
        {
            switch(recordType)
            {
                case FeederRecordType.TapeSection:
                    this.TapeRecords.Add(record);
                    break;
                case FeederRecordType.StickSection:
                    this.StickRecords.Add(record);
                    break;
                case FeederRecordType.TraySection:
                    this.TrayRecords.Add(record);
                    break;
            }
        }
    }
}
