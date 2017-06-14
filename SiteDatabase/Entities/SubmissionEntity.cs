namespace BITOJ.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 表示一个提交的实体对象。
    /// </summary>
    [Table("Submissions")]
    public sealed class SubmissionEntity
    {
        /// <summary>
        /// 获取或设置提交 ID 。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 在一个 VJ 提交中，获取或设置在第三方 OJ 系统中的提交 ID。若当前提交不是 VJ 提交，该属性值为 0。
        /// </summary>
        public int RemoteSubmissionId { get; set; }

        /// <summary>
        /// 获取或设置当前提交的创建时间。
        /// </summary>
        public DateTime CreationTimestamp { get; set; }

        /// <summary>
        /// 获取或设置当前提交的 Judge 时间。
        /// </summary>
        public DateTime? VerdictTimestamp { get; set; }

        /// <summary>
        /// 获取或设置创建该提交的用户名称。
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 获取或设置该提交所对应的题目 ID。
        /// </summary>
        public string ProblemId { get; set; }

        /// <summary>
        /// 获取或设置该提交所对应的代码文件名。
        /// </summary>
        public string CodeFilename { get; set; }

        /// <summary>
        /// 获取或设置该提交所使用的程序设计语言或编译系统。
        /// </summary>
        public SubmissionLanguage Language { get; set; }

        /// <summary>
        /// 获取或设置该提交的 Judge 状态。
        /// </summary>
        public SubmissionVerdictStatus VerdictStatus { get; set; }

        /// <summary>
        /// 获取或设置该提交的 Judge 结果。
        /// </summary>
        public SubmissionVerdict VerdictResult { get; set; }

        /// <summary>
        /// 初始化 SubmissionEntity 类的新实例。
        /// </summary>
        public SubmissionEntity()
        {
            Id = 0;
            RemoteSubmissionId = 0;
            CreationTimestamp = DateTime.Now;
            VerdictTimestamp = null;
            Username = string.Empty;
            ProblemId = string.Empty;
            CodeFilename = string.Empty;
            Language = SubmissionLanguage.GnuCPlusPlus;
            VerdictStatus = SubmissionVerdictStatus.Pending;
            VerdictResult = SubmissionVerdict.Unknown;
        }
    }
}
