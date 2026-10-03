using System.ComponentModel.DataAnnotations;

namespace NetCoreMVCLAB5.Models;

public class Category
{
    [Display(Name = "Mã danh mục")]
    public int Id { get; set; }

    [Display(Name = "Tên danh mục")]
    [Required(ErrorMessage = "Tên danh mục không được để trống")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Tên danh mục phải từ 2 đến 100 ký tự")]
    public string Name { get; set; } = string.Empty;
}
