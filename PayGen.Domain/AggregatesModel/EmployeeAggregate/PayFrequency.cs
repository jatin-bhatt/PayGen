using PayGen.Domain.Base;

namespace PayGen.Domain.AggregatesModel.EmployeeAggregate {
    public class PayFrequency : Enumeration {
        public static PayFrequency Monthly = new PayFrequency(1, nameof(Monthly).ToLowerInvariant());
        public static PayFrequency Weekly = new PayFrequency(2, nameof(Weekly).ToLowerInvariant());
        public static PayFrequency BiWeekly = new PayFrequency(3, nameof(BiWeekly).ToLowerInvariant());

        public PayFrequency(int id, string name) : base(id, name) {
        }

        /// <summary>
        /// List all possible values 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<PayFrequency> List() {
            return new[] { Monthly };
        }

        /// <summary>
        /// return PayFrequency for supplied month
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static PayFrequency FromName(string name) {
            var result = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (result == null) {
                throw new Exception($"Possible values for PayFrequency: {string.Join(",", List().Select(s => s.Name))}");
            }

            return result;
        }

        /// <summary>
        /// return PayFrequency for supplied identity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static PayFrequency From(int id) {
            var result = List().SingleOrDefault(s => s.Id == id);

            if (result == null) {
                throw new Exception($"Possible values for PayFrequency Ids: {string.Join(",", List().Select(s => s.Id))}");
            }

            return result;
        }
    }
}
