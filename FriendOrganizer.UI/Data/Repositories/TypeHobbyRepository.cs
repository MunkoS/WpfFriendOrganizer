
using FriendOrganizer.Model;
using FriendOrganizer.DataAccess;
using System.Threading.Tasks;
using System.Data.Entity;
using System;


namespace FriendOrganizer.UI.Data.Repositories
{
    public class TypeHobbyRepository :
         GenericRepository<TypeHobby, FriendOrganizerDbContext>,
        ITypeHobbyRepository
    {
        public TypeHobbyRepository(FriendOrganizerDbContext context) : base(context)
        {
        }

      public async Task<bool> IsReferenceByHobbyAsync(int hobbyId)
        {
            return await _context.Hobbys.AsNoTracking()
             .AnyAsync(f => f.TypeHobbyId == hobbyId);

        }
    
    }

     
    }


