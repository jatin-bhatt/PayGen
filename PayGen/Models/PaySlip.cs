using System.ComponentModel;

namespace Application.Models {
    public class PaySlip {

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Pay Period")]
        public string PayPeriod { get; set; }

        [DisplayName("Gross Income")]
        public string GrossIncome { get; set; }

        [DisplayName("Income Tax")]
        public string IncomeTax { get; set; }

        [DisplayName("Net Income")]
        public string NetIncome { get; set; }

        [DisplayName("Super")]
        public string Super { get; set; }


        public override bool Equals(object obj) {
            return Equals(obj as PaySlip);
        }

        private bool Equals(PaySlip other) {
            return other != null &&
                   Name == other.Name &&
                   PayPeriod == other.PayPeriod &&
                   GrossIncome == other.GrossIncome &&
                   IncomeTax == other.IncomeTax &&
                   NetIncome == other.NetIncome &&
                   Super == other.Super;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Name, PayPeriod, GrossIncome, IncomeTax, NetIncome, Super);
        }
    }

    
}


