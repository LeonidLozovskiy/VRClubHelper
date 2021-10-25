using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CorgiVR.Common;
using CorgiVR.Services.Contract;
using CorgiVR.Services.Contract.Entities;

namespace CorgiVR.ViewModelEntities
{
    public class CreateFlyoutViewModel : ViewModelBase
    {
        private readonly ILoyalityService _loyalityService;

        private bool _isBiglion;

        private bool _isCreateFlyoutOpen;

        private string _name;

        private string _notes;

        private string _phone;

        private readonly Func<Task> _reloadCliens;

        public CreateFlyoutViewModel(ILoyalityService loyalityService, Func<Task> reloadCliens)
        {
            _loyalityService = loyalityService;
            _isCreateFlyoutOpen = false;
            OpenFlyoutCommand = new RelayCommand(x => TogleFlyoutClick(x));
            CancelFlyoutCommand = new RelayCommand(x => TogleFlyoutClick(x));
            AddClientCommand = new RelayCommand(x => AddClientClick(x));
            _reloadCliens = reloadCliens;
        }

        public string Name
        {
            get => _name;

            set => Set(ref _name, value);
        }

        public string Phone
        {
            get => _phone;

            set => Set(ref _phone, value);
        }

        public bool IsBiglion
        {
            get => _isBiglion;

            set => Set(ref _isBiglion, value);
        }

        public string Notes
        {
            get => _notes;

            set => Set(ref _notes, value);
        }

        public bool IsCreateFlyoutOpen
        {
            get => _isCreateFlyoutOpen;

            set => Set(ref _isCreateFlyoutOpen, value);
        }

        public ICommand OpenFlyoutCommand { get; set; }

        public ICommand CancelFlyoutCommand { get; set; }

        public ICommand AddClientCommand { get; set; }

        private void TogleFlyoutClick(object sender)
        {
            Clean();
            IsCreateFlyoutOpen = !_isCreateFlyoutOpen;
        }

        private void AddClientClick(object sender)
        {
            var serviceModel = ToServiceModel();
            _ = SaveClient(serviceModel);
            Clean();
            IsCreateFlyoutOpen = !_isCreateFlyoutOpen;
        }

        private async Task SaveClient(Client serviceModel)
        {
            await _loyalityService.CreateClient(serviceModel);
            await _reloadCliens();
        }

        private void Clean()
        {
            Name = string.Empty;

            Phone = string.Empty;

            Notes = string.Empty;

            IsBiglion = false;
        }

        private Client ToServiceModel()
        {
            return new()
                   {
                       Name = _name,
                       Phone = _phone,
                       Visits = 1,
                       CreateDate = DateTime.Now,
                       LastVisitDate = DateTime.Now,
                       IsBiglion = _isBiglion,
                       Notes = _notes,
                   };
        }
    }
}