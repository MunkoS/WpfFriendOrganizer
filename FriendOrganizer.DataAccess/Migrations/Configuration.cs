namespace FriendOrganizer.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizer.DataAccess.FriendOrganizerDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(FriendOrganizer.DataAccess.FriendOrganizerDbContext context)
    {
            /* context.Friends.AddOrUpdate(
               f => f.FirstName,
               new Friend { FirstName = "Серега", LastName = "Мунько" },
               new Friend { FirstName = "Серега1", LastName = "Мунько1" },
               new Friend { FirstName = "Петя", LastName = "Иванов" },
               new Friend { FirstName = "Петя", LastName = "Иванов21" }
               );

            context.ProgrammingLanguages.AddOrUpdate(
             pl => pl.Name,
             new ProgrammingLanguage { Name="C#"},
             new ProgrammingLanguage { Name = "PHP" },
             new ProgrammingLanguage { Name = "JS" },
             new ProgrammingLanguage { Name = "F#" },
             new ProgrammingLanguage { Name = "1C" },
             new ProgrammingLanguage { Name = "C++" }
             );

                   context.SaveChanges();
                   context.FriendPhoneNumbers.AddOrUpdate(pn => pn.Number,
                       new FriendPhoneNumber { Number = "+22 222222", FriendId = context.Friends.First().Id });


                   context.Meetings.AddOrUpdate(m => m.Title,
                       new Meeting
                       {
                           Title = "Playing soccer",
                           DateFrom=new DateTime(2018,5,26),
                           DateTo = new DateTime(2018, 5, 26),
                           Friends = new List<Friend>
                           {
                               context.Friends.Single(f=>f.FirstName=="Серега" && f.LastName=="Мунько"),
                               context.Friends.Single(f=>f.FirstName=="Петя" && f.LastName=="Иванов21")
                           }
                       }


                   );     */

        }
    }
}
