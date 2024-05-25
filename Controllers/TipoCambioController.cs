using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lafacustorev2.Integration.currencyexchange;
using lafacustorev2.Integration.currencyexchange.dto;

namespace lafacustorev2.Controllers
{
    public class TipoCambioController : Controller
    {
        private readonly ILogger<TipoCambioController> _logger;
        private readonly CurrencyExchangeApiIntegration _currency;

        public TipoCambioController(ILogger<TipoCambioController> logger,
        CurrencyExchangeApiIntegration currency)
        {
            _logger = logger;
            _currency = currency;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Exchange(TipoCambio? tipoCambio)
        {
            double rate = await _currency.GetExchangeRate(tipoCambio.From, tipoCambio.To);
            var cambio = tipoCambio.Cantidad * rate;
            _logger.LogInformation($"Tipo de cambio de {tipoCambio.From} a {tipoCambio.To} es {rate} y cambio {cambio}");
            ViewData["rate"] =  String.Format("{0:F2}", rate);
            ViewData["cambio"] = String.Format("{0:N2}", cambio);;
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}