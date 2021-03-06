using SociétéProtectriceDesAnimaux.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SociétéProtectriceDesAnimaux.Repository;

namespace SociétéProtectriceDesAnimaux.Business
{
    public class AnimalService
    {
        private IAnimalRepository _animalRepository;
        public AnimalService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }
        public void EnregistrerNouvelAnimal(Animal animal)
        {
            //creation niches si n'exixtent pas
            CreateNewNiche();
            //creation animal
            var animalId = _animalRepository.Create(animal);
            //recup niche disponible
            var nicheId = GetAbailableNicheId();
            if (nicheId == 0)
            {
                throw new ArgumentOutOfRangeException("Désolé, il n'y a plus de niches de disponible!");
            }
            //creation niche-animal avec l'animal créé et la niche disponible

            var newNicheAnimal = new NicheAnimal();
            newNicheAnimal.AnimalId = animalId;
            newNicheAnimal.NicheId = nicheId;
            _animalRepository.Create(newNicheAnimal);
        }

        public void SupprimerAnimal(int animalId)
        {
            var listeNicheAnimals = _animalRepository.GetNicheAnimals();
            var nicheAnimal = listeNicheAnimals.FirstOrDefault(m => m.AnimalId == animalId);
            if(nicheAnimal!= null)
            {
                _animalRepository.Delete(nicheAnimal);
                
            }
            var listeAnimals = _animalRepository.GetAnimals();
            var monAnimal = listeAnimals.FirstOrDefault(m => m.Id == animalId);
            if(monAnimal != null)
            {
                _animalRepository.Delete(monAnimal);
            }
        }

        public bool IsHungy(Animal animal)
        {
            var monAnimal = _animalRepository.GetAnimals().FirstOrDefault(m => m.Id == animal.Id);
             if(monAnimal.LastFoodingTime == null)
            {
                return true;
            }
            var timeSpan = DateTime.Now.Subtract(monAnimal.LastFoodingTime.Value);
            return timeSpan.TotalSeconds > Constants.NombreDeSeondes;
        }

        public void NourrirAnimal(int animalId)
        {
            var listeAnimals = _animalRepository.GetAnimals();
            var monAnimal = listeAnimals.FirstOrDefault(m => m.Id == animalId);
            if (monAnimal != null)
            {
                monAnimal.LastFoodingTime = DateTime.Now;
                _animalRepository.Update(monAnimal);
            }
        }

        public List<Animal> GetAllAnimals()
        {
            return _animalRepository.GetAnimals();
        }
        public List<Niche> GetAllNiches()
        {
            return _animalRepository.GetNiches();
        }
        public List<NicheAnimal> GetAllNicheAnimals()
        {
            return _animalRepository.GetNicheAnimals();
        }

        private void CreateNewNiche()
        {
            var existingNiche = _animalRepository.GetNiches();
            if(existingNiche.Count == 0)
            {
                for(int i = 0; i < Constants.NombreNiche; i++)
                {
                    _animalRepository.Create(new Niche() {Name=$"{Constants.NicheNamePrefix}{i}"});
                }
            }
        }

        private int GetAbailableNicheId()
        {
            var listeNicheAnimals = _animalRepository.GetNicheAnimals();
            var listeNiches = _animalRepository.GetNiches();
            
            foreach (var niche in listeNiches)
            {
                if ( ! listeNicheAnimals.Select(m => m.NicheId).Contains(niche.Id))
                {
                    return niche.Id;
                }
            }
            return 0;
        }

    }
}
