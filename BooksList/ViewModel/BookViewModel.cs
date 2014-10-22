using BooksList.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace BooksList.ViewModel
{
    public class BookViewModel
    {
        public static ObservableCollection<Book> BookItems { get; set; }

        public BookViewModel()
        {
            BookItems = new ObservableCollection<Book>();

            BookItems.Add(new Book("To Kill a Mockingbird", "Harper Lee", 1960));
            BookItems.Add(new Book("Love in the Time of Cholera", "Gabriel García Márquez", 1985));
            BookItems.Add(new Book("Lisey's Story", "Stephen King", 2006));
            BookItems.Add(new Book("Clean Code", "Robert C. Martin", 2008));
            BookItems.Add(new Book("The Fault in Our Stars", "John Green", 2012));
        }
    }
}
