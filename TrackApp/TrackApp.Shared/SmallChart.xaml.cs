using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
using Windows.UI.Xaml.Shapes;

namespace TrackApp
{
    public sealed partial class SmallChart : UserControl
    {
        public SmallChart()
        {
            this.InitializeComponent();
            DataContextChanged += SmallChart_DataContextChanged;
        }

        private Category category;
        private List<double> Elements; 
        void SmallChart_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
             category = DataContext as Category;
            
            Loaded += (A, b) => Draw();
            category.Elements.CollectionChanged+=(a,b) => Draw();
        }

        void Draw()
        {
            Elements = new List<double>();
            for (int x = 0; x < 7; x++)
            {
                DateTime date = DateTime.Now.Subtract(TimeSpan.FromDays(x));
                var day = category.Elements.Where(y => (y.Date.DayOfYear == date.DayOfYear));
                var ans = day.Count() == 0 ? 0 : day.Select(k => k.Value).Aggregate((a, b) => a + b);
                Elements.Add(ans);
            }
            Elements.Reverse();

            root.Children.Clear();
            double unit = ActualWidth/7.0;
            for (int x = Elements.Count - 2; x >= 0; x--)
            {
                 Line line = new Line
                {
                    X1 = x * unit,
                    X2 = x * unit + unit,
                    Y1 = root.ActualHeight -  ((Elements[x] == 0? 0 : (Elements[x]/Elements.Max()))) * root.ActualHeight,
                    Y2 = root.ActualHeight - ((Elements[x+1] == 0? 0 : (Elements[x+1]/Elements.Max()))) * root.ActualHeight,
                    Stroke = new SolidColorBrush(Colors.White),
                    StrokeThickness = 2.5
                };;
                Ellipse ellipse = new Ellipse
                {
                    Width = 3 , Height = 3 , StrokeThickness = 0 , Fill = new SolidColorBrush(Colors.White)

                };
               // root.Children.Add(ellipse);
                Canvas.SetLeft(ellipse,line.X1 - 5);
                Canvas.SetTop(ellipse, line.Y1 - 5);


                Ellipse ellipse2 = new Ellipse
                {
                    Width = 3,
                    Height = 3,
                    StrokeThickness = 0,
                    Fill = new SolidColorBrush(Colors.White)
                };
               // root.Children.Add(ellipse2);
                Canvas.SetLeft(ellipse2, line.X2 - 5);
                Canvas.SetTop(ellipse2, line.Y2 - 5);
                root.Children.Add(line);
            }
           // polygon.Points.Add(new Point(root.ActualHeight, root.ActualWidth));
           // valueLayer.Children.Add(path);
            }
        }

    }

