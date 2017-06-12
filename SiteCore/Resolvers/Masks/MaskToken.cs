namespace BITOJ.Core.Resolvers.Masks
{
    /// <summary>
    /// 提供掩码 Token 的包装。
    /// </summary>
    internal class MaskToken
    {
        /// <summary>
        /// 获取或设置 MaskToken 的内容。
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 获取或设置当前 MaskToken 的类型标识。
        /// </summary>
        /// <remarks>
        /// 不要试图在掩码系统以外访问或修改这一字段。这一字段是根据实现而不同的。（Implementation-defined）
        /// </remarks>
        public int TypeId { get; set; }

        /// <summary>
        /// 使用指定的内容和类型 ID 初始化 MaskToken 类的一个新实例。
        /// </summary>
        /// <param name="content">Token 内容。</param>
        /// <param name="typeId">Token 的类型 ID。</param>
        public MaskToken(string content, int typeId)
        {
            Content = content;
            TypeId = typeId;
        }
    }
}
