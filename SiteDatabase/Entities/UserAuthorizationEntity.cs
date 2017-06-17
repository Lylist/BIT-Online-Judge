namespace BITOJ.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 表示用户身份认证实体数据记录。
    /// </summary>
    [Table("Authorizations")]
    public sealed class UserAuthorizationEntity
    {
        /// <summary>
        /// 获取或设置用户的用户名。
        /// </summary>
        [Key]
        public string Username { get; set; }

        /// <summary>
        /// 获取或设置用户密码的 SHA512 哈希值。
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// 获取或设置用户所属的用户组。
        /// </summary>
        public UserGroup Group { get; set; }

        /// <summary>
        /// 初始化 UserAuthorizationEntity 类的新实例。
        /// </summary>
        public UserAuthorizationEntity()
        {
            Username = string.Empty;
            PasswordHash = new byte[0];
            Group = UserGroup.Guests;
        }
    }
}
