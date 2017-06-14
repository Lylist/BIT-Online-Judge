namespace BITOJ.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 表示题目与题目类别的关系数据实体。
    /// </summary>
    [Table("Relations")]
    public sealed class ProblemCategoryRelationEntity
    {
        /// <summary>
        /// 获取或设置该实体对象在数据库中的主键值。
        /// </summary>
        /// <remarks>
        /// 注意：不应该在外部代码中手动修改此字段的值。
        /// </remarks>
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置题目的 ID。
        /// </summary>
        public string ProblemId { get; set; }

        /// <summary>
        /// 获取或设置题目的类别 ID。
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 初始化 ProblemCategoryRelationEntity 类的新实例。
        /// </summary>
        public ProblemCategoryRelationEntity()
        {
            Id = 0;
            ProblemId = string.Empty;
            CategoryId = 0;
        }
    }
}
