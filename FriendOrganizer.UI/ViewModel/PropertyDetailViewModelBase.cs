

using Prism.Events;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.View.Services;
using System.Collections.ObjectModel;
using Prism.Commands;
using System.Threading.Tasks;
using System.ComponentModel;
using FriendOrganizer.UI.Wrapper;
using FriendOrganizer.Model;
using System.Linq;
using System;

namespace FriendOrganizer.UI.ViewModel
{
    public abstract class PropertyDetailViewModelBase<R,P> : ViewModelBase
              where R : IGenericRepository<P>
              where P :Property,new()
    {

        protected R _repository;
        private PropertyItemWrapper<P> _selectedProperty;
        public PropertyDetailViewModelBase(IEventAggregator eventAggregator,
            IMessageDialogService messageDiadlogService,
            R repository) 
            : base(eventAggregator, messageDiadlogService)
        {

            _repository = repository;
            Properties = new ObservableCollection<PropertyItemWrapper<P>>();
            AddCommand = new DelegateCommand(OnAddExecute);
            RemoveCommand = new DelegateCommand(OnRemoveExecute, OnRemoveCanExecute);
        }

        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand RemoveCommand { get; private set; }
        public ObservableCollection<PropertyItemWrapper<P>> Properties { get;  }



        public PropertyItemWrapper<P> SelectedProperty
        {
            get { return _selectedProperty; }
            set
            {
                _selectedProperty = value;
                OnPropertyChanged();
                RemoveCommand.RaiseCanExecuteChanged();
            }
        }

        public async override Task LoadAsync(int id)
        {
            Id = id;

            foreach (var wrapper in Properties)
            {
                wrapper.PropertyChanged -= Wrapper_PropertyChanged;
            }
            Properties.Clear();


            var items = await _repository.GetAllAsync();

            foreach (var model in items)
            {
               
                var wrapper = new PropertyItemWrapper<P>(model);
                wrapper.PropertyChanged += Wrapper_PropertyChanged;
                Properties.Add(wrapper);
            }
        }

        private void Wrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _repository.HasChanges();
            }
            if (e.PropertyName == nameof(PropertyItemWrapper<P>.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        private bool OnRemoveCanExecute()
        {
            return SelectedProperty != null;

        }

        public  abstract Task<bool> IsReferenced();
       

        private async void OnRemoveExecute()
        {
           


            if (IsReferenced().Result)
            {
                await MessageDialogService.ShowInfoDialogAsync($"Язык {SelectedProperty.Name}" +
                   $"  не может быть удален , так как выбран в друге", "AHTUNG");
                return;
            }

            SelectedProperty.PropertyChanged -= Wrapper_PropertyChanged;
            _repository.Delete(SelectedProperty.Model);
            Properties.Remove(SelectedProperty);
            SelectedProperty = null;
            HasChanges = _repository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void OnAddExecute()
        {
            var wrapper = new PropertyItemWrapper<P>(new P());
            wrapper.PropertyChanged += Wrapper_PropertyChanged;
            _repository.Add(wrapper.Model);
            Properties.Add(wrapper);

            wrapper.Name = "";
        }
        protected override bool OnSaveCanExecute()
        {
            return HasChanges && Properties.All(p => !p.HasErrors);
        }

        protected async override void OnSaveExecute()
        {
            try
            {
                await _repository.SaveAsync();
                HasChanges = _repository.HasChanges();
                RaiseCollectionSavedEvent();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                await MessageDialogService.ShowInfoDialogAsync("Ошибка при сохранении объекта, " +
                     "данные будут перезагружены. Детали: " + ex.Message, "Ошибка");
                await LoadAsync(Id);
            }


        }
    }
}
