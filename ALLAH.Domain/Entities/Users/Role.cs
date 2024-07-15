using ALLAH.Domain.Entities.Common;

namespace ALLAH.Domain.Entities.Users
{
    public class Role:BaseEntity
    {
        
        public string Name { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set;}

    }
}
