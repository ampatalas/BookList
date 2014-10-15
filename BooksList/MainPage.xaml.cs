using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BooksList.Resources;
using BooksList.ViewModel;
using BooksList.Model;

namespace BooksList
{
    public partial class MainPage : PhoneApplicationPage
    {
        private BookViewModel vm;
        
        public MainPage()
        {
            InitializeComponent();
            vm = new BookViewModel();
            booksListBinding.DataContext = vm.BookItems;
        }

        private void ClickAdd(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/AddItem", UriKind.Relative));
        }

        private void ClickRemove(object sender, EventArgs e)
        {
            if (booksListBinding.SelectedItem == null) MessageBox.Show("No item is selected.", "Deleting items", MessageBoxButton.OK);
            else
            {
                Book toDelete = vm.BookItems[booksListBinding.SelectedIndex];
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete " + toDelete.Title + "?", "Deleting items", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    vm.BookItems.RemoveAt(booksListBinding.SelectedIndex);
                }
            }
        }
        private void ClickEdit(object sender, EventArgs e)
        {
            //PhoneApplicationService.Current.State["EditedItem"] = booksListBinding.SelectedItem as Book;
            //this.NavigationService.Navigate(new Uri("/EditItem", UriKind.Relative));
        }

        private void booksListBinding_GotFocus(object sender, RoutedEventArgs e)
        {
            ApplicationBarIconButton editButton = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            ApplicationBarIconButton deleteButton = (ApplicationBarIconButton)ApplicationBar.Buttons[2];

            editButton.IsEnabled = true;
            deleteButton.IsEnabled = true;
        }
       
    }
}