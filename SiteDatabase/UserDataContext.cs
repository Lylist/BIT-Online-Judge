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
        /// ���������û�Ȩ��ʵ��������������ݿ��С�
        /// </summary>
        /// <param name="entity">Ҫ��ӵ��û�Ȩ��ʵ�����ݡ�</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// ��������ʵ�������Ѿ����������ݿ��У��׳� InvalidOperationException �쳣��
        /// ��Ҫ���¸�����ʵ�����ݣ���ʹ�� UpdateUserAuthorizationEntity ������
        /// </remarks>
        public void AddAuthorizationEntity(UserAuthorizationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (QueryUserAuthorizationEntity(entity.Username) != null)
                throw new InvalidOperationException("������ʵ�������Ѿ����������ݿ��С�");

            UserAuthorization.Add(entity);
            SaveChanges();
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
        /// ��ȡ�û�Ȩ�����ݼ���
        /// </summary>
        protected virtual DbSet<UserAuthorizationEntity> UserAuthorization { get; set; }
    }
}
