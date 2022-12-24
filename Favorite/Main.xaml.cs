using Favorite.Librarys;
using Favorite.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Linq;

namespace Favorite
{
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch
            {
            }
        }

        private void ItemCate(object sender, RoutedEventArgs e)
        {
            AddCate add = new AddCate();
            add.ShowDialog();
        }

        private void ItemWeb(object sender, RoutedEventArgs e)
        {
            AddWebsite web = new AddWebsite();
            web.ShowDialog();
        }

        private void Expert(object sender, RoutedEventArgs e)
        {
            string db = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Fav.db3");
            string res = System.AppDomain.CurrentDomain.BaseDirectory + "/Res/";

            string root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            File.Copy(db, Path.Combine(root, "Fav.db3"));

            string adderss_res = Path.Combine(root, "Res");
            if (!Directory.Exists(adderss_res))
                Directory.CreateDirectory(adderss_res);

            foreach (var item in Directory.GetFiles(res))
            {
                string name = Path.GetFileName(item);
                string ds = Path.Combine(adderss_res, name);
                File.Copy(item, ds);
            }
        }

        private void RemoveCate(object sender, RoutedEventArgs e)
        {
            var cate = (ListCate.SelectedItem as Categoury);
            if (cate != null)
            {
                var result = MessageBox.Show("Remove item ' " + cate.Header + " '", "Categoury", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    new Database().Del_Cate(cate.Code);
                    new Database().Del_Cate_items(cate.Code);
                    All(sender, e);
                }
            }
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            var item = (ListViewData.SelectedItem as ListData);
            if (item != null)
            {
                var result = MessageBox.Show("Remove item ' " + item.title + " '", "Items", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    new Database().Del_Item(item.id);
                    // del tag
                    Load_ListViewDataForCate();
                }
            }
        }

        private void ListCate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Load_ListViewDataForCate();
            }
            catch
            {
                All(sender, e);
            }
        }

        public void Load_ListViewDataForCate()
        {
            if (ListCate.SelectedItem != null)
            {
                var data = ListCate.SelectedItem as Categoury;
                int code = data.Code;

                List<ListData> datas = new List<ListData>();
                string tag = string.Empty;

                foreach (var item in new Database().ReadCate_Item(code))
                {
                    foreach (var tg in new Database().Read_TagItems(item))
                    {
                        tag += "#" + tg.tag;
                    }
                    datas.Add(new Models.ListData()
                    {
                        categoury = item.categoury,
                        date = item.date,
                        id = item.id,
                        image = item.image,
                        text = item.text,
                        title = item.title,
                        url = item.url,
                        tags = tag
                    });
                    tag = null;
                }
                ListViewData.ItemsSource = datas;
            }
        }

        public void All(object sender, RoutedEventArgs e)
        {
            ListCate.ItemsSource = new Database().List_Cate();

            List<ListData> datas = new List<ListData>();
            string tag = string.Empty;

            foreach (var item in new Database().ReadAll_Item())
            {
                foreach (var tg in new Database().Read_TagItems(item))
                {
                    tag += "#" + tg.tag;
                }
                datas.Add(new Models.ListData()
                {
                    categoury = item.categoury,
                    date = item.date,
                    id = item.id,
                    image = item.image,
                    text = item.text,
                    title = item.title,
                    url = item.url,
                    tags = tag
                });
                tag = null;
            }

            ListViewData.ItemsSource = datas.OrderByDescending(x => x.date);
        }

        private void ListViewData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var tsg = (ListData)ListViewData.SelectedItem;
            if (tsg != null)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = tsg.url,
                    UseShellExecute = true
                });
            }
        }

        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                switch (TypeSearch.SelectedIndex)
                {
                    case 0:
                        Show_Search(new Database().Read_ItemsTags(Search.Text));
                        break;
                    default:
                        break;
                }
            }
        }

        public void Show_Search(List<int> list_id)
        {
            List<ListData> datas = new List<ListData>();
            string tag = string.Empty;
            foreach (var id in list_id)
            {
                foreach (var item in new Database().ReadAll_Item(id))
                {
                    foreach (var tg in new Database().Read_TagItems(item))
                    {
                        tag += "#" + tg.tag;
                    }
                    datas.Add(new Models.ListData()
                    {
                        categoury = item.categoury,
                        date = item.date,
                        id = item.id,
                        image = item.image,
                        text = item.text,
                        title = item.title,
                        url = item.url,
                        tags = tag
                    });
                    tag = null;
                }
            }
            ListViewData.ItemsSource = datas;
        }
    }
}