using RedisChaeGrowthplan.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedisChaeGrowthplan.Models
{
    //[Obsolete] //特性分成两类： 一类的影响编译器的 ， 另一类的是程序实现过程中的
    /// <summary>
    /// 用户表
    /// </summary>

    [MyCache]
    public class Users
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 组织Id
        /// </summary>
        public string OrganizationId { get; set; }

        /// <summary>
        /// 真实用户名
        /// </summary>
        public string TrueName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///密码
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>

        public bool IsDeleted { get; set; }

        /// <summary>
        /// 号码
        /// </summary>
        public string PhoneNumber { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }

        [MaxLength(127)]
        public string CreateUser { get; set; }

        public DateTime? CreateTime { get; set; }

        [MaxLength(127)]
        public string DeleteUser { get; set; }
        public DateTime? DeleteTime { get; set; }

        public string Avatar { get; set; }
        public string FilialeId { get; set; }

        public string WXOpenId { get; set; }

        //public int? AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Discriminator { get; set; }
        public string Email { get; set; }
        //public bool EmailConfirmed { get; set; }

        //public bool? LockoutEnabled { get; set; }
        //public DateTime? LockoutEnd { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        //public bool? PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        //public bool? TwoFactorEnabled { get; set; }

        public string Position { get; set; }



        public string ParentId { get; set; }
        //public bool IsDisplay { get; set; }
        public string QQ { get; set; }
        public string Wechat { get; set; }
        public string JobNumber { get; set; }
        public string PinYin { get; set; }
    }

}
