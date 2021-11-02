using System;
using System.Windows.Input;
using CorgiVR.Common;
using CorgiVR.Services.Contract;
using CorgiVR.Services.Contract.Entities;

namespace CorgiVR.ViewModelEntities
{
    public class ClientViewModel : ViewModelBase
    {
        private readonly ILoyalityService _loyalityService;

        private readonly Action<ClientViewModel> _openUpdateFlyout;

        private DateTime createDate;

        private int discount;

        private int id;

        private bool isBiglion;

        private DateTime lastVisitDate;

        private string name;

        private string notes;

        private string phone;

        private int visits;

        private int daysFromLastVisit;

        public ClientViewModel(
            ILoyalityService loyalityService,
            int id,
            string name,
            int visits,
            string phone,
            DateTime createDate,
            DateTime lastVisitDate,
            bool isBiglion,
            int discount,
            string notes,
            Action<ClientViewModel> openUpdateFlyout)
        {
            _loyalityService = loyalityService;
            this.id = id;
            this.name = name;
            this.visits = visits;
            this.phone = phone;
            this.createDate = createDate;
            this.lastVisitDate = lastVisitDate;
            this.isBiglion = isBiglion;
            this.discount = discount;
            this.notes = notes;
            _openUpdateFlyout = openUpdateFlyout;
            this.daysFromLastVisit = GetDaysFromLastVisit();

            EditCommand = new RelayCommand(x => EditCommandClick(x));
            AddVisitCommand = new RelayCommand(x => AddVisitClick(x));
        }

        public int Id
        {
            get => id;

            set => Set(ref id, value);
        }

        public string Name
        {
            get => name;

            set => Set(ref name, value);
        }

        public string Phone
        {
            get => phone;

            set => Set(ref phone, value);
        }

        public int Visits
        {
            get => visits;

            set => Set(ref visits, value);
        }

        public DateTime CreateDate
        {
            get => createDate;

            set => Set(ref createDate, value);
        }

        public DateTime LastVisitDate
        {
            get => lastVisitDate;

            set => Set(ref lastVisitDate, value);
        }

        public bool IsBiglion
        {
            get => isBiglion;

            set => Set(ref isBiglion, value);
        }

        public int Discount
        {
            get => discount;

            set => Set(ref discount, value);
        }

        public string Notes
        {
            get => notes;

            set
            {
                Set(ref notes, value);
                _ = _loyalityService.UpdateClient(ToServiceEntity());
            }
        }
        
        public int DaysFromLastVisit
        {
            get => daysFromLastVisit;

            set
            {
                Set(ref daysFromLastVisit, value);
            }
        }

        public ICommand EditCommand { get; set; }

        public ICommand AddVisitCommand { get; set; }

        public static ClientViewModel FromServiceEntity(Client entity, Action<ClientViewModel> openUpdateFlyout)
        {
            return new(
                       ServiceProviderFactory.Container.GetService(typeof(ILoyalityService)) as ILoyalityService,
                       entity.Id,
                       entity.Name,
                       entity.Visits,
                       entity.Phone,
                       entity.CreateDate,
                       entity.LastVisitDate,
                       entity.IsBiglion,
                       entity.Discount,
                       entity.Notes,
                       openUpdateFlyout);
        }

        public Client ToServiceEntity()
        {
            return new(
                       Id,
                       Name,
                       Phone,
                       Visits,
                       CreateDate,
                       LastVisitDate,
                       IsBiglion,
                       Discount,
                       Notes);
        }

        private int GetDaysFromLastVisit()
        {
            return ( DateTime.Now - this.lastVisitDate ).Days;
        }
        
        private void EditCommandClick(object o)
        {
            _openUpdateFlyout(this);
        }

        private void AddVisitClick(object o)
        {
            Visits++;
            LastVisitDate = DateTime.Now;
            DaysFromLastVisit = GetDaysFromLastVisit();
            _loyalityService.UpdateClient(ToServiceEntity());
        }
    }
}