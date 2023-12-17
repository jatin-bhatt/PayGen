using Payroll.Domain.AggregatesModel.EmployeeAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Payroll.Domain.Tests.AggregatesModel.EmployeeAggregate {
    public class PayFrequencyTests {
        [Fact]
        public void FromName_FrequencyNameMatch_ReturnsPayFrequency() {
            // Arrange
            PayFrequency expected = new PayFrequency(1, "Monthly".ToLowerInvariant());

            // Act
            var actual = PayFrequency.FromName("Monthly");

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FromName_FrequencyNameNotMatch_ThrowsException() {
            
            // Act & Assert
            Assert.Throws<Exception>(() => PayFrequency.FromName("Bi-Weekly"));
        }

        [Fact]
        public void From_FrequencyIdMatch_ReturnsPayFrequency() {
            // Arrange
            PayFrequency expected = new PayFrequency(1, "Monthly".ToLowerInvariant());

            // Act
            var actual = PayFrequency.From(1);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void From_FrequencyIdNotMatch_ThrowsException() {

            // Act & Assert
            Assert.Throws<Exception>(() => PayFrequency.From(10));
        }
    }
}
