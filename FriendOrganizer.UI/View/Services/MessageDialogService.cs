using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.View.Services
{
    public class MessageDialogService : IMessageDialogService
    {

        private MetroWindow MetroWindow {get { return (MetroWindow)App.Current.MainWindow; } }

        public async Task<MessageDialogResult> ShowOkCandelDialogAsync(string text,string title)
        {

            var result = 
              await MetroWindow.ShowMessageAsync(title, text, MessageDialogStyle.AffirmativeAndNegative);
         
            return result == MahApps.Metro.Controls.Dialogs.MessageDialogResult.Affirmative
                ? MessageDialogResult.OK
                : MessageDialogResult.Cancel;
        }


        public async Task ShowInfoDialogAsync(string text, string title)
        {
      
             await MetroWindow.ShowMessageAsync(title, text, MessageDialogStyle.Affirmative);
        
        
        }

       
    }

    public enum MessageDialogResult
    {
        OK,
        Cancel
    }
}

