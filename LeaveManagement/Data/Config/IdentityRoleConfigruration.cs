using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LeaveManagement.Data.Config
{
    public class IdentityRoleConfigruration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {

            builder.HasData(
              new IdentityRole
              {
                  Id = "6d9ed3ff-bebb-42bc-ad07-0255bb0f7edb",
                  Name = "Employee",
                  NormalizedName = "EMPLOYEE"
              },
              new IdentityRole
              {
                  Id = "cc4fcb01-de88-4c20-b4ac-8df5c2a65160",
                  Name = "Supervisor",
                  NormalizedName = "SUPERVISOR"
              },
              new IdentityRole
              {
                  Id = "e9f639de-624f-4a4e-b8bf-2381725462f1",
                  Name = "Administrator",
                  NormalizedName = "ADMINISTRATOR"
              }
          );

        }
    }

}
