﻿using Infrastructure.Mvvm;

namespace Arcade.ViewModels
{
    public class GameViewModel : EditableViewModel<GameViewModel>, IIdEntityViewModel<string>
    {

        private string _id;
        public string Id
        {
            get => _id;
            set => Set(nameof(Id), ref _id, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(nameof(Name), ref _name, value);
        }

        private string _category;
        public string Category
        {
            get => _category;
            set => Set(nameof(Category), ref _category, value);
        }

        private string _image;
        public string Image
        {
            get => _image;
            set => Set(nameof(Image), ref _image, value);
        }

    }
}
