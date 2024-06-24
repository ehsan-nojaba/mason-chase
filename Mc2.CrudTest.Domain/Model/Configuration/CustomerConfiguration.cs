using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2.CrudTest.DomainModel.Model.Configuration
{
    public class CustomerConfiguration:IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.CustomerId);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(25);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(25);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(15);
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.BankAccountNumber).IsRequired().HasMaxLength(16);
        }
    }
}
