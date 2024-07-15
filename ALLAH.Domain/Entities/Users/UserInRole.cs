using ALLAH.Domain.Entities.Common;

namespace ALLAH.Domain.Entities.Users
{
    public class UserInRole :BaseEntity
    {
        public long  Id { get; set; }
        public long UserId { get; set; } 
        public long RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

    }
}
