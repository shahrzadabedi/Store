using Microsoft.AspNetCore.Mvc;
using Store.Application.Orders.CommandHandlers;
using Store.Application.Orders.Dtos;
using Store.Presentation.Extensions;

namespace Store.Presentation.Controllers;

public class OrderController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<bool>> Buy(BuyProductRequest request, CancellationToken cancellationToken = default)
    {
        var command = Mapper.Map<BuyProductCommand>(request);

        var result = await Mediator.Send(command, cancellationToken);

        return result.ToActionResult();
    }
}
