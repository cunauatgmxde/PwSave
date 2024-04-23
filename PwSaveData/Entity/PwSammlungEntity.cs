using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PwSaveData.Interface;

namespace PwSaveData.Entity
{
    public class PwSammlungEntity
    {
        private readonly IDataService databaseProvider;
        private string returnMsg = string.Empty;

        public PwSammlungEntity(IDataService databaseService)
        {
            this.databaseProvider = databaseService;
        }
    }
}
