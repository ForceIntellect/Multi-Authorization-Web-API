using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class GetDocumentList
    {
        public string TableName { get; set; }
        public int TransactionId { get; set; }
    }
    public class DownloadDocument
    {
        public byte[] DocData { get; set; }
        public string FileName { get; set; }
        public string DocString { get; set; }
    }
    public class DownloadFile
    {
        public int DocNo { get; set; }
        public int Version { get; set; }
    }
}
