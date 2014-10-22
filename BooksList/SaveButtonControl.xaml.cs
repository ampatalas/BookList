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
using BooksList.ViewModel;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;

namespace BooksList
{
    public partial class SaveButtonControl : UserControl
    {
        public bool Leaving { get; set; }
        public EventHandler SaveEvent;
        public Boolean editMode;

        PhotoChooserTask photoChooserTask;

        // Dependency property
        public static readonly DependencyProperty BindingBookProperty = DependencyProperty.Register(
                                          "BindingBook",
                                          typeof(Book),
                                          typeof(SaveButtonControl),
                                          new PropertyMetadata(new Book("", "", 2000)));

        public Book BindingBook
        {
            get
            {
                return GetValue(BindingBookProperty) as Book;
            }
            set
            {
                SetValue(BindingBookProperty, value);
            }
        }


        public SaveButtonControl()
        {
            InitializeComponent();

            // Data binding
            LayoutRoot.DataContext = this;

            // Photo chooser
            this.photoChooserTask = new PhotoChooserTask();
            this.photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
        }

        private void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e != null && e.ChosenPhoto != null)
            {
                BitmapImage image = new BitmapImage();
                image.SetSource(e.ChosenPhoto);
                BindingBook.Cover = image;
                PhotoChooserButton.Content = e.OriginalFileName;
            }
        }

        protected virtual void OnSaveEvent()
        {
            if (SaveEvent != null)
            {
                EventArgs args = new EventArgs();
                SaveEvent(this, args);
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Leaving = true;
            OnSaveEvent();
        }

        private void AddCoverClick(object sender, RoutedEventArgs e)
        {
            OnSaveEvent();
            photoChooserTask.Show(); 
        }

        private void LayoutRoot_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                MessageBox.Show(e.Error.ErrorContent.ToString());
            }
        }

        
    }
}
