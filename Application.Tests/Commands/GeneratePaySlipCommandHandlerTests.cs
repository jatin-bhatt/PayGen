using Application.Commands;
using Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests {
    public class GeneratePaySlipCommandHandlerTests {
        
        [Fact]
        public void Constructor_ReceivesNullClockContext_ThrowsException() {
            // Arrange
            Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
            Mock<ILogger<GeneratePaySlipCommandHandler>> fakeLogger = new Mock<ILogger<GeneratePaySlipCommandHandler>>();
            
            _configurationMock.Setup(c => c.GetSection(It.IsAny<string>())).Verifiable();

            //Act Assert
            Assert.Throws<ArgumentNullException>(() => new GeneratePaySlipCommandHandler(fakeLogger.Object, null, _configurationMock.Object));
        }

        [Fact]
        public void Constructor_ReceivesNullLoggerContext_ThrowsException() {
            // Arrange
            Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
            Mock<ILogger<GeneratePaySlipCommandHandler>> fakeLogger = new Mock<ILogger<GeneratePaySlipCommandHandler>>();
            _configurationMock.Setup(c => c.GetSection(It.IsAny<string>())).Verifiable();

            Mock<IClock> fakeClock = new Mock<IClock>();
            fakeClock.Setup(c => c.GetCurrentInstant()).Verifiable();

            //Act Assert
            Assert.Throws<ArgumentNullException>(() => new GeneratePaySlipCommandHandler(null, fakeClock.Object, _configurationMock.Object));
        }

        [Fact]
        public void Constructor_ReceivesNullConfigContext_ThrowsException() {
            // Arrange
            Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
            Mock<ILogger<GeneratePaySlipCommandHandler>> fakeLogger = new Mock<ILogger<GeneratePaySlipCommandHandler>>();
            _configurationMock.Setup(c => c.GetSection(It.IsAny<string>())).Verifiable();

            Mock<IClock> fakeClock = new Mock<IClock>();
            fakeClock.Setup(c => c.GetCurrentInstant()).Verifiable();

            //Act Assert
            Assert.Throws<ArgumentNullException>(() => new GeneratePaySlipCommandHandler(fakeLogger.Object, fakeClock.Object, null));
        }

        [Fact]
        public async Task GeneratePaySlipHandler_EmployeeSalaryDetailsProvided_ReturnsPaySlipInfo() {
            // Arrange
            GeneratePaySlipCommand generatePaySlipCommandFake = new GeneratePaySlipCommand(
                "First",
                "Last",
                129765,
                9,
                "January"
                );
            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("1");
            var fooBarSectionMock = new Mock<IConfigurationSection>();
            fooBarSectionMock.Setup(s => s.GetChildren()).Returns(new List<IConfigurationSection> { oneSectionMock.Object });

            Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
            Mock<ILogger<GeneratePaySlipCommandHandler>> fakeLogger = new Mock<ILogger<GeneratePaySlipCommandHandler>>();
            _configurationMock.Setup(c => c.GetSection(It.IsAny<string>())).Returns(fooBarSectionMock.Object);

            Mock<IClock> fakeClock = new Mock<IClock>();
            fakeClock.Setup(c => c.GetCurrentInstant()).Verifiable();


            var commandHandler = new GeneratePaySlipCommandHandler(fakeLogger.Object, fakeClock.Object, _configurationMock.Object);
            var fakeCancellationToken = new System.Threading.CancellationToken();

            // Act
            PaySlip actual = await commandHandler.Handle(generatePaySlipCommandFake, fakeCancellationToken);

            //Assert
            Assert.IsType<PaySlip>(actual);
        }
    }
}