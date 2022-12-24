using Favorite.Librarys;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Favorite
{
    public partial class AddCate : Window
    {
        public string root = System.AppDomain.CurrentDomain.BaseDirectory + "/Res/";
        private string image = string.Empty;

        public AddCate()
        {
            InitializeComponent();
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);
        }

        private void ImportImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Title = "Choose";
            open.ShowDialog();
            try
            {
                if (open.FileName != null)
                {
                    ImageMain.Source = new BitmapImage(new Uri(open.FileName));
                    string names = "Cate-" + (System.IO.Path.GetFileNameWithoutExtension(open.FileName)) + "-" + (new Random().Next(0, 1000000)) + (System.IO.Path.GetExtension(open.FileName));
                    string adderss = System.IO.Path.Combine(root, names);
                    File.Copy(open.FileName, adderss);
                    image = adderss;
                }
            }
            catch
            {
            }
        }

        private void Addcate(object sender, RoutedEventArgs e)
        {
            Database db = new Database();
            if (image.Length > 0)
            {
                if (TBTitle.Text.Length >= 3)
                    db.Add_Cate(new Models.Categoury() { Header = TBTitle.Text, Picture = image });

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(Main))
                    {
                        (window as Main).ListCate.ItemsSource = new Database().List_Cate();
                    }
                }
                Close();
            }
        }
    }
}
