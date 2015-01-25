
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Data.Entity;

namespace EntityFrameworkGraph
{
    public static class Utils
    {
        public static A SetBindingFl<A>(this A element, DependencyProperty property, BindingBase binding) where A : FrameworkElement
        {
            element.SetBinding(property, binding);

            return element;
        }

        public static A AddColumnFl<A>(this A dataGrid, DataGridColumn column) where A : DataGrid
        {
            dataGrid.Columns.Add(column);

            return dataGrid;
        }

        public static A AddAt<A>(this A grid, UIElement element, int column, int row) where A : Grid
        {
            grid.Children.Add(element);
            Grid.SetColumn(element, column);
            Grid.SetRow(element, row);

            return grid;
        }

        public static A AddAt<A>(this A grid, int column, int row, UIElement element) where A : Grid
        {
            grid.Children.Add(element);
            Grid.SetColumn(element, column);
            Grid.SetRow(element, row);

            return grid;
        }

        public static A AddClick<A>(this A button, RoutedEventHandler handler) where A : System.Windows.Controls.Primitives.ButtonBase
        {
            button.Click += handler; 
            
            return button;
        }
    
    }

    public class BrowserWindow : Window
    {
        public BrowserWindow()
        {
            Title = "BrowserWindow";
            Width = 1000;
            Height = 700;

            var stackPanel = new StackPanel();

            Content = stackPanel;

            var context = new Context();

            context.Nodes.Load();
            context.Labels.Load();
            context.Relationships.Load();
            context.Tags.Load();

            {
                var grid = new Grid() { };

                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(200, GridUnitType.Pixel) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(200, GridUnitType.Pixel) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(200, GridUnitType.Pixel) });
                
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                stackPanel.Children.Add(grid);

                var tags_cvs = new CollectionViewSource() { Source = context.Tags.Local };

                grid.AddAt(1, 0, new DataGrid() { AutoGenerateColumns = false }
                    .SetBindingFl(DataGrid.ItemsSourceProperty, new Binding() { Source = tags_cvs })
                    .AddColumnFl(new DataGridTextColumn() { Header = "Title", Width = new DataGridLength(1, DataGridLengthUnitType.Star), Binding = new Binding("Title") })
                    );

