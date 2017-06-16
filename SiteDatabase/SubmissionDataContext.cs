namespace BITOJ.Data
{
    using BITOJ.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// 为用户提交数据库提供数据上下文。
    /// </summary>
    public partial class SubmissionDataContext : DbContext
    {
        /// <summary>
        /// 初始化 SubmissionDataContext 类的新实例。
        /// </summary>
        public SubmissionDataContext()
            : base("name=SubmissionDataContext")
        {
        }

        /// <summary>
        /// 将给定的用户提交记录添加至数据库中。
        /// </summary>
        /// <param name="entity">要添加的用户提交记录实体对象。</param>
        /// <exception cref="ArgumentNullException"/>
        public void AddSubmissionEntity(SubmissionEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Submissions.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// 使用指定的查询数据对象查询用户提交数据。
        /// </summary>
        /// <param name="data">要查询的查询数据对象。</param>
        /// <returns>一个列表，该列表包含了所有的查询结果。</returns>
        /// <exception cref="ArgumentNullException"/>
        public IList<SubmissionEntity> QuerySubmissionEntities(SubmissionQueryHandle data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            IQueryable<SubmissionEntity> temp = Submissions;
            if (data.UseId)
            {
                // 使用提交 ID 进行筛选。
                temp = temp.Where(entity => entity.Id == data.Id);
            }
            if (data.UseLanguage)
            {
                // 使用提交语言或编译系统进行筛选。
                temp = temp.Where(entity => entity.Language == data.Language);
            }
            if (data.UseProblemId)
            {
                // 使用题目 ID 进行筛选。
                temp = temp.Where(entity => entity.ProblemId == data.ProblemId);
            }
            if (data.UseTimePeriod)
            {
                // 使用题目时间区间进行筛选。
                temp = temp.Where(entity => 
                    entity.CreationTimestamp >= data.StartTime && entity.CreationTimestamp <= data.EndTime);
            }
            if (data.UseUsername)
            {
                // 使用用户名进行筛选。
                temp = temp.Where(entity => entity.Username == data.Username);
            }
            if (data.UseTeamId)
            {
                // 使用队伍 ID 进行筛选。
                temp = temp.Where(entity => entity.TeamId == data.TeamId);
            }
            if (data.UseVerdictResult)
            {
                // 使用 Judge 结果进行筛选。
                temp = temp.Where(entity => entity.VerdictResult == data.VerdictResult);
            }

            return temp.ToList();
        }

        /// <summary>
        /// 更新给定的用户提交记录实体数据。
        /// </summary>
        /// <param name="entity">要更新的用户提交记录实体数据。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// 若给定的用户提交记录实体数据不在数据库中，抛出 InvalidOperationException 异常。
        /// </remarks>
        public void UpdateSubmissionEntity(SubmissionEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            SubmissionEntity target = Submissions.Find(entity.Id);
            if (target == null)
                throw new InvalidOperationException("给定的用户提交记录实体未在数据库中找到。");

            // 更新用户提交记录实体数据。
            target.CodeFilename = entity.CodeFilename;
            target.CreationTimestamp = entity.CreationTimestamp;
            target.Language = entity.Language;
            target.ProblemId = entity.ProblemId;
            target.RemoteSubmissionId = entity.RemoteSubmissionId;
            target.Username = entity.Username;
            target.VerdictResult = entity.VerdictResult;
            target.VerdictStatus = entity.VerdictStatus;
            target.VerdictTimestamp = entity.VerdictTimestamp;

            SaveChanges();
        }

        /// <summary>
        /// 删除给定的用户提交记录实体数据。
        /// </summary>
        /// <param name="entity">要删除的用户提交记录实体数据。</param>
        /// <exception cref="ArgumentNullException"/>
        public void RemoveSubmissionEntity(SubmissionEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Submissions.Remove(entity);
            SaveChanges();
        }

        /// <summary>
        /// 获取或设置提交数据集。
        /// </summary>
        protected virtual DbSet<SubmissionEntity> Submissions { get; set; }
    }
}
