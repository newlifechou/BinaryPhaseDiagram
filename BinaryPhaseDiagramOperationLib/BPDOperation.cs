using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BinaryPhaseDiagramOperationLib
{
    public class BPDOperation : IBPDOperation
    {
        public BPDOperation(string imageFileFolderName)
        {
            this.ImageFileFolder = Path.Combine(Environment.CurrentDirectory, imageFileFolderName);
        }
        /// <summary>
        ///存放图片文件的文件夹名称
        ///只准外部读不允许写，只能通过构造函数的方式写入
        /// </summary>
        public string ImageFileFolder
        {
            get; private set;
        }

        public List<BPDDataItem> GetAllData()
        {
            List<BPDDataItem> list = GetBPDLists("*.jpg");
            return list;
        }

        public List<BPDDataItem> GetData(string elementA, string elementB)
        {
            if (string.IsNullOrEmpty(elementA) && string.IsNullOrEmpty(elementB))
            {
                return GetBPDLists("*.jpg");
            }
            //正序查询
            string searchStr = "*" + elementA + "*-*" + elementB + "*.jpg";
            List<BPDDataItem> listAll = GetBPDLists(searchStr);
            //逆序查询
            searchStr = "*" + elementB + "*-*" + elementA + "*.jpg";
            List<BPDDataItem> listLeft = GetBPDLists(searchStr);
            listAll.AddRange(listLeft);
            return listAll;
        }

        private List<BPDDataItem> GetBPDLists(string searchStr)
        {
            if (!Directory.Exists(ImageFileFolder))
            {
                throw new DirectoryNotFoundException();
            }

            //List<BPDDataItem> results = new List<BPDDataItem>();
            DirectoryInfo dir = new DirectoryInfo(ImageFileFolder);

            //使用linq来简化写法
            var allFiles = dir.GetFiles(searchStr, SearchOption.AllDirectories);
            var query = from file in allFiles
                        select new BPDDataItem() { BPDName = file.Name, FileCreationTime = file.LastWriteTime };

            var results = query.ToList<BPDDataItem>();
            //foreach (var file in dir.GetFiles(searchStr, SearchOption.AllDirectories))
            //{
            //    BPDDataItem bpd = new BPDDataItem()
            //    {
            //        BPDName = file.Name,
            //        FileCreationTime = file.LastWriteTime
            //    };
            //    results.Add(bpd);
            //}
            return results;
        }
    }
}
