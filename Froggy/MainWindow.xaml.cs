using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Froggy
{
    public partial class MainWindow : Window
    {
        private bool isDragging;
        private Point startPoint;

        private DispatcherTimer timer;
        private DateTime startTime;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick; // Wire up the event handler
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 2 && e.OriginalSource is Image)
                {
                    StartTimer();
                }
                else
                {
                    isDragging = true;
                    startPoint = e.GetPosition(this);
                    Mouse.Capture(this);
                }
            }
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(this);
                double offsetX = currentPosition.X - startPoint.X;
                double offsetY = currentPosition.Y - startPoint.Y;

                // Update the position of the window
                Left += offsetX;
                Top += offsetY;
            }
        }

        private void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                Mouse.Capture(null);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            timerLabel.Content = elapsed.ToString(@"mm\:ss");
        }

        private void StartTimer()
        {
            startTime = DateTime.Now;
            timer.Start(); // Start the timer
        }

        private void StopTimer()
        {
            timer.Stop();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartTimer();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            StopTimer();
        }
    }
}
