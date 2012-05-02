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
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace MediaPlayerMvvmSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        private DispatcherTimer Ticker;

        public MainWindow()
        {
            InitializeComponent();
            Ticker = new DispatcherTimer();
            Ticker.Interval = new TimeSpan(0, 0, 0, 0, 200);
            Ticker.Tick += Tick;
            mediaElement.MediaOpened += Element_MediaOpened;
            mediaElement.MediaEnded += Element_MediaEnded;
        }

        private void Element_MediaEnded(object sender, EventArgs e)
        {
            Ticker.Stop();
            mediaElement.Stop();
            //InitMediaElement();
        }

        private void InitMediaElement()
        {
            TotalTime.Text =
                String.Format("{0:00}:{1:00}:{2:00}",
                0, 0, 0);
        }

        private void Element_MediaOpened(object sender, EventArgs e)
        {
            Ticker.Start();
            TimeLine.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
            TimeSpan ts = TimeSpan.FromMilliseconds(TimeLine.Maximum);
            TotalTime.Text =
                String.Format("{0:00}:{1:00}:{2:00}",
                ts.Hours, ts.Minutes, ts.Seconds);
        }

        private void Tick(object sender, EventArgs e)
        {
            TimeSpan ts = TimeSpan.FromMilliseconds(mediaElement.Position.TotalMilliseconds);
            TimeValue.Text =
                String.Format("{0:00}:{1:00}:{2:00}",
                ts.Hours, ts.Minutes, ts.Seconds);
            TimeLine.Value = mediaElement.Position.TotalMilliseconds;
        }

        private void sliderPositionChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan ts = TimeSpan.FromMilliseconds(e.NewValue);
            TimeValue.Text =
                String.Format("{0:00}:{1:00}:{2:00}",
                ts.Hours, ts.Minutes, ts.Seconds);
        }

        private void VolumeChanged(
            object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Volume = Volume.Value;
        }

        private void DragBegin(
            object sender, DragStartedEventArgs e)
        {
            Ticker.Tick -= Tick;
        }

        private void DragEnd(
            object sender, DragCompletedEventArgs e)
        {
            mediaElement.Position = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(TimeLine.Value));
            Ticker.Tick += Tick;
        }
    }
}
