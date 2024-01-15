using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly OperationService _operationService;
        private readonly IOperationTransient _transientOperation;
        private readonly IOperationScoped _scopedOperation;
        private readonly IOperationSingleton _singletonOperation;

        public OperationController(OperationService operationService,
            IOperationTransient transientOperation,
            IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation)
        {
            _operationService = operationService;
            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _singletonOperation = singletonOperation;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var model = new
            {
                ControllerDependencies = new
                {
                    Transient = _transientOperation.OperationId,
                    Scoped = _scopedOperation.OperationId,
                    Singleton = _singletonOperation.OperationId,
                },
                ServiceDependencies = new 
                {
                    Transient = _operationService.TransientOperation.OperationId,
                    Scoped = _operationService.ScopedOperation.OperationId,
                    Singleton = _operationService.SingletonOperation.OperationId,
                }
            };

            return Ok(model);
        }
    }
}
