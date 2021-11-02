namespace CorgiVR.Repository.Contract.Models
{
    public class ClientKnowledgeSourceCreateModel
    {
        public string Name { get; set; }

        public int Count { get; set; }
        
        public string CreateDateTime { get; set; }
        
        public string UpdateDateTime { get; set; }
    }
}