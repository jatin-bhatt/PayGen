using Moq;
using NodaTime;
using PayGen.Domain.AggregatesModel.EmployeeAggregate;
using System;
using Xunit;

namespace PayGen.Domain.Tests.AggregatesModel.EmployeeAggregate {
    public class EmployeeTests {
        TaxSlab[] FakeTaxSlabs;

        public EmployeeTests() {
            FakeTaxSlabs = new TaxSlab[] {
                new TaxSlab(0, 14000, 0.105M),
                new TaxSlab(14000, 48000, 0.175M),
                new TaxSlab(48000, 70000, 0.3M),
                new TaxSlab(70000, 180000, 0.33M),
                new TaxSlab(180000, -1, 0.39M),
            };
        }

        [Fact]
        public void Constructor_ReceivesFirstNameAsNullOrEmpty_ThrowsArgumentNullException() {
            // Arrange
            int fakeId = 1;
            string fakeFirstName = String.Empty;
            string fakeLastName = "fakeLastName";
            int fakeAnnualSalary = 1000;
            decimal fakeSuperRate = 9.5M;
            string fakeMonth = "January";
            Mock<IClock> fakeClock = new Mock<IClock>();
            fakeClock.Setup(c => c.GetCurrentInstant()).Verifiable();



            //Act Assert
            Assert.Throws<ArgumentNullException>(() => 
                new Employee(
                    fakeId,
                    fakeFirstName,
                    fakeLastName,
                    fakeAnnualSalary,
                    fakeSuperRate,
                    fakeMonth,
                    fakeClock.Object,
                    FakeTaxSlabs
                    ));
        }

        [Fact]
        public void Constructor_ReceivesLastNameAsNullOrEmpty_ThrowsArgumentNullException() {
            // Arrange
            int fakeId = 1;
            string fakeFirstName = "fakeFirstName";
            string fakeLastName = String.Empty;
            int fakeAnnualSalary = 1000;
            decimal fakeSuperRate = 9.5M;
            string fakeMonth = "January";
            Mock<IClock> fakeClock = new Mock<IClock>();
            fakeClock.Setup(c => c.GetCurrentInstant()).Verifiable();

            //Act Assert
            Assert.Throws<ArgumentNullException>(() =>
                new Employee(
                    fakeId,
                    fakeFirstName,
                    fakeLastName,
                    fakeAnnualSalary,
                    fakeSuperRate,
                    fakeMonth,
                    fakeClock.Object,
                    FakeTaxSlabs
                    ));
        }

        [Fact]
        public void Constructor_ReceivesMonthAsNullOrEmpty_ThrowsArgumentNullException() {
            // Arrange
            int fakeId = 1;
            string fakeFirstName = "fakeFirstName";
            string fakeLastName = "fakeLastName";
            int fakeAnnualSalary = 1000;
            decimal fakeSuperRate = 9.5M;
            string fakeMonth = string.Empty;
            Mock<IClock> fakeClock = new Mock<IClock>();
            fakeClock.Setup(c => c.GetCurrentInstant()).Verifiable();

            //Act Assert
            Assert.Throws<ArgumentNullException>(() =>
                new Employee(
                    fakeId,
                    fakeFirstName,
                    fakeLastName,
                    fakeAnnualSalary,
                    fakeSuperRate,
                    fakeMonth,
                    fakeClock.Object,
                    FakeTaxSlabs
                    ));
        }

        [Fact]
        public void Constructor_ReceivesNullClockContext_ThrowsArgumentNullException() {
            // Arrange
            int fakeId = 1;
            string fakeFirstName = "fakeFirstName";
            string fakeLastName = "fakeLastName";
            int fakeAnnualSalary = 1000;
            decimal fakeSuperRate = 9.5M;
            string fakeMonth = "January";

            //Act Assert
            _ = Assert.Throws<ArgumentNullException>(() =>
                  new Employee(
                      fakeId,
                      fakeFirstName,
                      fakeLastName,
                      fakeAnnualSalary,
                      fakeSuperRate,
                      fakeMonth,
                      null,
                      FakeTaxSlabs
                      ));
        }

