using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruper.BLL.Dtos
{
    public class SliderDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ImageName { get; set; }
        public string ButtonName { get; set; }
        public string ButtonLink { get; set; }
        public bool IsActive { get; set; }

    }
}
