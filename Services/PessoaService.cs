
public class PessoaService
{
    private readonly List<Pessoa> _pessoas = new();
    private int _nextId = 1;

    public List<Pessoa> ListarPessoas() => _pessoas;

    public Pessoa CriarPessoa(Pessoa pessoa)
    {
        pessoa.Id = _nextId++;
        _pessoas.Add(pessoa);
        return pessoa;
    }

    public bool ExistePessoa(int id) => _pessoas.Any(p => p.Id == id);
    public Pessoa? ObterPorId(int id) => _pessoas.FirstOrDefault(p => p.Id == id);
    
    public bool DeletarPessoa(int id)
    {
        var pessoa = _pessoas.FirstOrDefault(p => p.Id == id);
        if (pessoa == null) return false;
        _pessoas.Remove(pessoa);
        return true;
    }
}