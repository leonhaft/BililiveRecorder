using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BililiveRecorder.WPF
{
    public class WorkDirService
    {
        private const string LAST_WORK_DIR_FILE = "lastworkdir";
        private const string NEW_WORK_DIR_FILE = "newworkdir";

        /// <summary>
        /// 获取最近使用工作目录
        /// </summary>
        /// <returns></returns>
        public string LastWorkDir()
        {
            if (File.Exists(LAST_WORK_DIR_FILE))
            {
                var dir = File.ReadAllText(LAST_WORK_DIR_FILE);
                if (Directory.Exists(dir))
                {
                    return dir;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 保存最近使用工作目录
        /// </summary>
        /// <param name="workDir"></param>
        public void WriteLastWorkDir(string workDir)
        {
            File.WriteAllText(LAST_WORK_DIR_FILE, workDir, Encoding.UTF8);
        }

        /// <summary>
        /// 保存新工作目录
        /// </summary>
        /// <param name="workDir"></param>
        public void WriteNewWorkDir(string workDir)
        {
            File.WriteAllText(NEW_WORK_DIR_FILE, workDir, Encoding.UTF8);
        }

        /// <summary>
        /// 获取新工作目录
        /// </summary>
        /// <returns></returns>
        public string NewWorkDir()
        {
            if (File.Exists(NEW_WORK_DIR_FILE))
            {
                var dir = File.ReadAllText(NEW_WORK_DIR_FILE);
                if (Directory.Exists(dir))
                {
                    return dir;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 删除新工作目录记录
        /// </summary>
        public void ClearNewWorkDir()
        {
            if (File.Exists(NEW_WORK_DIR_FILE))
            {
                File.Delete(NEW_WORK_DIR_FILE);
            }

        }

        /// <summary>
        /// 是否变更了工作目录
        /// </summary>
        /// <returns></returns>
        public bool IsChangedDir()
        {
            var last = LastWorkDir();
            var newDir = NewWorkDir();
            if (newDir == String.Empty)
            {
                return false;
            }

            return string.Compare(last, newDir, true) != 0;
        }
    }
}
