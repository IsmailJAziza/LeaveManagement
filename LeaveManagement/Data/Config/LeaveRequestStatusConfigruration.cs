using LeaveManagement.Data.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LeaveManagement.Data.Config
{
    public class LeaveRequestStatusConfigruration : IEntityTypeConfiguration<LeaveRequestStatus>
    {
        public void Configure(EntityTypeBuilder<LeaveRequestStatus> builder)
        {

            builder.HasData(new LeaveRequestStatus
            {
                Id = 1,
                Name = "Pending",
            },
            new LeaveRequestStatus
            {
                Id = 2,
                Name = "Approved",
            },
            new LeaveRequestStatus
            {
                Id = 3,
                Name = "Declined",
            },
            new LeaveRequestStatus
            {
                Id = 4,
                Name = "Cancelled",
            });

        }
    }

}
