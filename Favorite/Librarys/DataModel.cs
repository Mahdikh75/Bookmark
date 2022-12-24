using Favorite.Models;
using System.Collections.Generic;

namespace Favorite.Librarys
{
    public interface DataModel
    {
        void Add_Item(Items items);
        void Del_Item(int id_items);
        void Del_Cate_items(int id_cate);
        List<Items> ReadAll_Item();
        List<Items> ReadCate_Item(int Code_Cate);
        List<string> List_Items_Image();
        List<int> Read_ItemsTags(string tags);

        void Add_Cate(Categoury cate);
        void Del_Cate(int id_cate);
        List<Categoury> List_Cate();
        List<string> List_Cate_Image();

        void Add_Tag(Tags tags);
        void Del_Tag(int id_tags);
        List<Tags> Read_TagItems(Items item);
    }
}
