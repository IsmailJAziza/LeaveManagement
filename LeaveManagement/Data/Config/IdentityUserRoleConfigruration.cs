using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LeaveManagement.Data.Config
{
    public class IdentityUserRoleConfigruration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {

          //  builder.HasData(
          //new IdentityUserRole<string>
          //{
          //    RoleId = "e9f639de-624f-4a4e-b8bf-2381725462f1",
          //    UserId = "408aa945-3d84-4421-8342-7269ec64d949"
          //});

        }
    }

}
