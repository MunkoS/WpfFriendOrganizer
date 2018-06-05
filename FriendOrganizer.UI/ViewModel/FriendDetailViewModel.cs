
using FriendOrganizer.UI.Data.Repositories;
using Prism.Events;
using Prism.Commands;
using System.Threading.Tasks;
using System.Windows.Input;
using FriendOrganizer.UI.Wrapper;
using FriendOrganizer.Model;
using FriendOrganizer.UI.View.Services;
using FriendOrganizer.UI.Data.Lookups;

using System.Collections.ObjectModel;
using System.Collections.Generic;

using System.ComponentModel;
using System.Linq;
using System;
using FriendOrganizer.UI.Event;
using System.Data.Entity.Infrastructure;

namespace FriendOrganizer.UI.ViewModel
{
    class FriendDetailViewModel : DetailViewModelBase, IFriendDetailViewModel
    {
        private IProgrammingLanguageLookupDataService _programmingLanguageLookupDataService;
        private IFriendRepository _friendRepository;
        private FriendWrapper _friend;
        private FriendPhoneNumberWrapper _selectedPhoneNumber;
        public FriendDetailViewModel(IFriendRepository friendRepository, 
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialoService,
            IProgrammingLanguageLookupDataService programmingLanguageLookupDataService):base(eventAggregator,messageDialoService)
        {
            _programmingLanguageLookupDataService = programmingLanguageLookupDataService;
            _friendRepository = friendRepository;
           
  
            AddPhoneNumberCommand = new DelegateCommand(OnAddPhoneNumberExecute);
            RemovePhoneNumberCommand = new DelegateCommand(OnRemovePhoneNumberExecute, OnRemovePhoneNumberCanExecute);

            ProgrammingLanguages = new ObservableCollection<LookupItem>();

            PhoneNumbers = new ObservableCollection<FriendPhoneNumberWrapper>();

            EventAggregator.GetEvent<AfterCollectionSavedEvent>().Subscribe(AfterCollectionSaved);
        }

        private async void AfterCollectionSaved(AfterCollectionSavedEventArgs obj)
        {
            if (obj.ViewModelName == nameof(ProgrammingLanguageDetailViewModel))
            {
                await LoadProgrammingLanguagesLookupAsync();
            }
        
        }

        public override async Task LoadAsync(int friendId)
        {
            var friend = friendId>0
                ? await _friendRepository.GetByIdAsync(friendId)
                : CreateNewFriend();
            ;

            Id = friendId;

            InitializeFriend(friend);

            InitializeFriendPhoneNumbers(friend.PhoneNumbers);

            await LoadProgrammingLanguagesLookupAsync();



        }
        private void InitializeFriendPhoneNumbers(ICollection<FriendPhoneNumber> phoneNumbers)
        {
            foreach(var wrapper in PhoneNumbers)
            {
                wrapper.PropertyChanged -= FriendPhoneNumberWrapper_PropertyChanged;
            }
            PhoneNumbers.Clear();
            foreach (var friendPhoneNumber in phoneNumbers)
            {
                var wrapper = new FriendPhoneNumberWrapper(friendPhoneNumber);
                PhoneNumbers.Add(wrapper);
                wrapper.PropertyChanged += FriendPhoneNumberWrapper_PropertyChanged;

            }
        }

        private void FriendPhoneNumberWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _friendRepository.HasChanges();
            }
            if (e.PropertyName == nameof(FriendPhoneNumberWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        private void InitializeFriend(Friend friend)
        {
            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _friendRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                if(e.PropertyName==nameof(Friend.FirstName)
                || e.PropertyName == nameof(Friend.LastName))
                {
                    SetTitle();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Friend.Id == 0)
            {
                Friend.FirstName = "";
            }
            SetTitle();

        }

        private void SetTitle()
        {
            Title = $"{Friend.FirstName} {Friend.LastName}";
        }

        private async Task LoadProgrammingLanguagesLookupAsync()
        {
            ProgrammingLanguages.Clear();
            ProgrammingLanguages.Add(new NullLookupItem { DisplayMember = " - " }); ;
            var lookup = await _programmingLanguageLookupDataService.GetProgrammingLanguageLookupAsync();
            foreach (var value in lookup)
            {
                ProgrammingLanguages.Add(value);
            }
        }

        public FriendWrapper Friend
        {
            get { return _friend; }
            set
            {

                _friend = value;
                OnPropertyChanged();

            }

        }

        public FriendPhoneNumberWrapper SelectedPhoneNumber
        {
            get { return _selectedPhoneNumber; }
            set
            {
                _selectedPhoneNumber = value;
                OnPropertyChanged();
                ((DelegateCommand)RemovePhoneNumberCommand).RaiseCanExecuteChanged();
            }

        }

        public ICommand AddPhoneNumberCommand { get; }

        public ICommand RemovePhoneNumberCommand { get; }

        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }

        public ObservableCollection<FriendPhoneNumberWrapper> PhoneNumbers { get; }

        protected override bool OnSaveCanExecute()
        {
            return Friend != null
                && !Friend.HasErrors
                && PhoneNumbers.All(pn=>!pn.HasErrors)
                && HasChanges;
        }

        private bool OnRemovePhoneNumberCanExecute()
        {
            return SelectedPhoneNumber != null;
        }


        protected override async void OnSaveExecute()
        {

             await SaveWithOptimisticConcurrencyAsync(_friendRepository.SaveAsync, 
                  ()=>
                  {
                  HasChanges = _friendRepository.HasChanges();
                  Id = Friend.Id;
                  RaiseDetailSavedEvent(Friend.Id, Friend.FirstName + " " + Friend.LastName);
                  });
                  
        }

    

        private Friend CreateNewFriend()
        {
            var friend = new Friend();
         
            _friendRepository.Add(friend);
            return friend;
        }

        protected override async void OnDeleteExecute()
        {


            if(await _friendRepository.HasMeetingsAsync(Friend.Id))
            {
                await MessageDialogService.ShowInfoDialogAsync($"{Friend.FirstName}  {Friend.LastName} нельзя удалить так как друг указан во встрече","Удаление запрещено");
                return;
            }
                var result = await MessageDialogService.ShowOkCandelDialogAsync("Отменить внесенные изменения?", "Вопрос");

                if (result == MessageDialogResult.OK)
                {
                _friendRepository.Delete(Friend.Model);
                await _friendRepository.SaveAsync();

                RaiseDetailDeletedEvent(Friend.Id);
                }  
       
        }
        private void OnRemovePhoneNumberExecute()
        {
          
            SelectedPhoneNumber.PropertyChanged -= FriendPhoneNumberWrapper_PropertyChanged;
            _friendRepository.RemovePhoneNumber(SelectedPhoneNumber.Model);
            PhoneNumbers.Remove(SelectedPhoneNumber);
            SelectedPhoneNumber = null;
            HasChanges = _friendRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
       
        }

        private void OnAddPhoneNumberExecute()
        {
            var newNumber =  new FriendPhoneNumberWrapper(new FriendPhoneNumber());
            newNumber.PropertyChanged += FriendPhoneNumberWrapper_PropertyChanged;
            PhoneNumbers.Add(newNumber);
            Friend.Model.PhoneNumbers.Add(newNumber.Model);
            newNumber.Number = "";
        }


    }
}
