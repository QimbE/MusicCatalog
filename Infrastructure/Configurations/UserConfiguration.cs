using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // PK
        builder.HasKey(u => u.Id);

        builder
            .HasIndex(u => u.Email)
            .IsUnique();

        #region Properties
        
        builder.Property(u => u.Email)
            .HasMaxLength(254)
            .IsRequired();

        builder.Property(u => u.Username)
            .HasMaxLength(100)
            .IsRequired();
        
        // wrong logic, check "to do" in UserRole enum
        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<int>();

        #endregion
    }
}