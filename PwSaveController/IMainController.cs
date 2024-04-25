using PwSaveData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using gnCommon;
using System.Windows.Media.Imaging;

namespace PwSaveController
{
    public interface IMainController : IDisposable
    {
        List<string> GetRowsFromFile(string fileName, out string returnMsg);
        string GetWorkDir();
        List<PwSammlungRow> GetPwSammlungRowList();
        IMessageService GetMessageService();
        void SetEintragEdited(bool value);
        bool GetEintragEdited();
        void Log(string nachricht, LogLevel level = LogLevel.DEFAULT);
        void Log(Exception ex);
        void SavePwSammlungRow(PwSammlungRow pwSammlungRowItem);
        bool SavePwSammlungRowList(List<PwSammlungRow> liste);
        bool ChkPassword(string value);
        PwSammlungRow GetNewRowItem();
        void MakeRowBackup(PwSammlungRow item);
        void ResetRowFromBackup(PwSammlungRow item);
        
    }
}
