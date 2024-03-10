using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Store.Presentation;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    private IMediator _mediatorInstance;
    private IMapper _mapperInstance;
    protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    protected IMapper Mapper => _mapperInstance ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
}
