using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PwSaveData
{
    public class PwStart
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Passwort { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
