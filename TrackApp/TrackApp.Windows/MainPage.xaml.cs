﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TrackApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = App.model;
            Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Category category1 = new Category
            {
                Name = "Calories"
            };
            category1.Elements.Add(new Element{Comment = "This is some random test for testing." , Date = DateTime.Now.Subtract(TimeSpan.FromDays(1)) , Value = 15});
            category1.Elements.Add(new Element { Comment = "This is some random test for testing.", Date = DateTime.Now.Subtract(TimeSpan.FromDays(2)), Value = 16 });
            category1.Elements.Add(new Element { Comment = "This is some random test for testing.", Date = DateTime.Now.Subtract(TimeSpan.FromDays(3)), Value = 6 });
            category1.Elements.Add(new Element { Comment = "This is some random test for testing.", Date = DateTime.Now.Subtract(TimeSpan.FromDays(4)), Value = 11 });

            category1.Elements.Add(new Element { Comment = "This is some random test for testing.", Date = DateTime.Now.Subtract(TimeSpan.FromDays(7)).Subtract(TimeSpan.FromDays(1)), Value = 2 });
            category1.Elements.Add(new Element { Comment = "This is some random test for testing.", Date = DateTime.Now.Subtract(TimeSpan.FromDays(7)).Subtract(TimeSpan.FromDays(2)), Value = 23 });
            category1.Elements.Add(new Element { Comment = "This is some random test for testing.", Date = DateTime.Now.Subtract(TimeSpan.FromDays(7)).Subtract(TimeSpan.FromDays(3)), Value = 11 });
            category1.Elements.Add(new Element { Comment = "This is some random test for testing.", Date = DateTime.Now.Subtract(TimeSpan.FromDays(7)).Subtract(TimeSpan.FromDays(4)), Value = 9 });
            Category category2 = new Category { Name = "Pushups" };
            App.model.Categories.Add(category1);
            App.model.Categories.Add(category2);

            last7.Tapped += (z, q) =>
            {
                List<double> vals = new List<double>();

                for (int x = 0; x < 7; x++)
                {
                    DateTime date = DateTime.Now.Subtract(TimeSpan.FromDays(x));
                    var day = App.model.SelectedCategory.Elements.Where(y => (y.Date.DayOfYear == date.DayOfYear));
                    var ans = day.Count() == 0 ? 0 : day.Select(k => k.Value).Aggregate((a, b) => a + b);
                    vals.Add(ans);
                }
                vals.Reverse();

                txtMax.Text = Math.Round(vals.Max(), 3).ToString();
                txtMin.Text = Math.Round(vals.Min(), 3).ToString();
                txtAverage.Text = Math.Round(vals.Average(), 3).ToString();

                chart.Elements = vals;
                chart.Draw();
                foreach (var child in chartHolder.Children)
                {
                    child.Visibility = Visibility.Collapsed;
                }
                chart.Visibility = Visibility.Visible;
            };

            previous7.Tapped += (z, q) =>
            {
                List<double> vals2 = new List<double>();



                for (int x = 0; x < 7; x++)
                {
                    DateTime date = DateTime.Now.Subtract(TimeSpan.FromDays(7)).Subtract(TimeSpan.FromDays(x));
                    var day = App.model.SelectedCategory.Elements.Where(y => (y.Date.DayOfYear == date.DayOfYear));
                    var ans = day.Count() == 0 ? 0 : day.Select(k => k.Value).Aggregate((a, b) => a + b);
                    vals2.Add(ans);
                }


                txtMax.Text = Math.Round(vals2.Max(), 3).ToString();
                txtMin.Text = Math.Round(vals2.Min(), 3).ToString();
                txtAverage.Text = Math.Round(vals2.Average(), 3).ToString();

                vals2.Reverse();





                previousChart.Elements = vals2;
                previousChart.Draw();
                foreach (var child in chartHolder.Children)
                {
                    child.Visibility = Visibility.Collapsed;
                }
                previousChart.Visibility = Visibility.Visible;
            };

            last30.Tapped += (z, q) =>
            {
                List<double> vals30 = new List<double>();
                for (int x = 0; x < 30; x++)
                {
                    DateTime date = DateTime.Now.Subtract(TimeSpan.FromDays(x));
                    var day = App.model.SelectedCategory.Elements.Where(y => (y.Date.DayOfYear == date.DayOfYear));
                    var ans = day.Count() == 0 ? 0 : day.Select(k => k.Value).Aggregate((a, b) => a + b);
                    vals30.Add(ans);
                }
                vals30.Reverse();
                txtMax.Text = Math.Round(vals30.Max(), 3).ToString();
                txtMin.Text = Math.Round(vals30.Min(), 3).ToString();
                txtAverage.Text = Math.Round(vals30.Average(), 3).ToString();

                chart30.Elements = vals30;
                chart30.Draw();


                foreach (var child in chartHolder.Children)
                {
                    child.Visibility = Visibility.Collapsed;
                }



                chart30.Visibility = Visibility.Visible;
            };
        }

        private void addCategory(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //jesus
            var str = ((TextBox) ((Grid) ((Button) sender).Parent).Children[0]).Text;
            App.model.Categories.Add(new Category{Name =  str});
        }

        private void categorySelected(TrackApp.CategoryControl sender, TrackApp.Category g)
        {
            App.model.SelectedCategory = g;
            updateChart();
        }


        void updateChart()
        {
            List<double> vals = new List<double>();

            for (int x = 0; x < 7; x++)
            {
                DateTime date = DateTime.Now.Subtract(TimeSpan.FromDays(x));
                var day = App.model.SelectedCategory.Elements.Where(y => (y.Date.DayOfYear == date.DayOfYear));
                var ans = day.Count() == 0 ? 0 : day.Select(k => k.Value).Aggregate((a, b) => a + b);
                vals.Add(ans);
            }
            vals.Reverse();

            txtMax.Text = Math.Round(vals.Max(), 3).ToString();
            txtMin.Text = Math.Round(vals.Min(), 3).ToString();
            txtAverage.Text = Math.Round(vals.Average(), 3).ToString();

            chart.Elements = vals;
            chart.Draw();
            foreach (var child in chartHolder.Children)
            {
                child.Visibility = Visibility.Collapsed;
            }
            chart.Visibility = Visibility.Visible;
        }


        private void addElement(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var val = ((TextBox)((StackPanel)((Button)sender).Parent).Children[0]).Text;
            var date = ((DatePicker)((StackPanel)((Button)sender).Parent).Children[1]).Date.DateTime;
            var str = ((TextBox)((StackPanel)((Button)sender).Parent).Children[2]).Text;
            App.model.SelectedCategory.Elements.Add(new Element{Comment =  str , Date = date , Value = double.Parse(val)});
            updateChart();
        }
    }
}
