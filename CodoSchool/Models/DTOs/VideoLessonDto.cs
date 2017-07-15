using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Models.DTOs
{
    public class VideoLessonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public int? Number { get; set; }
    }
}
