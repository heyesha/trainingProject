using Microsoft.AspNetCore.Mvc;

namespace TrainingProject.api.Controlers
{
    public class ReportConroller : ControllerBase
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
