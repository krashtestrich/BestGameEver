using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace GameWeb.Models
{
    public class GameModel
    {
        [Required]
        [Display(Name = "Character Name")]
        public string CharacterName { get; set; }       
    }
}
