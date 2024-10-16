using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ThreeDbsPrOne.Models;

namespace ThreeDbsPrOne.DBs
{
    public class GraphDB : IDisposable
    {
        private readonly IDriver _driver;

        public GraphDB(string uri, string userName, string password)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(userName, password));
        }
        public async Task PrintGreetingAsync(string message)
        {
            var session = _driver.AsyncSession();  

            var greeting = await session.ExecuteWriteAsync(async tx =>
            {
                var result = await tx.RunAsync("CREATE (a:Greeting) " +
                                               "SET a.message = $message " +
                                               "RETURN a.message + ', from node ' + id(a)",
                                               new { message });

                var record = await result.SingleAsync();
                return record[0].As<string>();
            });

            Console.WriteLine(greeting); 

            await session.DisposeAsync();
        }
        public void Dispose()
        {
            _driver?.Dispose();
        }

        public async Task AddResumeAsync(Resume resume)
        {
            var session = _driver.AsyncSession();  

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync("CREATE (r:Resume {Id: $Id, UserId: $UserId, Name: $Name, Surname: $Surname, LivingPlace: $LivingPlace})",
                    new
                    {
                        Id = resume.Id,
                        UserId = resume.UserId,
                        Name = resume.Name,
                        Surname = resume.Surname,
                        LivingPlace = resume.LivingPlace
                    });

                if (resume.Hobbies != null)
                {
                    foreach (var hobbie in resume.Hobbies)
                    {
                        await tx.RunAsync("MATCH (r:Resume {Id: $Id}) " +
                                          "CREATE (h:Hobbie {Name: $HobbieName}) " +
                                          "CREATE (r)-[:HAS_HOBBIE]->(h)",
                                          new { Id = resume.Id, HobbieName = hobbie.ToString() });
                    }
                }

                if (resume.Institutions != null)
                {
                    foreach (var institution in resume.Institutions)
                    {
                        await tx.RunAsync("MATCH (r:Resume {Id: $Id}) " +
                                          "CREATE (i:Institution {Name: $InstitutionName}) " +
                                          "CREATE (r)-[:ATTENDED]->(i)",
                                          new { Id = resume.Id, InstitutionName = institution.ToString() });
                    }
                }

                if (resume.Cities != null)
                {
                    foreach (var city in resume.Cities)
                    {
                        await tx.RunAsync("MATCH (r:Resume {Id: $Id}) " +
                                          "CREATE (c:City {Name: $CityName}) " +
                                          "CREATE (r)-[:LIVES_IN]->(c)",
                                          new { Id = resume.Id, CityName = city.ToString() });
                    }
                }
            });

            await session.DisposeAsync();  // Закрываем сессию

        }
    }
}
