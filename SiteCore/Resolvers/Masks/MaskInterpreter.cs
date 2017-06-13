namespace BITOJ.Core.Resolvers.Masks
{
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// 提供对掩码解释的支持。
    /// </summary>
    internal sealed class MaskInterpreter
    {
        private static readonly Logger Log;

        static MaskInterpreter()
        {
            // 初始化日志记录器。
            Log = LogManager.GetCurrentClassLogger();
        }

        private MaskTokenReader m_reader;
        private ICollection<IMaskTokenHandler> m_handlers;

        /// <summary>
        /// 使用指定的 MaskTokenReader 以及 MaskToken 处理器集初始化 MaskInterpreter 类的新实例。
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public MaskInterpreter(MaskTokenReader reader, ICollection<IMaskTokenHandler> handlers)
        {
            m_reader = reader ?? throw new ArgumentNullException(nameof(reader));
            m_handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));
        }

        /// <summary>
        /// 获取当前解释器使用的基础 MaskToken 读取器。
        /// </summary>
        public MaskTokenReader BaseReader { get; }

        /// <summary>
        /// 解释处理 MaskToken 内容并返回处理结果。
        /// </summary>
        /// <param name="token">要处理的 MaskToken 。</param>
        /// <returns>处理结果。</returns>
        /// <exception cref="ArgumentNullException"/>
        private string InterpretToken(MaskToken token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            IMaskTokenHandler targetHandler = null;
            foreach (IMaskTokenHandler handler in m_handlers)
            {
                if (handler.ExpectedTokenTypeId == token.TypeId)
                {
                    targetHandler = handler;
                    break;
                }
            }

            if (targetHandler == null)
            {
                // 没有找到对应的 MaskToken 处理器。
                return token.Content;
            }
            else
            {
                return targetHandler.Process(token);
            }
        }

        /// <summary>
        /// 解释位于 MaskToken 读取器中的掩码并返回解释结果。
        /// </summary>
        /// <returns>解释结果。</returns>
        public string Interpret()
        {
            // 写入日志信息。
            Log.Debug("Interpreting mask: \"{0}\".", BaseReader.Mask);

            StringBuilder builder = new StringBuilder();
            m_reader.SetOffset(OffsetPositions.Begin);

            while (!m_reader.EndOfMask)
            {
                MaskToken token = m_reader.Read();
                builder.Append(InterpretToken(token));
            }

            string result = builder.ToString();
            Log.Debug("Interpreting complete. Result = \"{0}\".", result);

            return result;
        }
    }
}
