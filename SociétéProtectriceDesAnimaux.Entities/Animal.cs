using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SociétéProtectriceDesAnimaux.Entities
{
    public class Animal
    {

        public int Id { get; set; }

        [Range(minimum:1, maximum:2, ErrorMessage = "Veuillez renseigner le type de l'animal")]
        public TypeAnimal TypeAnimal { get; set; }

        [Required(ErrorMessage = "Veuillez renseigner le nom de l'animal")]
        public string Name { get; set; }

        public DateTime? LastFoodingTime { get; set; }
    }
}
