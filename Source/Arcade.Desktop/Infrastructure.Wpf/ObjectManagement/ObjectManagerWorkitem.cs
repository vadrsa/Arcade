using Infrastructure.Constants;
using Infrastructure.Workitems;
using Kernel.Workitems;
using Prism.Ioc;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Infrastructure.ObjectManagement
{
    public abstract class ObjectManagerWorkitem<TView, TList, TDetails, TDetailsWI> : WorkitemWpfBase, IObjectManagerWorkitem<TList, TDetails>
        where TDetailsWI : IWorkItem
    {

        public ObjectManagerWorkitem(IContainerExtension container) : base(container)
        {
        }


        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            container.Register(Container.Resolve<TView>(), KnownRegions.Content);
        }

        public async Task<IObservable<bool>> RunAddEdit(TDetails details, bool isAdding)
        {
            return (await CurrentContextService.LaunchModalWorkItem<TDetailsWI>(new ObjectManagerDetailsInitializer<TDetails> { Details = details, IsAdding = isAdding }, this)).Where(w => w.Data is bool).Select(w => (bool)w.Data);
        }
    }
}
