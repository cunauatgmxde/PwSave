using System;
using System.Collections.Generic;
using System.Linq;
using PwSaveData.Service;
using gnLogger;
using PwSaveData.Interface;
using gnCommon;
using PwSaveData;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace PwSaveController
{
    public class MainController : IMainController
    {
        
        private IDataService dataProvider;
        private ILogger logProvider;
        private IMessageService messageProvider;
        private bool eintragEdited = false;


        public MainController(IMessageService messageService)
        {
            this.messageProvider = messageService;
            ServiceLocator.Register<ILogger>(new Logger());
            ServiceLocator.Register<IDataService>(new DataService());
            logProvider = ServiceLocator.GetService<ILogger>();
            dataProvider = ServiceLocator.GetService<IDataService>();
            // TEST: Verschlüsselung
            //var original = "Das ist der Satz der verschlüsselt werden soll.";
            //var verschl = string.Empty;
            //var entschl = string.Empty;
            //Console.WriteLine($"Original string: {original}");
            //verschl = AESEncryption.Encrypt(original);
            //Console.WriteLine($"Verschlüsselter string: {verschl}");
            //entschl = AESEncryption.Decrypt(verschl);
            //Console.WriteLine($"Entschlüsselter string: {entschl}");
        }

        public void Dispose()
        {
            //
        }

        public IMessageService GetMessageService()
        {
            return messageProvider;
        }

        public string GetWorkDir()
        {
            return Helper.GetPfadZurAnwendung();
        }

        public bool ChkPassword(string value)
        {
            #if DEBUG
            return true; 
            #else
            value.Equals(dataProvider.GetLogOnPassword());
            #endif
        }

        public List<string> GetRowsFromFile(string fileName, out string returnMsg)
        {
            returnMsg = $"Datei ({fileName}) eingelesen.";
            var dateiInhalt = new List<string>();
            using (IFileHelper fh = new FileHelper())
            {
                dateiInhalt = fh.GetRowsFromFile(fileName);
            }
            if (!dateiInhalt.Any())
                returnMsg += "     ... ohne Inhalt.";

            return dateiInhalt;
        }

        public bool GetEintragEdited()
        {
            return this.eintragEdited;
        }

        public void SetEintragEdited(bool value)
        {
            this.eintragEdited = value;
        }

        public List<PwSammlungRow> GetPwSammlungRowList()
        {
            return dataProvider.GetPwSammlungRowList();
        }


        public void Log(string nachricht, LogLevel level = LogLevel.DEFAULT)
        {
            logProvider.Log(nachricht, level);
        }

        public void Log(Exception ex)
        {
            Log(ex.Message, LogLevel.ERROR);
        }

        public void SavePwSammlungRow(PwSammlungRow pwSammlungRowItem)
        {
            throw new NotImplementedException();
        }

        public PwSammlungRow GetNewRowItem()
        {
            return new PwSammlungRow()
            {
                Id = -1,
                Anbieter = string.Empty,
                Benutzername = string.Empty,
                Passwort = string.Empty,
                Kategorie = string.Empty,
                Beschreibung = string.Empty,
                Added = true,
                Changed = false,
                Deleted = false
            };
        }
    }
}
