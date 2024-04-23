using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwSaveData
{
    public class PwSammlungRow
    {
        public int Id { get; set; }
        public string Anbieter { get; set; } = string.Empty;
        public string Benutzername { get; set; } = string.Empty;
        public string Passwort { get; set; } = string.Empty;
        public string Kategorie { get; set; } = string.Empty;
        public string Beschreibung { get; set; } = string.Empty;
        public bool Deleted { get; set; } = false;
        public bool Added { get; set; } = false;
        public bool Changed { get; set; } = false;
    }
}
