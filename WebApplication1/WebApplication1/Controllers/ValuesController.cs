using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.BusinessService;

namespace WebApplication1.Controllers
{
    [Route("solveMaze")]
    public class ValuesController : Controller
    {

        [HttpPost]
        public async Task<JsonResult> SolveMaze()
        {
            using (var ms = new MemoryStream(2048))
            {
                await Request.Body.CopyToAsync(ms);
                var test = ms.ToArray();
                string s = Encoding.UTF8.GetString(test, 0, test.Length);
                var mazeService = new MazeService();
                return Json(mazeService.solveMaze(s));
            }
        }
    }
}
