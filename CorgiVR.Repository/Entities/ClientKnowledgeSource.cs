namespace CorgiVR.Repository.Entities
{
    public class ClientKnowledgeSource
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int Count { get; set; }
        
        public string CreateDateTime { get; set; }
        
        public string UpdateDateTime { get; set; }
    }
}