namespace Kernel.Configuration
{
    public abstract class Project : OptionConfiguration
    {
        public virtual void RegisterFeatures(IFeatureRegister featureRegister)
        {
        }
    }
}
