namespace BITOJ.Data
{
    using BITOJ.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// 为比赛数据提供上下文支持。
    /// </summary>
    public partial class ContestDataContext : DbContext
    {
        /// <summary>
        /// 初始化 ContestDataContext 类的新实例。
        /// </summary>
        public ContestDataContext()
            : base("name=ContestDataContext")
        {
        }

        /// <summary>
        /// 按比赛 ID 查询比赛实体对象。
        /// </summary>
        /// <param name="contestId">要查询的比赛 ID 。</param>
        /// <returns>
        /// 具有给定比赛 ID 值得比赛实体对象。若未在数据库中找到对应的比赛实体对象，返回 null 。
        /// </returns>
        public ContestEntity QueryContestById(int contestId)
        {
            return Contests.Find(contestId);
        }

        /// <summary>
        /// 按标题查询比赛实体对象。
        /// </summary>
        /// <param name="title">要查询的标题。</param>
        /// <returns>一个列表，该列表包含了所有标题为给定值的比赛实体对象。</returns>
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
        /// 按作者查询比赛实体对象。
        /// </summary>
        /// <param name="creator">要查询的作者的用户名。</param>
        /// <returns>一个列表，该列表包含了所有作者为给定值的比赛实体对象。</returns>
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

        // TODO: 为比赛数据上下文添加分页查询支持。

        /// <summary>
        /// 获取或设置比赛数据集。
        /// </summary>
        protected virtual DbSet<ContestEntity> Contests { get; set; }
    }
}
