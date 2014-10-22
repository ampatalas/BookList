using BooksList.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace BooksList.Model
{
    [DataContract]
    public class Book : INotifyPropertyChanged
    {
        private string _Title;
        private string _AuthorName;
        private int _PublishYear;
        private BitmapImage _Cover;

        public event PropertyChangedEventHandler PropertyChanged;

        public Book(string name, string authorName, int publishYear, BitmapImage cover)
        {
            _Title = name;
            _AuthorName = authorName;
            _PublishYear = publishYear;
            _Cover = cover;
        }

        public Book(string name, string authorName, int publishYear)
        {
            _Title = name;
            _AuthorName = authorName;
            _PublishYear = publishYear;
            _Cover = DefaultCover();
        }

        

        public string Title
        {
            get { return _Title; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) throw new InvalidBookDataException(AppResources.WarningTitle);
                else
                {
                    _Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public string AuthorName
        {
            get { return _AuthorName; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) throw new InvalidBookDataException(AppResources.WarningAuthor);
                else
                {
                    _AuthorName = value;
                    OnPropertyChanged("AuthorName");
                }
            }
        }

        public int PublishYear
        {
            get { return _PublishYear; }
            set
            {
                if (value <= 0) throw new InvalidBookDataException(AppResources.WarningYear);
                else
                {
                    _PublishYear = value;
                    OnPropertyChanged("PublishYear");
                }
            }
        }

        public BitmapImage Cover
        {
            get { return _Cover; }
            set
            { 
                _Cover = value;
                OnPropertyChanged("Cover");
            }
        }


        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private static BitmapImage DefaultCover()
        {
            Uri uri = new Uri("Images/bookcovertemplate.jpg", UriKind.Relative);
            StreamResourceInfo resourceInfo = Application.GetResourceStream(uri);
            BitmapImage bmp = new BitmapImage();
            bmp.SetSource(resourceInfo.Stream);
            return bmp;
        }
    }
}
