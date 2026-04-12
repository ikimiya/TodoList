using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers
{
    public class BaseController : ControllerBase
    {
        protected int GetUserId()
        {
            return int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        }
    }
}