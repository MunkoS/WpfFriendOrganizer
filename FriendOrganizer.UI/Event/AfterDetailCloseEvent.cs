using System;
using Prism.Events;

namespace FriendOrganizer.UI.Event 
{
    class AfterDetailCloseEvent :PubSubEvent<AfterDetailCloseEventArgs>
    {
    }

    public class AfterDetailCloseEventArgs
    {
        public int Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
