using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using ThreeDbsPrOne.Models;

namespace ThreeDbsPrOne.DBs
{
    public static class DocumentDB
    {
        private static MongoClient client;
        private static IMongoDatabase taskDB;

        private static IMongoCollection<Resume> resumeCol;
        public static List<Resume> resumes;


        private static IMongoCollection<User> userCol;
        public static List<User> users;

        static DocumentDB()
        {
            try
            {
                client = new MongoClient("mongodb://localhost:27017/");
                taskDB = client.GetDatabase("Task");

                resumeCol = taskDB.GetCollection<Resume>("Resume");
                userCol = taskDB.GetCollection<User>("User");

                resumes = resumeCol.Find(_ => true).ToList();
                users = userCol.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine("Ошибка при инициализации базы данных: " + ex.Message);
                throw;
            }
        }
        public static void AddResume(Resume resume)
        {
            if (resumes.Any(x => x.Id == resume.Id)) return;
            resumeCol.InsertOne(resume); 
            resumes.Add(resume); 
        }
        public static void AddUser(User user)
        {
            if (users.Any(x => x.Id == user.Id)) return;
            userCol.InsertOne(user); 
            users.Add(user); 
        }


        
    }
}
