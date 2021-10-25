using System;
using CorgiVR.Repository.Contract.Models;

namespace CorgiVR.Services.Contract.Entities
{
    public class Client
    {
        public Client()
        {
        }

        public Client(
            int id,
            string name,
            string phone,
            int visits,
            DateTime createDate,
            DateTime lastVisitDate,
            bool isBiglion,
            int discount,
            string notes)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Visits = visits;
            CreateDate = createDate;
            LastVisitDate = lastVisitDate;
            IsBiglion = isBiglion;
            Discount = discount;
            Notes = notes;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public int Visits { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastVisitDate { get; set; }

        public bool IsBiglion { get; set; }

        public int Discount { get; set; }

        public string Notes { get; set; }

        public ClientUpdateModel ToUpdateModel()
        {
            return new()
                   {
                       Id = Id,
                       Name = Name,
                       Phone = Phone,
                       Visits = Visits,
                       CreateDate = CreateDate.ToString(),
                       LastVisitDate = LastVisitDate.ToString(),
                       IsBiglion = IsBiglion
                                       ? 1
                                       : 0,
                       Notes = Notes,
                   };
        }

        public ClientCreateModel ToCreateModel()
        {
            return new()
                   {
                       Name = Name,
                       Phone = Phone,
                       Visits = Visits,
                       CreateDate = DateTime.Now.ToString(),
                       LastVisitDate = DateTime.Now.ToString(),
                       IsBiglion = IsBiglion
                                       ? 1
                                       : 0,
                       Notes = Notes,
                   };
        }
    }
}