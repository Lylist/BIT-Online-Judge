namespace BITOJ.Data
{
    using BITOJ.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// Ϊ���������ṩ������֧�֡�
    /// </summary>
    public partial class ContestDataContext : DbContext
    {
        /// <summary>
        /// ��ʼ�� ContestDataContext �����ʵ����
        /// </summary>
        public ContestDataContext()
            : base("name=ContestDataContext")
        {
        }

        /// <summary>
        /// �������ı���ʵ��������������ݿ��С�
        /// </summary>
        /// <param name="entity">Ҫ��ӵı���ʵ�塣</param>
        /// <exception cref="ArgumentNullException"/>
        public void AddContest(ContestEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Contests.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// ������ ID ��ѯ����ʵ�����
        /// </summary>
        /// <param name="contestId">Ҫ��ѯ�ı��� ID ��</param>
        /// <returns>
        /// ���и������� ID ֵ�ñ���ʵ�������δ�����ݿ����ҵ���Ӧ�ı���ʵ����󣬷��� null ��
        /// </returns>
        public ContestEntity QueryContestById(int contestId)
        {
            return Contests.Find(contestId);
        }

        /// <summary>
        /// �������ѯ����ʵ�����
        /// </summary>
        /// <param name="title">Ҫ��ѯ�ı��⡣</param>
        /// <returns>һ���б����б���������б���Ϊ����ֵ�ı���ʵ�����</returns>
        /// <exception cref="ArgumentNullException"/>
        public IList<ContestEntity> QueryContestsByTitle(string title)
        {
            if (title == null)
                throw new ArgumentNullException(nameof(title));

            var entities = from item in Contests
                           where item.Title == title
                           select item;
            return entities.ToList();
        }

        /// <summary>
        /// �����߲�ѯ����ʵ�����
        /// </summary>
        /// <param name="creator">Ҫ��ѯ�����ߵ��û�����</param>
        /// <returns>һ���б����б��������������Ϊ����ֵ�ı���ʵ�����</returns>
        /// <exception cref="ArgumentNullException"/>
        public IList<ContestEntity> QueryContestsByCreator(string creator)
        {
            if (creator == null)
                throw new ArgumentNullException(nameof(creator));

            var entities = from item in Contests
                           where item.Creator == creator
                           select item;
            return entities.ToList();
        }

        // TODO: Ϊ����������������ӷ�ҳ��ѯ֧�֡�

        /// <summary>
        /// �����ݿ����Ƴ������ı�������ʵ�塣
        /// </summary>
        /// <param name="entity">Ҫ�Ƴ��ı���ʵ�����</param>
        /// <exception cref="ArgumentNullException"/>
        public void RemoveContest(ContestEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Contests.Remove(entity);
        }

        /// <summary>
        /// ��ȡ�����ñ������ݼ���
        /// </summary>
        protected virtual DbSet<ContestEntity> Contests { get; set; }
    }
}
