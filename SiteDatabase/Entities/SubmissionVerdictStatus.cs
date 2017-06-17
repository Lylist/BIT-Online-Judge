namespace BITOJ.Data.Entities
{
    /// <summary>
    /// 表示提交的 Judge 状态。
    /// </summary>
    public enum SubmissionVerdictStatus : int
    {
        /// <summary>
        /// 用户提交正在 Judge 队列中。
        /// </summary>
        Pending = 0,

        /// <summary>
        /// 用户提交已经被提交至 Judge 服务器。
        /// </summary>
        Submitted = 1,

        /// <summary>
        /// 用户提交正在被便宜。
        /// </summary>
        Compiling = 2,

        /// <summary>
        /// 用户提交正在运行。
        /// </summary>
        Running = 3,

        /// <summary>
        /// 用户提交已经完成了 Judge 流程。
        /// </summary>
        Completed = 4,
    }
}
