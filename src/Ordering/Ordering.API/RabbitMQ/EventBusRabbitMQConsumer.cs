using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using MediatR;
using Newtonsoft.Json;
using Ordering.Application.Commands;
using Ordering.Core.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Ordering.API.RabbitMQ
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IMapper _mapper;
        private readonly IRabbitMQConnection _connection;
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;

        public EventBusRabbitMQConsumer(IMapper mapper, IRabbitMQConnection connection, 
            IMediator mediator, IOrderRepository orderRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public object JsonConver { get; private set; }

        public void Consume()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.BasketCheckoutQueue,
                durable: false, exclusive: false, autoDelete: false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.BasketCheckoutQueue, 
                autoAck: true, consumer: consumer);
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.BasketCheckoutQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var basketCheckoutEvent = 
                    JsonConvert.DeserializeObject<BasketCheckoutEvent>(message);

                await _mediator.Send(_mapper.Map<CheckoutOrderCommand>(basketCheckoutEvent));
            }
        }
    }
}