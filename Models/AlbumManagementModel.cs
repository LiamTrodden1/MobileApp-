using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AlbumApp.Models
{
    //define the database table with ID as primary key
    public class AlbumManagementModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } 

        public string albumTitle { get; set; }
        public int totalTracks { get; set; } 
        public string artistName { get; set; }
        public string releaseDate { get; set; }
        public string type { get; set; }
        public string albumImageURL { get; set; }
        public int count { get; set; } = 0;
    }
}

