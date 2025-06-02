public class TransacaoService
{
    private readonly List<Transacao> _transacoes = new();
    private int _nextId = 1;
    private readonly PessoaService _pessoaService;

    public TransacaoService(PessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    public List<Transacao> Listar() => _transacoes;

    public string CriarTransacao(Transacao transacao)
    {
        var pessoa = _pessoaService.ObterPorId(transacao.PessoaId);
        if (pessoa == null)
            return "Pessoa não encontrada";

        if (pessoa.Idade < 18 && transacao.Tipo == TipoTransacao.Receita)
            return "Menores de idade só podem registrar despesas";

        transacao.Id = _nextId++;
        _transacoes.Add(transacao);
        return "Sucesso";
    }


    public void RemoverPorPessoa(int pessoaId)
    {
        _transacoes.RemoveAll(t => t.PessoaId == pessoaId);
    }

    public List<object> ListarTotais()
    {
        var resultado = _pessoaService.ListarPessoas().Select(p => new
        {
            Pessoa = p.Nome,
            TotalReceitas = _transacoes.Where(t => t.PessoaId == p.Id && t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor),
            TotalDespesas = _transacoes.Where(t => t.PessoaId == p.Id && t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor),
            Saldo = _transacoes.Where(t => t.PessoaId == p.Id && t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor) -
                    _transacoes.Where(t => t.PessoaId == p.Id && t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor)
        }).ToList();

        var totalGeral = new
        {
            Pessoa = "Total Geral",
            TotalReceitas = _transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor),
            TotalDespesas = _transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor),
            Saldo = _transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor) -
                    _transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor)
        };

        resultado.Add(totalGeral);
        return resultado.Cast<object>().ToList();
    }

}