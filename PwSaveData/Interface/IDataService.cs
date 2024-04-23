using PwSaveData.Reopsitory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PwSaveData.Interface
{
    public interface IDataService : IDisposable
    {
        bool DeleteMergeList(int kopfId);
        bool ConnectDB(out string returnMsg);
        void SetLogOnPassword(string pwVerschluesselt);
        string GetLogOnPassword();
        DataTable ExecuteQuery(string sql, out string returnMsg);
        List<PwSammlungRow> GetPwSammlungRowList();
    }
}
