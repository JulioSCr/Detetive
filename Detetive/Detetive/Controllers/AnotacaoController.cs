using Detetive.Business.Business.Interfaces;
using Detetive.Business.Data.Interfaces;
using Detetive.Business.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Detetive.Controllers
{
    public class AnotacaoController : Controller
    {
        private readonly IAnotacaoArmaBusiness _anotacaoArmaBusiness;
        private readonly IAnotacaoLocalBusiness _anotacaoLocalBusiness;
        private readonly IAnotacaoSuspeitoBusiness _anotacaoSuspeitoBusiness;

        public AnotacaoController(IAnotacaoArmaBusiness anotacaoArmaBusiness,
                                    IAnotacaoLocalBusiness anotacaoLocalBusiness,
                                    IAnotacaoSuspeitoBusiness anotacaoSuspeitoBusiness)
        {
            _anotacaoArmaBusiness = anotacaoArmaBusiness;
            _anotacaoLocalBusiness = anotacaoLocalBusiness;
            _anotacaoSuspeitoBusiness = anotacaoSuspeitoBusiness;
        }

        [HttpPut]
        public string MarcarArma(int idArma, int idJogadorSala, bool valor)
        {
            try
            {
                _anotacaoArmaBusiness.Alterar(idArma, idJogadorSala, valor);

                return JsonConvert.SerializeObject(new Operacao("Anotação realizada com sucesso!"));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new Operacao($"Ocorreu um problema: {ex.Message}", false));
            }
        }

        [HttpPut]
        public string MarcarLocal(int idLocal, int idJogadorSala, bool valor)
        {
            try
            {
                var anotacao = _anotacaoLocalBusiness.Alterar(idLocal, idJogadorSala, valor);

                return JsonConvert.SerializeObject(new Operacao("Anotação realizada com sucesso!"));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new Operacao($"Ocorreu um problema: {ex.Message}", false));
            }
        }

        [HttpPut]
        public string MarcarSuspeito(int idSuspeito, int idJogadorSala, bool valor)
        {
            try
            {
                var anotacao = _anotacaoSuspeitoBusiness.Alterar(idSuspeito, idJogadorSala, valor);

                return JsonConvert.SerializeObject(new Operacao("Anotação realizada com sucesso!"));
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new Operacao($"Ocorreu um problema: {ex.Message}", false));
            }
        }
    }
}