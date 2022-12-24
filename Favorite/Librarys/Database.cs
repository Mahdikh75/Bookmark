using Favorite.Models;
using SQLite;
using System.Collections.Generic;
using System.IO;

namespace Favorite.Librarys
{
    public class Database : DataModel
    {
        private SQLiteConnection sqlite;
        private readonly string db_sqlite = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Fav.db3");

        public Database()
        {
            sqlite = new SQLiteConnection(db_sqlite, true);
            sqlite.CreateTable<Items>();
            sqlite.CreateTable<Tags>();
            sqlite.CreateTable<Categoury>();
        }

        ~Database() => sqlite.Close();

        public void Add_Cate(Categoury cate) => sqlite.Insert(cate);

        public void Add_Item(Items items) => sqlite.Insert(items);

        public void Add_Tag(Tags tags) => sqlite.Insert(tags);

        public void Del_Cate(int id_cate) => sqlite.Delete<Categoury>(id_cate);

        public void Del_Item(int id_items) => sqlite.Delete<Items>(id_items);

        public void Del_Tag(int id_tags) => sqlite.Delete<Tags>(id_tags);

        public List<Items> ReadAll_Item()
        {
            return sqlite.Table<Items>().ToList();
        }

        public List<Items> ReadAll_Item(int id)
        {
            return sqlite.Query<Items>("select * from Items where id = " + id);
        }

        public List<string> List_Items_Image()
        {
            List<string> items = new List<string>();
            foreach (var item in ReadAll_Item())
            {
                items.Add(item.image);
            }
            return items;
        }

        public List<Items> ReadCate_Item(int Code_Cate)
        {
            return sqlite.Query<Items>("select * from Items where categoury =" + Code_Cate);
        }

        public List<Categoury> List_Cate()
        {
            return sqlite.Query<Categoury>("select * from Categoury");
        }

        public List<string> List_Cate_Image()
        {
            List<string> items = new List<string>();
            foreach (var item in List_Cate())
            {
                items.Add(item.Picture);
            }
            return items;
        }

        public List<Tags> Read_TagItems(Items item)
        {
            return sqlite.Query<Tags>("select tag from Tags where item =" + item.id);
        }

        public void Del_Cate_items(int id_cate)
        {
            var list = sqlite.Query<Items>("select * from Items where categoury =" + id_cate);
            foreach (var im in list)
            {
                sqlite.Delete<Items>(im.id);
            }
        }

        public List<int> Read_ItemsTags(string tags)
        {
            List<int> list_tag = new List<int>();
            try
            {
                foreach (var t in sqlite.Query<Tags>("select * from Tags"))
                {
                    if (t.tag == tags)
                    {
                        list_tag.Add(t.item);
                    }
                }
            }
            catch
            {
            }
            return list_tag;
        }
    }
}