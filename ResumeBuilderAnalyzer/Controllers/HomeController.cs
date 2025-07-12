using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ResumeBuilderAnalyzer.Models;
using ResumeBuilderAnalyzer.Services;
using System.Diagnostics;

namespace ResumeBuilderAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PdfService _pdfService;

        public HomeController(ILogger<HomeController> logger, PdfService pdfService)
        {
            _logger = logger;
            _pdfService = pdfService;
        }

        // Redirect root to Build page
        public IActionResult Index()
        {
            return RedirectToAction("Build");
        }

        // Show resume form
        [HttpGet]
        public IActionResult Build()
        {
            return View();
        }

        // Receive resume form data
        [HttpPost]
        public IActionResult Build(Resume model)
        {
            if (ModelState.IsValid)
            {
                TempData["Resume"] = JsonConvert.SerializeObject(model);
                TempData.Keep("Resume");
                return RedirectToAction("Analyze");
            }
            return View(model);
        }

        // Show resume preview + upload JD
        [HttpGet]
        public IActionResult Analyze()
        {
            var json = TempData["Resume"] as string;
            if (string.IsNullOrEmpty(json))
                return RedirectToAction("Build");

            var resume = JsonConvert.DeserializeObject<Resume>(json);
            TempData.Keep("Resume");
            return View(resume);
        }

        // Download resume as PDF
        [HttpPost]
        public IActionResult GeneratePDF()
        {
            var json = TempData["Resume"] as string;
            if (string.IsNullOrEmpty(json))
                return RedirectToAction("Build");

            var resume = JsonConvert.DeserializeObject<Resume>(json);
            var pdfBytes = _pdfService.GenerateResumePDF(resume);

            return File(pdfBytes, "application/pdf", "Resume.pdf");
        }

        // Default pages
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
