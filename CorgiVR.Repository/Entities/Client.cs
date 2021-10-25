namespace CorgiVR.Repository.Entities
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public int? Visits { get; set; }

        public string CreateDate { get; set; }

        public string LastVisitDate { get; set; }

        public int? IsBiglion { get; set; }

        public string Notes { get; set; }
    }
}