                {
                    var nodes_cvs = new CollectionViewSource();

                    BindingOperations.SetBinding(nodes_cvs, CollectionViewSource.SourceProperty, new Binding("Nodes") { Source = tags_cvs });

                    var nodes_dg = new DataGrid() { AutoGenerateColumns = false, DataContext = nodes_cvs }

                        .SetBindingFl(DataGrid.ItemsSourceProperty, new Binding() { Source = nodes_cvs })

                        .AddColumnFl(new DataGridTextColumn() { Header = "Title", Width = new DataGridLength(1, DataGridLengthUnitType.Star), Binding = new Binding("Title") });

                    grid.AddAt(1, 1, nodes_dg);

                    {
                        var attr_cvs = new CollectionViewSource();

                        BindingOperations.SetBinding(attr_cvs, CollectionViewSource.SourceProperty, new Binding("Attributes") { Source = nodes_cvs });

                        var attr_dg = new DataGrid() { AutoGenerateColumns = false, DataContext = attr_cvs };

                        attr_dg.SetBinding(DataGrid.ItemsSourceProperty, new Binding() { Source = attr_cvs });

                        attr_dg.Columns.Add(new DataGridTextColumn() { Header = "Key", Width = new DataGridLength(1, DataGridLengthUnitType.Star), Binding = new Binding("Key") });
                        attr_dg.Columns.Add(new DataGridTextColumn() { Header = "Val", Width = new DataGridLength(1, DataGridLengthUnitType.Star), Binding = new Binding("Val") });

                        grid.AddAt(attr_dg, 1, 2);
                    }
                    
                    {
                        var in_cvs = new CollectionViewSource();

                        BindingOperations.SetBinding(in_cvs, CollectionViewSource.SourceProperty, new Binding("Incoming") { Source = nodes_cvs });

                        var in_DataGrid = new DataGrid() { AutoGenerateColumns = false, DataContext = in_cvs };

                        in_DataGrid.SetBinding(DataGrid.ItemsSourceProperty, new Binding() { Source = in_cvs });

                        in_DataGrid.Columns.Add(new DataGridTextColumn() { Header = "A", Width = new DataGridLength(1, DataGridLengthUnitType.Star), Binding = new Binding("A.Title") });
                        in_DataGrid.Columns.Add(new DataGridTextColumn() { Header = "Label", Width = new DataGridLength(1, DataGridLengthUnitType.Star), Binding = new Binding("Label.Title") });

                        grid.AddAt(in_DataGrid, 0, 1);
                    }

                    {
                        var outgoing_CollectionViewSource = new CollectionViewSource();

                        BindingOperations.SetBinding(outgoing_CollectionViewSource, CollectionViewSource.SourceProperty, new Binding("Outgoing") { Source = nodes_cvs });

                        var outgoing_DataGrid = new DataGrid() { AutoGenerateColumns = false, DataContext = outgoing_CollectionViewSource };

                        outgoing_DataGrid.SetBinding(DataGrid.ItemsSourceProperty, new Binding() { });

                        outgoing_DataGrid.Columns.Add(new DataGridTextColumn() { Header = "Label", Width = new DataGridLength(1, DataGridLengthUnitType.Star), Binding = new Binding("Label.Title") });
                        outgoing_DataGrid.Columns.Add(new DataGridTextColumn() { Header = "B", Width = new DataGridLength(1, DataGridLengthUnitType.Star), Binding = new Binding("B.Title") });

                        grid.AddAt(outgoing_DataGrid, 2, 1);

                        {
                            var prop_cvs = new CollectionViewSource();

                            BindingOperations.SetBinding(prop_cvs, CollectionViewSource.SourceProperty, new Binding("Properties") { Source = outgoing_CollectionViewSource });

                            var prop_dg = new DataGrid() { AutoGenerateColumns = false, DataContext = prop_cvs };

                            prop_dg.SetBinding(DataGrid.ItemsSourceProperty, new Binding() { Source = prop_cvs });

                            prop_dg.Columns.Add(new DataGridTextColumn() { Header = "Key", Width = new DataGridLength(1, DataGridLengthUnitType.Star), Binding = new Binding("Key") });
                            prop_dg.Columns.Add(new DataGridTextColumn() { Header = "Val", Width = new DataGridLength(1, DataGridLengthUnitType.Star), Binding = new Binding("Val") });

                            grid.AddAt(prop_dg, 2, 2);
                        }

                        {
                            var nodes_outgoing_B_outgoing_CollectionViewSource = new CollectionViewSource();

                            BindingOperations.SetBinding(nodes_outgoing_B_outgoing_CollectionViewSource, CollectionViewSource.SourceProperty, new Binding("B.Outgoing") { Source = outgoing_CollectionViewSource });

                            var nodes_outgoing_B_outgoing_DataGrid = new DataGrid() { AutoGenerateColumns = false, DataContext = nodes_outgoing_B_outgoing_CollectionViewSource };

                            nodes_outgoing_B_outgoing_DataGrid.SetBinding(DataGrid.ItemsSourceProperty, new Binding() { });

                            nodes_outgoing_B_outgoing_DataGrid.Columns.Add(new DataGridTextColumn() { Header = "Label", Width = new DataGridLength(1, DataGridLengthUnitType.Star), Binding = new Binding("Label.Title") });
                            nodes_outgoing_B_outgoing_DataGrid.Columns.Add(new DataGridTextColumn() { Header = "B", Width = new DataGridLength(1, DataGridLengthUnitType.Star), Binding = new Binding("B.Title") });

                            grid.AddAt(nodes_outgoing_B_outgoing_DataGrid, 3, 1);
                        }
                    }
                }
            }
        } 
    }
}
