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

        public HomeController(IConfiguration configuration, IAnimalRepository animalRepository)
        {
            //string connectionString = Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(configuration, "MyConnection");
            //var animalRepository = new AnimalRepositoryOnAdoNet(connectionString);
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

        [HttpDelete]
        public IActionResult DeleteAnimal(int animalId)
        {
            _animalService.SupprimerAnimal(animalId);
            return Json(new { IsError=false, Message="OK"});
        }

        [HttpPut]
        public IActionResult NourrirAnimal(int animalId)
        {
            _animalService.NourrirAnimal(animalId);
            return Json(new { IsError = false, Message = "OK" });

        }

        public IActionResult GetHungryAnimalStatus()
        {

            var model = _animalService.GetAllAnimals().Select(m => new { IsHungry = _animalService.IsHungy(m), AnimalId = m.Id }).ToArray();
            return Json(model);
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
          
            return View(model);
        }
    }
}
