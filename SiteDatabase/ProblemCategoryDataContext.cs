namespace BITOJ.Data
{
    using BITOJ.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// Ϊ��Ŀ����ϵͳ�ṩ����������֧�֡�
    /// </summary>
    public partial class ProblemCategoryDataContext : DbContext
    {
        /// <summary>
        /// ��ʼ�� ProblemCategoryDataContext �����ʵ����
        /// </summary>
        public ProblemCategoryDataContext()
            : base("name=ProblemCategoryDataContext")
        {
        }

        /// <summary>
        /// ����������Ŀ���ʵ�������ӵ����ݼ��С�
        /// </summary>
        /// <param name="entity">Ҫ��ӵ���Ŀ���ʵ�����</param>
        /// <exception cref="ArgumentNullException"/>
        public void AddProblemCategoryEntity(ProblemCategoryEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.Name = entity.Name.ToUpper();
            Categories.Add(entity);

            SaveChanges();
        }

        /// <summary>
        /// ��ָ������Ŀ - ��Ŀ����ϵʵ�������ӵ����ݼ��С�
        /// </summary>
        /// <param name="entity">Ҫ��ӵ���Ŀ - ��Ŀ����ϵʵ�����</param>
        /// <exception cref="ArgumentNullException"/>
        public void AddProblemCategoryRelationEntity(ProblemCategoryRelationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Relations.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// ��ȡ���еĸ����ʵ�����ݶ���
        /// </summary>
        /// <returns>һ���б���󣬸��б��а�����ǰ���ݼ������еĸ����ʵ�����</returns>
        public IList<ProblemCategoryEntity> QueryRootCategories()
        {
            var entities = from item in Categories
                           where item.ParentId == -1
                           select item;
            return entities.ToList();
        }

        /// <summary>
        /// ��ȡ�������ʵ�����ݶ�������������ʵ�����
        /// </summary>
        /// <param name="parentCategoryId">�����ʵ�����������</param>
        /// <returns>һ���б���󣬸��б��а�����ǰ���ݼ������еĸ����ʵ�����</returns>
        public IList<ProblemCategoryEntity> QuerySubCategoryEntities(int parentCategoryId)
        {
            var entities = from item in Categories
                           where item.ParentId == parentCategoryId
                           select item;
            return entities.ToList();
        }

        /// <summary>
        /// ��ȡ�������������ʵ�����ݶ���
        /// </summary>
        /// <param name="categoryId">Ҫ��ѯ�����ʵ�����ݶ����������</param>
        /// <returns>����Ϊ����ֵ�����ʵ�����ݶ������û��������ʵ�������ڣ����� null ��</returns>
        public ProblemCategoryEntity QueryCategoryEntity(int categoryId)
        {
            return Categories.Find(categoryId);
        }

        /// <summary>
        /// ʹ��ָ����������Ʋ�ѯ���ʵ��������ݡ�
        /// </summary>
        /// <param name="categoryName">Ҫ��ѯ��������ơ�</param>
        /// <returns>һ���б����б����������Ϊ����ֵ���������ʵ�����</returns>
        /// <exception cref="ArgumentNullException"/>
        public IList<ProblemCategoryEntity> QueryCategoryEntities(string categoryName)
        {
            if (categoryName == null)
                throw new ArgumentNullException(nameof(categoryName));

            var entities = from item in Categories
                           where item.Name == categoryName
                           select item;
            return entities.ToList();
        }

        /// <summary>
        /// ʹ��ָ������Ŀ ID ��ѯ����Ŀ�����ʵ��������ݡ�
        /// </summary>
        /// <param name="problemId">Ҫ��ѯ����Ŀ����Ŀ ID ��</param>
        /// <returns>һ���б����б�����˸�����Ŀ���������ʵ�����</returns>
        /// <exception cref="ArgumentNullException"/>
        public IList<ProblemCategoryEntity> QueryProblemCategoryEntities(string problemId)
        {
            if (problemId == null)
                throw new ArgumentNullException(nameof(problemId));

            // �� problemId ����Ϊ��д��ʾ�Է������ݿ��ѯ��
            problemId = problemId.ToUpper();

            // �ռ� problemId ����Ӧ����Ŀ�������������ֵ��
            var entities = from item in Relations
                           where item.ProblemId == problemId
                           select item.CategoryId;

            // ��ѯ����������ֵ���õ����ʵ�����
            List<ProblemCategoryEntity> ret = new List<ProblemCategoryEntity>();
            foreach (int id in entities)
            {
                ProblemCategoryEntity item = QueryCategoryEntity(id);
                if (item != null)
                {
                    ret.Add(item);
                }
            }

            return ret;
        }

        /// <summary>
        /// ʹ��ָ������� ID ��ѯ�ڸ�����µ�������Ŀ ID ��
        /// </summary>
        /// <param name="categoryId">Ҫ��ѯ����� ID ��</param>
        /// <returns>һ���б����б�������ڸ�������е�������Ŀ ID ��</returns>
        public IList<string> QueryProblemsInCategory(int categoryId)
        {
            var entities = from item in Relations
                           where item.CategoryId == categoryId
                           select item.ProblemId;
            return entities.ToList();
        }

        /// <summary>
        /// ��ȡ��Ŀ����������ݼ���
        /// </summary>
        protected virtual DbSet<ProblemCategoryEntity> Categories { get; set; }

        /// <summary>
        /// ��ȡ��Ŀ����Ŀ����Ĺ�ϵ���ݼ���
        /// </summary>
        protected virtual DbSet<ProblemCategoryRelationEntity> Relations { get; set; }
    }
}
