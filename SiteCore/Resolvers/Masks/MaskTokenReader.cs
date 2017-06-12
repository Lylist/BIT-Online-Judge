namespace BITOJ.Core.Resolvers.Masks
{
    using System;

    /// <summary>
    /// 提供掩码 Token 读取器的抽象基类。
    /// </summary>
    internal abstract class MaskTokenReader
    {
        /// <summary>
        /// 获取底层掩码字符串。
        /// </summary>
        public string Mask { get; private set; }

        /// <summary>
        /// 获取读取器的偏移量。
        /// </summary>
        public int Offset { get; private set; }

        /// <summary>
        /// 获取一个值，该值指示读取器是否已经读取到掩码的末尾。
        /// </summary>
        public bool EndOfMask
        {
            get
            {
                return Offset >= Mask.Length;
            }
        }

        /// <summary>
        /// 使用指定的掩码初始化 MaskTokenReader 类的新实例。
        /// </summary>
        /// <param name="mask">要执行读取操作的掩码字符串。</param>
        /// <exception cref="ArgumentNullException"/>
        protected MaskTokenReader(string mask)
        {
            if (mask == null)
                throw new ArgumentNullException(nameof(mask));

            Mask = mask;
            Offset = 0;
        }

        /// <summary>
        /// 将读取器位置设置为给定的新位置。
        /// </summary>
        /// <param name="position">读取器的新读取位置。</param>
        public void SetOffset(OffsetPositions position)
        {
            switch (position)
            {
                case OffsetPositions.Begin:
                    Offset = 0;
                    break;
                case OffsetPositions.End:
                    Offset = Mask.Length;
                    break;
            }
        }

        /// <summary>
        /// 当在派生类中重写时，从掩码中读取下一个 MaskToken 并返回。
        /// </summary>
        /// <returns>读取的下一个 MaskToken 。</returns>
        public abstract MaskToken Read();

        /// <summary>
        /// 从基础掩码字符串中读取下一个字符并递增读取器位置指针。如果已经读取到掩码字符串末尾，返回零字符。（'\0'）
        /// </summary>
        /// <returns>从基础掩码字符串中读取的下一个字符。</returns>
        protected char ReadChar()
        {
            if (EndOfMask)
            {
                return '\0';
            }
            else
            {
                return Mask[Offset++];
            }
        }
    }
}
