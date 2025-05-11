using BoardMVC.Enum;
using System.ComponentModel.DataAnnotations;

namespace BoardMVC.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Status Status { get; set; }
        [Required(ErrorMessage = "Категорія обов'язкова")]
        [Display(Name = "Категорія")]
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Підкатегорія обов'язкова")]
        [Display(Name = "Підкатегорія")]
        public int? SubCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
    }
}
