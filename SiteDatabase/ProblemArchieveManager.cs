namespace BITOJ.Data
{
    using BITOJ.Common.Cache.Settings;
    using System;
    using System.IO;

    /// <summary>
    /// 为 BIT Online Judge 系统的本地题目库提供管理接口。
    /// </summary>
    public sealed class ProblemArchieveManager
    {
        private static readonly string ArchieveDirectorySettingName = "archive_directory";
        private static readonly string DefaultArchieveDirectory = @"\Archieve";

        private static ProblemArchieveManager ms_default;
        private static object ms_syncLock;

        /// <summary>
        /// 获取或设置当前 Application Domain 中的唯一 ProblemArchieveManager 对象。
        /// </summary>
        public static ProblemArchieveManager DefaultManager
        {
            get
            {
                if (ms_default == null)
                {
                    lock (ms_syncLock)
                    {
                        if (ms_default == null)
                        {
                            ms_default = new ProblemArchieveManager();
                        }
                    }
                }
                return ms_default;
            }
        }

        private string m_archieveDirectory;

        /// <summary>
        /// 初始化 ProblemArchieveManager 类的新实例。
        /// </summary>
        private ProblemArchieveManager()
        {
            try
            {
                m_archieveDirectory = new FileSystemSettingProvider().Get<string>(ArchieveDirectorySettingName);
            }
            catch (InvalidOperationException)
            {
                // 未配置 archieve_directory 设置项。使用默认目录初始化题库根目录。
                m_archieveDirectory = DefaultArchieveDirectory;
            } 

            // 检查本地题目库目录是否存在于文件系统中。
            if (!Directory.Exists(m_archieveDirectory))
            {
                Directory.CreateDirectory(m_archieveDirectory);
            }
        }

        /// <summary>
        /// 获取给定题目的题目目录。
        /// </summary>
        /// <param name="problemId">要查询题目的题目 ID 。</param>
        /// <returns>给定题目所对应的题目目录。</returns>
        /// <exception cref="ArgumentNullException"/>
        private string GetProblemDirectory(string problemId)
        {
            if (problemId == null)
                throw new ArgumentNullException(nameof(problemId));

            return string.Concat(m_archieveDirectory, "\\", problemId);
        }

        /// <summary>
        /// 获取一个字符串数组，其中包含了本地题库中所有题目的 ID 。
        /// </summary>
        /// <returns>一个字符串数组，其中包含了本地题库中所有题目的 ID 。</returns>
        public string[] GetProblems()
        {
            return Directory.GetDirectories(m_archieveDirectory);
        }   

        // TODO: 为 ProblemArchieveManager 添加分页查询支持。

        /// <summary>
        /// 在本地题目库中创建一道新的题目。
        /// </summary>
        /// <param name="problemId">要创建的题目的题目 ID。</param>
        /// <returns>新创建的题目的访问句柄。</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// 若将要创建的题目已经存在于本地题目库中，抛出 InvalidOperationException 异常。
        /// </remarks>
        public ProblemEntryHandle CreateProblem(string problemId)
        {
            if (problemId == null)
                throw new ArgumentNullException(nameof(problemId));

            string directory = GetProblemDirectory(problemId);
            if (Directory.Exists(directory))
                throw new InvalidOperationException("给定的题目 ID 已经存在于本地题目库中。");

            Directory.CreateDirectory(directory);
            return new ProblemEntryHandle(directory);
        }

        /// <summary>
        /// 获取对题库中给定题目的访问句柄对象。若给定的题目 ID 不在本地题库中，返回 null 。
        /// </summary>
        /// <param name="problemId">待查询的题目 ID。</param>
        /// <returns>与给定题目对应的访问句柄对象。</returns>
        /// <exception cref="ArgumentNullException"/>
        public ProblemEntryHandle GetProblemHandle(string problemId)
        {
            if (problemId == null)
                throw new ArgumentNullException(nameof(problemId));

            string directory = GetProblemDirectory(problemId);
            if (Directory.Exists(directory))
            {
                // 给定的题目存在。
                return new ProblemEntryHandle(directory);
            }
            else
            {
                // 给定的题目不存在。
                return null;
            }
        }

        /// <summary>
        /// 从本地题目库中移除给定的题目。
        /// </summary>
        /// <param name="problemId">要移除的题目 ID 。</param>
        /// <exception cref="ArgumentNullException"/>
        public void RemoveProblem(string problemId)
        {
            if (problemId == null)
                throw new ArgumentNullException(nameof(problemId));

            string directory = GetProblemDirectory(problemId);
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory);
            }
        }
    }
}
