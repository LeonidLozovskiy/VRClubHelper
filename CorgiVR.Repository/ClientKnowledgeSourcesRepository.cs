using System;
using System.Linq;
using System.Threading.Tasks;
using CorgiVR.Repository.Contract;
using CorgiVR.Repository.Contract.Models;
using CorgiVR.Repository.Contract.Projections;
using CorgiVR.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace CorgiVR.Repository
{
    public class ClientKnowledgeSourcesRepository : IClientKnowledgeSourcesRepository
    {
        private readonly EfContext _context;

        public ClientKnowledgeSourcesRepository(EfContext context)
        {
            _context = context;
        }

        public async Task<ClientKnowledgeProjection[]> GetClientKnowledgeSources()
        {
            return await _context.ClientKnowledgeSources.Select(x => new ClientKnowledgeProjection()
                                                      {
                                                          Id = x.Id,
                                                          Name = x.Name,
                                                          Count = x.Count,
                                                          CreateDateTime = x.CreateDateTime,
                                                          UpdateDateTime = x.UpdateDateTime
                                                      })
                                 .ToArrayAsync();
        }

        public async Task UpdateClientKnowledgeSource(ClientKnowledgeSourceUpdateModel model)
        {
            var client = _context.ClientKnowledgeSources.FirstOrDefault(x => x.Id == model.Id);

            if (client != null)
            {
                client.Name = model.Name;
                client.Count = model.Count;
                client.UpdateDateTime = model.UpdateDateTime;
            }

            await _context.SaveChangesAsync();
        }

        public async Task CreateClientKnowledgeSource(ClientKnowledgeSourceCreateModel model)
        {
            var client = await _context.ClientKnowledgeSources.AddAsync(new ClientKnowledgeSource
                                                         {
                                                             Name = model.Name,
                                                             Count = model.Count,
                                                             CreateDateTime = model.CreateDateTime,
                                                             UpdateDateTime = model.UpdateDateTime,
                                                         });

            await _context.SaveChangesAsync();
        }
    }
}