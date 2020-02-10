using DevExpress.Xpf.Grid;
using Infrastructure.Security;

namespace Infrastructure.Mvvm
{
    /// <summary>
    /// Adds Grid Commands to WorkitemViewModel implementing IGridViewModel
    /// </summary>
    public class WorkitemGridViewModel : WorkitemViewModel, IGridViewModel
    {

        public GridControl Grid { get; set; }


        private SecureCommand searchCommand;
        public SecureCommand SearchCommand =>
            searchCommand ?? (searchCommand = Disposable(new SecureCommand(Search, CanExecuteSearchCommand)));

        private SecureCommand collapseAllCommand;
        public SecureCommand CollapseAllCommand =>
            collapseAllCommand ?? (collapseAllCommand = Disposable(new SecureCommand(ExecuteCollapseAllCommand, CanExecuteCollapseAllCommand)));


        private SecureCommand expandAllCommand;
        public SecureCommand ExpandAllCommand =>
            expandAllCommand ?? (expandAllCommand = Disposable(new SecureCommand(ExecuteExpandAllCommand, CanExecuteExpandAllCommand)));

        protected virtual bool CanExecuteCollapseAllCommand()
        {
            return true;
        }

        protected virtual bool CanExecuteExpandAllCommand()
        {
            return true;
        }

        protected virtual bool CanExecuteSearchCommand()
        {
            return true;
        }


        protected virtual void Search()
        {
            Grid.View.ShowSearchPanel(true);
        }


        protected virtual void ExecuteCollapseAllCommand()
        {
            for (int i = 0; i < Grid.VisibleRowCount; i++)
            {
                var handle = Grid.GetRowHandleByVisibleIndex(i);
                Grid.CollapseMasterRow(handle);
            }
            Grid.CollapseAllGroups();
        }

        protected virtual void ExecuteExpandAllCommand()
        {
            for (int i = 0; i < Grid.VisibleRowCount; i++)
            {
                var handle = Grid.GetRowHandleByVisibleIndex(i);
                Grid.ExpandMasterRow(handle);
            }
            Grid.ExpandAllGroups();
        }
    }
}
