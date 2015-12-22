using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryPhaseDiagramOperationLib
{
    /// <summary>
    /// 相图数据项
    /// </summary>
    public class BPDDataItem
    {
        //相图名称
        public string BPDName { get; set; }
        //相图图片名称
        public string ImagePath { get; set; }
        //相图文件创建日期
        public DateTime FileCreationTime { get; set; }
    }
}
