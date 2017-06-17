namespace BITOJ.Data
{
    using BITOJ.Data.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// 封装对本地题目库中一道题目的访问接口。
    /// </summary>
    public sealed class ProblemEntryHandle
    {
        private static readonly string ProblemConfigurationFileName = "config.json";
        private static readonly string ProblemDescriptionFileName = "description";
        private static readonly string ProblemInputDescriptionFileName = "input_description";
        private static readonly string ProblemOutputDescriptionFileName = "output_description";
        private static readonly string ProblemInputSampleFileName = "input_sample";
        private static readonly string ProblemOutputSampleFileName = "output_sample";
        private static readonly string ProblemHintFileName = "hint";
        private static readonly string ProblemSourceFileName = "source";
        private static readonly string ProblemResourcesDirectoryName = "Resources";

        private static readonly Dictionary<ProblemEntryParts, string> PartFileName;

        static ProblemEntryHandle()
        {
            // 初始化 PartFileName 。
            PartFileName = new Dictionary<ProblemEntryParts, string>();

            PartFileName.Add(ProblemEntryParts.Description, ProblemDescriptionFileName);
            PartFileName.Add(ProblemEntryParts.InputDescription, ProblemInputDescriptionFileName);
            PartFileName.Add(ProblemEntryParts.OutputDescription, ProblemOutputDescriptionFileName);
            PartFileName.Add(ProblemEntryParts.InputSample, ProblemInputSampleFileName);
            PartFileName.Add(ProblemEntryParts.OutputSample, ProblemOutputSampleFileName);
            PartFileName.Add(ProblemEntryParts.Hint, ProblemHintFileName);
            PartFileName.Add(ProblemEntryParts.Source, ProblemSourceFileName);
        }

        private string m_problemDirectory;
        private string m_resourceDirectory;
        private string m_configFileName;
        private ProblemConfigurationModel m_config;
        private Dictionary<ProblemEntryParts, string> m_parts;
        private bool m_dirty;

        /// <summary>
        /// 使用给定的题目目录初始化 ProblemEntryHandle 类的新实例。
        /// </summary>
        /// <param name="directory">目标题目的题目目录。</param>
        /// <exception cref="ArgumentNullException"/>
        internal ProblemEntryHandle(string directory)
        {
            m_problemDirectory = directory;
            m_resourceDirectory = string.Concat(directory, "\\", ProblemResourcesDirectoryName);

            m_configFileName = string.Concat(directory, "\\", ProblemConfigurationFileName);
            if (File.Exists(m_configFileName))
            {
                m_config = JsonConvert.DeserializeObject<ProblemConfigurationModel>(File.ReadAllText(m_configFileName));
            }
            else
            {
                // 该题目配置文件不存在。创建默认配置文件。
                m_config = new ProblemConfigurationModel();
                File.WriteAllText(m_configFileName, JsonConvert.SerializeObject(m_config));
            }

            m_parts = new Dictionary<ProblemEntryParts, string>();
            m_dirty = false;
        }

        /// <summary>
        /// 获取或设置题目的标题。
        /// </summary>
        public string Title
        {
            get { return m_config.Title; }
            set
            {
                m_config.Title = value;
                UpdateModifiedTime();
                m_dirty = true;
            }
        }

        /// <summary>
        /// 获取或设置题目的作者的用户名。
        /// </summary>
        public string[] Authors
        {
            get { return m_config.Authors; }
            set
            {
                m_config.Authors = value;
                UpdateModifiedTime();
                m_dirty = true;
            }
        }

        /// <summary>
        /// 将该题目的配置数据模型写入文件系统中。
        /// </summary>
        private void SaveConfigurationModel()
        {
            File.WriteAllText(m_configFileName, JsonConvert.SerializeObject(m_config));
        }

        /// <summary>
        /// 将当前题目的最后修改时间更新为调用该方法的时间。
        /// </summary>
        private void UpdateModifiedTime()
        {
            m_config.LastModifiedTime = DateTime.Now;
            m_dirty = true;
        }

        /// <summary>
        /// 获取题目描述中给定部分的文件名。
        /// </summary>
        /// <param name="part">要查询的题目描述逻辑部分。</param>
        /// <returns>给定部分的文件名。</returns>
        private string GetProblemPartFilename(ProblemEntryParts part)
        {
            return string.Concat(m_problemDirectory, "\\", PartFileName[part]);
        }

        /// <summary>
        /// 获取题目描述中给定部分的 HTML 表示。
        /// </summary>
        /// <param name="part">要查询的题目描述逻辑部分。</param>
        /// <returns>给定部分的 HTML 表示。</returns>
        public string GetProblemPart(ProblemEntryParts part)
        {
            if (m_parts.ContainsKey(part))
            {
                return m_parts[part];
            }

            // 缓存中未找到指定的逻辑部分。读取本地文件系统以查找。
            string filename = GetProblemPartFilename(part);
            if (File.Exists(filename))
            {
                // 将数据存入缓存中。
                string content = File.ReadAllText(filename);
                m_parts.Add(part, content);

                return content;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 设置题目描述中给定部分的 HTML 表示。
        /// </summary>
        /// <param name="part">要设置的题目描述逻辑部分。</param>
        /// <param name="html">给定部分的 HTML 表示。</param>
        /// <exception cref="ArgumentNullException"/>
        public void SetProblemPart(ProblemEntryParts part, string html)
        {
            if (html == null)
                throw new ArgumentNullException(nameof(html));

            if (m_parts.ContainsKey(part))
            {
                m_parts[part] = html;
            }
            else
            {
                m_parts.Add(part, html);
            }

            m_dirty = true;
            UpdateModifiedTime();
        }

        /// <summary>
        /// 将挂起的更改写入本地文件系统中。
        /// </summary>
        public void Save()
        {
            if (m_dirty)
            {
                foreach (KeyValuePair<ProblemEntryParts, string> item in m_parts)
                {
                    string filename = PartFileName[item.Key];
                    File.WriteAllText(filename, item.Value);
                }
                SaveConfigurationModel();

                m_dirty = false;
            }
        }
    }
}
