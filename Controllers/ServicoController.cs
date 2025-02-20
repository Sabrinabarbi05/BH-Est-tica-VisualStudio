using BH_Estética_VisualStudio.Models;
using BH_Estética_VisualStudio.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace BH_Estética_VisualStudio.Controllers
{
    public class ServicoController : Controller
    {
        private readonly ServicoRepositorio _servicoRepositorio;
        private readonly ILogger<ServicoController> _logger;
        public ServicoController(ServicoRepositorio servicoRepositorio, ILogger<ServicoController> logger)
        {
            _servicoRepositorio = servicoRepositorio;
            _logger = logger;
        }
        public IActionResult Index()
        {
            // Chama o método ListarNomesAgendamentos para obter a lista de usuários
            var nomeServicos = _servicoRepositorio.ListarNomesServicos();

            if (nomeServicos != null && nomeServicos.Any())
            {
                // Cria a lista de SelectListItem
                var selectList = nomeServicos.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),  // O valor do item será o ID do usuário
                    Text = u.TipoServico             // O texto exibido será o nome do usuário
                }).ToList();

                // Passa a lista para o ViewBag para ser utilizada na view
                ViewBag.Servicos = selectList;
            }
            var servicos = _servicoRepositorio.ListarServicos();

            // Criar a lista de SelectListItems, onde o 'Value' será o 'Id' e o 'Text' será o 'TipoServico'
            List<SelectListItem> tipoServico = new List<SelectListItem>
             {
                 new SelectListItem { Value = "0", Text = "Desing Sobrancelhas" },
                 new SelectListItem { Value = "1", Text = "Micropgmentação" },
                  new SelectListItem { Value = "2", Text = "Henna para Sobrancelhas" },
                 new SelectListItem { Value = "3", Text = "Laminação de Sobrancelhas" },
                  new SelectListItem { Value = "4", Text = "Extenção de Sobrancelhas" },
                 new SelectListItem { Value = "5", Text = "Bhow Lamination" }
             };

            // Passar a lista para a View usando ViewBag
            ViewBag.lstTipoServico = tipoServico;

            // Passar a lista para a View usando ViewBag
            ViewBag.lstTipoServico = new SelectList(tipoServico, "Value", "Text");
            return View(servicos);
        }
        public IActionResult InserirServico(string tipoServico, decimal Valor)
        {
            try
            {
                // Chama o método do repositório que realiza a inserção no banco de dados
                var resultado = _servicoRepositorio.InserirServico(tipoServico, Valor);

                // Verifica o resultado da inserção
                if (resultado)
                {
                    // Se o resultado for verdadeiro, significa que o usuário foi inserido com sucesso
                    return Json(new { success = true, message = "Servico inserido com sucesso!" });
                }
                else
                {
                    // Se o resultado for falso, significa que houve um erro ao tentar inserir o usuário
                    return Json(new { success = false, message = "Erro ao inserir o servico. Tente novamente." });
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro inesperado, captura e exibe o erro
                return Json(new { success = false, message = "Erro ao processar a solicitação. Detalhes: " + ex.Message });
            }
        }
        public IActionResult AtualizarServico(int id, string tipoServico, decimal valor)
        {
            try
            {
                // Chama o repositório para atualizar o usuário
                var resultado = _servicoRepositorio.AtualizarServico(id, tipoServico, valor);

                if (resultado)
                {
                    return Json(new { success = true, message = "Serviço atualizado com sucesso!" });
                }
                else
                {
                    return Json(new { success = false, message = "Erro ao atualizar o serviço. Verifique se o usuário existe." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro ao processar a solicitação. Detalhes: " + ex.Message });
            }
        }
        public IActionResult ExcluirServico(int id)
        {
            try
            {
                // Chama o repositório para excluir o usuário
                var resultado = _servicoRepositorio.ExcluirServico(id);

                if (resultado)
                {
                    return Json(new { success = true, message = "Servico excluído com sucesso!" });
                }
                else
                {
                    // Se o resultado for falso, você pode fornecer uma mensagem mais específica.
                    return Json(new { success = false, message = "Não foi possível excluir o serviço. Verifique se ele está vinculado a outros registros no sistema." });
                }
            }
            catch (Exception ex)
            {
                // Captura qualquer erro e inclui a mensagem detalhada da exceção
                return Json(new { success = false, message = "Erro ao processar a solicitação. Detalhes: " + ex.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
