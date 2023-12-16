using Payroll.Domain.AggregatesModel.EmployeeAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Payroll.Domain.Tests.AggregatesModel.EmployeeAggregate {
    public class PayMonthTests {
        [Fact]
        public void FromName_MonthNameMatch_ReturnsPayMonth() {
            // Arrange
            PayMonth expected = new PayMonth(1, "January".ToLowerInvariant());

            // Act
            var actual = PayMonth.FromName("January");

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FromName_MonthNameNotMatch_ThrowsException() {
            
            // Act & Assert
            Assert.Throws<Exception>(() => PayMonth.FromName("Jan"));
        }

        [Fact]
        public void From_MonthNameMatch_ReturnsPayMonth() {
            // Arrange
            PayMonth expected = new PayMonth(1, "January".ToLowerInvariant());

            // Act
            var actual = PayMonth.From(1);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void From_MonthNameNotMatch_ThrowsException() {

            // Act & Assert
            Assert.Throws<Exception>(() => PayMonth.From(24));
        }
    }
}
