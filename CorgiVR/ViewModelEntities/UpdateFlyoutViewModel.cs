using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CorgiVR.Common;
using CorgiVR.Services.Contract;
using CorgiVR.Services.Contract.Entities;

namespace CorgiVR.ViewModelEntities
{
    public class UpdateFlyoutViewModel : ViewModelBase
    {
        private readonly ILoyalityService _loyalityService;

        private readonly Func<Task> _reloadCliens;

        private bool _isUpdateFlyoutOpen;

        private DateTime createDate;

        private int discount;

        private int id;

        private bool isBiglion;

        private DateTime lastVisitDate;

        private string name;

        private string notes;

        private string phone;

        private int visits;

        public UpdateFlyoutViewModel(
            ILoyalityService loyalityService,
            int id,
            string name,
            int visits,
            string phone,
            DateTime createDate,
            DateTime lastVisitDate,
            bool isBiglion,
            int discount,
            string notes)
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
        }

        public UpdateFlyoutViewModel(ILoyalityService loyalityService, Func<Task> reloadCliens)
        {
            _loyalityService = loyalityService;
            _reloadCliens = reloadCliens;
            CancelFlyoutCommand = new RelayCommand(x => CloseFlyoutClick(x));
            UpdateClientCommand = new RelayCommand(x => UpdateClientClick(x));
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

        public ICommand CancelFlyoutCommand { get; set; }

        public ICommand UpdateClientCommand { get; set; }

        public bool IsUpdateFlyoutOpen
        {
            get => _isUpdateFlyoutOpen;

            set => Set(ref _isUpdateFlyoutOpen, value);
        }

        private void UpdateClientClick(object o)
        {
            _ = UpdateClient();
            IsUpdateFlyoutOpen = false;
        }

        private async Task UpdateClient()
        {
            await _loyalityService.UpdateClient(ToServiceEntity());
            await _reloadCliens();
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

        public void LoadClient(ClientViewModel client)
        {
            Id = client.Id;
            Name = client.Name;
            Phone = client.Phone;
            Visits = client.Visits;
            CreateDate = client.CreateDate;
            LastVisitDate = client.LastVisitDate;
            IsBiglion = client.IsBiglion;
            Discount = client.Discount;
            Notes = client.Notes;
        }

        public void OpenFlyout(ClientViewModel client)
        {
            LoadClient(client);
            IsUpdateFlyoutOpen = true;
        }

        public void CloseFlyoutClick(object sender)
        {
            IsUpdateFlyoutOpen = false;
        }
    }
}