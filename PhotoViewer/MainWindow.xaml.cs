using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace PhotoViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Presenter DataContext;
        System.Windows.Threading.DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Presenter(1000);
            DataContext.ImageSelected += DataContext_ImageSelect;
            DataContext.VideoSelected += DataContext_VideoSelect;
            mediaem.LoadedBehavior = MediaState.Play;
            mediaem.UnloadedBehavior = MediaState.Stop;
            mediaem.MediaEnded += Mediaem_MediaEnded;
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(ShowImage);
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();
        }

      
        private void ShowImage(object o, EventArgs e)
        {
            DataContext.ShowImages();
        }

        private void DataContext_ImageSelect(ImgItem item)
        {
            mediaem.Visibility = Visibility.Hidden;
            img.Visibility = Visibility.Visible;
            img.Source = new BitmapImage(new Uri(item.Location));
        }

        private void DataContext_VideoSelect(ImgItem item)
        {
            timer.Stop();
            mediaem.Visibility = Visibility.Visible;
            img.Visibility = Visibility.Hidden;
            mediaem.Source = new Uri(item.Location);
        }
        private void Mediaem_MediaEnded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }


    }
}
