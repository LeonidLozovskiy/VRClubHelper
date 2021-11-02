using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using CorgiVR.Common;
using CorgiVR.Services.Contract;
using CorgiVR.Services.Contract.Entities;

namespace CorgiVR.ViewModelEntities
{
    public class AddKnowledgeSourceFlyoutViewModel : ViewModelBase
    {
        private readonly IClientKnowledgeSourcesService _knowledgeSourceService;

        private readonly Func<Task> _reloadKnowledgeSources;

        private bool _isCreateKnowledgeSourceFlyoutOpen;

        private DateTime createDate;

        private DateTime updateDate;

        private int count;

        private string name;

        public AddKnowledgeSourceFlyoutViewModel(IClientKnowledgeSourcesService knowledgeSourceService, Func<Task> reloadKnowledgeSources)
        {
            _knowledgeSourceService = knowledgeSourceService;
            _reloadKnowledgeSources = reloadKnowledgeSources;
            CancelFlyoutCommand = new RelayCommand(x => CloseFlyoutClick(x));
            OpenFlyoutCommand = new RelayCommand(x => OpenFlyoutClick(x));
            CreateKnowledgeSourceCommand = new RelayCommand(x => CreateClick(x));
        }

        public string Name
        {
            get => name;

            set => Set(ref name, value);
        }

        public int Count
        {
            get => count;

            set => Set(ref count, value);
        }

        public DateTime CreateDate
        {
            get => createDate;

            set => Set(ref createDate, value);
        }

        public DateTime UpdateDate
        {
            get => updateDate;

            set => Set(ref updateDate, value);
        }
        
        public ICommand OpenFlyoutCommand { get; set; }
        
        public ICommand CancelFlyoutCommand { get; set; }

        public ICommand CreateKnowledgeSourceCommand { get; set; }

        public bool IsKnowledgeSourceFlyoutOpen
        {
            get => _isCreateKnowledgeSourceFlyoutOpen;

            set => Set(ref _isCreateKnowledgeSourceFlyoutOpen, value);
        }

        private void CreateClick(object o)
        {
            _ = CreateKnowledgeSource();
            IsKnowledgeSourceFlyoutOpen = false;
        }

        private async Task CreateKnowledgeSource()
        {
            await _knowledgeSourceService.CreateClientKnowledge(ToServiceEntity());
            await _reloadKnowledgeSources();
        }

        public ClientKnowledgeSource ToServiceEntity()
        {
            return new ClientKnowledgeSource
                   {
                       Name = Name,
                       Count = Count,
                       CreateDateTime = CreateDate,
                       UpdateDateTime = UpdateDate,
                   };
        }

        public void OpenFlyoutClick(object o)
        {
            Clear();
            IsKnowledgeSourceFlyoutOpen = true;
        }

        private void Clear()
        {
             CreateDate = DateTime.Now;
             UpdateDate = DateTime.Now;
             Count = 1;
             Name = string.Empty;
        }

        public void CloseFlyoutClick(object sender)
        {
            Clear();
            IsKnowledgeSourceFlyoutOpen = false;
        }
    }
}