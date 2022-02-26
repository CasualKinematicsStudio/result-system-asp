using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using result_example.Entity;
using result_system;

namespace result_example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResultController : ControllerBase
    {
        [HttpGet("Success")]
        public async Task<IResult> Success()
        { 
            return Result.Success();
        }
        [HttpGet("Fail")]
        public async Task<IResult> Fail()
        { 
            return Result.Failure(500,"Fail message for this url");
        }
        
        [HttpGet("SuccessOperation")]
        public async Task<IResult> SuccessOperation()
        { 
            return await Result.From(() => Task.CompletedTask);
        }
        [HttpGet("FailOperation")]
        public async Task<IResult> FailOperation()
        { 
            return await Result.From(() => throw new Exception("Fail message for this url"));
        }
        
        [HttpGet("SuccessOperationWithObject")]
        public async Task<IResult> SuccessOperationWithObject()
        { 
            return Result.From(() => new UserEntity());
        }
    }
}