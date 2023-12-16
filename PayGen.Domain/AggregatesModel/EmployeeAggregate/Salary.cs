using Payroll.Domain.Base;

namespace Payroll.Domain.AggregatesModel.EmployeeAggregate {
    public class Salary : ValueObject {
        public int AnnualSalary { get; private set; }
        public decimal SuperRate { get; private set; }

        public Salary(int annualSalary, decimal superRate) {
            AnnualSalary = annualSalary ;
            SuperRate = superRate ;
        }

        protected override IEnumerable<object> GetEqualityComponents() {
            yield return AnnualSalary;
            yield return SuperRate;
        }
    }

    public class TaxSlab {
        public int StartRange { get; private set; }
        public int EndRange { get; private set; }
        public decimal Rate { get; private set; }

        public TaxSlab(int startRange, int endRange, decimal rate) {
            StartRange = startRange;
            EndRange = endRange;
            Rate = rate;
        }

    }
}
