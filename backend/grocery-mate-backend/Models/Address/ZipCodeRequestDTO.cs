using System.ComponentModel.DataAnnotations;

namespace grocery_mate_backend.Models.Shopping;

public class ZipCodeRequestDto
{
    [Required] public int ZipCode  { get; set; }
}