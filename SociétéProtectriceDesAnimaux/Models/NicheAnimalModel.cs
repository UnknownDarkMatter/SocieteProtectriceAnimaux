using SociétéProtectriceDesAnimaux.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SociétéProtectriceDesAnimaux.Models
{
    public class NicheAnimalModel
    {
        public Animal Animal { get; set; }
        public Niche Niche { get; set; }

    }
}
