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
        public static BookViewModel vm;
        
        public MainPage()
        {
            InitializeComponent();
            setUpMenuBar();
            vm = new BookViewModel();
            booksListBinding.DataContext = BookViewModel.BookItems;
        }

        private void setUpMenuBar()
        {
            ApplicationBarIconButton addButton = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
            ApplicationBarIconButton editButton = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            ApplicationBarIconButton deleteButton = (ApplicationBarIconButton)ApplicationBar.Buttons[2];

            addButton.Text = AppResources.MenuBarAdd;
            editButton.Text = AppResources.MenuBarEdit;
            deleteButton.Text = AppResources.MenuBarDelete;
        }

        private void ClickAdd(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/AddItem", UriKind.Relative));
        }

        private void ClickRemove(object sender, EventArgs e)
        {
            if (booksListBinding.SelectedItem == null) MessageBox.Show(AppResources.DeletingItemsNoObject, AppResources.DeletingItemsTitle, MessageBoxButton.OK);
            else
            {
                Book toDelete = BookViewModel.BookItems[booksListBinding.SelectedIndex];
                MessageBoxResult result = MessageBox.Show(AppResources.DeletingItemsWarning + toDelete.Title + "?", AppResources.DeletingItemsTitle, MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    BookViewModel.BookItems.RemoveAt(booksListBinding.SelectedIndex);
                }
            }
        }
        private void ClickEdit(object sender, EventArgs e)
        {
            PhoneApplicationService.Current.State["Book"] = booksListBinding.SelectedItem as Book;
            this.NavigationService.Navigate(new Uri("/EditItem", UriKind.Relative));
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