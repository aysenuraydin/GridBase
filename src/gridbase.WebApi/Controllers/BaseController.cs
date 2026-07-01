using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using gridbase.Domain.Common;

namespace gridbase.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]

public class BaseController<TEntity, TKey> : ControllerBase
where TEntity : class, IEntity<TKey>
{
    protected readonly IService<TEntity, TKey> _service;
    public BaseController(IService<TEntity, TKey> service) => _service = service;
}