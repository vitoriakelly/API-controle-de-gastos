using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/transacoes")]
public class TransacoesController : ControllerBase
{
    private readonly TransacaoService _service;
    public TransacoesController(TransacaoService service) => _service = service;

    [HttpGet]
    public IActionResult ListarTransacoes() => Ok(_service.Listar());

    [HttpPost]
    public IActionResult CriarTransacao([FromBody] Transacao transacao)
    {
        var resultado = _service.CriarTransacao(transacao);
        return resultado == "Sucesso" ? Ok(transacao) : BadRequest(resultado);
    }

    [HttpGet("totais")]
    public IActionResult ListarTotais() => Ok(_service.ListarTotais());
}
