namespace BITOJ.Core.Resolvers.Masks
{
    /// <summary>
    /// 为 MaskToken 处理器提供接口。
    /// </summary>
    internal interface IMaskTokenHandler
    {
        /// <summary>
        /// 获取当前的 MaskToken 处理器的期望 Token 类型 ID。
        /// </summary>
        /// <remarks>
        /// 具有与该字段相同的 TypeId 的 MaskToken 将会被指派给当前处理器进行处理。
        /// </remarks>
        int ExpectedTokenTypeId { get; }

        /// <summary>
        /// 处理给定的 MaskToken 并返回处理结果。
        /// </summary>
        /// <param name="token">待处理的 MaskToken。</param>
        /// <returns>处理结果。</returns>
        string Process(MaskToken token);
    }
}
