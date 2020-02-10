using Infrastructure.Mvvm;

namespace Arcade.ViewModels
{
    public class GameUploadViewModel : EditableViewModel<GameUploadViewModel>, IIdEntityViewModel<string>
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

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set => Set(nameof(ImagePath), ref _imagePath, value);
        }

        private byte[] _image;
        public byte[] Image
        {
            get => _image;
            set => Set(nameof(Image), ref _image, value);
        }

        private string _category;
        public string Category
        {
            get => _category;
            set => Set(nameof(Category), ref _category, value);
        }

        private int _ageLimit;
        public int AgeLimit
        {
            get => _ageLimit;
            set => Set(nameof(AgeLimit), ref _ageLimit, value);
        }
    }
}
