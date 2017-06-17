namespace BITOJ.Data
{
    /// <summary>
    /// 表示题目描述中的各逻辑部分。
    /// </summary>
    public enum ProblemEntryParts : int
    {
        /// <summary>
        /// 题目主题干描述。
        /// </summary>
        Description = 1,

        /// <summary>
        /// 题目输入描述。
        /// </summary>
        InputDescription = 2,

        /// <summary>
        /// 题目输出描述。
        /// </summary>
        OutputDescription = 3,

        /// <summary>
        /// 题目输入样例。
        /// </summary>
        InputSample = 4,

        /// <summary>
        /// 题目输出样例，
        /// </summary>
        OutputSample = 5,

        /// <summary>
        /// 题目提示。
        /// </summary>
        Hint = 6,

        /// <summary>
        /// 题目来源。
        /// </summary>
        Source = 7,
    }
}
