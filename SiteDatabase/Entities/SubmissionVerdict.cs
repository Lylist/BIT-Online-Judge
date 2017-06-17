namespace BITOJ.Data.Entities
{
    /// <summary>
    /// 表示一个提交的 Judge 结果。
    /// </summary>
    public enum SubmissionVerdict : int
    {
        /// <summary>
        /// 未知的 Judge 结果。
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 用户提交成功通过所有测试数据。
        /// </summary>
        Accepted = 1,

        /// <summary>
        /// 用户提交在至少一个测试数据上产生错误结果。
        /// </summary>
        WrongAnswer = 2,

        /// <summary>
        /// 用户提交在至少一个测试数据上产生运行时错误。
        /// </summary>
        RuntimeError = 3,

        /// <summary>
        /// 用户提交在至少一个测试数据上运行时间超过阈值。
        /// </summary>
        TimeLimitExceeded = 4,

        /// <summary>
        /// 用户提交在至少一个测试数据上使用了超过阈值的峰值内存资源。
        /// </summary>
        MemoryLimitExceeded = 5,

        /// <summary>
        /// 用户提交在至少一个测试数据上的输出超出了最长输出长度限制。
        /// </summary>
        OutputLimitExceeded = 6,

        /// <summary>
        /// 用户提交在至少一个测试数据上的输出不符合格式要求。
        /// </summary>
        RepresentationError = 7,

        /// <summary>
        /// 用户提交无法通过编译。
        /// </summary>
        CompilationError = 8,

        /// <summary>
        /// Judge 系统发生内部错误。
        /// </summary>
        SystemError = 9,
    }
}
