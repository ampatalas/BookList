using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BooksList.Model;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;

namespace BooksList
{
    public partial class AddingPage : PhoneApplicationPage
    {

        private Book book;
        private bool editMode;
        private bool comingFromChooser;

        PhotoChooserTask photoChooserTask;

        public AddingPage()
        {
            InitializeComponent();
            this.photoChooserTask = new PhotoChooserTask();
            this.photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed); 
        }

        private void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e != null)
            {
                BitmapImage image = new BitmapImage();
                image.SetSource(e.ChosenPhoto);
                book.Cover = image;
                PhotoChooserButton.Content = e.OriginalFileName;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (comingFromChooser)
            {

            }
            else
            {
                if (this.NavigationContext.QueryString.ContainsKey("editmode"))
                {
                    editMode = Boolean.Parse(this.NavigationContext.QueryString["editmode"]);
                    if (editMode)
                    {
                        book = PhoneApplicationService.Current.State["EditedItem"] as Book;
                    }
                }
                else
                {
                    book = new Book("", "", 0);
                    editMode = false;
                }
                setUpView(book);
            }
        }

        private void setUpView(Book item)
        {
            BookTitle.DataContext = item;
            BookAuthor.DataContext = item;
            BookYear.DataContext = item;
        }

        private void ContentPanel_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                MessageBox.Show(e.Error.ErrorContent.ToString());
            }
        }

        private void AddItemClick(object sender, RoutedEventArgs e)
        {
            int year = (int)Double.Parse(BookYear.Text);
            if (!String.IsNullOrWhiteSpace(BookTitle.Text) && !String.IsNullOrWhiteSpace(BookAuthor.Text) && year > 0)
            {
                book.Title = BookTitle.Text;
                book.AuthorName = BookAuthor.Text;
                book.PublishYear = year;
                if (!editMode) MainPage.vm.BookItems.Add(book);

                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
            else
            {
                MessageBox.Show("You didn't put some of the information.");
            }
        }

        private void AddCoverClick(object sender, RoutedEventArgs e)
        {
            comingFromChooser = true;
            photoChooserTask.Show(); 
        }
    }
}