using System.Linq;
using FriendOrganizer.UI.Data.Repositories;
using System.Threading.Tasks;
using FriendOrganizer.UI.View.Services;
using FriendOrganizer.UI.Wrapper;
using Prism.Events;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Prism.Commands;
using FriendOrganizer.Model;
using System;

namespace FriendOrganizer.UI.ViewModel
{


      public class TypeHobbyDetailViewModel : PropertyDetailViewModelBase<ITypeHobbyRepository, TypeHobby>
      {
          public TypeHobbyDetailViewModel(IEventAggregator eventAggregator,
              IMessageDialogService messageDiadlogService,
              ITypeHobbyRepository repository) :
              base(eventAggregator, messageDiadlogService, repository)
          {
          }

          public override async Task<bool> IsReferenced()
          {
       
              return await _repository.IsReferenceByHobbyAsync(SelectedProperty.Id);
          }


      }
}
