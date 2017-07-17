using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Models.DTOs
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public ICollection<MenuItemDto> Children { get; set; }
        public SectionTypeDto SectionType { get; set; }
        public MenuItemDto()
        {
            Children = new List<MenuItemDto>();
        }
    }
}