        [Fact]
        public void SetPayMonth_InputMonth_SetNewValue() {
            // Arrange
            int fakeId = 1;
            string fakeFirstName = "fakeFirstName";
            string fakeLastName = "fakeLastName";
            int fakeAnnualSalary = 1000;
            decimal fakeSuperRate = 9.5M;
            string fakeMonth = "January";

            Mock<IClock> fakeClock = new Mock<IClock>();
            fakeClock.Setup(c => c.GetCurrentInstant()).Verifiable();

            var employee = new Employee(
                    fakeId,
                    fakeFirstName,
                    fakeLastName,
                    fakeAnnualSalary,
                    fakeSuperRate,
                    fakeMonth,
                    fakeClock.Object,
                    FakeTaxSlabs
                    );

            string fakeNewMonth = "February";
            var expected = PayMonth.FromName(fakeNewMonth);

            // Act
            employee.SetPayMonth(fakeNewMonth);

            //Assert
            Assert.Equal(expected, employee.PayMonth);
        }

        [Fact]
        public void GetFullName_ReturnsName() {
            // Arrange
            int fakeId = 1;
            string fakeFirstName = "fakeFirstName";
            string fakeLastName = "fakeLastName";
            int fakeAnnualSalary = 1000;
            decimal fakeSuperRate = 9.5M;
            string fakeMonth = "January";

            Mock<IClock> fakeClock = new Mock<IClock>();
            fakeClock.Setup(c => c.GetCurrentInstant()).Verifiable();

            var employee = new Employee(
                    fakeId,
                    fakeFirstName,
                    fakeLastName,
                    fakeAnnualSalary,
                    fakeSuperRate,
                    fakeMonth,
                    fakeClock.Object,
                    FakeTaxSlabs
                    );

            var expected = "fakeFirstName fakeLastName";

            // Act
            string actual = employee.GetFullName();

            //Assert
            Assert.Equal(expected, actual);
        }

        public static TheoryData<int, decimal> TheoryDataAnnualSalaryAndExpectedMonthlyTax =>
            new()
            {
                { 10000, 87.50M },
                { 15000, 137.08M },
                { 20000, 210.00M },
                { 30000, 355.83M },
                { 37000, 457.92M },
                { 40000, 501.67M },
                { 40500, 508.96M },
                { 50050, 669.58M },
                { 60789, 938.06M },
                { 74980, 1305.28M },
                { 88600, 1679.83M },
                { 99999, 1993.31M },
                { 120010, 2543.61M },
                { 138710, 3057.86M },
                { 158000, 3588.33M },
                { 175000, 4055.83M },
                { 190000, 4518.33M },
                { 2000000, 63343.33M },
            };

        [Theory]
        [MemberData(nameof(TheoryDataAnnualSalaryAndExpectedMonthlyTax))]
        public void GetTax_WhenAnnualSalaryIsSupplied_ReturnsExpectedTaxAmount(int annualSalary, decimal expected) {
            // Arrange
            int fakeId = 1;
            string fakeFirstName = "fakeFirstName";
            string fakeLastName = "fakeLastName";
            decimal fakeSuperRate = 9.5M;
            string fakeMonth = "January";

            Mock<IClock> fakeClock = new Mock<IClock>();
            fakeClock.Setup(c => c.GetCurrentInstant()).Verifiable();

            var employee = new Employee(
                    fakeId,
                    fakeFirstName,
                    fakeLastName,
                    annualSalary,
                    fakeSuperRate,
                    fakeMonth,
                    fakeClock.Object,
                    FakeTaxSlabs
                    );

            // Act
            var actual = employee.GetTax();

            //Assert
            Assert.Equal(expected, actual);
        }

