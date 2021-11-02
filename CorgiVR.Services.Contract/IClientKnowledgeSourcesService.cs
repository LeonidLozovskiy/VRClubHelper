using System.Threading.Tasks;
using CorgiVR.Services.Contract.Entities;

namespace CorgiVR.Services.Contract
{
    public interface IClientKnowledgeSourcesService
    {
        Task<ClientKnowledgeSource[]> GetClientKnowledgeSorces();

        Task UpdateClientKnowledge(ClientKnowledgeSource model);

        Task CreateClientKnowledge(ClientKnowledgeSource model);
    }
}