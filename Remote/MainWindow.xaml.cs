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
        public FilterType Filter { get; set; }

        //simulation variable


        public MainWindow()
        {
            InitializeComponent();
            
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
                this.Filter = (FilterType)filter.SelectedIndex;
            }
            catch (Exception e)
            {
                MessageBox.Show("잘못된 입력!\r\n"+e.ToString());
                InitializingInput();
            }
        }

        private void InitializingInput()
        {
            length.Text = "0";
            velocity.Text = "0";
            startscope.Text = "0";
            finishscope.Text = "0";
            delay.Text = "0";
            filter.SelectedIndex = 0;
        }

        //test click event
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InitializingParameter();
        }
    }

    public enum FilterType
    {
        Kalman,        
        LinearRegression,
        LowPass
    }
}
