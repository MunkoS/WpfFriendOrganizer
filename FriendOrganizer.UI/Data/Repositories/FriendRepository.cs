﻿
using FriendOrganizer.Model;
using FriendOrganizer.DataAccess;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;

namespace FriendOrganizer.UI.Data.Repositories
{

    public class FriendRepository :GenericRepository<Friend,FriendOrganizerDbContext>,
        IFriendRepository
    {
      
        public FriendRepository(FriendOrganizerDbContext context):
            base(context)
        {
        }

      
        public override async Task<Friend> GetByIdAsync(int friendId)
        {

            return await _context.Friends
                .Include(f=>f.PhoneNumbers)
                .SingleAsync(f => f.Id == friendId);
        }

        public  async Task<bool> HasMeetingsAsync(int friendId)
        {

            return await _context.Meetings.AsNoTracking()
                .Include(m => m.Friends)
                .AnyAsync(m => m.Friends.Any(f => f.Id == friendId));


        }

        public void RemovePhoneNumber(FriendPhoneNumber friendPhoneNumber)
        {
            _context.FriendPhoneNumbers.Remove(friendPhoneNumber);
        }

       
    }
}
