namespace BITOJ.Data.Models
{
    using BITOJ.Data.Entities;
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// 表示一道题目的配置数据模型。
    /// </summary>
    internal sealed class ProblemConfigurationModel
    {
        /// <summary>
        /// 获取或设置题目的标题。
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置一个字符串数组，该数组包含了该题目所有作者的用户 ID 。
        /// </summary>
        [JsonProperty("authors")]
        public string[] Authors { get; set; }

        /// <summary>
        /// 获取或设置该题目的创建时间。
        /// </summary>
        [JsonProperty("creation_time")]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 获取或设置该题目最后一次被修改的时间。
        /// </summary>
        [JsonProperty("last_modified_time")]
        public DateTime LastModifiedTime { get; set; }

        /// <summary>
        /// 获取或设置访问该题目所需要的最低权限。
        /// </summary>
        [JsonProperty("group")]
        public UserGroup AuthorizationGroup { get; set; }

        /// <summary>
        /// 初始化 ProblemConfigurationModel 类的新实例。
        /// </summary>
        public ProblemConfigurationModel()
        {
            Title = string.Empty;
            Authors = new string[0];
            CreationTime = DateTime.Now;
            LastModifiedTime = DateTime.Now;
            AuthorizationGroup = UserGroup.Guests;
        }
    }
}
