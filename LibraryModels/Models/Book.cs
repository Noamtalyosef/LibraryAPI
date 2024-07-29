using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModels.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfPublish { get; set; }
        public string PicturePath { get; set; }
        public string CopyPath { get; set; }
        public List<Author>? Authors { get; set; } = new List<Author>(); 
        public Publisher? Publisher { get; set; }    
    }
}
