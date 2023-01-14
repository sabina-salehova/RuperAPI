using Microsoft.AspNetCore.Http;

namespace Ruper.BLL.Dtos
{
    public class CategoryUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public IFormFile? Image { get; set; }
        public string ImageName { get; set; } = String.Empty;

        //public DateTime UpdatedAt = DateTime.Now;

        //public string UpdatedBy = "Admin";
    }
}
