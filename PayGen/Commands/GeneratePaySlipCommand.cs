using Application.Models;
using MediatR;
using System.Runtime.Serialization;

namespace Application.Commands {
    public class GeneratePaySlipCommand : IRequest<PaySlip> {
        [DataMember]
        public string FirstName { get; private set; }

        [DataMember]
        public string LastName { get; private set; }

        [DataMember]
        public int AnnualSalary { get; private set; }

        [DataMember]
        public decimal SuperRate { get; private set; }

        [DataMember]
        public string PayPeriod { get; private set; }

        public GeneratePaySlipCommand(string firstName, string lastName, int annualSalary, decimal superRate, string payPerid) {
            FirstName = firstName;
            LastName = lastName;
            AnnualSalary = annualSalary;
            SuperRate = superRate;
            PayPeriod = payPerid;
        }
    }
}
