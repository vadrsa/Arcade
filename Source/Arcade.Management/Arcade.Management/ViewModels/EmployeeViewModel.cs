using Infrastructure.Mvvm;
using System.Collections.Generic;

namespace Arcade.Management.ViewModels
{
    public class EmployeeViewModel : EditableViewModel<EmployeeViewModel>, IIdEntityViewModel<string>
    {

        private string _id;
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value, nameof(Id));
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value, nameof(UserName));
        }

        private List<string> _roles;
        public List<string> Roles
        {
            get => _roles;
            set => SetProperty(ref _roles, value, nameof(Roles));
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value, nameof(FirstName));
        }

        private string _lastname;
        public string LastName
        {
            get => _lastname;
            set => SetProperty(ref _lastname, value, nameof(LastName));
        }

    }
}
