using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace MediaPlayerMvvmSample
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MediaPlayerMvvmSample"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MediaPlayerMvvmSample;assembly=MediaPlayerMvvmSample"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CheckFilters/>
    ///
    /// </summary>
    public class CheckFilters : ComboBox, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ComboBoxItem ItemToDisplay = null;

        public CheckFilters()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckFilters), new FrameworkPropertyMetadata(typeof(CheckFilters)));
            this.AddHandler(CheckBox.ClickEvent, new RoutedEventHandler(SelectFilter));
        }

        private void SelectFilter(object sender, RoutedEventArgs e)
        {
            string label = string.Empty;

            foreach (ComboBoxItem item in this.Items)
            {
                Border borderSelect = item.Template.FindName("borderSelect", item) as Border;

                if (borderSelect != null)
                {
                    CheckBox chkSelect = borderSelect.FindName("SelectFilter") as CheckBox;

                    if (chkSelect != null)
                    {
                        if (chkSelect.IsChecked.GetValueOrDefault())
                        {
                            if (!string.IsNullOrEmpty(label))
                            {
                                label += ", ";
                            }

                            label += chkSelect.Content;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(label))
            {
                // Remove item from the list
                if (ItemToDisplay != null)
                {
                    if (this.Items.Contains(ItemToDisplay))
                    {
                        this.Items.Remove(ItemToDisplay);
                    }
                }
                ItemToDisplay = new ComboBoxItem();
                ItemToDisplay.Content = label;
                ItemToDisplay.Visibility = Visibility.Collapsed;

                this.Items.Add(ItemToDisplay);
                this.SelectedItem = ItemToDisplay;
            }
        }
    }

}
