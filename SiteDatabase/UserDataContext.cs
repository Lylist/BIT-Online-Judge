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
        /// 将给定的用户权限实体数据添加至数据库中。
        /// </summary>
        /// <param name="entity">要添加的用户权限实体数据。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// 若给定的实体数据已经存在于数据库中，抛出 InvalidOperationException 异常。
        /// 若要更新给定的实体数据，请使用 UpdateUserAuthorizationEntity 方法。
        /// </remarks>
        public void AddUserAuthorizationEntity(UserAuthorizationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (QueryUserAuthorizationEntity(entity.Username) != null)
                throw new InvalidOperationException("给定的实体数据已经存在于数据库中。");

            UserAuthorization.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// 将给定的用户信息实体数据添加至数据库中。
        /// </summary>
        /// <param name="entity">要添加的用户信息实体数据。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// 若给定的实体数据已经存在于数据库中，抛出 InvalidOperationException 异常。
        /// 若要更新给定的实体数据，请使用 UpdateUserProfileEntity 方法。
        /// </remarks>
        public void AddUserProfileEntity(UserProfileEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (QueryUserProfileEntity(entity.Username) != null)
                throw new InvalidOperationException("给定的实体对象已经存在于数据库中。");

            UserProfiles.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// 将给定的队伍信息实体数据添加至数据库中。
        /// </summary>
        /// <param name="entity">要添加的队伍信息实体数据。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// 若给定的实体对象已经存在于数据库中，抛出 InvalidOperationException 异常。
        /// 若要更新数据库中对应的实体数据，请调用 UpdateTeamProfileEntity 方法。
        /// </remarks>
        public void AddTeamProfileEntity(TeamProfileEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (QueryTeamProfileEntity(entity.Name) != null)
                throw new InvalidOperationException("给定的队伍信息实体对象已经存在于数据库中。");

            TeamProfiles.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// 将给定的用户 - 队伍关系实体添加至数据库中。
        /// </summary>
        /// <param name="entity">要添加的用户 - 队伍关系实体对象。</param>
        /// <exception cref="ArgumentNullException"/>
        public void AddUserTeamRelationEntity(UserTeamRelationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            UserTeams.Add(entity);
            SaveChanges();
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
        /// 使用指定的用户名查询用户信息实体对象。
        /// </summary>
        /// <param name="username">要查询的用户名。</param>
        /// <returns>对应的用户信息实体对象。如果给定的用户名未在数据库中找到，返回 null。</returns>
        /// <exception cref="ArgumentNullException"/>
        public UserProfileEntity QueryUserProfileEntity(string username)
        {
            if (username == null)
                throw new ArgumentNullException(nameof(username));

            return UserProfiles.Find(username);
        }

        /// <summary>
        /// 使用给定的队伍 ID 查询队伍信息实体对象。
        /// </summary>
        /// <param name="teamId">要查询的队伍 ID 。</param>
        /// <returns>与队伍 ID 相对应的队伍信息实体对象。若给定的队伍 ID 未在数据库中找到，返回 null 。</returns>
        public TeamProfileEntity QueryTeamProfileEntity(int teamId)
        {
            return TeamProfiles.Find(teamId);
        }

        /// <summary>
        /// 使用给定的队伍名称查询队伍信息实体数据。
        /// </summary>
        /// <param name="teamName">要查询的队伍名称。</param>
        /// <returns>
        /// 一个列表，该列表包含了给定队伍名称所对应的队伍信息实体对象。
        /// 如果给定的队伍名称未在数据库中找到，返回 null。
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
        /// 使用给定的用户名查询指定用户的所有用户 - 队伍关系实体对象。
        /// </summary>
        /// <param name="username">要查询的用户名。</param>
        /// <returns>一个列表，该列表包含了与指定用户相关联的所有用户 - 队伍关系实体对象。</returns>
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
        /// 使用给定的队伍 ID 查询给定队伍的所有用户 - 队伍关系实体对象。
        /// </summary>
        /// <param name="teamId">要查询的队伍 ID 。</param>
        /// <returns>一个列表，该列表包含了与指定队伍相关联的所有用户 - 队伍关系实体对象。</returns>
        public IList<UserTeamRelationEntity> QueryUserTeamRelationEntitiesByTeamId(int teamId)
        {
            var entities = from item in UserTeams
                           where item.TeamId == teamId
                           select item;
            return entities.ToList();
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
        /// 更新数据库中给定的用户信息实体数据。
        /// </summary>
        /// <param name="entity">要更新的实体数据。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// 若给定的实体数据不在数据库中，抛出 InvalidOperationException 异常。
        /// 若要将给定的实体数据添加至数据库中，请调用 AddUserProfileEntity 方法。
        /// </remarks>
        public void UpdateUserProfileEntity(UserProfileEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            UserProfileEntity target = QueryUserProfileEntity(entity.Username);
            if (target == null)
                throw new InvalidOperationException("给定的用户信息实体对象未在数据库中找到。");

            // 更新实体对象数据。
            target.ProfileFileName = entity.ProfileFileName;
            SaveChanges();
        }

        /// <summary>
        /// 更新数据库中给定的队伍信息实体数据。
        /// </summary>
        /// <param name="entity">要更新的队伍信息实体数据。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <remarks>
        /// 若给定的实体对象未在数据库中找到，抛出 InvalidOperationException 异常。
        /// 若要将给定的实体对象添加至数据库，请调用 AddTeamProfileEntity 方法。
        /// </remarks>
        public void UpdateTeamProfileEntity(TeamProfileEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            TeamProfileEntity target = QueryTeamProfileEntity(entity.Name);
            if (target == null)
                throw new InvalidOperationException("给定的队伍信息实体数据未在数据库中找到。");

            // 更新实体对象数据。
            target.ProfileFile = entity.ProfileFile;
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
        /// 从数据库中删除给定的用户信息实体数据。
        /// </summary>
        /// <param name="entity">要删除的用户信息实体数据。</param>
        /// <exception cref="ArgumentNullException"/>
        public void RemoveUserProfileEntity(UserProfileEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            UserProfiles.Remove(entity);
        }

        /// <summary>
        /// 从数据库中删除给定的队伍信息实体数据。
        /// </summary>
        /// <param name="entity">要删除的队伍信息实体数据。</param>
        /// <exception cref="ArgumentNullException"/>
        public void RemoveTeamProfileEntity(TeamProfileEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            TeamProfiles.Remove(entity);
        }

        /// <summary>
        /// 从数据库中删除给定的用户 - 队伍关系实体对象。
        /// </summary>
        /// <param name="entity">要删除的用户 - 队伍关系实体对象。</param>
        /// <exception cref="ArgumentNullException"/>
        public void RemoveUserTeamRelationEntity(UserTeamRelationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            UserTeams.Remove(entity);
            SaveChanges();
        }

        /// <summary>
        /// 获取或设置用户权限数据集。
        /// </summary>
        protected virtual DbSet<UserAuthorizationEntity> UserAuthorization { get; set; }

        /// <summary>
        /// 获取或设置用户信息数据集。
        /// </summary>
        protected virtual DbSet<UserProfileEntity> UserProfiles { get; set; }

        /// <summary>
        /// 获取或设置队伍信息数据集。
        /// </summary>
        protected virtual DbSet<TeamProfileEntity> TeamProfiles { get; set; }

        /// <summary>
        /// 获取或设置用户 - 队伍关系数据集。
        /// </summary>
        protected virtual DbSet<UserTeamRelationEntity> UserTeams { get; set; }
    }
}
