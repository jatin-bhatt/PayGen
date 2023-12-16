using MediatR;
using System.Runtime.Serialization;

namespace Payroll.API.Application.Commands {
    public class GeneratePaySlipCommand : IRequest<PaySlipInfoDTO> {

        [DataMember]
        public string FirstName { get; private set; }

        [DataMember]
        public string LastName { get; private set; }

        [DataMember]
        public double AnnualSalary { get; private set; }

        [DataMember]
        public float SuperRate { get; private set; }

        [DataMember]
        public string PayPeriod { get; private set; }

        public GeneratePaySlipCommand(string fName, string lName, double annualSalary, float superRate, string payPerid) {
            FirstName = fName;
            LastName = lName;
            AnnualSalary = annualSalary;
            SuperRate = superRate;
            PayPeriod = payPerid;
        }
    }
}
