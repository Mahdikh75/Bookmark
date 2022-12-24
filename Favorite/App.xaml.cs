using Favorite.Librarys;
using System.IO;
using System.Linq;
using System.Windows;

namespace Favorite
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var files_cate = Directory.GetFiles(new AddCate().root, "*.*", SearchOption.AllDirectories).Where(x => x.Contains("Cate"));
            var files_items = Directory.GetFiles(new AddCate().root, "*.*", SearchOption.AllDirectories).Where(x => x.Contains("Img"));

            var images = new Database().List_Cate_Image();
            foreach (var item in files_cate.Except(images))
            {
                File.Delete(item);
            }

            var items = new Database().List_Items_Image();
            foreach (var item in files_items.Except(items))
            {
                File.Delete(item);
            }
        }
    }
}
