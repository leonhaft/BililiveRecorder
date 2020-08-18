using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BililiveRecorder.WPF
{
    public enum WorkDirectoryDialogResult
    {
        /// <summary>
        /// 马上更新
        /// </summary>
        Immediately,
        /// <summary>
        /// 下次打开程序时更新
        /// </summary>
        NextRun,
        /// <summary>
        /// 取消操作
        /// </summary>
        Cancel
    }
}
