using CoolLibrary.Domain.Entities;
using CoolLibrary.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoolLibrary.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for Customer entity
/// </summary>
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        // Primary key
        builder.HasKey(c => c.CustomerId);
        
        // Properties
        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(c => c.Phone)
            .HasMaxLength(20);
            
        builder.Property(c => c.Address)
            .HasMaxLength(300);
            
        builder.Property(c => c.City)
            .HasMaxLength(100);
            
        builder.Property(c => c.PostalCode)
            .HasMaxLength(20);
            
        builder.Property(c => c.MembershipDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");
            
        builder.Property(c => c.MembershipStatus)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(MembershipStatus.Active);
            
        builder.Property(c => c.MaxBooksAllowed)
            .IsRequired()
            .HasDefaultValue(5);
            
        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");
            
        builder.Property(c => c.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");
        
        // Indexes
        builder.HasIndex(c => c.Email)
            .IsUnique()
            .HasDatabaseName("IX_Customers_Email");
            
        builder.HasIndex(c => new { c.LastName, c.FirstName })
            .HasDatabaseName("IX_Customers_Name");
            
        builder.HasIndex(c => c.MembershipStatus)
            .HasDatabaseName("IX_Customers_MembershipStatus");
        
        // Ignore computed properties
        builder.Ignore(c => c.FullName);
        builder.Ignore(c => c.CurrentLoanCount);
        builder.Ignore(c => c.CanBorrowMoreBooks);
        
        // Check constraints
        builder.HasCheckConstraint("CK_Customers_MaxBooksAllowed", "[MaxBooksAllowed] > 0");
    }
}