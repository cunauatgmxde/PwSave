using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PwSaveData.Interface;
using SQLite;

namespace PwSaveData.Reopsitory
{
    public class PwSammlungRepository : IPwSammlungRepository
    {
        private string databasePath;
        private List<PwSammlungRow> pwSammlungRowList = new List<PwSammlungRow>();

        public PwSammlungRepository(string path)
        {
            this.databasePath = path;
        }

        public void Dispose()
        {
            //
        }

        public List<PwSammlungRow> GetPwSammlungRowList()
        {
            if (!pwSammlungRowList.Any())
            {
                var list = new List<PwSammlung>();
                using (var connection = new SQLiteConnection(databasePath))
                {
                    list = connection.Table<PwSammlung>().ToList();
                }

                if (list != null)
                {
                    foreach (var listItem in list)
                    {
                        pwSammlungRowList.Add(new PwSammlungRow()
                        {
                            Id = listItem.Id,
                            Anbieter = listItem.Anbieter,
                            Benutzername = listItem.Benutzername,
                            Passwort = listItem.Passwort,
                            Kategorie = listItem.Kategorie,
                            Beschreibung = listItem.Beschreibung,
                            Added = false,
                            Changed = false,
                            Deleted = false
                        });
                    }
                }
            }

            return pwSammlungRowList;
        }
    }
}
