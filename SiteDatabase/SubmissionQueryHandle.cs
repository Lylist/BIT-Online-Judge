namespace BITOJ.Data
{
    using BITOJ.Data.Entities;
    using System;

    /// <summary>
    /// 封装查询用户记录的待查询数据。
    /// </summary>
    public sealed class SubmissionQueryHandle
    {
        /// <summary>
        /// 获取或设置要查询的提交 ID。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置要查询的用户提交所在的时间区间的起始时间点。
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 获取或设置要查询的用户提交所在的时间区间的结束时间点。
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 获取或设置要查询的用户提交的用户名。
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 获取或设置要查询的用户提交的题目 ID。
        /// </summary>
        public string ProblemId { get; set; }

        /// <summary>
        /// 获取或设置要查询的用户提交的提交语言或编译系统。
        /// </summary>
        public SubmissionLanguage Language { get; set; }

        /// <summary>
        /// 获取或设置要查询的用户提交的 Judge 结果。
        /// </summary>
        public SubmissionVerdict VerdictResult { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否使用 ID 属性值进行数据查询。
        /// </summary>
        public bool UseId { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否使用时间区间进行数据查询。
        /// </summary>
        public bool UseTimePeriod { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否使用用户名进行查询。
        /// </summary>
        public bool UseUsername { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否使用题目 ID 进行查询。
        /// </summary>
        public bool UseProblemId { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否使用提交语言或编译系统进行查询。
        /// </summary>
        public bool UseLanguage { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否使用 Judge 结果进行查询。
        /// </summary>
        public bool UseVerdictResult { get; set; }

        /// <summary>
        /// 初始化 SubmissionQueryHandle 类的新实例。
        /// </summary>
        public SubmissionQueryHandle()
        {
            Id = 0;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MaxValue;
            Username = string.Empty;
            ProblemId = string.Empty;
            Language = SubmissionLanguage.GnuCPlusPlus;
            VerdictResult = SubmissionVerdict.Unknown;

            UseId = false;
            UseTimePeriod = false;
            UseUsername = false;
            UseProblemId = false;
            UseLanguage = false;
            UseVerdictResult = false;
        }
    }
}
