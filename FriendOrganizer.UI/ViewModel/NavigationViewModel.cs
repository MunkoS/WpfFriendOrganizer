
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.Data.Lookups;
using Prism.Events;
using System.Collections.ObjectModel;

using System.Threading.Tasks;
using System.Linq;
using System;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBasePropertyChanged, INavigationViewModel
    {
        private IFriendLookupDataService _friendDataService;

        private IMeetingLookupDataService _meetingDataService;

        private IEventAggregator _eventAggregator;

      

        public NavigationViewModel(IFriendLookupDataService friendDataService,
            IMeetingLookupDataService meetingDataService,
            IEventAggregator eventAggregator)
        {

            _meetingDataService = meetingDataService;
            _friendDataService = friendDataService;
            _eventAggregator = eventAggregator;
            Friends = new ObservableCollection<NavigationItemViewModel>();
            Meetings = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {

            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    AfterDetailSaved(Friends, args);
                    break;

                case nameof(MeetingDetailViewModel):
                    AfterDetailSaved(Meetings, args);
                    break;
            }
          
        }

        private void AfterDetailSaved(ObservableCollection<NavigationItemViewModel> items,
            AfterDetailSavedEventArgs args)
        {
            var lookupItem = items.SingleOrDefault(i => i.Id == args.Id);

            if (lookupItem == null)
            {
                items.Add(new NavigationItemViewModel(args.Id, args.DisplayMember,
                    args.ViewModelName,
                    _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {

            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    AfterDetailDeleted(Friends, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailDeleted(Meetings, args);
                    break;
            }

        }

        private void AfterDetailDeleted(ObservableCollection<NavigationItemViewModel> items,
            AfterDetailDeletedEventArgs args)
        {
            var item = items.SingleOrDefault(i => i.Id == args.Id);

            if (item != null)
            {
                items.Remove(item);
            }
        }

        public ObservableCollection<NavigationItemViewModel> Friends { get; }

        public ObservableCollection<NavigationItemViewModel> Meetings { get; }
        

        public async Task LoadAsync()
        {
      
            var lookup = await _friendDataService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
            {
                Friends.Add(new NavigationItemViewModel (item.Id, item.DisplayMember,
                    nameof(FriendDetailViewModel),
                    _eventAggregator));
            }

            lookup = await _meetingDataService.GetMeetingLookupAsync();
            Meetings.Clear();
            foreach (var item in lookup)
            {
                Meetings.Add(new NavigationItemViewModel(item.Id, item.DisplayMember,
                    nameof(MeetingDetailViewModel),
                    _eventAggregator));
            }


        }


      
    }
}
