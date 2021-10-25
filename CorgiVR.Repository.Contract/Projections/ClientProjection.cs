using System;

namespace CorgiVR.Repository.Contract.Projections
{
    public class ClientProjection
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public int Visits { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastVisitDate { get; set; }

        public bool IsBiglion { get; set; }

        public string Notes { get; set; }
    }
}