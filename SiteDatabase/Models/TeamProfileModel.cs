namespace BITOJ.Data.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// 表示队伍信息数据模型。
    /// </summary>
    public sealed class TeamProfileModel
    {
        /// <summary>
        /// 获取或设置队伍的名称。
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置队伍成员的用户名。
        /// </summary>
        [JsonProperty("members")]
        public ICollection<string> Members { get; set; }

        /// <summary>
        /// 初始化 TeamProfileModel 类的新实例。
        /// </summary>
        public TeamProfileModel()
        {
            Name = string.Empty;
            Members = new List<string>();
        }
    }
}
