using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TrackApp
{
    public sealed partial class CategoryControl : UserControl
    {
        public delegate void CategorySelectedHandler(CategoryControl sender, Category g);

        public event CategorySelectedHandler CategorySelected;

        public CategoryControl()
        {
            this.InitializeComponent();
            Loaded += CategoryControl_Loaded;
        }

        void CategoryControl_Loaded(object sender, RoutedEventArgs e)
        {
            PointerEntered += (a, b) => Opacity = 0.5;
            PointerExited += (a, b) => Opacity = 1;
            Tapped += (a, b) =>
            {
                if (CategorySelected != null) CategorySelected(this, DataContext as Category);
            };
        }
    }
}
