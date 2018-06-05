using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public interface IDetailViewModel1
    {
        bool HasChanges { get; }
        int Id { get;  }

        Task LoadAsync(int Id);
    }
}