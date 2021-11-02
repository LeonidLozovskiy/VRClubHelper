using System;
using System.Windows.Input;
using CorgiVR.Common;
using CorgiVR.Services.Contract;
using CorgiVR.Services.Contract.Entities;

namespace CorgiVR.ViewModelEntities
{
    public class ClientKnowledgeSourceViewModel : ViewModelBase
    {
        private readonly IClientKnowledgeSourcesService _clientKnowledgeSourcesService;

        private int id;
        
        private string name;

        private int count;

        private DateTime createDate;

        private DateTime updateDate;

        public ClientKnowledgeSourceViewModel(
            IClientKnowledgeSourcesService clientKnowledgeSourcesService,
            int id,
            string name,
            int count)
        {
            this._clientKnowledgeSourcesService = clientKnowledgeSourcesService;
            this.id = id;
            this.name = name;
            this.count = count;

            IncreaseCountCommand = new RelayCommand(x => IncreaseCountClick(x));
            DecreaseCountCommand = new RelayCommand(x => DecreaseCountClick(x));
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

        public ICommand IncreaseCountCommand { get; set; }
        
        public ICommand DecreaseCountCommand { get; set; }

        public static ClientKnowledgeSourceViewModel FromServiceEntity(ClientKnowledgeSource entity)
        {
            return new(
                       ServiceProviderFactory.Container.GetService(typeof(IClientKnowledgeSourcesService)) as IClientKnowledgeSourcesService,
                       entity.Id,
                       entity.Name,
                       entity.Count
                      );
        }

        public ClientKnowledgeSource ToServiceEntity()
        {
            return new(
                       Id,
                       Name,
                       Count,
                       CreateDate,
                       UpdateDate);
        }

        private void IncreaseCountClick(object o)
        {
            Count++;
            _clientKnowledgeSourcesService.UpdateClientKnowledge(ToServiceEntity());
        }
        
        private void DecreaseCountClick(object o)
        {
            if (count != 0)
            {
                Count--;
                _clientKnowledgeSourcesService.UpdateClientKnowledge(ToServiceEntity());
            }
        }
        
    }
}