using System;
using System.Collections.Generic;
using System.Linq;
using PwSaveData.Interface;
using gnCommon;
using SQLite;

namespace PwSaveData.Reopsitory
{
    public class PwStartRepository : IPwStartRepository
    {
        private string databasePath;
        private List<PwStart> pwstartList = new();

        public PwStartRepository(string path)
        {
            this.databasePath = path;
        }

        public void Dispose()
        {
            //
        }

        public string GetLogOnPassword()
        {
            PwStart item;
            var sql = "SELECT * FROM PwStart WHERE Id=1";
            using (var connection = new SQLiteConnection(databasePath))
            {
                 var list = connection.Table<PwStart>().ToList();
                 item = list.FirstOrDefault();
            }

            if (item == null)
                return string.Empty;

            return AESEncryption.Decrypt(item.Passwort);
        }

        public void SetLogOnPassword(string pwVerschluesselt)
        {
            var retCnt = -1;
            var item = new PwStart()
            {
                Id = 1,
                Passwort = pwVerschluesselt,
                LastLogin = DateTime.Now
            };
            using (var connection = new SQLiteConnection(databasePath))
            {
                retCnt = connection.Update(item);
            }

            Console.WriteLine($"Anzahl Rows: {retCnt}");
        }
    }
}
