namespace BITOJ.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 表示题目分类类别对象实体数据。
    /// </summary>
    [Table("ProblemCategories")]
    public sealed class ProblemCategoryEntity
    {
        /// <summary>
        /// 获取或设置该记录在数据库中的主键值。
        /// </summary>
        /// <remarks>
        /// 注意：该字段不应该被外部代码手动修改。
        /// </remarks>
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置该类别的名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置该类别的父类别主键。如果该类别没有父类别，该值为 -1。
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 初始化 ProblemCategoryEntity 类的新实例。
        /// </summary>
        public ProblemCategoryEntity()
        {
            Id = 0;
            Name = string.Empty;
            ParentId = -1;
        }
    }
}
