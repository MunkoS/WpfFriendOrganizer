using System;
using System.Collections.Generic;
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

namespace FriendOrganizer.UI.ViewModel
{
    public class ProgrammingLanguageDetailViewModel : PropertyDetailViewModelBase<IProgrammingLanguageRepository, ProgrammingLanguage>
    {
        public ProgrammingLanguageDetailViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDiadlogService,
            IProgrammingLanguageRepository repository) : 
            base(eventAggregator, messageDiadlogService, repository)
        {
        }

        public async override Task<bool> IsReferenced()
        {
          return
                await _repository.IsReferenceByFriendAsync(
                    SelectedProperty.Id);
        }
    }
}
