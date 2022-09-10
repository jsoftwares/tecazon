using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    //here we implement the business logic of CheckoutOrderCommand
    //IRequestHandler<> takes d Command we want to implement & d response type
    internal class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository; //for inserting records
        private readonly IMapper _mapper;    //for mapping commandHanlder incoming request to entity object
        private readonly IEmailService _emailService;   //for sending email
        private readonly ILogger<CheckoutOrderCommand> _logger; //for logging operations

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckoutOrderCommand> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var newOrder = await _orderRepository.AddAsync(orderEntity);

            _logger.LogInformation($"Order {newOrder.Id} is successfully created.");
            await sendEmail(newOrder);

            return newOrder.Id;
        }

        private async Task sendEmail(Order order)
        {
            var email = new Email() { To = "jeff.ict@gmail.com", Subject = "Order created!", Body = $"Order {order.Id} was created" };

            try
            {
                await _emailService.SendEMail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the email service: {ex.Message}");
            }
        }
    }
}
