namespace BITOJ.Core.Resolvers
{
    using BITOJ.Core.Cache.Settings;
    using BITOJ.Core.Data;
    using BITOJ.Core.Resolvers.Masks;
    using System;

    /// <summary>
    /// 为 ProblemEntry 提供全局统一定位符（URL）解析服务。
    /// </summary>
    public sealed class ProblemUrlResolver
    {
        private static readonly string RulesSettingName = "prob_url_resolver_rules";

        private static ProblemUrlResolverRule[] ms_rules;
        private static object ms_syncLock;

        private static void InitializeRulesFromSettings()
        {
            FileSystemSettingProvider settings = new FileSystemSettingProvider();
            if (!settings.Contains(RulesSettingName))
            {
                // 不存在相应的规则设置。
                return;
            }
            else
            {
                ms_rules = settings.Get<ProblemUrlResolverRule[]>(RulesSettingName);
            }
        }

        /// <summary>
        /// 使用指定的题目编号查询解析规则。
        /// </summary>
        /// <param name="problemId">待查询的题目编号。</param>
        /// <returns>如果期望的解析规则存在，返回相应的解析规则；否则返回 null。</returns>
        /// <exception cref="ArgumentNullException"/>
        private static ProblemUrlResolverRule GetRule(string problemId)
        {
            if (problemId == null)
                throw new ArgumentNullException(nameof(problemId));

            foreach (ProblemUrlResolverRule rule in ms_rules)
            {
                if (problemId.StartsWith(rule.Prefix, StringComparison.CurrentCultureIgnoreCase))
                {
                    return rule;
                }
            }
            return null;
        }

        static ProblemUrlResolver()
        {
            ms_rules = null;
            ms_syncLock = new object();
        }

        /// <summary>
        /// 初始化 ProblemUrlResolver 类的新实例。
        /// </summary>
        public ProblemUrlResolver()
        { }

        /// <summary>
        /// 解析指定 ProblemEntry 对象的全局统一定位符（URL）。
        /// </summary>
        /// <param name="entry">待解析的 ProblemEntry 对象。</param>
        /// <returns>给定 ProblemEntry 对象所对应的全局统一定位符（URL）。</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidProblemException"/>
        public string Get(ProblemEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            ProblemUrlResolverRule rule = GetRule(entry.ProblemId);
            if (rule == null)
                throw new InvalidProblemException(entry);

            // 解析对应的掩码项得到给定 ProblemEntry 的全局统一定位符。
            /*
            
            MaskTokenReader reader = new MaskTokenReader(rule.Mask);
            MaskTokenHandler[] handlers = new MaskTokenHandlers[] {
                ...
            };

            MaskInterpreter interpreter = new MaskInterpreter(reader, handlers);
            return interpreter.Interpret();

             */
        }
    }
}
