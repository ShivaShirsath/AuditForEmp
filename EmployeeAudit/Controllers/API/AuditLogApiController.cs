using EmployeeAudit.Infrastructure.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAudit.Controllers.API
{
  [Route("api/audit")]
  [ApiController]
  public class AuditLogApiController : ControllerBase
  {
    private readonly IUnitOfWork _unitOfWork;
    public AuditLogApiController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
  }
}

