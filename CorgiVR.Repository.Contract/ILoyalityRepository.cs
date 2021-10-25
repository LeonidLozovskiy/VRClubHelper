using System.Threading.Tasks;
using CorgiVR.Repository.Contract.Models;
using CorgiVR.Repository.Contract.Projections;

namespace CorgiVR.Repository.Contract
{
    public interface ILoyalityRepository
    {
        Task<ClientProjection[]> GetClients();

        Task UpdateClient(ClientUpdateModel model);

        Task CreateClient(ClientCreateModel model);
    }
}