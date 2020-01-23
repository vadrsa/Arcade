using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using Infrastructure.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace Infrastructure
{
    public class GridProperties
    {
        public static readonly DependencyProperty GenerateColumnsProperty =
        DependencyProperty.RegisterAttached("GenerateColumns",
                                            typeof(bool),
                                            typeof(GridProperties),
                                            new UIPropertyMetadata(false, GenerateColumnsPropertyChanged));

        private static void GenerateColumnsPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (((bool)e.NewValue))
            {
                GridControl grid = (GridControl)source;

                grid.ItemsSourceChanged += (o, h) => { GenerateColumns(o as GridControl); };
            }
        }

        private static void GenerateColumns(GridControl grid)
        {
            ICollection list = grid.ItemsSource as ICollection;
            if (list != null)
            {
                Type[] genericTypes = list.GetType().GetGenericArguments();
                if ((genericTypes.Length == 0 && list.Count == 0)) return;
                Type listItemType;
                grid.Columns.Clear();
                if (genericTypes.Length == 0)
                {
                    IEnumerator enumarator = list.GetEnumerator();
                    enumarator.MoveNext();
                    listItemType = enumarator.Current.GetType();
                }
                else
                {
                    listItemType = genericTypes[0];
                }
                PropertyInfo[] properties = listItemType.GetProperties();
                List<GridColumnAttribute> columns = new List<GridColumnAttribute>();

                foreach (PropertyInfo propertyInfo in properties)
                {
                    GridColumnAttribute gridColumnAttribute = propertyInfo.GetCustomAttribute<GridColumnAttribute>();
                    if (gridColumnAttribute != null)
                    {
                        gridColumnAttribute.Header = gridColumnAttribute.Header ?? propertyInfo.Name;
                        gridColumnAttribute.FieldName = gridColumnAttribute.FieldName ?? propertyInfo.Name;
                        columns.Add(gridColumnAttribute);
                    }
                }
                columns.Sort((o, t) =>
                {
                    if (o.Order == 0 && t.Order == 0) return 0;
                    if (o.Order == 0) return 1;
                    if (t.Order == 0) return -1;
                    return o.Order.CompareTo(t.Order);

                });
                foreach (GridColumnAttribute columnAttribute in columns)
                {
                    GridColumn column = new GridColumn();
                    column.Header = columnAttribute.Header;
                    column.FieldName = columnAttribute.FieldName;
                    column.Name = columnAttribute.FieldName;
                    if (columnAttribute.IsSortField)
                        grid.DefaultSorting = columnAttribute.FieldName;
                    if (columnAttribute.BestFitMode != BestFitMode.Default)
                        column.BestFitMode = columnAttribute.BestFitMode;

                    grid.Columns.Add(column);
                }
            }
        }
        public static void SetGenerateColumns(DependencyObject element, bool value)
        {
            element.SetValue(GenerateColumnsProperty, value);
        }
        public static bool GetGenerateColumns(DependencyObject element)
        {
            return (bool)element.GetValue(GenerateColumnsProperty);
        }
    }
}
