using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

using ThreeDbsPrOne.Models;
using ThreeDbsPrOne.Models.Enums;
using System.Runtime.CompilerServices;
using System.Net.Http.Headers;
using System.Collections;
using ThreeDbsPrOne.Windows;


namespace ThreeDbsPrOne.DBs
{
    internal static class SsmsUsage
    {
        private static readonly string _connectionString =
         @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ThreeDBsPrOne;Integrated Security=True;";

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM [User]";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User newUser = new User();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string colName = reader.GetName(i);
                        newUser[colName] = reader[i];
                    }
                    users.Add(newUser);
                }
                connection.Close();
            }
            return users;
        }

        public static List<Resume> GetResumes()
        {
            List<Resume> resumes = new List<Resume>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM [Resume]";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Resume newResume = new Resume();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string colName = reader.GetName(i);

                        if (colName == "Id") newResume.Id = reader[i].ToString();
                        else if (colName == "UserId") newResume.UserId = (int)reader[i];
                        else newResume[colName] = reader[i];
                    }
                    //set other params for resume

                    newResume.Hobbies = GetHobbieForResume(newResume.Id);
                    newResume.Cities = GetCityForResume(newResume.Id);
                    newResume.Institutions = GetInstitutionForResume(newResume.Id);

                    resumes.Add(newResume);
                }
                connection.Close();
            }
            return resumes;
        }

        private static List<Institution> GetInstitutionForResume(string resumeId)
        {
            List<Institution> institutions = new List<Institution>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM [InstitutionResume]";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    bool check = false;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string colName = reader.GetName(i);

                        if (check && colName == "InstitutionId")
                        {
                            Institution? insRes = GetInstitutionById((int)reader[i]);

                            if (!(institutions is null)) institutions.Add((Institution)insRes);
                            check = !check;
                        }
                        if (colName == "ResumeId" && reader[i].ToString() == resumeId)
                        {
                            check = true;
                        }
                    }
                }
                connection.Close();
            }
            return institutions;
        }
        private static Institution? GetInstitutionById(int institutionId)
        {
            for (int i = (int)Institution.University; i <= (int)Institution.Corporation; i++)
            {
                if (i == institutionId)
                {
                    return (Institution)i;
                }
            }
            return null;
        }

        private static List<City> GetCityForResume(string resumeId)
        {
            List<City> cities = new List<City>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM [CityResume]";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    bool check = false;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string colName = reader.GetName(i);

                        if (check && colName == "CityId")
                        {
                            City? cityRes = GetCityById((int)reader[i]);

                            if (!(cities is null)) cities.Add((City)cityRes);
                            check = !check;
                        }
                        if (colName == "ResumeId" && reader[i].ToString() == resumeId)
                        {
                            check = true;
                        }
                    }
                }
                connection.Close();
            }
            return cities;
        }
        private static City? GetCityById(int cityId)
        {
            for (int i = (int)City.Paris; i <= (int)City.Toronto; i++)
            {
                if (i == cityId)
                {
                    return (City)i;
                }
            }
            return null;
        }


        private static List<Hobbie> GetHobbieForResume(string resumeId)
        {
            List<Hobbie> hobbies = new List<Hobbie>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM [HobbieResume]";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    bool check = false;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string colName = reader.GetName(i);

                        if (check && colName == "HobbieId")
                        {
                            Hobbie? hobRes = GetHobbieById((int)reader[i]);

                            if (!(hobbies is null)) hobbies.Add((Hobbie)hobRes);
                            check = !check;
                        }
                        if (colName == "ResumeId" && reader[i].ToString() == resumeId)
                        {
                            check = true;
                        }
                    }
                }
                connection.Close();
            }
            return hobbies;
        }
        private static Hobbie? GetHobbieById(int hobbieId)
        {
            for (int i = (int)Hobbie.Reading; i <= (int)Hobbie.Singing; i++)
            {
                if (i == hobbieId)
                {
                    return (Hobbie)i;
                }
            }
            return null;
        }

        public static void RemoveResume(string resumeId)
        {
            GetResumeById(resumeId);


            //Remove resume
            /*            using (SqlConnection connection = new SqlConnection(_connectionString))
                        {
                            connection.Open();
                            RemoveFromCityResumeByResumeId(resumeId);
                            RemoveFromInstitutionResumeByResumeId(resumeId);
                            RemoveFromHobbieResumeByResumeId(resumeId);

                            RemoveResumeById(resumeId);
                            connection.Close();
                        }*/
        }
        public static Resume GetResumeById(string resumeId)
        {
            List<Resume> resume = GetResumes();

            return resume.Where(x => x.Id == resumeId).First();
        }

        private static void RemoveResumeById(int resumeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM [Resume] WHERE [Id] = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", resumeId);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        private static void RemoveFromCityResumeByResumeId(int resumeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM [CityResume] WHERE [ResumeId] = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", resumeId);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        private static void RemoveFromInstitutionResumeByResumeId(int resumeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM [InstitutionResume] WHERE [ResumeId] = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", resumeId);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        private static void RemoveFromHobbieResumeByResumeId(string resumeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM [HobbieResume] WHERE [ResumeId] = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", resumeId);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public static void RemoveHobbiesByResumeId(string resumeId)
        {
            RemoveFromHobbieResumeByResumeId(resumeId);
        }
        public static string GetHobbiesResumeById(string resumeId)
        {
            string res = "";

            List<Hobbie> hobbies = GetHobbiesByResumeId(resumeId);
            res += "Hobbies: \n";
            for (int i = 0; i < hobbies.Count; i++)
            {
                res += hobbies[i].ToString() + "\n";
            }

            return res;
        }
        private static List<Hobbie> GetHobbiesByResumeId(string resumeId)
        {
            List<Hobbie> res = new List<Hobbie>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT [HobbieId] FROM [HobbieResume] WHERE [ResumeId] = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", resumeId);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    res.Add((Hobbie)id);
                }
                connection.Close();
            }

            return res;
        }
        public static string GetCitiesResumeById(string resumeId)
        {
            string res = "";
            List<City> cities = GetCitiesByResumeId(resumeId);
            res += "Cities: \n";
            for (int i = 0; i < cities.Count; i++)
            {
                res += cities[i].ToString() + "\n";
            }
            return res;
        }
        private static List<City> GetCitiesByResumeId(string resumeId)
        {
            List<City> res = new List<City>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT [CityId] FROM [CityResume] WHERE [ResumeId] = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", resumeId);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    res.Add((City)id);
                }
                connection.Close();
            }
            return res;
        }

        public static void RemovePlacesByResumeId(int resumeId)
        {
            RemoveFromCityResumeByResumeId(resumeId);
        }
        public static void RemoveHobbiesWithChosenCity(City city)
        {
            //Convert city in number
            int cityNumber = (int)city;

            //get resumes(their numbers) with such number
            List<string> resumeIds = GetResumesIdsWithGivenCityId(cityNumber.ToString());

            //Remove hobbies in that resumes
            for (int i = 0; i < resumeIds.Count; i++)
            {
                RemoveFromHobbieResumeByResumeId(resumeIds[i]);
            }
        }
        public static string GetHobbieStringWithChosenCity(City city)
        {
            string res = "";

            //Convert city in number
            int cityNumber = (int)city;

            //get resumes(their numbers) with such number
            List<string> resumeIds = GetResumesIdsWithGivenCityId(cityNumber.ToString());

            //Remove hobbies in that resumes
            for (int i = 0; i < resumeIds.Count; i++)
            {
                List<Hobbie> hobbies = GetHobbiesByResumeId(resumeIds[i]);
                res += $"\nResume Index - {resumeIds[i].ToString()}\n";

                for (int j = 0; j < hobbies.Count; j++)
                {
                    res += $"{((Hobbie)hobbies[j])} ";
                }
            }
            return res;
        }
        private static List<string> GetResumesIdsWithGivenCityId(string cityId)
        {
            List<string> res = new List<string>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT [ResumeId] FROM [CityResume] WHERE [CityId] = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", cityId);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    if (!res.Contains(id.ToString()))
                    {
                        res.Add(id.ToString());
                    }
                }
                connection.Close();
            }
            return res;
        }

        public static void RemoveUsersWitchWorkInOnePlace(List<int> userIds, List<int> resumeIds)
        {
            for (int i = 0; i < resumeIds.Count; i++)
            {
                RemoveResume(resumeIds[i].ToString());
            }
            for (int i = 0; i < userIds.Count; i++)
            {
                RemoveUserById(userIds[i]);
            }
        }

        private static void RemoveUserById(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM [User] WHERE [Id] = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", userId);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public static string GetFifthTaskString()
        {
            string res = "";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT u.Id, u.Login, u.Password " +
                    "FROM [User] u " +
                    "INNER JOIN [Resume] r ON u.Id = r.UserId " +
                    "INNER JOIN [InstitutionResume] ir ON r.Id = ir.ResumeId " +
                    "GROUP BY u.Id, u.Login, u.Password " +
                    "HAVING COUNT(ir.InstitutionId) != 1;";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string colName = reader.GetName(i);

                        if (colName == "Id")
                        {
                            res += $"User id: {reader[i].ToString()} \n";
                        }
                        else if (colName == "Name")
                        {
                            res += $"User name: {reader[i].ToString()} \n";
                        }
                    }
                    res += "\n";
                }
                connection.Close();
            }
            return res;
        }

    }
}
