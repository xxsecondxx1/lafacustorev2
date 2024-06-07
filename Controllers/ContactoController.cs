using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lafacustorev2.Models;
using lafacustorev2.Data;
using lafacustorev2.Service;
using SentimentAnalysis;
using Microsoft.Extensions.ML;

namespace lafacustorev2.Controllers
{
    public class ContactoController : Controller
    {
         private readonly ILogger<ContactoController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly ContactoService _contactoService;
        private readonly PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> _predictionEnginePool;


        public ContactoController(ILogger<ContactoController> logger,
        ApplicationDbContext context,
        ContactoService contactoService,
         PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> predictionEnginePool)
        {
            _logger = logger;
            _context = context;
            _contactoService = contactoService;
            _predictionEnginePool = predictionEnginePool;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnviarMensaje(Contacto objcontato)
        {
            _logger.LogDebug("Ingreso a Enviar Mensaje");

            
            _context.Add(objcontato);
            _context.SaveChanges();

            ViewData["Message"] = "Se registro el contacto";
            return View("Index");
        }
        public async Task<IActionResult> VerContactos()
        {
            var contactos = await _contactoService.GetAllContactos();
            return View(contactos);
        }
        public async Task<IActionResult> Sentimientos()
        {
            var contactos = await _contactoService.GetAllContactos();
            var contactosRecomendation = new List<ContactoRecomendation>();

            foreach (var contacto in contactos)
            {
                var modelInput = new MLModel1.ModelInput()
                {
                    SentimentText = contacto.Message
                };

                var prediction = _predictionEnginePool.Predict(modelInput);
                var predictionText = "";
                if(prediction.PredictedLabel==1){
                    predictionText="Mensage Position";
                }else if(prediction.PredictedLabel==0){
                    predictionText="Mensage Negativo";
                }
                contactosRecomendation.Add(new ContactoRecomendation()
                {
                    Contacto = contacto,
                    Sentimiento = predictionText
                });
            }

            return View(contactosRecomendation);
        }

        public class ContactoRecomendation
        {
            public Contacto? Contacto { get; set; }
            public string? Sentimiento { get; set; }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}