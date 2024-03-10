using Microsoft.AspNetCore.Mvc;
using Store.Application.Products.CommandHandlers;
using Store.Application.Products.Dtos;
using Store.Application.Products.QueryHandlers;
using Store.Presentation.Extensions;

namespace Store.Presentation.Controllers;

public class ProductController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<bool>> Add([FromBody] AddProductRequest request
    , CancellationToken cancellationToken = default)
    {
        var command = Mapper.Map<AddProductCommand>(request);

        var result = await Mediator.Send(command, cancellationToken);

        return result.ToActionResult();
    }

    [HttpPut]
    [Route("{id:long}/inventory")]
    public async Task<ActionResult<bool>> UpdateInventory([FromRoute] long id, [FromBody] UpdateInventoryCountRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = Mapper.Map<UpdateInventoryCountCommand>(request);
        command.ProductId = id;

        var result = await Mediator.Send(command, cancellationToken);

        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetProductByIdResponse>> Get([FromRoute] long id
    , CancellationToken cancellationToken = default)
    {
        var query = new GetProductByIdQuery(id);

        var result = await Mediator.Send(query, cancellationToken);

        return result.ToActionResult();
    }
}
