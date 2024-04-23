using System;
using System.Collections.Generic;

namespace PwSaveData.Interface
{
    public interface IPwSammlungRepository : IDisposable
    {
        List<PwSammlungRow> GetPwSammlungRowList();
    }
}