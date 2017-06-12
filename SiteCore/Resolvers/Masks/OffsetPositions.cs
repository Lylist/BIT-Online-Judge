namespace BITOJ.Core.Resolvers.Masks
{
    /// <summary>
    /// 表示读取器偏移量基础位置。
    /// </summary>
    internal enum OffsetPositions : int
    {
        /// <summary>
        /// 数据内容的开头
        /// </summary>
        Begin = 0,

        /// <summary>
        /// 数据内容的结尾
        /// </summary>
        End = -1,
    }
}
