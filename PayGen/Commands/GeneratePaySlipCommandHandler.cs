using Application.Models;
using MediatR;
using NodaTime;
using Employee = Payroll.Domain.AggregatesModel.EmployeeAggregate.Employee;
using TaxSlab = Payroll.Domain.AggregatesModel.EmployeeAggregate.TaxSlab;

namespace Application.Commands {
    public class GeneratePaySlipCommandHandler : IRequestHandler<GeneratePaySlipCommand, PaySlip> {
        private readonly IClock _clock;
        private readonly ILogger<GeneratePaySlipCommandHandler> _logger;
        private readonly Models.TaxSlab[] _taxSlabs;

        public GeneratePaySlipCommandHandler(ILogger<GeneratePaySlipCommandHandler> logger, IClock clock, IConfiguration configuration) {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
            if (configuration  is null) {
                throw new ArgumentNullException(nameof(configuration));
            }
            _taxSlabs = configuration.GetSection("TaxSlab")?.Get<Models.TaxSlab[]>() ?? throw new Exception("taxSlab"); ;
        }

        public Task<PaySlip> Handle(GeneratePaySlipCommand message, CancellationToken cancellationToken) {
            try {
                var empId = new Random().Next();
                var employee = new Employee(
                        empId,
                        message.FirstName,
                        message.LastName,
                        message.AnnualSalary,
                        message.SuperRate,
                        message.PayPeriod,
                        _clock,
                        _taxSlabs.Select(c =>
                            new TaxSlab(
                                c.StartRange,
                                c.EndRange,
                                c.Rate
                           )).ToArray()
                        );

                var result = new PaySlip() {
                    Name = employee.GetFullName(),
                    PayPeriod = employee.GetPayPeriodString(),
                    GrossIncome = employee.GetGrossIncome().ToString("0.00"),
                    IncomeTax = employee.GetTax().ToString("0.00"),
                    NetIncome = employee.GetNetIncome().ToString("0.00"),
                    Super = employee.GetSuper().ToString("0.00"),
                };
                
                return Task.FromResult(result);
            } catch (Exception ex) {
                _logger.LogError("An error occurred", ex);
                throw;
            }
            
        }
    }
}
