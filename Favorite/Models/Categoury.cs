using SQLite;
namespace Favorite.Models
{
    public class Categoury
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Code { get; set; }

        public string Header { get; set; }

        public string Picture { get; set; }
    }
}