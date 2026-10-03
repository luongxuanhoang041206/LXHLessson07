using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace NetCoreMVCLAB5.Models;

public class Product : IValidatableObject
{
    [Display(Name = "Mã sản phẩm")]
    public int Id { get; set; }

    [Display(Name = "Tên sản phẩm")]
    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    [StringLength(150, MinimumLength = 6, ErrorMessage = "Tên sản phẩm phải từ 6 đến 150 ký tự")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Ảnh sản phẩm")]
    public string? Image { get; set; }

    [Display(Name = "Chọn ảnh upload")]
    public IFormFile? ImageFile { get; set; }

    [Display(Name = "Giá chuẩn")]
    [Required(ErrorMessage = "Giá chuẩn không được để trống")]
    [Range(100000, float.MaxValue, ErrorMessage = "Giá sản phẩm phải nhỏ nhất là 100.000 VNĐ")]
    public float Price { get; set; }

    [Display(Name = "Giá khuyến mãi")]
    [Required(ErrorMessage = "Giá khuyến mãi không được để trống")]
    [Range(0, float.MaxValue, ErrorMessage = "Giá khuyến mãi không được âm")]
    public float SalePrice { get; set; }

    [Display(Name = "Mô tả sản phẩm")]
    [Required(ErrorMessage = "Mô tả sản phẩm không được để trống")]
    [StringLength(1500, ErrorMessage = "Mô tả không được vượt quá 1500 ký tự")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Danh mục sản phẩm")]
    [Required(ErrorMessage = "Vui lòng chọn danh mục")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (SalePrice < 0)
        {
            yield return new ValidationResult("Giá khuyến mãi không được âm", new[] { nameof(SalePrice) });
        }

        if (SalePrice >= Price * 0.9f)
        {
            yield return new ValidationResult("Giá khuyến mãi phải nhỏ hơn giá chuẩn ít nhất 10%", new[] { nameof(SalePrice) });
        }

        string[] sensitiveWords = new[] { "die", "admin", "fack" };
        if (!string.IsNullOrEmpty(Description))
        {
            foreach (var word in sensitiveWords)
            {
                if (Description.Contains(word, StringComparison.OrdinalIgnoreCase))
                {
                    yield return new ValidationResult($"Mô tả không được chứa từ nhạy cảm: {word}", new[] { nameof(Description) });
                }
            }
        }

        if (Id == 0 && ImageFile == null && string.IsNullOrEmpty(Image))
        {
            yield return new ValidationResult("Vui lòng chọn và upload ảnh sản phẩm", new[] { nameof(ImageFile) });
        }
    }
}
