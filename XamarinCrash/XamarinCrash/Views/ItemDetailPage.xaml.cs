using System.ComponentModel;
using Xamarin.Forms;
using XamarinCrash.ViewModels;

namespace XamarinCrash.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}