using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
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
using Path = Windows.UI.Xaml.Shapes.Path;

namespace TrackApp
{
    public sealed partial class Chart : UserControl
    {
        public List<double> Elements = new List<double>();
        private Brush _color;

        public int NumberOfDays { get; set; }
        public bool PreviousChart { get; set; }

        public Brush Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public Chart()
        {
            this.InitializeComponent();
            SizeChanged += Chart_Loaded;
        }

        private void Chart_Loaded(object sender, RoutedEventArgs e)
        {
            double unit = root.ActualWidth/NumberOfDays;
            root.Children.Clear();
            uiLayer.Children.Clear();

            for (int x = 0; x < NumberOfDays; x++)
            {
                Line line = new Line
                {
                    X1 = x*unit,
                    X2 = x*unit,
                    Y1 = 0,
                    Y2 = root.ActualHeight,
                    Stroke = new SolidColorBrush(Colors.LightGray),
                    StrokeThickness = 1
                };

                root.Children.Add(line);

                var date = DateTime.Now.Subtract(TimeSpan.FromDays(x));
                if (PreviousChart) date = date.Subtract(TimeSpan.FromDays(NumberOfDays));
                var txt = new TextBlock
                {
                    Text = NumberOfDays>7 ? date.Day.ToString() : date.ToString("ddd") + " " + date.Day,
                    FontSize = NumberOfDays>7 ? 8: 15,
                    Foreground = new SolidColorBrush(Colors.Gray)
                };

                uiLayer.Children.Add(txt);
                Canvas.SetLeft(txt, ActualWidth - (x +  0.5)*(unit));
                txt.Loaded +=
                    (a, b) =>
                        Canvas.SetLeft((TextBlock) a, Canvas.GetLeft((TextBlock) a) - ((TextBlock) a).ActualWidth/2.0);
                Canvas.SetTop(txt, 5);
            }
            Draw();
        }



        public void Draw()
        {
            if (Elements.Count == 0) return;
            double unit = ActualWidth/NumberOfDays;
            valueLayer.Children.Clear();

            //Polygon polygon = new Polygon { Fill = new SolidColorBrush(Colors.Gray) , FillRule = FillRule.EvenOdd};
            Path path = new Path {Fill = new SolidColorBrush(Colors.Gray)};
            var data = new PathGeometry {FillRule = FillRule.Nonzero};
            PathFigure pathFigure = new PathFigure();

            /*First Point After M is the Start Point*/
            pathFigure.StartPoint = new Point(ActualWidth, root.ActualHeight);

            pathFigure.IsClosed = true;

            data.Figures.Add(pathFigure); /* Adding it to path Geometry*/

            for (int x = 0; x < Elements.Count; x++)
            {
                /* Line line = new Line
                {
                    X1 = x * unit,
                    X2 = x * unit + unit,
                    Y1 = root.ActualHeight -  ((Elements[x] == 0? 0 : (Elements[x]/Elements.Max()))) * root.ActualHeight,
                    Y2 = root.ActualHeight - ((Elements[x+1] == 0? 0 : (Elements[x+1]/Elements.Max()))) * root.ActualHeight,
                    Stroke = Color,
                    StrokeThickness = 2.5
                };
                pathFigure.Segments.Add(new LineSegment {  Point = new Point(line.X1 , line.Y1) });
                pathFigure.Segments.Add(new LineSegment {  Point =  new Point(line.X2,line.Y2) });
                Ellipse ellipse = new Ellipse
                {
                    Width = 20 , Height = 20 , StrokeThickness = 0 , Fill = Color

                };
                valueLayer.Children.Add(ellipse);
                Canvas.SetLeft(ellipse,line.X1 - 5);
                Canvas.SetTop(ellipse, line.Y1 - 5);


                Ellipse ellipse2 = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    StrokeThickness = 0,
                    Fill = Color
                };
                valueLayer.Children.Add(ellipse2);
                Canvas.SetLeft(ellipse2, line.X2 - 5);
                Canvas.SetTop(ellipse2, line.Y2 - 5);
                valueLayer.Children.Add(line);
            }
            pathFigure.Segments.Add(new LineSegment{Point = new Point(0,root.ActualHeight)
                });
            path.Data = data;
           // polygon.Points.Add(new Point(root.ActualHeight, root.ActualWidth));
           // valueLayer.Children.Add(path);
                * 
                */
                double height =  ((Elements[x] == 0 ? 0 : (Elements[x] / Elements.Max()))) * root.ActualHeight;
                Rectangle rectangle = new Rectangle
                {
                    Width = unit,
                    Height = height,
                    Fill = new SolidColorBrush(Colors.Orange),
                    Stroke = new SolidColorBrush(Colors.White),
                    StrokeThickness = 2.5
                };
                 
                valueLayer.Children.Add(rectangle);
                Canvas.SetLeft(rectangle , x*unit);
                Canvas.SetTop(rectangle , root.ActualHeight-height);

                TextBlock block = new TextBlock
                {
                    Text = Elements[x].ToString(),
                    FontSize = NumberOfDays>7?19 : 25,
                    Foreground = rectangle.Fill
                };
                block.Loaded += (a, b) =>
                {
                    var bl = a as TextBlock;
                    Canvas.SetLeft((a as TextBlock), Canvas.GetLeft(bl) - bl.ActualWidth / 2.0);
                    Canvas.SetTop(bl, Canvas.GetTop(bl) - bl.ActualHeight - 5);
                };
                valueLayer.Children.Add(block);
                Canvas.SetLeft(block , Canvas.GetLeft(rectangle) + unit/2.0);
                Canvas.SetTop(block,Canvas.GetTop(rectangle));

                
            }
        }
    }
}
