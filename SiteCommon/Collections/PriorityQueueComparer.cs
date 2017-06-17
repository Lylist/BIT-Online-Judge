namespace BITOJ.Common.Collections
{
    /// <summary>
    /// 为优先队列元素比较器提供接口。
    /// </summary>
    /// <typeparam name="T">处于优先队列中的元素类型。</typeparam>
    public interface IPriorityQueueComparer<T>
    {
        /// <summary>
        /// 比较优先队列中的两元素。
        /// </summary>
        /// <param name="obj1">第一个元素。</param>
        /// <param name="obj2">第二个元素。</param>
        /// <returns>一个值，该值指示第一个元素是否应该比第二个元素先出队列。</returns>
        bool Compare(T obj1, T obj2);
    }
}
