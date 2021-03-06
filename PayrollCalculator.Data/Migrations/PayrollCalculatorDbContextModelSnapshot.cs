// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PayrollCalculator.Data;

namespace PayrollCalculator.Data.Migrations
{
    [DbContext(typeof(PayrollCalculatorDbContext))]
    partial class PayrollCalculatorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("PayrollCalculator.Data.DependentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Dependents");
                });

            modelBuilder.Entity("PayrollCalculator.Data.EmployeeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PayrollCalculator.Data.PreviewCostEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("DependentCost")
                        .HasColumnType("REAL");

                    b.Property<double>("EmployeeCost")
                        .HasColumnType("REAL");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("TotalCost")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("PreviewCosts");
                });

            modelBuilder.Entity("PayrollCalculator.Data.DependentEntity", b =>
                {
                    b.HasOne("PayrollCalculator.Data.EmployeeEntity", "Employee")
                        .WithMany("Dependents")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PayrollCalculator.Data.PreviewCostEntity", b =>
                {
                    b.HasOne("PayrollCalculator.Data.EmployeeEntity", "Employee")
                        .WithOne("PreviewCost")
                        .HasForeignKey("PayrollCalculator.Data.PreviewCostEntity", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PayrollCalculator.Data.EmployeeEntity", b =>
                {
                    b.Navigation("Dependents");

                    b.Navigation("PreviewCost");
                });
#pragma warning restore 612, 618
        }
    }
}
