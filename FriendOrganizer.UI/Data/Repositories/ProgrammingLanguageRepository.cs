
using FriendOrganizer.Model;
using FriendOrganizer.DataAccess;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System;

namespace FriendOrganizer.UI.Data.Repositories
{
    public class ProgrammingLanguageRepository 
        : GenericRepository<ProgrammingLanguage, FriendOrganizerDbContext>,
        IProgrammingLanguageRepository
    {
        public ProgrammingLanguageRepository(FriendOrganizerDbContext context) 
            : base(context)
        {
        }

        public  async Task<bool> IsReferenceByFriendAsync(int programmingLanguageId)
        {
            return await _context.Friends.AsNoTracking()
             .AnyAsync(f => f.FavoriteLanguageId == programmingLanguageId);
    
        }
    }
}
