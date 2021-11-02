using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using CorgiVR.Common;
using CorgiVR.Services.Contract;

namespace CorgiVR.ViewModelEntities
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ILoyalityService loyalityService;

        private readonly IClientKnowledgeSourcesService knowledgeSourcesService;

        private bool _isUpdateFlyoutOpen;

        private ClientViewModel[] clients;

        private List<ClientViewModel> filteredClients = new();

        private string phoneFilter = "";

        private int clientCount;

        private int activeClientsPersent;

        private List<ClientKnowledgeSourceViewModel> knowledgeSources;

        private bool isAddKnowledgeSourceOpen;
        
        private ICollectionView clientsView;

        private bool isGridScroll;

        public MainWindowViewModel(ILoyalityService loyalityService, IClientKnowledgeSourcesService knowledgeSourcesService)
        {
            this.loyalityService = loyalityService;
            this.knowledgeSourcesService = knowledgeSourcesService;
            CreateFlyoutViewModel = new CreateFlyoutViewModel(loyalityService, InitClients);
            UpdateFlyoutViewModel = new UpdateFlyoutViewModel(loyalityService, InitClients);
            AddKnowledgeSourceFlyoutViewModel = new AddKnowledgeSourceFlyoutViewModel(knowledgeSourcesService, InitKnowledgeSources);

            _ = InitClients();
            _ = InitKnowledgeSources();
        }

        public List<ClientKnowledgeSourceViewModel> KnowledgeSources
        {
            get => knowledgeSources;

            set => Set(ref knowledgeSources, value);
        }

        public ClientViewModel[] Clients { get => clients; set => Set(ref clients, value); }

        public List<ClientViewModel> FilteredClients { get => filteredClients; set => Set(ref filteredClients, value); }

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
        
        public AddKnowledgeSourceFlyoutViewModel AddKnowledgeSourceFlyoutViewModel { get; set; }

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

        public bool IsAddKnowledgeSourceOpen
        {
            get => isAddKnowledgeSourceOpen;

            set => Set(ref isAddKnowledgeSourceOpen, value);
        }

        public bool IsGridScroll
        {
            get => isGridScroll;

            set => Set(ref isGridScroll, value);
        }

        private async Task InitKnowledgeSources()
        {
            KnowledgeSources = new List<ClientKnowledgeSourceViewModel>(( await knowledgeSourcesService.GetClientKnowledgeSorces() ).Select(x => ClientKnowledgeSourceViewModel.FromServiceEntity(x)));
        }
        
        private async Task InitClients()
        {
            Clients = ( await loyalityService.GetClients() ).Select(x => ClientViewModel.FromServiceEntity(x, UpdateFlyoutViewModel.OpenFlyout)).ToArray();
            
            clientsView = CollectionViewSource.GetDefaultView(clients);
            clientsView.Filter = o => string.IsNullOrEmpty(PhoneFilter) || ( ( o as  ClientViewModel )?.Phone ?? "" ).Contains(PhoneFilter); 

            
            FilterClients();
        }

        private void FilterClients()
        {
            var test = Stopwatch.StartNew();
            isGridScroll = true;
            clientsView.Refresh();
            isGridScroll = false;
            test.Stop();
            Console.WriteLine(test.ElapsedMilliseconds);
            ClientCount = clientsView.Cast<object>().Count();
            ActiveClientsPersent = (int)((clients.Count(x => x.Discount != 0) / (decimal)ClientCount) * 100);
        }
    }

}