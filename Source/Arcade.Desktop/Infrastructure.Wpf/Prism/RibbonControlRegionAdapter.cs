using DevExpress.Xpf.Ribbon;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Infrastructure.Prism
{
    public class RibbonControlRegionAdapter : RegionAdapterBase<RibbonControl>
    {
        private Dictionary<RibbonPageCategoryBase, RibbonControl> _ribbonMap;

        public RibbonControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
            _ribbonMap = new Dictionary<RibbonPageCategoryBase, RibbonControl>();
        }


        protected override void Adapt(IRegion region, RibbonControl regionTarget)
        {
            region.Views.CollectionChanged += (sender, e) =>
            {
                bool isFirstAdd = true;
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (RibbonPageCategoryBase cat in e.NewItems.OfType<RibbonPageCategoryBase>())
                        {
                            RibbonControl toMerge = new RibbonControl();
                            toMerge.Items.Add(cat);
                            regionTarget.Merge(toMerge);
                            _ribbonMap[cat] = toMerge;
                            var page = cat.GetFirstSelectablePage();
                            if (page != null && isFirstAdd)
                                regionTarget.SelectedPage = page;
                            isFirstAdd = false;
                        }

                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (RibbonPageCategoryBase cat in e.OldItems.OfType<RibbonPageCategoryBase>())
                        {
                            if (_ribbonMap.ContainsKey(cat))
                            {
                                regionTarget.UnMerge(_ribbonMap[cat]);
                                _ribbonMap.Remove(cat);
                            }
                        }

                        break;
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new Region();
        }
    }

}