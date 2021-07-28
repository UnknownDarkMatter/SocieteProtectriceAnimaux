using System;
using System.Collections.Generic;
using System.Text;
using SociétéProtectriceDesAnimaux.Entities;
namespace SociétéProtectriceDesAnimaux.Repository
{
    public interface IAnimalRepository
    {
        int Create(Animal animal);
        void Delete(Animal animal);

        void Update(Animal animal);
        List<Animal> GetAnimals();

        int Create(Niche niche);
        List<Niche> GetNiches();

        int Create(NicheAnimal nicheAnimal);
        void Delete(NicheAnimal nicheAnimal);
        List<NicheAnimal> GetNicheAnimals();


    }
}
