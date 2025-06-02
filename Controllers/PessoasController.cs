using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/pessoas")]
public class PessoasController : ControllerBase
{
    private readonly PessoaService _service;
    private readonly TransacaoService _transacaoService;

    public PessoasController(PessoaService service, TransacaoService transacaoService)
    {
        _service = service;
        _transacaoService = transacaoService;
    }

    [HttpGet]
    public IActionResult ListarPessoas() => Ok(_service.ListarPessoas());

    [HttpPost]
    public IActionResult CriarPessoa([FromBody] Pessoa pessoa) => Ok(_service.CriarPessoa(pessoa));

    [HttpDelete("{id}")]
    public IActionResult DeletarPessoa(int id)
    {
        if (!_service.ExistePessoa(id)) return NotFound("Pessoa não encontrada");
        _transacaoService.RemoverPorPessoa(id);
        _service.DeletarPessoa(id);
        return NoContent();
    }
}