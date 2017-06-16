namespace BITOJ.Data
{
    using BITOJ.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// Ϊ�û��ύ���ݿ��ṩ���������ġ�
    /// </summary>
    public partial class SubmissionDataContext : DbContext
    {
        /// <summary>
        /// ��ʼ�� SubmissionDataContext �����ʵ����
        /// </summary>
        public SubmissionDataContext()
            : base("name=SubmissionDataContext")
        {
        }

        /// <summary>
        /// ���������û��ύ��¼��������ݿ��С�
        /// </summary>
        /// <param name="entity">Ҫ��ӵ��û��ύ��¼ʵ�����</param>
        /// <exception cref="ArgumentNullException"/>
        public void AddSubmissionEntity(SubmissionEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Submissions.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// ʹ��ָ���Ĳ�ѯ���ݶ����ѯ�û��ύ���ݡ�
        /// </summary>
        /// <param name="data">Ҫ��ѯ�Ĳ�ѯ���ݶ���</param>
        /// <returns>һ���б����б���������еĲ�ѯ�����</returns>
        /// <exception cref="ArgumentNullException"/>
        public IList<SubmissionEntity> QuerySubmissionEntities(SubmissionQueryHandle data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            IQueryable<SubmissionEntity> temp = Submissions;
            if (data.UseId)
            {
                // ʹ���ύ ID ����ɸѡ��
                temp = temp.Where(entity => entity.Id == data.Id);
            }
            if (data.UseLanguage)
            {
                // ʹ���ύ���Ի����ϵͳ����ɸѡ��
                temp = temp.Where(entity => entity.Language == data.Language);
            }
            if (data.UseProblemId)
            {
                // ʹ����Ŀ ID ����ɸѡ��
                temp = temp.Where(entity => entity.ProblemId == data.ProblemId);
            }
            if (data.UseTimePeriod)
            {
                // ʹ����Ŀʱ���������ɸѡ��
                temp = temp.Where(entity => 
                    entity.CreationTimestamp >= data.StartTime && entity.CreationTimestamp <= data.EndTime);
            }
            if (data.UseUsername)
            {
                // ʹ���û�������ɸѡ��
                temp = temp.Where(entity => entity.Username == data.Username);
            }
            if (data.UseTeamId)
            {
                // ʹ�ö��� ID ����ɸѡ��
                temp = temp.Where(entity => entity.TeamId == data.TeamId);
            }
            if (data.UseVerdictResult)
            {
                // ʹ�� Judge �������ɸѡ��
                temp = temp.Where(entity => entity.VerdictResult == data.VerdictResult);
            }

            return temp.ToList();
        }

        /// <summary>
        /// ���¸������û��ύ��¼ʵ�����ݡ�
        /// </summary>
        /// <param name="entity">Ҫ���µ��û��ύ��¼ʵ�����ݡ�</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// ���������û��ύ��¼ʵ�����ݲ������ݿ��У��׳� InvalidOperationException �쳣��
        /// </remarks>
        public void UpdateSubmissionEntity(SubmissionEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            SubmissionEntity target = Submissions.Find(entity.Id);
            if (target == null)
                throw new InvalidOperationException("�������û��ύ��¼ʵ��δ�����ݿ����ҵ���");

            // �����û��ύ��¼ʵ�����ݡ�
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
        /// ɾ���������û��ύ��¼ʵ�����ݡ�
        /// </summary>
        /// <param name="entity">Ҫɾ�����û��ύ��¼ʵ�����ݡ�</param>
        /// <exception cref="ArgumentNullException"/>
        public void RemoveSubmissionEntity(SubmissionEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Submissions.Remove(entity);
            SaveChanges();
        }

        /// <summary>
        /// ��ȡ�������ύ���ݼ���
        /// </summary>
        protected virtual DbSet<SubmissionEntity> Submissions { get; set; }
    }
}
