using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Models;
using MediatR;
using Application.Commands;

namespace PayGen.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        [BindProperty]
        public Employee Employee { get; set; }

        public PaySlip PaySlip { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator) {
            _logger = logger;
            _mediator = mediator;
        }

        public void OnGet() {
            
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }


            var paySlipInfo = await _mediator.Send(new GeneratePaySlipCommand(
            Employee.FirstName,
            Employee.LastName,
            Employee.AnnualSalary,
            Employee.SuperRate,
            Employee.PayPeriod
            ));

            if (paySlipInfo != null) {
                PaySlip = paySlipInfo;
            }
            return Page();
            
        }

    }
}