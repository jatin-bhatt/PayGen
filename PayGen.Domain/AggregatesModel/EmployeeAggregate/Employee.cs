using NodaTime;
using PayGen.Domain.AggregatesModel.EmployeeAggregate.DomainEvents;
using PayGen.Domain.Base;
using System.Globalization;

namespace PayGen.Domain.AggregatesModel.EmployeeAggregate {
    public class Employee : Entity, IAggregateRoot {
        private const int AMOUNT_DECIMAL_PRECISION = 2;
        private readonly IClock _clock;
        private readonly TaxSlab[] _taxSlabs;
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Salary Salary { get; private set; }
        public PayMonth PayMonth { get; private set; }
        public PayFrequency PayFrequency { get; private set; }

        public Employee(int id, string firstName, string lastName, int annualSalary, decimal superRate, string month, IClock clock, TaxSlab[] taxSlabs) {
            Id = id;
            FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : throw new ArgumentNullException(nameof(firstName));
            LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : throw new ArgumentNullException(nameof(lastName));
            _taxSlabs = taxSlabs ?? throw new ArgumentNullException(nameof(taxSlabs));

            if (string.IsNullOrWhiteSpace(month)) {
                throw new ArgumentNullException(nameof(month));
            }

            PayMonth = PayMonth.FromName(month);
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));

            PayFrequency = PayFrequency.Monthly;
            Salary = new Salary(annualSalary, superRate);
        }

        /// <summary>
        /// Method to set Current Month.
        /// </summary>
        /// <param name="month"></param>
        public void SetPayMonth(string month) {
            PayMonth = PayMonth.FromName(month);
        }

        /// <summary>
        /// Get Full Name for Employee.
        /// </summary>
        /// <returns></returns>
        public string GetFullName() {
            return $"{FirstName} {LastName}";
        }

        /// <summary>
        /// Compute Tax and return value rounded off to 2 decimal places.
        /// </summary>
        /// <returns></returns>
        public decimal GetTax() {
            var tax = TaxCompute();
            AddDomainEvent(new TaxComputedDomainEvent());   // This is only for reference that the model is capable to publish domain events so that other aggregates in future can listen and act accordingly (if required)

            return decimal.Round(tax, AMOUNT_DECIMAL_PRECISION);
        }

        /// <summary>
        /// Compute monthly Gross Income and return value rounded off to 2 decimal places.
        /// </summary>
        /// <returns></returns>
        public decimal GetGrossIncome() {
            return decimal.Round(Salary.AnnualSalary / 12M, AMOUNT_DECIMAL_PRECISION);
        }

        /// <summary>
        /// Compute monthly Net Income and return value rounded off to 2 decimal places.
        /// </summary>
        /// <returns></returns>
        public decimal GetNetIncome() {
            var income = Salary.AnnualSalary / 12M - GetTax();
            return decimal.Round(income, AMOUNT_DECIMAL_PRECISION);
        }

        /// <summary>
        /// Compute monthly Super Amount and return value rounded off to 2 decimal places.
        /// </summary>
        /// <returns></returns>
        public decimal GetSuper() {
            var super = (Salary.AnnualSalary / 12M) * (Salary.SuperRate / 100);
            return decimal.Round(super, AMOUNT_DECIMAL_PRECISION);
        }

        /// <summary>
        /// Return Pay Period
        /// </summary>
        /// <returns></returns>
        public string GetPayPeriodString() {
            DateTime now = _clock.GetCurrentInstant().ToDateTimeUtc();
            DateTime startDate = new DateTime(now.Year, PayMonth.Id, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            return $"{startDate.Day.ToString().PadLeft(2, '0')} {startDate.ToString("MMMM", CultureInfo.InvariantCulture)} - {endDate.Day.ToString().PadLeft(2, '0')} {endDate.ToString("MMMM", CultureInfo.InvariantCulture)}";
        }

        private decimal TaxCompute() {
            var totalTax = 0m;
            if (_taxSlabs.Length > 0) {
                int currentSlabIndex = 0;
                while (currentSlabIndex < _taxSlabs.Count() && Salary.AnnualSalary > _taxSlabs[currentSlabIndex].StartRange) {
                    var taxableAmount = Math.Min((_taxSlabs[currentSlabIndex].EndRange < 0 ? int.MaxValue : _taxSlabs[currentSlabIndex].EndRange) - _taxSlabs[currentSlabIndex].StartRange, Salary.AnnualSalary - _taxSlabs[currentSlabIndex].StartRange);
                    var taxForThisSlab = taxableAmount * _taxSlabs[currentSlabIndex].Rate;
                    totalTax += taxForThisSlab;
                    currentSlabIndex++;
                };
            }

            return totalTax / 12M;
        }

    }
}
