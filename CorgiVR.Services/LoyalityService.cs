using System;
using System.Linq;
using System.Threading.Tasks;
using CorgiVR.Repository.Contract;
using CorgiVR.Repository.Contract.Projections;
using CorgiVR.Services.Contract;
using CorgiVR.Services.Contract.Entities;

namespace CorgiVR.Services
{
    public class LoyalityService : ILoyalityService
    {
        private readonly ILoyalityRepository _loyalityRepository;

        public LoyalityService(ILoyalityRepository loyalityRepository)
        {
            _loyalityRepository = loyalityRepository;
        }

        public async Task<Client[]> GetClients()
        {
            return ( await _loyalityRepository.GetClients() )
                  .Select(x => new Client
                               {
                                   Id = x.Id,
                                   Name = x.Name,
                                   Phone = x.Phone,
                                   Visits = x.Visits,
                                   CreateDate = x.CreateDate,
                                   LastVisitDate = x.LastVisitDate,
                                   IsBiglion = x.IsBiglion,
                                   Discount = CalculateDiscount(x),
                                   Notes = x.Notes,
                               })
                  .ToArray();
        }

        public async Task UpdateClient(Client client)
        {
            await _loyalityRepository.UpdateClient(client.ToUpdateModel());
        }

        public async Task CreateClient(Client client)
        {
            await _loyalityRepository.CreateClient(client.ToCreateModel());
        }

        private int CalculateDiscount(ClientProjection clientProjection)
        {
            if ((DateTime.Now - clientProjection.LastVisitDate).Days <= 14)
            {
                return clientProjection.IsBiglion
                           ? 50
                           : 20;
            }

            return 0;
        }
    }

}