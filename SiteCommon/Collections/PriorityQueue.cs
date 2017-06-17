namespace BITOJ.Common.Collections
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 封装基于最小堆的优先队列实现。
    /// </summary>
    /// <typeparam name="TElement">优先队列中元素的类型。</typeparam>
    /// <typeparam name="TComparer">用于比较优先队列中元素的比较器。</typeparam>
    public sealed class PriorityQueue<TElement, TComparer> : IPriorityQueue<TElement> 
        where TComparer: IPriorityQueueComparer<TElement>
    {
        private const int RootIndex = 1;

        private IList<TElement> m_buffer;
        private TComparer m_comparer;

        /// <summary>
        /// 使用指定的基础列表实现及元素比较器初始化 PriorityQueue 类的新实例。
        /// </summary>
        /// <param name="baseList">要使用的基础列表实现。</param>
        /// <param name="comparer">要使用的元素比较器。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <remarks>
        /// 注意：调用此构造器后，baseList 中原有的元素将会被移除。
        /// </remarks>
        public PriorityQueue(IList<TElement> baseList, TComparer comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));
            if (baseList == null)
                throw new ArgumentNullException(nameof(baseList));

            baseList.Clear();
            m_buffer = baseList;
            m_comparer = comparer;

            // 填充基础列表的 [0] 位置。
            m_buffer.Add(default(TElement));
        }

        /// <summary>
        /// 使用给定的元素比较器初始化 PriorityQueue 类的新实例。
        /// </summary>
        /// <param name="comparer">要使用的元素比较器。</param>
        /// <exception cref="ArgumentNullException"/>
        public PriorityQueue(TComparer comparer) : this(new List<TElement>(), comparer)
        { }

        /// <summary>
        /// 获取指定索引处的左孩子索引。
        /// </summary>
        /// <param name="index">当前索引。</param>
        /// <returns>当前索引的左孩子索引。</returns>
        private int GetLeftChildIndex(int index)
        {
            return index << 1;
        }

        /// <summary>
        /// 获取指定索引处的右孩子索引。
        /// </summary>
        /// <param name="index">当前索引。</param>
        /// <returns>当前索引的右孩子索引。</returns>
        private int GetRightChildIndex(int index)
        {
            return GetLeftChildIndex(index) | 1;
        }

        /// <summary>
        /// 获取指定索引处的父节点索引。
        /// </summary>
        /// <param name="index">当前索引。</param>
        /// <returns>当前索引的父亲索引。</returns>
        private int GetParentIndex(int index)
        {
            return index >> 1;
        }

        /// <summary>
        /// 交换给定两位置处的元素。
        /// </summary>
        /// <param name="pos1">第一个位置。</param>
        /// <param name="pos2">第二个位置。</param>
        private void SwapElements(int pos1, int pos2)
        {
            TElement tmp = m_buffer[pos1];
            m_buffer[pos1] = m_buffer[pos2];
            m_buffer[pos2] = tmp;
        }

        /// <summary>
        /// 从指定位置处开始向最小堆树根方向重新平衡最小堆。
        /// </summary>
        /// <param name="pos">重新平衡最小堆的起始位置。</param>
        private void BalanceToRoot(int pos)
        {
            while (pos > RootIndex)
            {
                int parent = GetParentIndex(pos);
                if (m_comparer.Compare(m_buffer[pos], m_buffer[parent]))
                {
                    // 当前元素优先级高于父节点优先级。交换当前元素与父节点元素并继续向树根方向平衡。
                    SwapElements(pos, parent);
                    pos = parent;
                }
                else
                {
                    // 当前元素优先级低于父节点优先级。平衡完毕。
                    break;
                }
            }
        }

        /// <summary>
        /// 从最小堆树根开始向叶子方向重新平衡最小堆。
        /// </summary>
        private void BalanceToLeaf()
        {
            int pos = RootIndex;
            while (pos < Count)
            {
                int leftChild = GetLeftChildIndex(pos);
                int rightChild = GetRightChildIndex(pos);

                int largerChild = leftChild;
                if (leftChild > Count)
                {
                    // 当前位置为叶子。平衡结束。
                    break;
                }
                else if (rightChild <= Count)
                {
                    // 比较两孩子节点上元素的优先级关系。
                    if (m_comparer.Compare(m_buffer[rightChild], m_buffer[leftChild]))
                    {
                        // 右孩子优先级高于左孩子优先级，应先出队列。
                        largerChild = rightChild;
                    }
                }

                // 比较当前结点元素与两个孩子中较大元素的优先级关系。
                if (m_comparer.Compare(m_buffer[largerChild], m_buffer[pos]))
                {
                    // 孩子节点优先级高于当前节点。交换孩子节点与当前节点的元素并继续向叶子方向平衡。
                    SwapElements(largerChild, pos);
                }
                else
                {
                    // 孩子节点优先级均低于当前节点。平衡结束。
                    break;
                }
            }
        }

        /// <summary>
        /// 获取处于优先队列中的元素数量。
        /// </summary>
        public int Count
        {
            get { return m_buffer.Count - 1; }
        }

        /// <summary>
        /// 获取下一个即将弹出优先队列的元素。
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        public TElement Top
        {
            get
            {
                if (Count == 0)
                    throw new InvalidOperationException("优先队列为空。");
                return m_buffer[0];
            }
        }

        /// <summary>
        /// 将给定的元素压入优先队列。
        /// </summary>
        /// <param name="element"></param>
        public void Push(TElement element)
        {
            m_buffer.Add(element);
            Balance(Count);
        }

        /// <summary>
        /// 将堆顶端元素弹出优先队列。
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        public void Pop()
        {
            if (Count == 0)
                throw new InvalidOperationException("优先队列为空。");

            m_buffer[1] = m_buffer[Count];
            m_buffer.RemoveAt(Count);

            Balance(RootIndex);
        }
    }
}
