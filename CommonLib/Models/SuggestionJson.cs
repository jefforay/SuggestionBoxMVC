using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CommonLib.Models
{
    public class SuggestionJson : IValidatableObject
    {
        [Display(Name = "id")]
        public int Id { get; set; } = 0;

        [Required(ErrorMessage = "Onderwerp is required")]
        [MaxLength(512, ErrorMessage = "Onderwerp can be at most 512 characters")]
        public string Onderwerp { get; set; }

        [Required(ErrorMessage = "Beschrijving is required")]
        public string Beschrijving { get; set; }

        public int? UserId { get; set; }

        [MaxLength(512, ErrorMessage = "UserName can be at most 512 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [RegularExpression("suggestie|uitje", ErrorMessage = "Type must be 'suggestie' or 'uitje'")]
        public string Type { get; set; }

        public string BeginDatum { get; set; } = DateTime.Now.ToString();

        public string EindDatum { get; set; } = DateTime.Now.ToString();

        [MaxLength(512, ErrorMessage = "Each category can be at most 512 characters")]
        public List<string> Categories { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Type == "uitje")
            {
                if (string.IsNullOrEmpty(BeginDatum))
                {
                    yield return new ValidationResult("BeginDatum is required when Type is 'uitje'.", new[] { nameof(BeginDatum) });
                }
                if (string.IsNullOrEmpty(EindDatum))
                {
                    yield return new ValidationResult("EindDatum is required when Type is 'uitje'.", new[] { nameof(EindDatum) });
                }
            }
        }
    }
}
