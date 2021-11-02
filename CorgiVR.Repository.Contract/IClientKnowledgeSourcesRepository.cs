using System.Threading.Tasks;
using CorgiVR.Repository.Contract.Models;
using CorgiVR.Repository.Contract.Projections;

namespace CorgiVR.Repository.Contract
{
    public interface IClientKnowledgeSourcesRepository
    {
        Task<ClientKnowledgeProjection[]> GetClientKnowledgeSources();

        Task UpdateClientKnowledgeSource(ClientKnowledgeSourceUpdateModel model);

        Task CreateClientKnowledgeSource(ClientKnowledgeSourceCreateModel model);
    }
}