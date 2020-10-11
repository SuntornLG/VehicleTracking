using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.ModelConfiguration
{
    public class RoleMasterConfiguration : IEntityTypeConfiguration<RoleMaster>
    {
        public void Configure(EntityTypeBuilder<RoleMaster> builder)
        {
            builder.HasData(new RoleMaster
            {
                RoleId = 1,
                RoleCode = "ADMIN",
                RoleName = "Administrator"

            }, new RoleMaster
            {
                RoleId = 2,
                RoleCode = "USER",
                RoleName = "User"
            }, new RoleMaster
            {
                RoleId = 3,
                RoleCode = "SUPERUSER",
                RoleName = "SuperUser"
            });
        }
    }
}
