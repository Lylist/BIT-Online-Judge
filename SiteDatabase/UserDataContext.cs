namespace BITOJ.Data
{
    using BITOJ.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// 为用户数据库数据提供上下文支持。
    /// </summary>
    public partial class UserDataContext : DbContext
    {
        /// <summary>
        /// 初始化 UserDataContext 类的新实例。
        /// </summary>
        public UserDataContext()
            : base("name=UserDataContext")
        {
        }

        /// <summary>
        /// 使用给定的用户名查询用户权限实体数据。
        /// </summary>
        /// <param name="username">要查询的用户名。</param>
        /// <returns>用户名对应的用户权限实体数据。如果未找到给定用户名的用户权限实体数据，返回 null。</returns>
        /// <exception cref="ArgumentNullException"/>
        public UserAuthorizationEntity QueryUserAuthorizationEntity(string username)
        {
            if (username == null)
                throw new ArgumentNullException(nameof(username));

            return UserAuthorization.Find(username);
        }

        /// <summary>
        /// 使用给定的用户组查询用户权限实体数据。
        /// </summary>
        /// <param name="group">要查询的用户组。</param>
        /// <returns>给定用户组中所有的用户实体数据。</returns>
        public IList<UserAuthorizationEntity> QueryUserAuthorizationEntities(UserGroup userGroup)
        {
            var entities = from item in UserAuthorization
                           where item.Group == userGroup
                           select item;
            return entities.ToList();
        }

        /// <summary>
        /// 将给定的用户权限实体数据添加至数据库中。
        /// </summary>
        /// <param name="entity">要添加的用户权限实体数据。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// 若给定的实体数据已经存在于数据库中，抛出 InvalidOperationException 异常。
        /// 若要更新给定的实体数据，请使用 UpdateUserAuthorizationEntity 方法。
        /// </remarks>
        public void AddAuthorizationEntity(UserAuthorizationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (QueryUserAuthorizationEntity(entity.Username) != null)
                throw new InvalidOperationException("给定的实体数据已经存在于数据库中。");

            UserAuthorization.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// 更新数据库中给定的用户权限实体数据。
        /// </summary>
        /// <param name="entity">要更新的实体数据。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// 若给定的实体数据未在数据库中找到，抛出 InvalidOperationException 异常。
        /// 若要将给定的实体数据添加至数据库中，请调用 AddAuthorizationEntity 方法。
        /// </remarks>
        public void UpdateUserAuthorizationEntity(UserAuthorizationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            UserAuthorizationEntity targetEntity = QueryUserAuthorizationEntity(entity.Username);
            if (targetEntity == null)
            {
                // 给定的数据实体不在数据库中。
                throw new InvalidOperationException("给定的数据实体不在数据库中。");
            }

            // 复制给定的用户权限实体数据到数据库中。
            targetEntity.PasswordHash = new byte[entity.PasswordHash.Length];
            Buffer.BlockCopy(entity.PasswordHash, 0, targetEntity.PasswordHash, 0, entity.PasswordHash.Length);
            targetEntity.Group = entity.Group;

            // 更新数据库。
            SaveChanges();
        }

        /// <summary>
        /// 从数据库中删除给定的用户权限实体数据。
        /// </summary>
        /// <param name="entity">要删除的实体数据。</param>
        /// <exception cref="ArgumentNullException"/>
        public void RemoveUserAuthorizationEntity(UserAuthorizationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            UserAuthorization.Remove(entity);
            SaveChanges();
        }

        /// <summary>
        /// 获取用户权限数据集。
        /// </summary>
        protected virtual DbSet<UserAuthorizationEntity> UserAuthorization { get; set; }
    }
}
