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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Remote
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        //environment variable
        public double Length { get; set; }
        public double Velocity { get; set; }
        public double StartScope { get; set; }
        public double FinishScope { get; set; }
        public double Delay { get; set; }
        public double Direction { get; set; }
        public FilterType Filter { get; set; }
        public FilterClass SelectedFilter1;
        public FilterClass SelectedFilter2;
        public long ScanFreq { get; set; }
        public long FreqTick { get; set; }

        public Point Target { get; set; }

        //simulation variable
        DispatcherTimer MainTimer = new DispatcherTimer();
        Queue<data> Queue1 = new Queue<data>();
        Queue<data> Queue2 = new Queue<data>();


        public MainWindow()
        {
            InitializeComponent();
            SelectedFilter1 = new FilterClass();
            SelectedFilter2 = new FilterClass();
            MainTimer.Tick += new EventHandler(TimerEvent);
            ScanFreq = 10000000;
            FreqTick = 1000000;
            MainTimer.Interval = new TimeSpan(ScanFreq);
            InitializingInput();
        }

        private void InitializingParameter()
        {
            try
            {
                this.Length = double.Parse(length.Text);
                this.Velocity = double.Parse(velocity.Text);
                this.StartScope = double.Parse(startscope.Text);
                this.FinishScope = double.Parse(finishscope.Text);
                this.Delay = double.Parse(delay.Text);
                this.Direction = double.Parse(direction.Text);
                this.Filter = (FilterType)filter.SelectedIndex;
            }
            catch (Exception e)
            {
                MessageBox.Show("잘못된 입력!\r\n" + e.ToString());
                InitializingInput();
            }
        }

        private void InitializingInput()
        {
            length.Text = "0";
            velocity.Text = "50";
            startscope.Text = "10000";
            finishscope.Text = "10000";
            direction.Text = "30";
            delay.Text = "0";
            filter.SelectedIndex = 0;
        }



        //test click event
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InitializingParameter();
            MainTimer.Start();
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((FilterType)filter.SelectedIndex == FilterType.Kalman)
            {

            }

            else if ((FilterType)filter.SelectedIndex == FilterType.LinearRegression)
            {

            }
            else if ((FilterType)filter.SelectedIndex == FilterType.LinearRegression)
            {

            }
            else
            {
                SelectedFilter1 = new FilterClass();
                SelectedFilter2 = new FilterClass();
            }
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            EstimateTarget();
            MoveTarget();
        }

        private void EstimateTarget()
        {
            SelectedFilter1.Add(new Point(Target.X, Target.Y));
            SelectedFilter2.Add(new Point(Target.X, Target.Y));
            if (Math.Sqrt(Target.X * Target.X + Target.Y * Target.Y) <= StartScope)
            {
                Point tmp = SelectedFilter1.Estimate();
                data remotedata = new data();
                remotedata.EstimatedPoint = new Point(Target.X, Target.Y);
                remotedata.PredictedPoint = tmp;
                Queue1.Enqueue(remotedata);
                start.Children.Add(MakeEllipse(Target.X, Target.Y, start.Height));
                start.Children.Add(MakeRectangle(remotedata.PredictedPoint.X, remotedata.PredictedPoint.Y, start.Height));
            }
            if (Math.Sqrt((Target.X - Length) * (Target.X - Length) + Target.Y * Target.Y) <= FinishScope)
            {
                Point tmp = SelectedFilter2.Estimate();
                data remotedata = new data();
                remotedata.EstimatedPoint = new Point(Target.X, Target.Y);
                remotedata.PredictedPoint = tmp;
                Queue2.Enqueue(remotedata);
                finish.Children.Add(MakeEllipse(Target.X, Target.Y, finish.Height));
                finish.Children.Add(MakeRectangle(remotedata.PredictedPoint.X, remotedata.PredictedPoint.Y, finish.Height));
            }
        }

        private void PredictTarget()
        {

        }

        private void DisplayTarget_1(data target)
        {

        }

        private void DisplayTarget_2(data target)
        {

        }

        private Ellipse MakeEllipse(double x, double y, double height)
        {
            Ellipse tmp = new Ellipse();
            tmp.Stroke = Brushes.Red;
            tmp.StrokeThickness = 1.0;
            tmp.Width = 5;
            tmp.Height = 5;
            Canvas.SetTop(tmp, height - y - 5);
            Canvas.SetLeft(tmp, x);
            return tmp;
        }

        private Rectangle MakeRectangle(double x, double y, double height)
        {
            Rectangle tmp = new Rectangle();
            tmp.Stroke = Brushes.Blue;
            tmp.StrokeThickness = 1.0;
            tmp.Width = 5;
            tmp.Height = 5;
            Canvas.SetTop(tmp, height - y - 5);
            Canvas.SetLeft(tmp, x);
            return tmp;
        }

        public long count = 0;
        public double gravity = -9.8;
        private void MoveTarget()
        {
            Target = new Point(Target.X + Velocity * Math.Cos(Math.PI / 180.0 * Direction), Target.Y + (Velocity * Math.Sin(Math.PI / 180.0 * Direction) + count * gravity));
            count++;
        }

    }

    public enum FilterType
    {
        Kalman,        
        LinearRegression,
        LowPass,
        Default
    }
}
