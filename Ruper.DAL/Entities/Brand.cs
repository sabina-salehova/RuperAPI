using Ruper.DAL.Base;
using System.ComponentModel.DataAnnotations;

namespace Ruper.DAL.Entities
{
    public class Brand : TimeStample, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
