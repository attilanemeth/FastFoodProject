// <copyright file="AdminWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Szt2
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using BusinessLogic;
    using DataLayer;
    using Szt2.ViewModels;

    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private AdminViewModel avm;
        private DataConverter converter;
        private ProductRepository productRepo;
        private CategoryRepository catRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminWindow"/> class.
        /// </summary>
        public AdminWindow()
        {
            this.InitializeComponent();
            this.avm = this.FindResource("VM") as AdminViewModel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminWindow"/> class.
        /// </summary>
        /// <param name="vm">The view model to use by the window.</param>
        public AdminWindow(ViewModel vm)
            : this()
        {
            this.avm.Ctx = vm.Ctx;
            this.converter = new DataConverter(this.avm.Ctx);
            this.catRepo = new CategoryRepository(this.avm.Ctx);
            this.productRepo = new ProductRepository(this.avm.Ctx);
            this.avm.ProductList = vm.ProductList;
            this.avm.CategoryList = vm.CategoryList;
            this.avm.Order = vm.Order;
            this.avm.SelectedCategory = this.avm.CategoryList[0] as Category;
            var filterProductsQuery = vm.ProductList.Where(x => (x as Termek).Category.Name.Equals(this.avm.SelectedCategory.Name));
            this.avm.FilteredProducts = new ObservableCollection<Termek>(filterProductsQuery);
            this.avm.Stats.Update(this.avm.Stats.DateFrom, this.avm.Stats.DateTo, this.avm.Ctx);
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.avm.SelectedCategory != null)
            {
                var filterProductsQuery = this.avm.ProductList.Where(x => (x as Termek).Category.Name.Equals(this.avm.SelectedCategory.Name));
                this.avm.FilteredProducts = new ObservableCollection<Termek>(filterProductsQuery);
            }
        }

        private void DeleteProductClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Biztosan törölni akarja ezt a terméket? " + this.avm.SelectedProduct.Name, "Figyelem!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                this.avm.Ctx.ORDERLISTITEMs.RemoveRange(this.avm.Ctx.ORDERLISTITEMs.Where(x => x.PRODUCT1.PRODUCTID == this.avm.SelectedProduct.TermekId));
                this.avm.Ctx.PRODUCTs.Remove(this.avm.Ctx.PRODUCTs.SingleOrDefault(x => x.PRODUCTID == this.avm.SelectedProduct.TermekId));
                this.avm.Ctx.SaveChanges();

                this.avm.Order.TermekList.Remove(oli => oli.Termek.TermekId == this.avm.SelectedProduct.TermekId);
                this.avm.ProductList.Remove(this.avm.SelectedProduct);
                this.avm.FilteredProducts.Remove(this.avm.SelectedProduct);
            }
        }

        private void DeleteCategoryClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Biztosan törölni akarja ezt a kategóriát? Ez törölni fogja a kategória összes termékét is! " + this.avm.SelectedCategory.Name, "Figyelem!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                this.avm.Ctx.ORDERLISTITEMs.RemoveRange(this.avm.Ctx.ORDERLISTITEMs.Where(x => x.PRODUCT1.CATEGORY.CATEGORYID == this.avm.SelectedCategory.CategoryId));
                var toRemove = this.avm.Ctx.PRODUCTs.Where(x => x.CATEGORY.CATEGORYID == this.avm.SelectedCategory.CategoryId).ToList();
                this.avm.Ctx.PRODUCTs.RemoveRange(toRemove);
                var toRemoveCat = this.avm.Ctx.CATEGORies.SingleOrDefault(x => x.CATEGORYID == this.avm.SelectedCategory.CategoryId);
                this.avm.Ctx.CATEGORies.Remove(toRemoveCat);
                this.avm.Ctx.SaveChanges();

                this.avm.ProductList = DataConverter.ProductListConverter(this.avm.Ctx.PRODUCTs.ToObservableCollection());
                this.avm.FilteredProducts = this.avm.ProductList.Where(x => x.Category.CategoryId == this.avm.SelectedCategory.CategoryId).ToObservableCollection();
                this.avm.CategoryList.Remove(this.avm.SelectedCategory);
            }
        }

        private void EditCategoryClick(object sender, RoutedEventArgs e)
        {
            Window win = new MenuItemEditor(this.avm.SelectedCategory);
            if (this.avm.SelectedCategory != null)
            {
                if (win.ShowDialog() == true)
                {
                    this.catRepo.Update(DataConverter.CategoryConverter(this.avm.SelectedCategory));

                    this.categoryListBox.Items.Refresh();
                    MessageBox.Show("Sikeres módosítás!");
                }
            }
        }

        private void EditProductClick(object sender, RoutedEventArgs e)
        {
            MenuItemEditor win = new MenuItemEditor(this.avm.SelectedProduct);
            if (this.avm.SelectedProduct != null)
            {
                if (win.ShowDialog() == true)
                {
                    this.productRepo.Update(this.converter.ProductConverter(this.avm.SelectedProduct));

                    this.productListBox.Items.Refresh();
                    MessageBox.Show("Sikeres módosítás!");
                }
            }
        }

        private void AddProductClick(object sender, RoutedEventArgs e)
        {
            MenuItemEditor win = new MenuItemEditor(true);
            if (win.ShowDialog() == true)
            {
                win.Product.Category = this.avm.SelectedCategory;

                win.Product.TermekId = this.productRepo.Insert(this.converter.ProductConverter(win.Product));

                this.avm.FilteredProducts.Add(win.Product);
                this.avm.ProductList.Add(win.Product);
                this.productListBox.Items.Refresh();

                // MessageBox.Show("Sikeres hozzáadás!");
            }
        }

        private void AddCategoryClick(object sender, RoutedEventArgs e)
        {
            MenuItemEditor win = new MenuItemEditor();
            if (win.ShowDialog() == true)
            {
                win.Category.CategoryId = this.catRepo.Insert(DataConverter.CategoryConverter(win.Category));

                this.avm.CategoryList.Add(win.Category);
                this.categoryListBox.Items.Refresh();

                // MessageBox.Show("Sikeres hozzáadás!");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((MainWindow)this.Owner).DrawButtonGrid(this.avm.CategoryList.Select(x => x as IMenuItem).ToObservableCollection());
            ((MainWindow)this.Owner).backButton.Visibility = Visibility.Hidden;
            ((MainWindow)this.Owner).prevOrdersButton.Visibility = Visibility.Visible;
            ((MainWindow)this.Owner).Activate();
        }

        private void DatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.avm != null)
            {
                Task.Run(() => this.avm.Stats.Update(this.avm.Stats.DateFrom, this.avm.Stats.DateTo, this.avm.Ctx));
            }
        }
    }
}
