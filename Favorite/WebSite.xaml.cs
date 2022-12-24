using Favorite.Librarys;
using Favorite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Net;

namespace Favorite
{
    public partial class AddWebsite : Window
    {
        Dictionary<int, string> KeyItem = new Dictionary<int, string>(new Database().List_Cate().Count);

        public AddWebsite() { InitializeComponent(); }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in new Database().List_Cate())
            {
                KeyItem.Add(item.Code, item.Header);
            }

            TypeCate.ItemsSource = KeyItem.Values.ToList();

            try
            {
                foreach (var item in Application.Current.Windows)
                {
                    if (item.GetType() == typeof(Main))
                    {
                        var cate = (item as Main).ListCate.SelectedItem;
                        if ((((Categoury)cate).Header) != null)
                        {
                            TypeCate.SelectedItem = ((Categoury)cate).Header;
                        }
                    }
                }
            }
            catch 
            {
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            int code_categoury = KeyItem.Where(x => x.Value == TypeCate.SelectedItem.ToString()).Select(x => x.Key).FirstOrDefault();
            string[] tags = Tag.Text.Split("#");
            string url = AdderssUrl.Text.Trim();

            try
            {
                string image_ads = url + "/favicon.ico";
                var web = new WebClient();
                var name = new Random().Next(1000000, 10000000);
                string adderss = System.AppDomain.CurrentDomain.BaseDirectory + "/Res/" + ("Img" + name + ".png");
                web.DownloadFile(image_ads, adderss);

                Database db = new Database();
                Items items = new Items() { categoury = code_categoury, date = DateTime.Now, url = url, title = TB_Title.Text, text = TB_Text.Text, image = adderss };
                db.Add_Item(items);

                // ad tags
                for (int i = 0; i < tags.Length; i++)
                {
                    if (tags[i].Length > 0)
                    {
                        Tags tg = new Tags() { tag = tags[i], item = items.id };
                        db.Add_Tag(tg);
                    }
                }
                Reload_Win_Main(sender, e);
                Close();
            }
            catch 
            {
            }
        }

        public void Reload_Win_Main(object sender, RoutedEventArgs e)
        {
            foreach (var item in Application.Current.Windows)
            {
                if (item.GetType() == typeof(Main))
                {
                    (item as Main).All(sender, e);
                }
            }
        }
    }
}
