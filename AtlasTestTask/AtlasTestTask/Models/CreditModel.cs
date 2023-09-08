using System.ComponentModel.DataAnnotations;

namespace AtlasTestTask.Models;

public class CreditModel
{
    [Required(ErrorMessage = "Сумма не может быть пустой")]
    [Range(1, Int32.MaxValue, ErrorMessage = "Сумма кредита не может быть меньше 1")]
    public float Value { get; set; }
    
    [Required(ErrorMessage = "Срок кредита не может быть пустым")]
    [Range(1, Int32.MaxValue, ErrorMessage = "Срок кредита не может быть меньше 1 месяца")]
    public int Term { get; set; }
    
    [Required(ErrorMessage = "Процент кредита не может быть пустым")]
    [Range(0, Int32.MaxValue, ErrorMessage = "Процент кредита не может быть меньше 0")]
    public float Rate { get; set; }
}