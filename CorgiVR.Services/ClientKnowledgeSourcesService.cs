using System;
using System.Linq;
using System.Threading.Tasks;
using CorgiVR.Repository.Contract;
using CorgiVR.Repository.Contract.Projections;
using CorgiVR.Services.Contract;
using CorgiVR.Services.Contract.Entities;

namespace CorgiVR.Services
{
    public class ClientKnowledgeSourcesService : IClientKnowledgeSourcesService
    {
        private readonly IClientKnowledgeSourcesRepository _clientKnowledgeSourcesRepository;

        public ClientKnowledgeSourcesService(IClientKnowledgeSourcesRepository clientKnowledgeSourcesRepository)
        {
            _clientKnowledgeSourcesRepository = clientKnowledgeSourcesRepository;
        }

        public async Task<ClientKnowledgeSource[]> GetClientKnowledgeSorces()
        {
            return ( await _clientKnowledgeSourcesRepository.GetClientKnowledgeSources() )
                  .Select(x => new ClientKnowledgeSource(
                                                         x.Id,
                                                         x.Name,
                                                         x.Count,
                                                         DateTime.Parse(x.CreateDateTime),
                                                         DateTime.Parse(x.UpdateDateTime)
                                                        ))
                  .ToArray();
        }

        public async Task UpdateClientKnowledge(ClientKnowledgeSource model)
        {
            await _clientKnowledgeSourcesRepository.UpdateClientKnowledgeSource(model.ToUpdateModel());
        }

        public async Task CreateClientKnowledge(ClientKnowledgeSource model)
        {
            await _clientKnowledgeSourcesRepository.CreateClientKnowledgeSource(model.ToCreateModel());
        }
    }

}