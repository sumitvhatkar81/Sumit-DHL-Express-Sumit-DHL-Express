
using System;

namespace DHL_Document_App.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
