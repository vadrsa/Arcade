using Infrastructure.Mvvm;
using SharedEntities;
using System.Collections.Generic;

namespace Arcade.ViewModels
{
    public class EmployeeViewModel : EditableViewModel<EmployeeViewModel>, IIdEntityViewModel<string>
    {

        private string _id;
        public string Id
        {
            get => _id;
            set => Set(ref _id, value, nameof(Id));
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value, nameof(UserName));
        }

        private ApplicationRole _role;
        public ApplicationRole Role
        {
            get => _role;
            set => Set(ref _role, value, nameof(Role));
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value, nameof(FirstName));
        }

        private string _lastname;
        public string LastName
        {
            get => _lastname;
            set => Set(ref _lastname, value, nameof(LastName));
        }

    }

    public class EmployeeUploadViewModel : EditableViewModel<EmployeeUploadViewModel>, IIdEntityViewModel<string>
    {

        private string _id;
        public string Id
        {
            get => _id;
            set => Set(ref _id, value, nameof(Id));
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value, nameof(UserName));
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => Set(ref _password, value, nameof(Password));
        }

        private ApplicationRole _role;
        public ApplicationRole Role
        {
            get => _role;
            set => Set(ref _role, value, nameof(Role));
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value, nameof(FirstName));
        }

        private string _lastname;
        public string LastName
        {
            get => _lastname;
            set => Set(ref _lastname, value, nameof(LastName));
        }

    }
}
