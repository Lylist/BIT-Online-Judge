namespace BITOJ.Core.Data
{
    /// <summary>
    /// 封装一道题目的基本信息。
    /// </summary>
    /// <remarks>
    /// 题目的具体信息，例如题干、样例输入等不被封装在该类中。
    /// </remarks>
    public class ProblemHandle
    {
        /// <summary>
        /// 获取或设置题目的 ID。该 ID 包括但不限于 BITOJ 的题目 ID 格式。
        /// </summary>
        public string ProblemId { get; set; }

        /// <summary>
        /// 初始化 ProblemHandle 类的新实例。
        /// </summary>
        public ProblemHandle()
        {
            ProblemId = string.Empty;
        }
    }
}
