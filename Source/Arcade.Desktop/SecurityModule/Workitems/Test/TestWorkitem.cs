using Infrastructure.Constants;
using Infrastructure.Workitems;
using Kernel.Configuration;
using Kernel.Workitems;
using Prism.Ioc;
using SecurityModule.Workitems.Test.Views;

namespace SecurityModule.Workitems.Test
{
    public class TestWorkitem : WorkitemWpfBase
    {
        public TestWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName => "Test";

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            container.Register(new TestView(), KnownRegions.Content);
            if (Project.GetOption<RegionOptions>().IsSupported(KnownRegions.Ribbon))
                container.Register(new TestRibbonCategory(), KnownRegions.Ribbon);
            else
                UIManager.ShowMessageBox("Incompatible workitem", "Incompatible workitem", System.Windows.MessageBoxButton.OK);
        }
    }
}
