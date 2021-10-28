using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CorgiVR.Common;
using CorgiVR.Services.Contract;

namespace CorgiVR.ViewModelEntities
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ILoyalityService _loyalityService;

        private bool _isUpdateFlyoutOpen;

        private ObservableCollection<ClientViewModel> clients;

        private ObservableCollection<ClientViewModel> filteredClients = new();

        private string phoneFilter = "";

        private int clientCount;

        private int activeClientsPersent;

        public MainWindowViewModel(ILoyalityService loyalityService)
        {
            _loyalityService = loyalityService;
            CreateFlyoutViewModel = new CreateFlyoutViewModel(loyalityService, InitClients);
            UpdateFlyoutViewModel = new UpdateFlyoutViewModel(loyalityService, InitClients);
            _ = InitClients();
        }

        public ObservableCollection<ClientViewModel> Clients { get => clients; set => Set(ref clients, value); }

        public ObservableCollection<ClientViewModel> FilteredClients { get => filteredClients; set => Set(ref filteredClients, value); }

        public string PhoneFilter
        {
            get => phoneFilter;

            set
            {
                Set(ref phoneFilter, value);
                FilterClients();
            }
        }

        public CreateFlyoutViewModel CreateFlyoutViewModel { get; set; }

        public UpdateFlyoutViewModel UpdateFlyoutViewModel { get; set; }

        public int ClientCount
        {
            get => clientCount;

            set => Set(ref clientCount, value);
        }

        public int ActiveClientsPersent
        {
            get => activeClientsPersent;

            set => Set(ref activeClientsPersent, value);
        }

        private async Task InitClients()
        {
            clients = new ObservableCollection<ClientViewModel>(( await _loyalityService.GetClients() ).Select(x => ClientViewModel.FromServiceEntity(x, UpdateFlyoutViewModel.OpenFlyout)));
            FilterClients();
        }

        private void FilterClients()
        {
            filteredClients.Clear();
            FilteredClients = new ObservableCollection<ClientViewModel>(clients.Where(x => x.Phone?.Contains(PhoneFilter) ?? false));
            ClientCount = filteredClients.Count;
            ActiveClientsPersent = (int)((filteredClients.Count(x => x.Discount != 0) / (decimal)ClientCount) * 100);

        }
    }

}