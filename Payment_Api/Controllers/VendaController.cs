using Microsoft.AspNetCore.Mvc;
using Payment_Api.Context;
using Payment_Api.Models;
using PaymentApi.Models;

namespace Payment_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {

        private readonly VendaContext _context;

        public VendaController(VendaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Venda venda)
        {

            _context.Add(venda);

            _context.SaveChanges();
            return Ok(venda);
        }

        [HttpGet("{idVenda}")]
        public IActionResult ObterPorIdVenda(int idVenda)
        {
            var venda = _context.Produtos.Find(idVenda);

            if (venda == null)
                return NotFound();


            return Ok(venda);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Venda venda)
        {
            var vendaBanco = _context.Produtos.Find(id);
            if (vendaBanco.Status == EnumStatusPedido.AguardandoPagamento)

            {
                if (venda.Status == EnumStatusPedido.PagamentoAprovado)
                    vendaBanco.Status = venda.Status;
                if (venda.Status == EnumStatusPedido.Cancelada)
                    vendaBanco.Status = venda.Status;
            }
            if (vendaBanco.Status == EnumStatusPedido.PagamentoAprovado)
            {
                if (venda.Status == EnumStatusPedido.EnviadoParaTransportadora)
                    vendaBanco.Status = venda.Status;
                if (venda.Status == EnumStatusPedido.Cancelada)
                    vendaBanco.Status = venda.Status;
            }
            if (vendaBanco.Status == EnumStatusPedido.EnviadoParaTransportadora)
            {
                if (venda.Status == EnumStatusPedido.Entregue)
                    vendaBanco.Status = venda.Status;
            }

            _context.Produtos.Update(vendaBanco);
            _context.SaveChanges();
            return Ok(venda);
        }
    }
}
