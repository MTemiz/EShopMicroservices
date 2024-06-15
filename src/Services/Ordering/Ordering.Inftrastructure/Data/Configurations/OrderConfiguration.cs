using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Inftrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>()
        .WithMany()
        .HasForeignKey(c => c.CustomerId)
        .IsRequired();

        builder.HasMany<OrderItem>()
        .WithOne()
        .HasForeignKey(c => c.OrderId);

        builder.ComplexProperty(c => c.OrderName, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
            .HasColumnName(nameof(Order.OrderName))
            .HasMaxLength(100)
            .IsRequired();
        });

        builder.ComplexProperty(c => c.ShippingAddress, shippingAddressBuilder =>
        {
            shippingAddressBuilder.Property(a => a.FirstName)
            .HasMaxLength(50)
            .IsRequired();

            shippingAddressBuilder.Property(a => a.LastName)
            .HasMaxLength(50)
            .IsRequired();

            shippingAddressBuilder.Property(a => a.EmailAddres)
            .HasMaxLength(50);

            shippingAddressBuilder.Property(a => a.AddressLine)
            .HasMaxLength(180)
            .IsRequired();

            shippingAddressBuilder.Property(a => a.Country)
            .HasMaxLength(50);

            shippingAddressBuilder.Property(a => a.State)
            .HasMaxLength(50);

            shippingAddressBuilder.Property(a => a.ZipCode)
            .HasMaxLength(5)
            .IsRequired();
        });

        builder.ComplexProperty(c => c.BillingAddress, billingAddressBuilder =>
        {
            billingAddressBuilder.Property(a => a.FirstName)
            .HasMaxLength(50)
            .IsRequired();

            billingAddressBuilder.Property(a => a.LastName)
            .HasMaxLength(50)
            .IsRequired();

            billingAddressBuilder.Property(a => a.EmailAddres)
            .HasMaxLength(50);

            billingAddressBuilder.Property(a => a.AddressLine)
            .HasMaxLength(180)
            .IsRequired();

            billingAddressBuilder.Property(a => a.Country)
            .HasMaxLength(50);

            billingAddressBuilder.Property(a => a.State)
            .HasMaxLength(50);

            billingAddressBuilder.Property(a => a.ZipCode)
            .HasMaxLength(5)
            .IsRequired();
        });

        builder.ComplexProperty(c => c.Payment, paymentBuilder =>
        {
            paymentBuilder.Property(a => a.CardName).HasMaxLength(50);

            paymentBuilder.Property(a => a.CardNumber).HasMaxLength(24).IsRequired();

            paymentBuilder.Property(a => a.Expiration).HasMaxLength(10);

            paymentBuilder.Property(a => a.CVV).HasMaxLength(3);

            paymentBuilder.Property(a => a.PaymentMethod);
        });

        builder.Property(c => c.Status)
        .HasDefaultValue(OrderStatus.Draft)
        .HasConversion(s => s.ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));
    }
}