// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Szt2
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
    using BusinessLogic;
    using DataLayer;
    using Szt2.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel vm;
        private DispatcherTimer timer;
        private CustomerSide cs;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.vm = this.FindResource("VM") as ViewModel;
            this.timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            this.timer.Tick += this.Timer_Tick;

            this.DrawButtonGrid(this.vm.CategoryList.Select(x => x as IMenuItem).ToObservableCollection());
            this.orderList.ItemsSource = this.vm.Order.TermekList;
        }

        /// <summary>
        /// Adds the buttons of the menu items and calculates the size needed.
        /// </summary>
        /// <param name="menuItems">The collection of menu items.</param>
        public void DrawButtonGrid(ObservableCollection<IMenuItem> menuItems)
        {
            this.buttons.Children.Clear();
            this.buttons.RowDefinitions.Clear();
            this.buttons.ColumnDefinitions.Clear();

            double gridX;
            double gridY;

            gridX = Math.Pow(menuItems.Count, 0.5);
            gridY = menuItems.Count / gridX;

            for (int i = 0; i < gridX; i++)
            {
                this.buttons.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < gridY; i++)
            {
                this.buttons.RowDefinitions.Add(new RowDefinition());
            }

            this.buttons.ShowGridLines = true;
            this.buttons.Background = Brushes.LightGray;

            List<Button> buttonList = new List<Button>();

            foreach (IMenuItem c in menuItems)
            {
                Image image = new Image { Source = new BitmapImage(new Uri(c.Picture ?? System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"/Pictures/blank.png", UriKind.RelativeOrAbsolute)), Stretch = Stretch.UniformToFill };

                Viewbox vb = new Viewbox
                {
                    Child = new TextBlock { Text = c.Name, HorizontalAlignment = HorizontalAlignment.Center }
                };

                Grid sp = new Grid();
                sp.RowDefinitions.Add(new RowDefinition { Height = new GridLength(8, GridUnitType.Star) });
                sp.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
                Grid.SetRow(image, 0);
                sp.Children.Add(image);
                Grid.SetRow(vb, 1);
                sp.Children.Add(vb);

                Button b = new Button
                {
                    Content = sp,
                    Tag = c
                };

                b.Click += this.B_Click;
                b.PreviewMouseDown += this.B_MouseDown;
                b.PreviewMouseUp += this.B_MouseUp;
                buttonList.Add(b);
            }

            int buttonIndex = 0;
            for (int i = 0; i < gridY; i++)
            {
                for (int j = 0; j < gridX; j++)
                {
                    if (buttonIndex < menuItems.Count)
                    {
                        Grid.SetRow(buttonList[buttonIndex], i);
                        Grid.SetColumn(buttonList[buttonIndex], j);

                        this.buttons.Children.Add(buttonList[buttonIndex]);
                        buttonIndex++;
                    }
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Window win = new AmountSelector(this.vm);
            if (this.timer.Tag is Termek)
            {
                win.Show();
            }

            this.timer.Stop();
        }

        private void B_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.timer.Stop();
        }

        private void B_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.timer.Start();
            this.timer.Tag = (sender as Button).Tag;

            OrderListItem selecteditem = this.vm.Order.TermekList.FirstOrDefault(x => x.Termek.Equals((sender as Button).Tag as Termek));

            if (selecteditem != null)
            {
                this.vm.SelectedProduct = selecteditem;
            }
            else
            {
                this.vm.SelectedProduct = new OrderListItem() { Termek = (sender as Button).Tag as Termek };
            }
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            var selected = (e.Source as Button).Tag;

            if (selected is Category)
            {
                ObservableCollection<IMenuItem> filteredList = new ObservableCollection<IMenuItem>(this.vm.ProductList.Where(x => (x as Termek).Category.CategoryId == (selected as Category).CategoryId));

                this.DrawButtonGrid(filteredList);
                this.backButton.Visibility = Visibility.Visible;
                this.prevOrdersButton.Visibility = Visibility.Hidden;
            }

            Termek newtermek = new Termek();
            newtermek = selected as Termek;

            if (newtermek != null)
            {
                if (!this.vm.OrderListEnabled)
                {
                    this.vm.Order = new Order();
                    this.orderList.ItemsSource = this.vm.Order.TermekList;
                }

                this.vm.Order.AddToTermekList(newtermek);
                this.vm.Order.Total = this.vm.Order.TotalPrice(this.vm.Order.TermekList);
                this.vm.OrderListEnabled = true;

                // this.orderList.Items.Refresh();
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window win = new AmountSelector(this.vm);
            if (sender is ListViewItem item && item.IsSelected)
            {
                win.Show();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.DrawButtonGrid(this.vm.CategoryList.Select(x => x as IMenuItem).ToObservableCollection());
            this.backButton.Visibility = Visibility.Hidden;
            this.prevOrdersButton.Visibility = Visibility.Visible;
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.vm.Order.TermekList.Count > 0)
            {
                this.vm.SelectedProduct = null;
                this.vm.OrderListEnabled = false;

                this.vm.Order.Date = DateTime.Now;

                OrderRepository repo = new OrderRepository(this.vm.Ctx);
                DataConverter converter = new DataConverter(this.vm.Ctx);
                this.vm.Order.OrderId = repo.Insert(converter.OrderConverter(this.vm.Order));

                this.vm.OrderList.Insert(0, this.vm.Order);
                this.backButton.Visibility = Visibility.Collapsed;
                this.prevOrdersButton.Visibility = Visibility.Visible;
                this.DrawButtonGrid(this.vm.CategoryList.Select(x => x as IMenuItem).ToObservableCollection());
            }

            // Rating
            if (this.vm.Order.TermekList.Count > 0)
            {
                Window win = new RatingWindow(this.vm.Order)
                {
                    DataContext = this.vm
                };

                    if (System.Windows.Forms.Screen.AllScreens.Length > 1)
                    {
                        System.Windows.Forms.Screen s2 = System.Windows.Forms.Screen.AllScreens[1];
                        win.Top = (s2.WorkingArea.Height / 2) - (win.Height / 2);
                        win.Left = (s2.WorkingArea.Width / 2) - (win.Width / 2);
                        win.Show();
                    }
                    else
                    {
                        System.Windows.Forms.Screen s2 = System.Windows.Forms.Screen.AllScreens[0];
                        win.Top = (s2.WorkingArea.Height / 2) - (win.Height / 2);
                        win.Left = (s2.WorkingArea.Width / 2) - (win.Width / 2);

                        win.Show();
                    }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (this.cs != null)
            {
                this.cs.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.cs = new CustomerSide
            {
                DataContext = this.vm
            };

            if (System.Windows.Forms.Screen.AllScreens.Length > 1)
            {
                System.Windows.Forms.Screen s2 = System.Windows.Forms.Screen.AllScreens[1];
                this.cs.Width = s2.WorkingArea.Width;
                this.cs.Height = s2.WorkingArea.Height;
                this.cs.Top = s2.WorkingArea.Location.Y;
                this.cs.Left = s2.WorkingArea.Location.X;
                this.cs.Show();
            }
            else
            {
                System.Windows.Forms.Screen s2 = System.Windows.Forms.Screen.AllScreens[0];
                this.cs.Width = s2.WorkingArea.Width;
                this.cs.Height = s2.WorkingArea.Height;
                this.cs.Top = s2.WorkingArea.Location.Y;
                this.cs.Left = s2.WorkingArea.Location.X;
                this.cs.Show();
            }

            var mainWindow = sender as Window;
            mainWindow.WindowState = WindowState.Maximized;
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            Window win = new HistoryWindow()
            {
                DataContext = this.vm
            };
            win.Show();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            this.orderList.ItemsSource = this.vm.Order.TermekList;
            this.orderList.Items.Refresh();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && Keyboard.Modifiers == ModifierKeys.Control)
            {
                Window win = new AdminWindow(this.vm)
                {
                    Owner = this
                };
                win.Show();
            }
        }
    }
}
