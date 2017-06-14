namespace BITOJ.Data
{
    using BITOJ.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// Ϊ�û����ݿ������ṩ������֧�֡�
    /// </summary>
    public partial class UserDataContext : DbContext
    {
        /// <summary>
        /// ��ʼ�� UserDataContext �����ʵ����
        /// </summary>
        public UserDataContext()
            : base("name=UserDataContext")
        {
        }

        /// <summary>
        /// ���������û�Ȩ��ʵ��������������ݿ��С�
        /// </summary>
        /// <param name="entity">Ҫ��ӵ��û�Ȩ��ʵ�����ݡ�</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// ��������ʵ�������Ѿ����������ݿ��У��׳� InvalidOperationException �쳣��
        /// ��Ҫ���¸�����ʵ�����ݣ���ʹ�� UpdateUserAuthorizationEntity ������
        /// </remarks>
        public void AddUserAuthorizationEntity(UserAuthorizationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (QueryUserAuthorizationEntity(entity.Username) != null)
                throw new InvalidOperationException("������ʵ�������Ѿ����������ݿ��С�");

            UserAuthorization.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// ���������û���Ϣʵ��������������ݿ��С�
        /// </summary>
        /// <param name="entity">Ҫ��ӵ��û���Ϣʵ�����ݡ�</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// ��������ʵ�������Ѿ����������ݿ��У��׳� InvalidOperationException �쳣��
        /// ��Ҫ���¸�����ʵ�����ݣ���ʹ�� UpdateUserProfileEntity ������
        /// </remarks>
        public void AddUserProfileEntity(UserProfileEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (QueryUserProfileEntity(entity.Username) != null)
                throw new InvalidOperationException("������ʵ������Ѿ����������ݿ��С�");

            UserProfiles.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// �������Ķ�����Ϣʵ��������������ݿ��С�
        /// </summary>
        /// <param name="entity">Ҫ��ӵĶ�����Ϣʵ�����ݡ�</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// ��������ʵ������Ѿ����������ݿ��У��׳� InvalidOperationException �쳣��
        /// ��Ҫ�������ݿ��ж�Ӧ��ʵ�����ݣ������ UpdateTeamProfileEntity ������
        /// </remarks>
        public void AddTeamProfileEntity(TeamProfileEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (QueryTeamProfileEntity(entity.Name) != null)
                throw new InvalidOperationException("�����Ķ�����Ϣʵ������Ѿ����������ݿ��С�");

            TeamProfiles.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// ���������û� - �����ϵʵ����������ݿ��С�
        /// </summary>
        /// <param name="entity">Ҫ��ӵ��û� - �����ϵʵ�����</param>
        /// <exception cref="ArgumentNullException"/>
        public void AddUserTeamRelationEntity(UserTeamRelationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            UserTeams.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// ʹ�ø������û�����ѯ�û�Ȩ��ʵ�����ݡ�
        /// </summary>
        /// <param name="username">Ҫ��ѯ���û�����</param>
        /// <returns>�û�����Ӧ���û�Ȩ��ʵ�����ݡ����δ�ҵ������û������û�Ȩ��ʵ�����ݣ����� null��</returns>
        /// <exception cref="ArgumentNullException"/>
        public UserAuthorizationEntity QueryUserAuthorizationEntity(string username)
        {
            if (username == null)
                throw new ArgumentNullException(nameof(username));

            return UserAuthorization.Find(username);
        }

        /// <summary>
        /// ʹ�ø������û����ѯ�û�Ȩ��ʵ�����ݡ�
        /// </summary>
        /// <param name="group">Ҫ��ѯ���û��顣</param>
        /// <returns>�����û��������е��û�ʵ�����ݡ�</returns>
        public IList<UserAuthorizationEntity> QueryUserAuthorizationEntities(UserGroup userGroup)
        {
            var entities = from item in UserAuthorization
                           where item.Group == userGroup
                           select item;
            return entities.ToList();
        }

        /// <summary>
        /// ʹ��ָ�����û�����ѯ�û���Ϣʵ�����
        /// </summary>
        /// <param name="username">Ҫ��ѯ���û�����</param>
        /// <returns>��Ӧ���û���Ϣʵ���������������û���δ�����ݿ����ҵ������� null��</returns>
        /// <exception cref="ArgumentNullException"/>
        public UserProfileEntity QueryUserProfileEntity(string username)
        {
            if (username == null)
                throw new ArgumentNullException(nameof(username));

            return UserProfiles.Find(username);
        }

        /// <summary>
        /// ʹ�ø����Ķ��� ID ��ѯ������Ϣʵ�����
        /// </summary>
        /// <param name="teamId">Ҫ��ѯ�Ķ��� ID ��</param>
        /// <returns>����� ID ���Ӧ�Ķ�����Ϣʵ������������Ķ��� ID δ�����ݿ����ҵ������� null ��</returns>
        public TeamProfileEntity QueryTeamProfileEntity(int teamId)
        {
            return TeamProfiles.Find(teamId);
        }

        /// <summary>
        /// ʹ�ø����Ķ������Ʋ�ѯ������Ϣʵ�����ݡ�
        /// </summary>
        /// <param name="teamName">Ҫ��ѯ�Ķ������ơ�</param>
        /// <returns>
        /// һ���б����б�����˸���������������Ӧ�Ķ�����Ϣʵ�����
        /// ��������Ķ�������δ�����ݿ����ҵ������� null��
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        public IList<TeamProfileEntity> QueryTeamProfileEntity(string teamName)
        {
            if (teamName == null)
                throw new ArgumentNullException(nameof(teamName));

            var entities = from item in TeamProfiles
                           where item.Name == teamName
                           select item;
            return entities.ToList();
        }

        /// <summary>
        /// ʹ�ø������û�����ѯָ���û��������û� - �����ϵʵ�����
        /// </summary>
        /// <param name="username">Ҫ��ѯ���û�����</param>
        /// <returns>һ���б����б��������ָ���û�������������û� - �����ϵʵ�����</returns>
        /// <exception cref="ArgumentNullException"/>
        public IList<UserTeamRelationEntity> QueryUserTeamRelationEntitiesByUsername(string username)
        {
            if (username == null)
                throw new ArgumentNullException(nameof(username));

            var entities = from item in UserTeams
                           where item.Username == username
                           select item;
            return entities.ToList();
        }

        /// <summary>
        /// ʹ�ø����Ķ��� ID ��ѯ��������������û� - �����ϵʵ�����
        /// </summary>
        /// <param name="teamId">Ҫ��ѯ�Ķ��� ID ��</param>
        /// <returns>һ���б����б��������ָ������������������û� - �����ϵʵ�����</returns>
        public IList<UserTeamRelationEntity> QueryUserTeamRelationEntitiesByTeamId(int teamId)
        {
            var entities = from item in UserTeams
                           where item.TeamId == teamId
                           select item;
            return entities.ToList();
        }

        /// <summary>
        /// �������ݿ��и������û�Ȩ��ʵ�����ݡ�
        /// </summary>
        /// <param name="entity">Ҫ���µ�ʵ�����ݡ�</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// ��������ʵ������δ�����ݿ����ҵ����׳� InvalidOperationException �쳣��
        /// ��Ҫ��������ʵ��������������ݿ��У������ AddAuthorizationEntity ������
        /// </remarks>
        public void UpdateUserAuthorizationEntity(UserAuthorizationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            UserAuthorizationEntity targetEntity = QueryUserAuthorizationEntity(entity.Username);
            if (targetEntity == null)
            {
                // ����������ʵ�岻�����ݿ��С�
                throw new InvalidOperationException("����������ʵ�岻�����ݿ��С�");
            }

            // ���Ƹ������û�Ȩ��ʵ�����ݵ����ݿ��С�
            targetEntity.PasswordHash = new byte[entity.PasswordHash.Length];
            Buffer.BlockCopy(entity.PasswordHash, 0, targetEntity.PasswordHash, 0, entity.PasswordHash.Length);
            targetEntity.Group = entity.Group;

            // �������ݿ⡣
            SaveChanges();
        }

        /// <summary>
        /// �������ݿ��и������û���Ϣʵ�����ݡ�
        /// </summary>
        /// <param name="entity">Ҫ���µ�ʵ�����ݡ�</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// ��������ʵ�����ݲ������ݿ��У��׳� InvalidOperationException �쳣��
        /// ��Ҫ��������ʵ��������������ݿ��У������ AddUserProfileEntity ������
        /// </remarks>
        public void UpdateUserProfileEntity(UserProfileEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            UserProfileEntity target = QueryUserProfileEntity(entity.Username);
            if (target == null)
                throw new InvalidOperationException("�������û���Ϣʵ�����δ�����ݿ����ҵ���");

            // ����ʵ��������ݡ�
            target.ProfileFileName = entity.ProfileFileName;
            SaveChanges();
        }

        /// <summary>
        /// �������ݿ��и����Ķ�����Ϣʵ�����ݡ�
        /// </summary>
        /// <param name="entity">Ҫ���µĶ�����Ϣʵ�����ݡ�</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// ��������ʵ�����δ�����ݿ����ҵ����׳� InvalidOperationException �쳣��
        /// ��Ҫ��������ʵ�������������ݿ⣬����� AddTeamProfileEntity ������
        /// </remarks>
        public void UpdateTeamProfileEntity(TeamProfileEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            TeamProfileEntity target = QueryTeamProfileEntity(entity.Name);
            if (target == null)
                throw new InvalidOperationException("�����Ķ�����Ϣʵ������δ�����ݿ����ҵ���");

            // ����ʵ��������ݡ�
            target.ProfileFile = entity.ProfileFile;
            SaveChanges();
        }

        /// <summary>
        /// �����ݿ���ɾ���������û�Ȩ��ʵ�����ݡ�
        /// </summary>
        /// <param name="entity">Ҫɾ����ʵ�����ݡ�</param>
        /// <exception cref="ArgumentNullException"/>
        public void RemoveUserAuthorizationEntity(UserAuthorizationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            UserAuthorization.Remove(entity);
            SaveChanges();
        }

        /// <summary>
        /// �����ݿ���ɾ���������û���Ϣʵ�����ݡ�
        /// </summary>
        /// <param name="entity">Ҫɾ�����û���Ϣʵ�����ݡ�</param>
        /// <exception cref="ArgumentNullException"/>
        public void RemoveUserProfileEntity(UserProfileEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            UserProfiles.Remove(entity);
        }

        /// <summary>
        /// �����ݿ���ɾ�������Ķ�����Ϣʵ�����ݡ�
        /// </summary>
        /// <param name="entity">Ҫɾ���Ķ�����Ϣʵ�����ݡ�</param>
        /// <exception cref="ArgumentNullException"/>
        public void RemoveTeamProfileEntity(TeamProfileEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            TeamProfiles.Remove(entity);
        }

        /// <summary>
        /// �����ݿ���ɾ���������û� - �����ϵʵ�����
        /// </summary>
        /// <param name="entity">Ҫɾ�����û� - �����ϵʵ�����</param>
        /// <exception cref="ArgumentNullException"/>
        public void RemoveUserTeamRelationEntity(UserTeamRelationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            UserTeams.Remove(entity);
            SaveChanges();
        }

        /// <summary>
        /// ��ȡ�������û�Ȩ�����ݼ���
        /// </summary>
        protected virtual DbSet<UserAuthorizationEntity> UserAuthorization { get; set; }

        /// <summary>
        /// ��ȡ�������û���Ϣ���ݼ���
        /// </summary>
        protected virtual DbSet<UserProfileEntity> UserProfiles { get; set; }

        /// <summary>
        /// ��ȡ�����ö�����Ϣ���ݼ���
        /// </summary>
        protected virtual DbSet<TeamProfileEntity> TeamProfiles { get; set; }

        /// <summary>
        /// ��ȡ�������û� - �����ϵ���ݼ���
        /// </summary>
        protected virtual DbSet<UserTeamRelationEntity> UserTeams { get; set; }
    }
}
