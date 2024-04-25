using System.Collections.Generic;
using System.IO;
using System.Reflection;
using gnLogger;
using PwSaveData.Interface;
using PwSaveData.Reopsitory;
using gnCommon;
using System;
using System.Drawing;
using System.Linq;
using SQLite;
using System.Data;
using System.Windows.Media.Imaging;
using PwSaveData.Entity;

namespace PwSaveData.Service
{
    public class DataService: IDataService
    {
        private static string databaseName = "datenbank.db";
        private static string folderPath = Path.Combine(Environment.CurrentDirectory, "datenbank");
        private string databasePath = Path.Combine(folderPath, databaseName);
        private readonly ILogger logProvider;
        private IPwSammlungRepository pwSammlungRepository;
        private IPwStartRepository pwsRepository;


        #region ctor

        public DataService()
        {
            ServiceLocator.Register<ILogger>(new Logger());

            logProvider = ServiceLocator.GetService<ILogger>();
            if (!ConnectDB(out string returnMsg))
            {
                logProvider.Log(returnMsg, LogLevel.ERROR);
                return;
            }

            pwsRepository = new PwStartRepository(databasePath);
            pwSammlungRepository = new PwSammlungRepository(databasePath);
            //artRowList = artRepository.BasisdatenLaden();
            var swp = new PwStartEntity(this);

        }


        public void Dispose()
        {
            //if(databaseProvider != null)
            //{
            //    databaseProvider.Dispose();
            //}
            pwsRepository = null;
        }


        #endregion

        #region Art



        #endregion

        public bool DeleteMergeList(int kopfId)
        {
            //if(schrittDatenRepository.DeleteDaten(kopfId))
            //{
            //    if(kopfRepository.DeleteKopf(kopfId))
            //    {
            //        return true;
            //    }
            //}

            return false;
        }

        #region Datenbank


        public bool ConnectDB(out string returnMsg)
        {
            returnMsg = string.Empty;
            try
            {
                using (var connection = new SQLiteConnection(databasePath))
                {
                    connection.CreateTable<PwStart>();
                    connection.CreateTable<PwSammlung>();
                    // ... und weitere
                }
            }
            catch (Exception e)
            {
                logProvider.Log($"Nix connected: {e.Message}", LogLevel.ERROR);
                Console.WriteLine(e.Message);
                return false;
            }
            
            return true;
        }

        public DataTable ExecuteQuery(string sql, out string returnMsg)
        {
            returnMsg = string.Empty;

            return new DataTable();
        }

        #endregion


        public bool SavePwSammlungRowItem(PwSammlungRow pwSammlungRowItem)
        {
            return false;//pwsRepository.SetPwSammlungRowItem(pwSammlungRowItem);
        }

        public void SetLogOnPassword(string pwVerschluesselt)
        {
            pwsRepository.SetLogOnPassword(pwVerschluesselt);
        }

        public string GetLogOnPassword()
        {
            return pwsRepository.GetLogOnPassword();
        }

        public List<PwSammlungRow> GetPwSammlungRowList()
        {
            return pwSammlungRepository.GetPwSammlungRowList();
        }

        public bool SetPwSammlungRowList(List<PwSammlungRow> liste)
        {
            return pwSammlungRepository.SetPwSammlungRowList(liste);
        }
    }
}
