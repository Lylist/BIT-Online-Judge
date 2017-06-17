namespace BITOJ.Core.Data
{
    /// <summary>
    /// 当某个题目无效时触发此异常。
    /// </summary>
    public sealed class InvalidProblemException : ProblemException
    {
        /// <summary>
        /// 使用给定的 ProblemEntry 对象初始化 InvalidProblemException 的新实例。
        /// </summary>
        /// <param name="entry">要使用的 ProblemEntry 对象。</param>
        public InvalidProblemException(ProblemHandle entry) : base(entry, $"编号为 {entry.ProblemId} 的题目无效。")
        { }

        /// <summary>
        /// 使用给定的 ProblemEntry 对象和异常消息初始化 InvalidProblemException 的新实例。
        /// </summary>
        /// <param name="entry">要使用的 ProblemEntry 对象。</param>
        /// <param name="message">产生该异常的异常消息。</param>
        public InvalidProblemException(ProblemHandle entry, string message) : base(entry, message)
        { }
    }
}
