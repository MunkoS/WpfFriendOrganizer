
using FriendOrganizer.UI.Event;
using Prism.Events;
using Prism.Commands;
using System.Threading.Tasks;
using System.Windows.Input;
using FriendOrganizer.UI.View.Services;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System;

namespace FriendOrganizer.UI.ViewModel
{
    public abstract class DetailViewModelBase : ViewModelBasePropertyChanged, IDetailViewModel1
    {
        private bool _hasChanges;
        protected readonly IEventAggregator EventAggregator;
        protected readonly IMessageDialogService MessageDialogService;
        private int _id;
        private string  _title;
        public DetailViewModelBase(IEventAggregator eventAggregator,
            IMessageDialogService messageDiadlogService)
        {
            EventAggregator = eventAggregator;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
            CloseDetailViewCommand = new DelegateCommand(OnCloseDetailViewExecute);
            MessageDialogService = messageDiadlogService;

        }


     
        public abstract Task LoadAsync(int id);

        public ICommand SaveCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public ICommand CloseDetailViewCommand { get; private set; }
        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
            protected set { _id = value; }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            protected set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

       

        protected abstract void OnDeleteExecute();

        protected abstract bool OnSaveCanExecute();

        protected abstract void OnSaveExecute();



        protected virtual void RaiseDetailDeletedEvent(int modelId)
        {
            EventAggregator.GetEvent<AfterDetailDeletedEvent>().Publish(
                new AfterDetailDeletedEventArgs
                {
                    Id = modelId,
                    ViewModelName = this.GetType().Name
            });
        }

      

        protected virtual void RaiseDetailSavedEvent(int modelId,string displayMember)
        {
            EventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(
                new AfterDetailSavedEventArgs
                {
                    Id = modelId,
                    DisplayMember=displayMember,
                    ViewModelName = this.GetType().Name
                });
        }


        protected virtual void RaiseCollectionSavedEvent()
        {
            EventAggregator.GetEvent<AfterCollectionSavedEvent>().Publish(
                new AfterCollectionSavedEventArgs
                {
                    ViewModelName = this.GetType().Name
                });
        }

        protected virtual async void OnCloseDetailViewExecute()
        {
            if (HasChanges)
            {
                var result = await MessageDialogService.ShowOkCandelDialogAsync("Отменить изменения?", "Закрывашка");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
                    EventAggregator.GetEvent<AfterDetailCloseEvent>().Publish(
                       new AfterDetailCloseEventArgs
                       {
                           Id = this.Id,
                           ViewModelName = this.GetType().Name
                       });
                

       }
        protected  async  Task SaveWithOptimisticConcurrencyAsync(Func<Task> saveFunc,
            Action afterSaveAction)
        {

            try
            {
                await saveFunc();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var databaseValues = ex.Entries.Single().GetDatabaseValues();
                if (databaseValues == null)
                {
                   await MessageDialogService.ShowInfoDialogAsync("Запись была удалена другим пользователем","АХТУНГ");
                    RaiseDetailDeletedEvent(Id);
                    return;
                }
                var result = await MessageDialogService.ShowOkCandelDialogAsync("Запись была изменена "
                    + ".Нажмите ок чтобы сохранить изменения, нажмите Сancel "
                    + "чтобы перезагрузить значение.", "Вопрос");

                if (result == MessageDialogResult.OK)
                {
                    var entry = ex.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    await saveFunc();
                }
                else
                {
                    await ex.Entries.Single().ReloadAsync();
                    await LoadAsync(Id);
                }
            }

            afterSaveAction();
        }
    }
}