        public static TheoryData<int, decimal> TheoryDataAnnualSalaryAndExpectedGrossIncome =>
            new()
            {
                { 10000, 833.33M },
                { 15000, 1250M },
                { 20000, 1666.67M },
                { 30000, 2500M },
                { 37000, 3083.33M },
                { 40000, 3333.33M },
                { 40500, 3375M },
                { 50050, 4170.83M },
                { 60789, 5065.75M },
                { 74980, 6248.33M },
                { 88600, 7383.33M },
                { 99999, 8333.25M },
                { 120010, 10000.83M },
                { 138710, 11559.17M },
                { 158000, 13166.67M },
                { 175000, 14583.33M },
                { 190000, 15833.33M },
                { 2000000, 166666.67M },
            };

        [Theory]
        [MemberData(nameof(TheoryDataAnnualSalaryAndExpectedGrossIncome))]
        public void GetGrossIncome_WhenAnnualSalaryIsSupplied_ReturnsExpectedGrossIncome(int annualSalary, decimal expected) {
            // Arrange
            int fakeId = 1;
            string fakeFirstName = "fakeFirstName";
            string fakeLastName = "fakeLastName";
            decimal fakeSuperRate = 9.5M;
            string fakeMonth = "January";

            Mock<IClock> fakeClock = new Mock<IClock>();
            fakeClock.Setup(c => c.GetCurrentInstant()).Verifiable();

            var employee = new Employee(
                    fakeId,
                    fakeFirstName,
                    fakeLastName,
                    annualSalary,
                    fakeSuperRate,
                    fakeMonth,
                    fakeClock.Object,
                    FakeTaxSlabs
                    );

            // Act
            var actual = employee.GetGrossIncome();

            //Assert
            Assert.Equal(expected, actual);
        }

        public static TheoryData<int, decimal> TheoryDataAnnualSalaryAndExpectedNetIncome =>
            new()
            {
                { 10000, 745.83M },
                { 15000, 1112.92M },
                { 37000, 2625.41M },
                { 40500, 2866.04M },
                { 60789, 4127.69M },
                { 74980, 4943.05M },
                { 99999, 6339.94M },
                { 138710, 8501.31M },
                { 158000, 9578.34M },
                { 190000, 11315.00M },
            };

        [Theory]
        [MemberData(nameof(TheoryDataAnnualSalaryAndExpectedNetIncome))]
        public void GetNetIncome_WhenAnnualSalaryIsSupplied_ReturnsExpectedNetIncome(int annualSalary, decimal expected) {
            // Arrange
            int fakeId = 1;
            string fakeFirstName = "fakeFirstName";
            string fakeLastName = "fakeLastName";
            decimal fakeSuperRate = 9.5M;
            string fakeMonth = "January";

            Mock<IClock> fakeClock = new Mock<IClock>();
            fakeClock.Setup(c => c.GetCurrentInstant()).Verifiable();

            var employee = new Employee(
                    fakeId,
                    fakeFirstName,
                    fakeLastName,
                    annualSalary,
                    fakeSuperRate,
                    fakeMonth,
                    fakeClock.Object,
                    FakeTaxSlabs
                    );

            // Act
            var actual = employee.GetNetIncome();

            //Assert
            Assert.Equal(expected, actual);
        }

        public static TheoryData<int, decimal, decimal> TheoryDataAnnualSalarySuperRateAndExpectedSuperAmount =>
            new()
            {
                { 10000, 5M, 41.67M },
                { 50000, 6M, 250.00M },
                { 100000, 7M, 583.33M },
                { 150000, 8M , 1000.00M },
                { 200000, 9M, 1500.00M },
            };

