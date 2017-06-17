namespace BITOJ.Common.Collections
{
    /// <summary>
    /// 为优先队列的实现提供接口。
    /// </summary>
    /// <typeparam name="T">优先队列中元素的类型。</typeparam>
    public interface IPriorityQueue<T>
    {
        /// <summary>
        /// 获取下一个即将出队列的元素。
        /// </summary>
        T Top { get; }

        /// <summary>
        /// 获取在优先队列中的元素数量。
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 将给定的元素压入优先队列。
        /// </summary>
        /// <param name="element">要压入优先队列的元素。</param>
        void Push(T element);

        /// <summary>
        /// 将一个元素弹出优先队列。
        /// </summary>
        void Pop();
    }
}
