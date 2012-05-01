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
using System.Windows.Threading;
using System.Windows.Controls.Primitives;

namespace MediaElementTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string path;
        private DispatcherTimer Ticker;

        public MainWindow()
        {
            InitializeComponent(); 
            Ticker = new DispatcherTimer();
            Ticker.Interval = new TimeSpan(0, 0, 0, 0, 200);
            Ticker.Tick += Tick;
            myMediaElement.MediaOpened += Element_MediaOpened;
            //timelineSlider.Thumb.DragCompleted += SeekToMediaPosition; 
            path = "C:\\Users\\Tulimyrsky\\Videos\\Person.of.Interest.1x02.avi";
        }

        private void Tick(object sender, EventArgs e)
        {
        //    timelineSlider.ValueChanged -= SeekToMediaPosition;
            timelineSlider.Value = myMediaElement.Position.TotalMilliseconds;
         //   timelineSlider.ValueChanged += SeekToMediaPosition;
        }

        void OnMouseDownPlayMedia(object sender, RoutedEventArgs args)
        {
            myMediaElement.Source = new Uri(path);
            myMediaElement.Play();
        }

        // Pause the media.
        void OnMouseDownPauseMedia(object sender, RoutedEventArgs args)
        {

            // The Pause method pauses the media if it is currently running.
            // The Play method can be used to resume.
            myMediaElement.Pause();

        }

        // Stop the media.
        void OnMouseDownStopMedia(object sender, MouseButtonEventArgs args)
        {

            // The Stop method stops and resets the media to be played from
            // the beginning.
            myMediaElement.Stop();

        }

        // Change the volume of the media.
        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            myMediaElement.Volume = (double)volumeSlider.Value;
        }

        // Change the speed of the media.
        private void ChangeMediaSpeedRatio(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            myMediaElement.SpeedRatio = (double)speedRatioSlider.Value;
        }

        // When the media opens, initialize the "Seek To" slider maximum value
        // to the total number of miliseconds in the length of the media clip.
        private void Element_MediaOpened(object sender, EventArgs e)
        {
            timelineSlider.Maximum = myMediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
            myMediaElement.Volume = Convert.ToDouble(volumeSlider.Value);
            myMediaElement.SpeedRatio = 1;
        }

        // When the media playback is finished. Stop() the media to seek to media start.
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            myMediaElement.Stop();
        }

        // Jump to different parts of the media (seek to). 
        private void SeekToMediaPosition(object sender, DragCompletedEventArgs e)
        {
            Ticker.Tick -= Tick;
            myMediaElement.Position = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(timelineSlider.Value));
            Ticker.Tick += Tick;
        }
    }
}
