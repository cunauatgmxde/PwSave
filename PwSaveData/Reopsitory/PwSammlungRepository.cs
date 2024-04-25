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

        public bool SetPwSammlungRowList(List<PwSammlungRow> liste)
        {
            var toDelete = liste.Where(x => x.Deleted).ToList();
            var toAdd = liste.Where(x => x.Added).ToList();
            var toChange = liste.Where(x => x.Changed).ToList();
            using (var connection = new SQLiteConnection(databasePath))
            {
                //DELETE
                try
                {
                    foreach (var row in toDelete)
                    {
                        PwSammlung item = GetPwSammlungItem(row);
                        connection.Delete(item);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"DELETE: {e.Message}");
                    return false;
                }
                //INSERT
                try
                {
                    foreach (var row in toAdd)
                    {
                        PwSammlung item = GetPwSammlungItem(row);
                        connection.Insert(item);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"INSERT: {e.Message}");
                    return false;
                }
                //UPDATE
                try
                {
                    foreach (var row in toChange)
                    {
                        PwSammlung item = GetPwSammlungItem(row);
                        connection.Update(item);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"UPDATE: {e.Message}");
                    return false;
                }
            }
            

            return true;
        }

        private PwSammlung GetPwSammlungItem(PwSammlungRow row)
        {
            return new PwSammlung()
            {
                Id = row.Id,
                Anbieter = row.Anbieter,
                Benutzername = row.Benutzername,
                Passwort = row.Passwort,
                Kategorie = row.Kategorie,
                Beschreibung = row.Beschreibung
            };
        }
    }
}
