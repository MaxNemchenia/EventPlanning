using EventPlanning.DAL.DAL;
using EventPlanning.DAL.Entities;
using EventPlanning.DAL.Interfaces;

namespace EventPlanning.DAL.Repositories
{
    public class ClientManager:IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }


        public void Create(ClientProfile clientProfile)
        {
            Database.ClientProfiles.Add(clientProfile);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
