using FriendOrganizer.Model;
using System;
using System.Collections.Generic;
namespace FriendOrganizer.UI.Wrapper
{
    public class PropertyItemWrapper<M> : ModelWrapper<M>
        where M : Property
    {
        public PropertyItemWrapper(M model) 
            : base(model)
        {

        }
        public int Id { get { return Model.Id; } }

        public string Name
        {
            get { return GetValue<string>(); }

            set { SetValue(value); }

        }
    }
}
