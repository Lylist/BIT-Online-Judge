namespace BITOJ.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 表示竞赛场次实体对象。
    /// </summary>
    [Table("Contests")]
    public sealed class ContestEntity
    {
        /// <summary>
        /// 获取或设置该实体对象在数据库系统中的主键。
        /// </summary>
        /// <remarks>
        /// 不应该在外部代码中手动修改该属性的值。
        /// </remarks>
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置该场比赛的标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置该场比赛的创建时间。
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 获取或设置该场比赛的起始时间。
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 获取或设置该场比赛的结束时间。
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 获取或设置该场比赛的创建者。
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 获取或设置存储该场比赛详细配置数据的本地文件系统目录。
        /// </summary>
        public string ContestDirectory { get; set; }

        /// <summary>
        /// 初始化 ContestEntity 类的新实例。
        /// </summary>
        public ContestEntity()
        {
            Id = 0;
            Title = string.Empty;
            CreationTime = DateTime.Now;
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            Creator = string.Empty;
            ContestDirectory = string.Empty;
        }
    }
}
