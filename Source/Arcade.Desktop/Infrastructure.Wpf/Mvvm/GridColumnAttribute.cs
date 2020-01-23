using DevExpress.Xpf.Core;
using System;

namespace Infrastructure.Mvvm
{
    /// <summary>
    /// Property should be rendered as a grid column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class GridColumnAttribute : Attribute
    {
        public GridColumnAttribute(string header = null, int order = 0)
        {
            Header = header;
            Order = order;
        }

        /// <summary>
        /// The column header
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Columns order in the grid
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Can be specified to specify the field name to 
        /// bind to if it is different than the property name
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Shows if the column is sort field
        /// </summary>
        public bool IsSortField { get; set; }

        /// <summary>
        /// Specifies the BestFitMode for the column
        /// </summary>
        public BestFitMode BestFitMode { get; set; }
    }
}
