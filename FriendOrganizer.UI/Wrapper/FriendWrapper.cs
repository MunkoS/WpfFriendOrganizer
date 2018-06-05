using FriendOrganizer.Model;
using System;
using System.Collections.Generic;

namespace FriendOrganizer.UI.Wrapper
{
     class FriendWrapper : ModelWrapper<Friend>
    {

        public FriendWrapper(Friend model) : base(model)
        {
         
        }

        public int Id {get { return Model.Id; } }

        public string FirstName
        {
            get { return GetValue<string>(); }

            set{ SetValue(value); }

        }

        public string LastName
        {
            get { return GetValue<string>(); }

            set
            {
                SetValue(value);
          
            }

        }

        public string Email
        {
            get { return GetValue<string>(); }

            set
            {
                SetValue(value);
            }

        }

        public int? FavoriteLanguageId
        {
            get { return GetValue<int?>(); }

            set
            {
                SetValue(value);
            }

        }



        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.Equals(FirstName, "Test", StringComparison.CurrentCulture))
                    {
                        yield return "ошибочка test";
                    }
                    break;
                case nameof(LastName):
                    if (string.Equals(LastName, "Test", StringComparison.CurrentCulture))
                    {
                        yield return "ошибочка LastName";
                    }
                    break;
            }
        }

    }

    
}
