using DevExpress.Xpf.Grid;

namespace Infrastructure.Mvvm
{
    public interface IGridView
    {
        GridControl Grid
        {
            get;
        }
    }
}
