using Ruper.DAL.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.DAL.Entities
{
    public class SubCategory : TimeStample, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public bool IsDeleted { get; set; }
        public int? CategoryId { get; set; }

        //[ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
