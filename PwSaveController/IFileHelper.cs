using System;
using System.Collections.Generic;
using System.Text;

namespace PwSaveController
{
    public interface IFileHelper: IDisposable
    {
        List<string> GetRowsFromFile(string filePath);
        bool WriteToFile(string filePath, List<string> lines);
    }
}
