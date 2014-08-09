using System.ComponentModel.DataAnnotations;

namespace GameMvc.Models
{
    public class UiCharacterModel
    {
        [Required(ErrorMessage="Name is required")]
        [Display(Name = "Character name")]
        public string Name
        { get; set; }

        [Required]
        [Display(Name = "Health")]
        public int Health { get; set; }
    }
}