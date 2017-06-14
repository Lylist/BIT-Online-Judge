namespace BITOJ.Data.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// 表示用户信息数据模型。
    /// </summary>
    public sealed class UserProfileModel
    {
        /// <summary>
        /// 获取或设置用户名。
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// 获取或设置用户性别。
        /// </summary>
        [JsonProperty("sex")]
        public UserSex Sex { get; set; }

        /// <summary>
        /// 获取或设置用户头像图片路径。
        /// </summary>
        [JsonProperty("image")]
        public string ImagePath { get; set; }

        /// <summary>
        /// 获取或设置用户所属的组织名称。
        /// </summary>
        [JsonProperty("organization")]
        public string Organization { get; set; }

        /// <summary>
        /// 获取或设置用户提交统计信息模型。
        /// </summary>
        [JsonProperty("sub_stat")]
        public UserSubmissionStatisticsModel SubmissionStatistics { get; set; }

        /// <summary>
        /// 初始化 UserProfileModel 类的新实例。
        /// </summary>
        [JsonConstructor]
        public UserProfileModel()
        {
            Username = string.Empty;
            Sex = UserSex.Unknown;
            ImagePath = string.Empty;
            Organization = string.Empty;
            SubmissionStatistics = new UserSubmissionStatisticsModel();
        }
    }
}
