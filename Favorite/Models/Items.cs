using System;
using SQLite;
namespace Favorite.Models
{
    public class Items
    {
        [PrimaryKey]
        [AutoIncrement]
        public int id { get; set; }

        public string image { get; set; }

        public string title { get; set; }

        public string text { get; set; }

        public string url { get; set; }

        public DateTime date { get; set; }

        public int categoury { get; set; }
    }
}
