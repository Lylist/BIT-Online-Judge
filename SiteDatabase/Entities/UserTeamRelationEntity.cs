namespace BITOJ.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 表示用户 - 队伍关系的实体对象。
    /// </summary>
    [Table("UserTeams")]
    public sealed class UserTeamRelationEntity
    {
        /// <summary>
        /// 获取或设置该实体对象在数据库系统中的主键。
        /// </summary>
        /// <remarks>
        /// 注意：不应该在外部代码中手动修改该属性的值。
        /// </remarks>
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置队伍的 ID 。
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// 获取或设置用户名称。
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 初始化 UserTeamRelationEntity 类的新实例。
        /// </summary>
        public UserTeamRelationEntity()
        {
            Id = 0;
            TeamId = 0;
            Username = string.Empty;
        }
    }
}
