using MediatR;

namespace Payroll.API.Application.Commands {
    public class GeneratePaySlipCommandHandler : IRequestHandler<GeneratePaySlipCommand, PaySlipInfoDTO> {

        public Task<PaySlipInfoDTO> Handle(GeneratePaySlipCommand message, CancellationToken cancellationToken) {
            //var order = Order.NewDraft();
            //var orderItems = message.Items.Select(i => i.ToOrderItemDTO());
            //foreach (var item in orderItems) {
            //    order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.PictureUrl, item.Units);
            //}

            return Task.FromResult(PaySlipInfoDTO.FromEmployee());
        }
    }

    public record PaySlipInfoDTO {
        //public IEnumerable<OrderItemDTO> OrderItems { get; init; }
        //public decimal Total { get; init; }

        public static PaySlipInfoDTO FromEmployee() {
            return new PaySlipInfoDTO() {
               
            };
        }
    }
}
