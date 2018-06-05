using FriendOrganizer.Model;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data.Repositories
{
    public interface ITypeHobbyRepository :
        IGenericRepository<TypeHobby>
    {
        Task<bool> IsReferenceByHobbyAsync(int hobbyId);
    }
}
