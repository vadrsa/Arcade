using System.Windows.Media.Imaging;

namespace Kernel.Configuration
{
    public abstract class Project : OptionConfiguration
    {

        public abstract string AppDataFolderName { get; }

        public virtual void RegisterFeatures(IFeatureRegister featureRegister)
        {
        }
    }
}
