using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiki.Repository.Models
{
    public class WikiArticle
    {
        public int pageid { get; set; }
        public int ns { get; set; }
        public string title { get; set; }
        public string extract { get; set; }
        public string contentmodel { get; set; }
        public string pagelanguage { get; set; }
        public DateTime touched { get; set; }
        public ulong lastrevid { get; set; }
    }
}
