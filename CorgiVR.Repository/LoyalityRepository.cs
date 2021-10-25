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
    public class LoyalityRepository : ILoyalityRepository
    {
        private readonly EfContext _context;

        public LoyalityRepository(EfContext context)
        {
            _context = context;
        }

        public async Task<ClientProjection[]> GetClients()
        {
            return await _context.Clients.Select(x => new ClientProjection
                                                      {
                                                          Id = x.Id,
                                                          Name = x.Name,
                                                          Phone = x.Phone,
                                                          Visits = x.Visits ?? 0,
                                                          CreateDate = string.IsNullOrWhiteSpace(x.CreateDate)
                                                                           ? new DateTime()
                                                                           : DateTime.Parse(x.CreateDate),
                                                          LastVisitDate = string.IsNullOrWhiteSpace(x.LastVisitDate)
                                                                              ? new DateTime()
                                                                              : DateTime.Parse(x.LastVisitDate),
                                                          IsBiglion = x.IsBiglion == 1,
                                                          Notes = x.Notes,
                                                      })
                                 .ToArrayAsync();
        }

        public async Task UpdateClient(ClientUpdateModel model)
        {
            var client = _context.Clients.FirstOrDefault(x => x.Id == model.Id);

            if (client != null)
            {
                client.Name = model.Name;
                client.Phone = model.Phone;
                client.Visits = model.Visits;
                client.CreateDate = model.CreateDate;
                client.LastVisitDate = model.LastVisitDate;
                client.IsBiglion = model.IsBiglion;
                client.Notes = model.Notes;
            }

            await _context.SaveChangesAsync();
        }

        public async Task CreateClient(ClientCreateModel model)
        {
            var client = await _context.Clients.AddAsync(new Client
                                                         {
                                                             Name = model.Name,
                                                             Phone = model.Phone,
                                                             Visits = model.Visits,
                                                             CreateDate = model.CreateDate,
                                                             LastVisitDate = model.LastVisitDate,
                                                             IsBiglion = model.IsBiglion,
                                                             Notes = model.Notes,
                                                         });

            await _context.SaveChangesAsync();
        }
    }
}