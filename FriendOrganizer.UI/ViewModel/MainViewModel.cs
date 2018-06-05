using FriendOrganizer.UI.Event;
using Prism.Events;
using System;
using System.Threading.Tasks;
using FriendOrganizer.UI.View.Services;
using System.Windows.Input;
using Prism.Commands;
using Autofac.Features.Indexed;
using System.Collections.ObjectModel;
using System.Linq;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel : ViewModelBasePropertyChanged
  {

        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialoService;

        private IDetailViewModel1 _selectedDetailViewModel;
        private IIndex<string, IDetailViewModel1> _detailViewModelCreator;

        public MainViewModel(INavigationViewModel navigationViewModel,
        IIndex<string,IDetailViewModel1> detailViewModelCreator,
        IEventAggregator eventAggregator,
        IMessageDialogService messageDialoService)
    {
            _messageDialoService = messageDialoService;
            _eventAggregator = eventAggregator;
            _detailViewModelCreator = detailViewModelCreator;

            DetailViewModels = new ObservableCollection<IDetailViewModel1>();

            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Subscribe(OnOpenDetailView);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);

            OpenSingleDetailViewCommand = new DelegateCommand<Type>(OnOpenSingleDetailViExecute);

            _eventAggregator.GetEvent<AfterDetailDeletedEvent>()
             .Subscribe(AfterDetailDeleted);

            _eventAggregator.GetEvent<AfterDetailCloseEvent>()
           .Subscribe(AfterDetailClosed);

            NavigationViewModel = navigationViewModel;
        }


        public async Task LoadAsync()
        {
               await NavigationViewModel.LoadAsync();
        }


        public ICommand CreateNewDetailCommand { get; }

        public ICommand OpenSingleDetailViewCommand { get;  }

        public INavigationViewModel NavigationViewModel { get; }

        public ObservableCollection<IDetailViewModel1> DetailViewModels { get; }

        public IDetailViewModel1 SelectedDetailViewModel
        {
            get { return _selectedDetailViewModel; }
            set
            {
                _selectedDetailViewModel = value;
                OnPropertyChanged();
            }
        }



        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {

           var detailViewModel = DetailViewModels
                .SingleOrDefault(vm => vm.Id == args.Id
                && vm.GetType().Name == args.ViewModelName);

            if (detailViewModel == null)
            {
                detailViewModel = _detailViewModelCreator[args.ViewModelName];
                try
                {

                    await detailViewModel.LoadAsync(args.Id);
                }
                catch 
                {
                    await _messageDialoService.ShowInfoDialogAsync("Невозможно загрузить элемент,возможно он был удален.", "АХТУНГ");
                    await NavigationViewModel.LoadAsync();
                    return;
                }

                DetailViewModels.Add(detailViewModel);
            }


            SelectedDetailViewModel = detailViewModel;


        }

        private void AfterDetailClosed(AfterDetailCloseEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        
        }

        private int nextNewItemId = 0;
        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(
                new OpenDetailViewEventArgs{Id = nextNewItemId--,
                    ViewModelName = viewModelType.Name });
        }


        private void OnOpenSingleDetailViExecute(Type viewModelType)
        {
            OnOpenDetailView(
              new OpenDetailViewEventArgs
              {
                  Id = -1,
                  ViewModelName = viewModelType.Name
              });
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);

        }
        private void RemoveDetailViewModel(int id, string viewModelName)
        {
            var detailViewModel = DetailViewModels
              .SingleOrDefault(vm => vm.Id == id
              && vm.GetType().Name == viewModelName);

            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);
            }
        }

       


    }
}
