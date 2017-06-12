namespace BITOJ.Core.Resolvers
{
    /// <summary>
    /// 封装 ProblemUrlResolver 的一个解析规则。
    /// </summary>
    public sealed class ProblemUrlResolverRule
    {
        /// <summary>
        /// 获取或设置满足解析条件的题目 ID 前缀。
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 获取或设置解析掩码。
        /// </summary>
        public string Mask { get; set; }

        /// <summary>
        /// 初始化 ProblemUrlResolverRule 的新实例。
        /// </summary>
        public ProblemUrlResolverRule()
        {
            Prefix = string.Empty;
            Mask = string.Empty;
        }
    }
}
