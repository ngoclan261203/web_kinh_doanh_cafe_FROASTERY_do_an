using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FROASTERY.Models
{
    public class SliderDto
    {
        public Guid Guid { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? TitleColor { get; set; }
        public string? DescriptionColor { get; set; }
        public bool IsShow { get; set; } = true;
    }
}
