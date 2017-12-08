using System;
using System.Collections.Generic;

namespace WebApiLab.Models
{
    public class Document
    {
        public string Title { get; set; }
        public int? NumberOfPages { get; set; }
        public bool? HasSummary { get; set; }
        public DateTime? PublishedFrom { get; set; }
        public decimal? Price { get; set; }
        public DocumentType? Type { get; set; }
        public List<string> TagList { get; set; }
        public int? Rating { get; set; }
    }
}
