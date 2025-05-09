using LeaveManagement.Data.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LeaveManagement.Data.Config
{
    public class ApplicationUserConfigruration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            builder.HasData(new ApplicationUser
            {
                Id = "408aa945-3d84-4421-8342-7269ec64d949",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                UserName = "admin@localhost.com",
                PasswordHash = "AQAAAAIAAYagAAAAEApoOrRFC1N4RRjDSnWORXV5Glbm6fPpLj3TbDaqqd9IVwWcsAYxA9VjOvynEUf3jg==",
                EmailConfirmed = true,
                FirstName = "Default",
                LastName = "Admin",
                DateOfBirth = new DateOnly(1995, 02, 09),
                ConcurrencyStamp = "stamp-admin-user", // Adding a static value
                SecurityStamp = "SECURITYSTAMP", // Adding a static value
            });

        }
    }

}
