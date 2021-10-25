using System.Threading.Tasks;
using CorgiVR.Services.Contract.Entities;

namespace CorgiVR.Services.Contract
{
    public interface ILoyalityService
    {
        Task<Client[]> GetClients();

        Task UpdateClient(Client client);

        Task CreateClient(Client client);
    }
}