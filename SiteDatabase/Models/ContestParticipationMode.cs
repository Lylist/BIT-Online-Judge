namespace BITOJ.Data.Models
{
    using System;

    /// <summary>
    /// 表示比赛的参与模式。
    /// </summary>
    [Flags]
    public enum ContestParticipationMode : int
    {
        /// <summary>
        /// 仅允许个人用户参与比赛。
        /// </summary>
        Individual = 0x00000001,

        /// <summary>
        /// 仅允许队伍参与比赛。
        /// </summary>
        Teamwork = 0x00000002,

        /// <summary>
        /// 允许个人用户与队伍参加比赛。
        /// </summary>
        IndividualAndTeamwork = Individual | Teamwork,
    }
}
