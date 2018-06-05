﻿using FriendOrganizer.Model;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data.Repositories
{

    public interface IFriendRepository : IGenericRepository<Friend>
    {
        void RemovePhoneNumber(FriendPhoneNumber friendPhoneNumber);
        Task<bool> HasMeetingsAsync(int friendId);
    }
}