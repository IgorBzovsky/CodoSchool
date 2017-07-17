using CodoSchool.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Models
{
    public class SectionViewModel
    {
        public SectionDto SectionDto { get; set; }
        public List<SectionTypeDto> ParentForSectionTypes { get; set; }
        public List<SectionDto> Parents { get; set; }

        public SectionViewModel()
        {
            SectionDto = new SectionDto();
            ParentForSectionTypes = new List<SectionTypeDto>();
            Parents = new List<SectionDto>();
        }
    }
}
