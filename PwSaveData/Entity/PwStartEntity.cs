using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using gnCommon;
using PwSaveData.Interface;

namespace PwSaveData.Entity
{
    public class PwStartEntity
    {
        private readonly IDataService databaseProvider;
        private string returnMsg = string.Empty;

        public PwStartEntity(IDataService databaseService)
        {
            this.databaseProvider = databaseService;
            //SetPassword("HmQ7w123");
        }

        public void SetPassword(string value)
        {
            var pwVerschluesselt = AESEncryption.Encrypt(value);
            databaseProvider.SetLogOnPassword(pwVerschluesselt);
            
        }

        public string GetPassword()
        {
            var pw = string.Empty;
            var list = new List<PwStart>();
            var sql = "SELECT * FROM PwStart";
            var dt = databaseProvider.ExecuteQuery(sql, out returnMsg);
            if (dt == null || dt.Rows == null || dt.Rows.Count <= 0)
                return pw;
            
            try
            {
                foreach(DataRow row in dt.Rows)
                {
                    list.Add(new PwStart
                    {
                        Passwort = Helper.GetIt<string>(row, "Passwort", string.Empty),
                        LastLogin = Helper.GetIt<DateTime>(row, "Kuenstler", new DateTime(1970, 1, 1))
                    });
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (!list.Any())
                return pw;

            var eintrag = list.First();
            if (eintrag == null)
                return pw;

            try
            {
                return AESEncryption.Decrypt(eintrag.Passwort);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("Fehler beim entschlüsseln des Passworts.");
            }

            return pw;
        }
    }
}