        [Theory]
        [MemberData(nameof(TheoryDataAnnualSalarySuperRateAndExpectedSuperAmount))]
        public void GetSuper_WhenAnnualSalaryIsSupplied_ReturnsExpectedSuper(int annualSalary, decimal superRate, decimal expected) {
            // Arrange
            int fakeId = 1;
            string fakeFirstName = "fakeFirstName";
            string fakeLastName = "fakeLastName";
            decimal fakeSuperRate = superRate;
            string fakeMonth = "January";

            Mock<IClock> fakeClock = new Mock<IClock>();
            fakeClock.Setup(c => c.GetCurrentInstant()).Verifiable();

            var employee = new Employee(
                    fakeId,
                    fakeFirstName,
                    fakeLastName,
                    annualSalary,
                    fakeSuperRate,
                    fakeMonth,
                    fakeClock.Object,
                    FakeTaxSlabs
                    );

            // Act
            var actual = employee.GetSuper();

            //Assert
            Assert.Equal(expected, actual);
        }

        
        [Theory]
        [InlineData("January", "01 January - 31 January")]
        [InlineData("March", "01 March - 31 March")]
        [InlineData("April", "01 April - 30 April")]
        [InlineData("May", "01 May - 31 May")]
        [InlineData("June", "01 June - 30 June")]
        [InlineData("July", "01 July - 31 July")]
        [InlineData("August", "01 August - 31 August")]
        [InlineData("September", "01 September - 30 September")]
        [InlineData("October", "01 October - 31 October")]
        [InlineData("November", "01 November - 30 November")]
        [InlineData("December", "01 December - 31 December")]
        public void GetPayPeriodString_ForSuppliedMonth_ReturnsExpectedPayPeriod(string month, string expected) {
            // Arrange
            int fakeId = 1;
            string fakeFirstName = "fakeFirstName";
            string fakeLastName = "fakeLastName";
            int fakeAnnualSalary = 14000;
            decimal fakeSuperRate = 9.5M;
            string fakeMonth = month;

            Mock<IClock> fakeClock = new Mock<IClock>();
            fakeClock.Setup(c => c.GetCurrentInstant()).Verifiable();

            var employee = new Employee(
                    fakeId,
                    fakeFirstName,
                    fakeLastName,
                    fakeAnnualSalary,
                    fakeSuperRate,
                    fakeMonth,
                    fakeClock.Object,
                    FakeTaxSlabs
                    );

            // Act
            var actual = employee.GetPayPeriodString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPayPeriodString_ForFebruraryMonthInLeapYear_ReturnsPayPeriod() {
            // Arrange
            int fakeId = 1;
            string fakeFirstName = "fakeFirstName";
            string fakeLastName = "fakeLastName";
            int fakeAnnualSalary = 14000;
            decimal fakeSuperRate = 9.5M;
            string fakeMonth = "February";

            Mock<IClock> fakeClock = new Mock<IClock>();
            fakeClock.Setup(c => c.GetCurrentInstant()).Returns(Instant.FromDateTimeOffset(new DateTime(2020, 02, 01))); // Leap Year

            var employee = new Employee(
                    fakeId,
                    fakeFirstName,
                    fakeLastName,
                    fakeAnnualSalary,
                    fakeSuperRate,
                    fakeMonth,
                    fakeClock.Object,
                    FakeTaxSlabs
                    );

            string expected = "01 February - 29 February";

            // Act
            var actual = employee.GetPayPeriodString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPayPeriodString_ForFebruraryMonthInNonLeapYear_ReturnsPayPeriod() {
            // Arrange
            int fakeId = 1;
            string fakeFirstName = "fakeFirstName";
            string fakeLastName = "fakeLastName";
            int fakeAnnualSalary = 14000;
            decimal fakeSuperRate = 9.5M;
            string fakeMonth = "February";

            Mock<IClock> fakeClock = new Mock<IClock>();
            fakeClock.Setup(c => c.GetCurrentInstant()).Returns(Instant.FromDateTimeOffset(new DateTime(2019, 02, 01))); // Non Leap Year

            var employee = new Employee(
                    fakeId,
                    fakeFirstName,
                    fakeLastName,
                    fakeAnnualSalary,
                    fakeSuperRate,
                    fakeMonth,
                    fakeClock.Object,
                    FakeTaxSlabs
                    );

            string expected = "01 February - 28 February";

            // Act
            var actual = employee.GetPayPeriodString();

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
