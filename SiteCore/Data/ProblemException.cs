namespace BITOJ.Core.Data
{
    using System;

    /// <summary>
    /// 由于题目出现问题引发的异常。
    /// </summary>
    public class ProblemException : Exception
    {
        /// <summary>
        /// 获取引发异常的题目。
        /// </summary>
        public ProblemEntry Problem { get; private set; }

        /// <summary>
        /// 使用 ProblemEntry 对象初始化 ProblemException 类的新实例。
        /// </summary>
        /// <param name="entry">要使用的 ProblemEntry 对象。</param>
        /// <exception cref="NullReferenceException"/>
        public ProblemException(ProblemEntry entry) : base($"编号为 {entry.ProblemId} 的题目出现错误。")
        {
            Problem = entry;
        }

        /// <summary>
        /// 使用给定的 ProblemEntry 对象和异常消息初始化 ProblemException 类的新实例。
        /// </summary>
        /// <param name="entry">要使用的 ProblemEntry 对象。</param>
        /// <param name="message">要使用的异常消息。</param>
        public ProblemException(ProblemEntry entry, string message) : base(message)
        {
            Problem = entry ?? throw new ArgumentNullException(nameof(entry));
        }
    }
}
