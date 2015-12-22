using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryPhaseDiagramOperationLib
{
    public interface IBPDOperation
    {
        List<BPDDataItem> GetAllData();
        List<BPDDataItem> GetData(string elementA, string elementB);
    }
}
