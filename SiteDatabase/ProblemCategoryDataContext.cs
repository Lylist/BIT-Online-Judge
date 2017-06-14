namespace BITOJ.Data
{
    using BITOJ.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// 为题目分类系统提供数据上下文支持。
    /// </summary>
    public partial class ProblemCategoryDataContext : DbContext
    {
        /// <summary>
        /// 初始化 ProblemCategoryDataContext 类的新实例。
        /// </summary>
        public ProblemCategoryDataContext()
            : base("name=ProblemCategoryDataContext")
        {
        }

        /// <summary>
        /// 将给定的题目类别实体对象添加到数据集中。
        /// </summary>
        /// <param name="entity">要添加的题目类别实体对象。</param>
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
        /// 将指定的题目 - 题目类别关系实体对象添加到数据集中。
        /// </summary>
        /// <param name="entity">要添加的题目 - 题目类别关系实体对象。</param>
        /// <exception cref="ArgumentNullException"/>
        public void AddProblemCategoryRelationEntity(ProblemCategoryRelationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Relations.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// 获取所有的根类别实体数据对象。
        /// </summary>
        /// <returns>一个列表对象，该列表中包含当前数据集中所有的根类别实体对象。</returns>
        public IList<ProblemCategoryEntity> QueryRootCategories()
        {
            var entities = from item in Categories
                           where item.ParentId == -1
                           select item;
            return entities.ToList();
        }

        /// <summary>
        /// 获取给定类别实体数据对象的所有子类别实体对象。
        /// </summary>
        /// <param name="parentCategoryId">父类别实体对象主键。</param>
        /// <returns>一个列表对象，该列表中包含当前数据集中所有的根类别实体对象。</returns>
        public IList<ProblemCategoryEntity> QuerySubCategoryEntities(int parentCategoryId)
        {
            var entities = from item in Categories
                           where item.ParentId == parentCategoryId
                           select item;
            return entities.ToList();
        }

        /// <summary>
        /// 获取给定主键的类别实体数据对象。
        /// </summary>
        /// <param name="categoryId">要查询的类别实体数据对象的主键。</param>
        /// <returns>主键为给定值的类别实体数据对象。如果没有这样的实体对象存在，返回 null 。</returns>
        public ProblemCategoryEntity QueryCategoryEntity(int categoryId)
        {
            return Categories.Find(categoryId);
        }

        /// <summary>
        /// 使用指定的类别名称查询类别实体对象数据。
        /// </summary>
        /// <param name="categoryName">要查询的类别名称。</param>
        /// <returns>一个列表，该列表包含了名称为给定值的所有类别实体对象。</returns>
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
        /// 使用指定的题目 ID 查询该题目的类别实体对象数据。
        /// </summary>
        /// <param name="problemId">要查询的题目的题目 ID 。</param>
        /// <returns>一个列表，该列表包含了给定题目的所有类别实体对象。</returns>
        /// <exception cref="ArgumentNullException"/>
        public IList<ProblemCategoryEntity> QueryProblemCategoryEntities(string problemId)
        {
            if (problemId == null)
                throw new ArgumentNullException(nameof(problemId));

            // 将 problemId 处理为大写表示以方便数据库查询。
            problemId = problemId.ToUpper();

            // 收集 problemId 所对应的题目的类别对象的主键值。
            var entities = from item in Relations
                           where item.ProblemId == problemId
                           select item.CategoryId;

            // 查询类别对象主键值并得到类别实体对象。
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
        /// 使用指定的类别 ID 查询在该类别下的所有题目 ID 。
        /// </summary>
        /// <param name="categoryId">要查询的类别 ID 。</param>
        /// <returns>一个列表，该列表包含了在给定类别中的所有题目 ID 。</returns>
        public IList<string> QueryProblemsInCategory(int categoryId)
        {
            var entities = from item in Relations
                           where item.CategoryId == categoryId
                           select item.ProblemId;
            return entities.ToList();
        }

        /// <summary>
        /// 获取题目分类类别数据集。
        /// </summary>
        protected virtual DbSet<ProblemCategoryEntity> Categories { get; set; }

        /// <summary>
        /// 获取题目与题目分类的关系数据集。
        /// </summary>
        protected virtual DbSet<ProblemCategoryRelationEntity> Relations { get; set; }
    }
}
