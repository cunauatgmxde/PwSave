using System;
using System.Collections.Generic;

namespace PwSaveData.Interface
{
    public interface IPwStartRepository : IDisposable
    {
        void SetLogOnPassword(string pwVerschluesselt);
        string GetLogOnPassword();
        //List<PwSammlungRow> GetPwSammlungRowList();
    }
}