namespace BITOJ.Data.Entities
{
    /// <summary>
    /// 表示用户组。
    /// </summary>
    public enum UserGroup : int
    {
        /// <summary>
        /// 访客用户组。
        /// </summary>
        Guests = int.MinValue,

        /// <summary>
        /// 标准用户组。
        /// </summary>
        Standard = 1,

        /// <summary>
        /// 内部成员用户组。
        /// </summary>
        Insiders = 2,

        /// <summary>
        /// 管理员用户组。
        /// </summary>
        Administrators = int.MaxValue,
    }
}