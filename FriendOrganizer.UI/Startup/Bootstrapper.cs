using Autofac;
using FriendOrganizer.DataAccess;
using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.View.Services;
using FriendOrganizer.UI.ViewModel;
using Prism.Events;
namespace FriendOrganizer.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();


            builder.RegisterType<FriendOrganizerDbContext>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();

            //index autofac
            builder.RegisterType<FriendDetailViewModel>().
                Keyed<IDetailViewModel1>(nameof(FriendDetailViewModel));
            builder.RegisterType<MeetingDetailViewModel>().
                Keyed<IDetailViewModel1>(nameof(MeetingDetailViewModel));
            builder.RegisterType<ProgrammingLanguageDetailViewModel>().
             Keyed<IDetailViewModel1>(nameof(ProgrammingLanguageDetailViewModel));
            builder.RegisterType<TypeHobbyDetailViewModel>().
            Keyed<IDetailViewModel1>(nameof(TypeHobbyDetailViewModel));

            builder.RegisterType<FriendRepository>().As<IFriendRepository>();
            builder.RegisterType<FriendRepository>().As<IFriendRepository>();
            builder.RegisterType<MeetingRepository>().As<IMeetingRepository>();
            builder.RegisterType<TypeHobbyRepository>().As<ITypeHobbyRepository>();
            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<ProgrammingLanguageRepository>().As<IProgrammingLanguageRepository>();



            return builder.Build();
        }
    }
}
