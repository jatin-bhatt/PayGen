using Payroll.Domain.Base;

namespace Payroll.Domain.AggregatesModel.EmployeeAggregate {
    public class PayMonth : Enumeration {
        public static PayMonth January = new PayMonth(1, nameof(January).ToLowerInvariant());
        public static PayMonth February = new PayMonth(2, nameof(February).ToLowerInvariant());
        public static PayMonth March = new PayMonth(3, nameof(March).ToLowerInvariant());
        public static PayMonth April = new PayMonth(4, nameof(April).ToLowerInvariant());
        public static PayMonth May = new PayMonth(5, nameof(May).ToLowerInvariant());
        public static PayMonth June = new PayMonth(6, nameof(June).ToLowerInvariant());
        public static PayMonth July = new PayMonth(7, nameof(July).ToLowerInvariant());
        public static PayMonth August = new PayMonth(8, nameof(August).ToLowerInvariant());
        public static PayMonth September = new PayMonth(9, nameof(September).ToLowerInvariant());
        public static PayMonth October = new PayMonth(10, nameof(October).ToLowerInvariant());
        public static PayMonth November = new PayMonth(11, nameof(November).ToLowerInvariant());
        public static PayMonth December = new PayMonth(12, nameof(December).ToLowerInvariant());

        public PayMonth(int id, string name) : base(id, name) {
        }

        /// <summary>
        /// List all possible values
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<PayMonth> List() {
            return new[] { January, February, March, April, May, June, July, August, September, October, November, December };
        }


        /// <summary>
        /// PayMonth for supplied month
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static PayMonth FromName(string name) {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null) {
                throw new Exception($"Possible values for PayPeriodMonth: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        /// <summary>
        /// PayMonth for supplied Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static PayMonth From(int id) {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null) {
                throw new Exception($"Possible values for PayPeriodMonth: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        
    }
}
