using System;
using CorgiVR.Repository.Contract.Models;

namespace CorgiVR.Services.Contract.Entities
{
    public class ClientKnowledgeSource
    {
        public ClientKnowledgeSource(int id, string name, int count, DateTime createDateTime, DateTime updateDateTime)
        {
            Id = id;
            Name = name;
            Count = count;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
        }

        public ClientKnowledgeSource()
        {
        }

        public int Id { get; set; }
        
        public string Name { get; set; }

        public int Count { get; set; }

        public DateTime CreateDateTime { get; set; }
        
        public DateTime UpdateDateTime { get; set; }

        public ClientKnowledgeSourceUpdateModel ToUpdateModel()
        {
            return new()
                   {
                       Id = Id,
                       Name = Name,
                       Count = Count,
                       UpdateDateTime = UpdateDateTime.ToString(),
                   };
        }

        public ClientKnowledgeSourceCreateModel ToCreateModel()
        {
            return new()
                   {
                       Name = Name,
                       Count = Count,
                       CreateDateTime = CreateDateTime.ToString(),
                       UpdateDateTime = UpdateDateTime.ToString(),
                   };
        }
    }
}