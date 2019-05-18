// <copyright file="CustomerSide.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Szt2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using Szt2.ViewModels;

    /// <summary>
    /// Interaction logic for CustomerSide.xaml
    /// </summary>
    public partial class CustomerSide : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerSide"/> class.
        /// </summary>
        public CustomerSide()
        {
            this.InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var senderWindow = sender as Window;
            senderWindow.WindowState = WindowState.Maximized;
        }

        private void ActualImage_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            var scale = new DoubleAnimation();
            var fade = new DoubleAnimation();

            if ((this.DataContext as ViewModel).PrevTermek != null)
            {
                scale = new DoubleAnimation()
                {
                    To = 0,
                    Duration = TimeSpan.FromSeconds(1)
                };

                fade = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(1000));

                this.prevImage.BeginAnimation(Image.OpacityProperty, fade);
                this.prevImage.RenderTransform = new ScaleTransform();
                this.prevImage.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scale);
                this.prevImage.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scale);
            }

            scale = new DoubleAnimation()
            {
                From = 0,
                Duration = TimeSpan.FromSeconds(1)
            };

            fade = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(1000));

            this.actualImage.BeginAnimation(Image.OpacityProperty, fade);
            this.actualImage.RenderTransform = new ScaleTransform();
            this.actualImage.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scale);
            this.actualImage.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scale);
        }
    }
}
