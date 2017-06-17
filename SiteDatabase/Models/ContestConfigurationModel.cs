namespace BITOJ.Data.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// 表示比赛配置数据模型。
    /// </summary>
    public sealed class ContestConfigurationModel
    {
        /// <summary>
        /// 获取或设置该场比赛的授权配置。
        /// </summary>
        [JsonProperty("authorization")]
        public ContestAuthorizationModel Authorization { get; set; }

        /// <summary>
        /// 获取或设置该场比赛的参与模式。
        /// </summary>
        [JsonProperty("participate_mode")]
        public ContestParticipationMode ParticipationMode { get; set; }

        /// <summary>
        /// 初始化 ContestConfigurationModel 类的新实例。
        /// </summary>
        [JsonConstructor]
        public ContestConfigurationModel()
        {
            Authorization = new ContestAuthorizationModel();
            ParticipationMode = ContestParticipationMode.Individual;
        }
    }
}
