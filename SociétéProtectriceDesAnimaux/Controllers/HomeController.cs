using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using SociétéProtectriceDesAnimaux.Entities;
using SociétéProtectriceDesAnimaux.Business;
using SociétéProtectriceDesAnimaux.Repository;
using Microsoft.Extensions.Configuration;
using SociétéProtectriceDesAnimaux.Models;

namespace SociétéProtectriceDesAnimaux.Controllers
{
    public class HomeController : Controller
    {
        private AnimalService _animalService;

        public HomeController(IConfiguration configuration)
        {
            string connectionString = Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(configuration, "MyConnection");
            var animalRepository = new AnimalRepositoryOnAdoNet(connectionString);
            _animalService = new AnimalService(animalRepository);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NouvelAnimal()
        {
            var animal = new Animal();
            return View(animal);
        }

        [HttpPost]
        public IActionResult NouvelAnimal(Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _animalService.EnregistrerNouvelAnimal(animal);
            return View("Index");
        }

        public IActionResult Niches()
        {
            var animals = _animalService.GetAllAnimals();
            var niches = _animalService.GetAllNiches();
            var nicheAnimals = _animalService.GetAllNicheAnimals();

            var model = niches.Select( niche => 
                new NicheAnimalModel() {
                    Niche = niche,
                    Animal = animals.FirstOrDefault( animal => nicheAnimals.Any(na => na.AnimalId == animal.Id && na.NicheId == niche.Id) )
                }).ToList();

            //foreach (var nicheAnimal in nicheAnimals)
            //{
            //    var newModel = new NicheAnimalModel();
            //    foreach(var niche in niches)
            //    {
            //        if(nicheAnimal.NicheId == niche.Id)
            //        {
            //            newModel.Niche = niche;
            //            break;
            //        }
            //    }
            //    foreach(var animal in animals)
            //    {
            //        if (nicheAnimal.AnimalId == animal.Id)
            //        {
            //            newModel.Animal = animal;
            //            break;
            //        }
                   
            //    }
            //    model.Add(newModel);
            //}
            
            return View(model);
        }
    }
}
