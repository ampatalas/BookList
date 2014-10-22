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
using BooksList.Resources;
using BooksList.ViewModel;

namespace BooksList
{
    public partial class AddingPage : PhoneApplicationPage
    {

        private bool editMode;

        public AddingPage()
        {
            InitializeComponent();
            SaveControl.SaveEvent += new EventHandler(this.LeavePage);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (!e.NavigationMode.Equals(NavigationMode.Back))
            {
                if (this.NavigationContext.QueryString.ContainsKey("editmode"))
                {
                    editMode = Boolean.Parse(this.NavigationContext.QueryString["editmode"]);
                    if (editMode)
                    {
                        SaveControl.editMode = true;
                        AddingPageTitle.Text = AppResources.AddEditPositionTitle;
                        SaveControl.BindingBook = PhoneApplicationService.Current.State["Book"] as Book;
                    }
                }
            }
        }

        public void LeavePage (object sender, EventArgs args)
        {
            SaveButtonControl senderControl = (SaveButtonControl) sender;
            if (senderControl.Leaving)
            {
                if (!editMode) ViewModel.BookViewModel.BookItems.Add(SaveControl.BindingBook);
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
        }
    }
}