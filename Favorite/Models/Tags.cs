using SQLite;
namespace Favorite.Models
{
    public class Tags
    {
        [PrimaryKey]
        [AutoIncrement]
        public int id { get; set; }

        // id item
        public int item { get; set; }

        public string tag { get; set; }
    }
}
