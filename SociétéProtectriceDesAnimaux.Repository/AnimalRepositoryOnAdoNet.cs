using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SociétéProtectriceDesAnimaux.Entities;

namespace SociétéProtectriceDesAnimaux.Repository
{
    public class AnimalRepositoryOnAdoNet: IAnimalRepository
    {
        private string _connectionString;

        public AnimalRepositoryOnAdoNet(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Animal: CREATE, DELETE and GET.
        public int Create(Animal animal)
        {
            using(var con = new SqlConnection(_connectionString))
            {
                con.Open();
                var paramName = new SqlParameter("@name", animal.Name);
                var paramType = new SqlParameter("@type", (int)animal.TypeAnimal);
                string sql = "INSERT INTO Animal(Name, Type) VALUES (@name, @type)";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramType);
                cmd.ExecuteNonQuery();

                sql = "SELECT @@Identity FROM Animal";
                cmd = new SqlCommand(sql, con);
                int animalId = (int) (decimal) cmd.ExecuteScalar();

                con.Close();
                return animalId;
            }
        }
        public void Delete(Animal animal)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                var paramId = new SqlParameter("@Id", animal.Id);
                string sql = "DELETE Animal WHERE Id = @Id";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(paramId);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public List<Animal> GetAnimals()
        {
            DataTable dataTable = new DataTable();
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id, Name, Type FROM Animal";
                var cmd = new SqlCommand(sql, connection);
                var sda = new SqlDataAdapter(cmd);
                sda.Fill(dataTable);
            }

            var animals = new List<Animal>();
            foreach(DataRow dr in dataTable.Rows)
            {
                if(int.TryParse(dr["Type"].ToString(), out int typeAnimal) && int.TryParse(dr["Id"].ToString(), out int id))
                {
                    var animal = new Animal();
                    animal.Id = id;
                    animal.Name = dr["Name"].ToString();
                    animal.TypeAnimal = (TypeAnimal)typeAnimal;
                    animals.Add(animal);
                }
            }
            return animals;
        }

        // Niche: CREATE, DELETE and GET.
        public int Create(Niche niche)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                var paramName = new SqlParameter("@name", niche.Name);
                string sql = "INSERT INTO Niche(Name) VALUES (@name)";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(paramName);
                cmd.ExecuteNonQuery();

                sql = "SELECT @@Identity FROM Niche";
                cmd = new SqlCommand(sql, con);
                int nicheId = (int)(decimal)cmd.ExecuteScalar();
                con.Close();
                return nicheId;
            }
        }

        public List<Niche> GetNiches()
        {
            DataTable dataTable = new DataTable();
            using (var connection = new SqlConnection(_connectionString)) 
            {
                string sql = "SELECT Id, Name FROM Niche";
                var cmd = new SqlCommand(sql, connection);
                var sda = new SqlDataAdapter(cmd);
                sda.Fill(dataTable);
            }

            var niches = new List<Niche>();
            foreach(DataRow drow in dataTable.Rows)
            {
                if (int.TryParse(drow["Id"].ToString(), out int id))
                {
                    var niche = new Niche();
                    niche.Id = id;
                    niche.Name = drow["Name"].ToString();
                    niches.Add(niche);
                }
            }
            return niches;
        }

        // NicheAnimal: CREATE, DELETE and GET.
        public int Create(NicheAnimal nicheAnimal)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                var paramIdAnimal = new SqlParameter("@IdAnimal", nicheAnimal.AnimalId);
                var paramIdNiche = new SqlParameter("@IdNiche", nicheAnimal.NicheId);

                string sql = "INSERT INTO NicheAnimal(IdAnimal, IdNiche) VALUES (@IdAnimal, @IdNiche)";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(paramIdAnimal);
                cmd.Parameters.Add(paramIdNiche);
                cmd.ExecuteNonQuery();

                sql = "SELECT @@Identity FROM NicheAnimal";
                cmd = new SqlCommand(sql, con);
                int id = (int)(decimal)cmd.ExecuteScalar();
                con.Close();
                return id;
            }
        }

        public void Delete(NicheAnimal nicheAnimal)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                var paramIdAnimal = new SqlParameter("@IdAnimal", nicheAnimal.AnimalId);
                var paramIdNiche = new SqlParameter("@IdNiche", nicheAnimal.NicheId);
              
                string sql = "DELETE NicheAnimal WHERE IdAnimal = @AnimalId AND IdNiche = @IdNiche";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(paramIdAnimal);
                cmd.Parameters.Add(paramIdNiche);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public List<NicheAnimal> GetNicheAnimals()
        {
            var dataTable = new DataTable();
            using(var connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id, IdAnimal, IdNiche FROM NicheAnimal";
                var cmd = new SqlCommand(sql, connection);
                var sda = new SqlDataAdapter(cmd);
                sda.Fill(dataTable);
            }

            var nicheAnimals = new List<NicheAnimal>();
            foreach (DataRow drow in dataTable.Rows)
            {
                if (int.TryParse(drow["Id"].ToString(), out int id) && int.TryParse(drow["IdAnimal"].ToString(), out int idAnimal) 
                    && int.TryParse(drow["IdNiche"].ToString(), out int idNiche))
                {
                    var nicheAnimal = new NicheAnimal();
                    nicheAnimal.Id = id;
                    nicheAnimal.AnimalId = idAnimal;
                    nicheAnimal.NicheId = idNiche;
                    nicheAnimals.Add(nicheAnimal);
                }
            }
            return nicheAnimals;
        }

    }
}
