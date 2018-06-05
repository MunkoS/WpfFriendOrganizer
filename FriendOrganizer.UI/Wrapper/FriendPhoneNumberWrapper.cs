using FriendOrganizer.Model;
using System;
using System.Collections.Generic;

namespace FriendOrganizer.UI.Wrapper
{
     class FriendPhoneNumberWrapper : ModelWrapper<FriendPhoneNumber>
    {

        public FriendPhoneNumberWrapper(FriendPhoneNumber model) : base(model)
        {
         
        }

    

        public string Number
        {
            get { return GetValue<string>(); }

            set{ SetValue(value); }

        }

     



      /*  protected override IEnumerable<string> ValidateProperty(string propertyName)
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
        }*/

    }

    
}
