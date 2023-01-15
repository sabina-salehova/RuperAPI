using Ruper.DAL.Base;
using System.ComponentModel.DataAnnotations;

namespace Ruper.DAL.Entities
{
    public class Slider : TimeStample, IEntity
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
