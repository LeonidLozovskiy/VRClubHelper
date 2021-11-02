using System;

namespace CorgiVR.Repository.Contract.Projections
{
    public class ClientKnowledgeProjection
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public string CreateDateTime { get; set; }
        
        public string UpdateDateTime { get; set; }
    }
}