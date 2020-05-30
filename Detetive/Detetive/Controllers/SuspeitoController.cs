using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Detetive.Controllers
{
    public class SuspeitoController : Controller
    {
        /// <summary>
        /// Método controller para trazer a view de seleção de personagem
        /// </summary>
        /// <returns>Tela de seleção de personagem</returns>
        public ActionResult Listar()
        {
            return View();
        }

        public ActionResult Teste()
        {
            return View();
        }
    }
}