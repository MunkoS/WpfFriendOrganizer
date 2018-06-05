using Prism.Events;

namespace FriendOrganizer.UI.Event
{
    class AfterDetailSavedEvent : PubSubEvent<AfterDetailSavedEventArgs>
    {
    }

    internal class AfterDetailSavedEventArgs
    {
        public int Id { get; set; }

        public string ViewModelName { get; set; }

        public string DisplayMember { get; set; }
    }
}
