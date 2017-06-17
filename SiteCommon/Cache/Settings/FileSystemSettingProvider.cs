namespace BITOJ.Common.Cache.Settings
{
    using Newtonsoft.Json;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// 为应用程序的可持久化设置项提供管理访问。
    /// </summary>
    public sealed class FileSystemSettingProvider : ISettingProvider
    {
        private sealed class SettingItemInfo
        {
            public object Value { get; set; }

            public Type ValueType { get; set; }

            public SettingItemInfo()
            {
                Value = null;
                ValueType = null;
            }

            public SettingItemInfo(object value, Type valueType)
            {
                Value = value;
                ValueType = valueType;
            }
        }

        private static readonly string SettingsDirectory = @".\Settings";
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private static string MakeFilename(string name)
        {
            return string.Concat(SettingsDirectory, "\\", name, ".json");
        }

        private Dictionary<string, SettingItemInfo> m_settingsCache;
        private bool m_disposed;
        private bool m_dirty;

        /// <summary>
        /// 初始化 FileSystemSettingProvider 类的新实例。
        /// </summary>
        public FileSystemSettingProvider()
        {
            m_settingsCache = new Dictionary<string, SettingItemInfo>();
            m_disposed = false;
            m_dirty = false;

            // 检查底层文件系统是否包含设置集目录。
            if (!Directory.Exists(SettingsDirectory))
            {
                // 写入日志信息。
                Log.Warn("Settings directory not found. Creating settings directory.");
                Directory.CreateDirectory(SettingsDirectory);
            }
        }

        ~FileSystemSettingProvider()
        {
            Dispose();
        }

        /// <summary>
        /// 强制将指定名称的设置项从底层文件系统迁移到设置集缓存中。
        /// </summary>
        /// <typeparam name="T">设置项内容的类型。</typeparam>
        /// <param name="name">要加载的设置项名称。</param>
        /// <returns>一个 bool 值，该值指示加载操作是否成功。</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <remarks>
        /// 如果指定的设置项已经处于设置集缓存中，设置集缓存中的数据将被覆盖。
        /// </remarks>
        private bool Load<T>(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            string filename = MakeFilename(name);
            if (!File.Exists(filename))
            {
                // 底层文件系统不存在目标文件。
                return false;
            }

            // 尝试读取目标文件内容。
            string content = string.Empty;
            try
            {
                content = File.ReadAllText(filename);
            }
            catch
            {
                // 写入日志信息。
                Log.Error("Unable to read setting file: \"{0}\".", filename);

                return false;
            }

            // 将目标文件内容反序列化为对象数据。
            object obj = null;
            try
            {
                obj = JsonConvert.DeserializeObject(content, typeof(T));
            }
            catch
            {
                // 写入日志信息。
                Log.Error("Unable to convert JSON to object data. Setting file: \"{0}\"", filename);

                return false;
            }

            // 将设置项内容写入缓存中。
            SettingItemInfo item = new SettingItemInfo(obj, typeof(T));
            if (m_settingsCache.ContainsKey(name))
            {
                // 指定设置项已经处于设置集缓存中。执行覆盖。
                m_settingsCache[name] = item;
            }
            else
            {
                m_settingsCache.Add(name, item);
            }

            return true;
        }

        /// <summary>
        /// 将给定的应用程序设置项添加到当前的设置项管理中。
        /// </summary>
        /// <typeparam name="T">设置项内容的类型。</typeparam>
        /// <param name="name">该设置项的名称。</param>
        /// <param name="value">该设置项的内容。</param>
        /// <remarks>
        /// 由于设置项实例对象在写入底层文件系统时会使用 JSON 编码，故请确保该对象能被 JSON 序列化器解析。
        /// </remarks>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        public void Add<T>(string name, T value)
        {
            if (m_disposed)
                throw new ObjectDisposedException("FileSystemSettingProvider");
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (Contains(name))
                throw new InvalidOperationException("给定的设置项名称重复。");

            m_settingsCache.Add(name, new SettingItemInfo(value, typeof(T)));
            m_dirty = true;
        }

        /// <summary>
        /// 从当前的设置集中移除指定名称的设置项。
        /// </summary>
        /// <param name="name">要移除的设置项名称。</param>
        public bool Remove(string name)
        {
            if (m_disposed)
                throw new ObjectDisposedException("FileSystemSettingProvider");

            if (name == null)
            {
                return false;
            }
            string filename = MakeFilename(name);
            if (File.Exists(filename))
            {
                // 移除本地文件系统的设置文件。
                try
                {
                    File.Delete(filename);
                }
                catch
                {
                    return false;
                }
            }

            // 移除处于设置集缓存中的内容。
            if (m_settingsCache.ContainsKey(name))
            {
                return m_settingsCache.Remove(name);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检查给定的设置项是否在当前的设置项集中。
        /// </summary>
        /// <param name="name">待检查的设置项名称。</param>
        /// <returns>
        /// 返回一个 bool 值，该值指示给定的设置项是否在当前的设置集中。
        /// </returns>
        /// <exception cref="ObjectDisposedException"/>
        public bool Contains(string name)
        {
            if (m_disposed)
                throw new ObjectDisposedException("FileSystemSettingProvider");

            if (name == null)
            {
                return false;
            }

            // 检查设置集缓存。
            if (m_settingsCache.ContainsKey(name))
            {
                return true;
            }

            // 检查底层文件系统。
            return File.Exists(MakeFilename(name));
        }

        /// <summary>
        /// 获取指定名称的设置项。
        /// </summary>
        /// <typeparam name="T">设置项内容的类型。</typeparam>
        /// <param name="name">要获取的设置项的名称。</param>
        /// <returns>获取到的设置项内容。</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="IOException"/>
        public T Get<T>(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            // 检查设置集缓存。
            if (!m_settingsCache.ContainsKey(name))
            {
                // 读取底层文件系统数据。
                string filename = MakeFilename(name);
                if (!File.Exists(filename))
                {
                    throw new InvalidOperationException("要获取的设置项不存在于当前的设置集中。");
                }
                if (!Load<T>(name))
                {
                    throw new IOException("访问底层文件系统时出现错误。");
                }
            }

            return (T)m_settingsCache[name].Value;
        }

        /// <summary>
        /// 将指定名称的设置项修改为指定的内容。
        /// </summary>
        /// <typeparam name="T">设置项内容的类型。</typeparam>
        /// <param name="name">欲修改的设置项的名称。</param>
        /// <param name="value">设置项的新内容。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="IOException"/>
        public void Set<T>(string name, T value)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (!m_settingsCache.ContainsKey(name))
            {
                // 检查底层文件系统。
                string filename = MakeFilename(name);
                if (!File.Exists(filename))
                {
                    throw new InvalidOperationException("指定的设置项不存在于当前的设置集中。");
                }
                if (!Load<T>(name))
                {
                    throw new IOException("访问底层文件系统时出现错误。");
                }
            }

            // 更新缓存数据。
            SettingItemInfo info = m_settingsCache[name];
            info.Value = value;
            info.ValueType = typeof(T);

            m_dirty = true;
        }
        
        /// <summary>
        /// 将所有更改写入底层文件系统以反映更新。
        /// </summary>
        /// <exception cref="ObjectDisposedException"/>
        public void Flush()
        {
            if (m_disposed)
                throw new ObjectDisposedException("FileSystemSettingProvider");

            if (!m_dirty)
            {
                return;
            }

            // 将设置集缓存中的内容写入底层文件系统。
            foreach (KeyValuePair<string, SettingItemInfo> item in m_settingsCache)
            {
                string json = JsonConvert.SerializeObject(item.Value.Value);
                string filename = MakeFilename(item.Key);
                try
                {
                    File.WriteAllText(filename, json);
                }
                catch
                {
                    // 写入日志信息。
                    Log.Error("Unable to flush setting item: \"{0}\" from cache to local file system.",
                        item.Key);
                }
            }

            m_dirty = false;
        }

        /// <summary>
        /// 释放该对象占有的所有资源。
        /// </summary>
        public void Dispose()
        {
            if (!m_disposed && m_dirty)
            {
                Flush();
            }
            m_disposed = true;
        }
    }
}
