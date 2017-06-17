namespace BITOJ.Data.Models
{
    /// <summary>
    /// 表示比赛的授权模式。
    /// </summary>
    public enum ContestAuthorizationMode : int
    {
        /// <summary>
        /// 私有级别比赛。仅允许处在白名单中的用户或队伍参与比赛。
        /// </summary>
        Private = 0,

        /// <summary>
        /// 保护级别比赛。仅允许正确输入指定密码的用户或队伍参与比赛。
        /// </summary>
        Protected = 1,

        /// <summary>
        /// 公共级别比赛。任何具有 Standard 或更高授权级别的用户或队伍均能参加比赛。
        /// </summary>
        Public = 2,
    }
}
