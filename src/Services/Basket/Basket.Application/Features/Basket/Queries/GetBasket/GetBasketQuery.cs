using Basket.Domain.Entities;
using MediatR;

namespace Basket.Application.Features.Basket.Queries.GetBasket;

public class GetBasketQuery : IRequest<CustomerBasket>
{
    public string CustomerId { get; set; }
}
