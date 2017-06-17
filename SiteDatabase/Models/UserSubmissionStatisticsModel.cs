namespace BITOJ.Data.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// 表示用户的提交信息统计数据模型。
    /// </summary>
    public sealed class UserSubmissionStatisticsModel
    {
        /// <summary>
        /// 获取或设置总提交数目。
        /// </summary>
        [JsonProperty("total_sub")]
        public int TotalSubmissions { get; set; }

        /// <summary>
        /// 获取或设置 AC 提交数目。
        /// </summary>
        [JsonProperty("ac_sub")]
        public int AcceptedSubmissions { get; set; }

        /// <summary>
        /// 获取或设置一个集合对象，该对象中包含了当前用户所有已经尝试的题目 ID 。
        /// </summary>
        [JsonProperty("attmpted_prob")]
        public ICollection<string> AtteptedProblemId { get; set; }

        /// <summary>
        /// 获取或设置一个集合对象，该对象中包含了当前用户所有已经 AC 的题目 ID。
        /// </summary>
        [JsonProperty("ac_prob")]
        public ICollection<string> AcceptedProblemId { get; set; }

        /// <summary>
        /// 初始化 UserSubmissionStatisticsModel 类的新实例。
        /// </summary>
        [JsonConstructor]
        public UserSubmissionStatisticsModel()
        {
            TotalSubmissions = 0;
            AcceptedSubmissions = 0;
            AtteptedProblemId = new List<string>();
            AcceptedProblemId = new List<string>();
        }
    }
}
