using Ruper.DAL.Base;

namespace Ruper.DAL.Entities
{
    public class ProductImage : TimeStample, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int ProductColorId { get; set; }
        public virtual ProductColor ProductColor { get; set; }
    }
}
