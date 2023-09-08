using AutoMapper;
using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;
using VL_VendasLanches.Services.Interfaces;
using VL_VendasLanches.ViewModels;

namespace VL_VendasLanches.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IMapper _mapper;

        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ITamanhoRepository _tamanhoRepository;
        private readonly ITamanhoProdutosRepository _tamanhoProdutosRepository;
        private readonly IComplementoRepository _complementoRepository;
        private readonly IComplementoProdutoRepository _complementoProdutoRepository;

        public ProdutoService(IProdutoRepository lancheRepository,
                              IMapper mapper,
                              ICategoriaRepository categoriaRepository,
                              ITamanhoRepository tamanhoRepository,
                              ITamanhoProdutosRepository tamanhoProdutosRepository,
                              IComplementoRepository complementoRepository,
                              IComplementoProdutoRepository complementoProdutoRepository)
        {
            _produtoRepository=lancheRepository;
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
            _tamanhoRepository = tamanhoRepository;
            _tamanhoProdutosRepository = tamanhoProdutosRepository;
            _complementoRepository = complementoRepository;
            _complementoProdutoRepository = complementoProdutoRepository;
        }

        public async Task<List<Categoria>> ObterCategorias() =>
             await _categoriaRepository.ObterCategorias().ConfigureAwait(false);

        public async Task<List<Tamanho>> ObterTamanhos() =>
            await _tamanhoRepository.ObterTamanhos().ConfigureAwait(false);

        public async Task<List<Complemento>> ObterComplementos() =>
            await _complementoRepository.ObterTodos().ConfigureAwait(false);


        public async Task<ProdutoListViewModel> ObterListaProdutoPorCategoria(string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria)) return new ProdutoListViewModel(await _produtoRepository.ProdutoOrdenadoPorCategoriaId().ConfigureAwait(false), "Todos os lanches");

            return new ProdutoListViewModel(await _produtoRepository.ProdutoFiltroCategoriaNomeOrdenadoNome(categoria).ConfigureAwait(false), categoria);
        }

        public async Task<IEnumerable<Produto>> ObterListaProdutoComCategoriaComTamanho() =>
            await _produtoRepository.ObterProdutosTamanhosCategorias().ConfigureAwait(false);

        public async Task<ProdutoViewModel> ObterProdutoPorId(Guid produtoId) =>
            _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoCategoriaComplementoTamanhoPorId(produtoId).ConfigureAwait(false));

        public async Task<ProdutoListViewModel> PesquisarProduto(string palavraChave)
        {
            if (string.IsNullOrWhiteSpace(palavraChave)) return new ProdutoListViewModel(await _produtoRepository.ProdutoOrdenadoId().ConfigureAwait(false), "Todos os Lanches");

            var produto = await _produtoRepository.ProdutoPesquisaNome(palavraChave).ConfigureAwait(false);
            if (produto.Any())
                return new ProdutoListViewModel(produto, "Lanches");
            else
                return new ProdutoListViewModel(produto, "Nenhum lanche foi encontrado");
        }

        public async Task<Produto> ObterProdutoComCategoriaComTamanhoPorId(Guid id) =>
            await _produtoRepository.ObterProdutoCategoriaComplementoTamanhoPorId(id).ConfigureAwait(false);

        public async Task CriarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Produto>(produtoViewModel);

            await _produtoRepository.Adicionar(produto).ConfigureAwait(false);
            if (produtoViewModel.TamanhosSelecionados.Any()) await AdicionarTamanhos(produtoViewModel, produto).ConfigureAwait(false);
            if (produtoViewModel.ComplementosSelecionados.Any()) await AdicionarComplementos(produtoViewModel, produto.Id).ConfigureAwait(false);
        }

        public async Task<ProdutoViewModel> ObterProdutoViewModelParaAtualizar(Guid id)
        {
            var produtoViewModel = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoCategoriaComplementoTamanhoPorId(id).ConfigureAwait(false));

            produtoViewModel.TodosTamanhos = await ObterTamanhos().ConfigureAwait(false);
            produtoViewModel.TodosComplementos = await ObterComplementos().ConfigureAwait(false);
            return produtoViewModel;
        }

        public async Task AtualizarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Produto>(produtoViewModel);

            var listaProdutoTamanho = await _tamanhoProdutosRepository.ObterTamanhoPorProdutoId(produto.Id).ConfigureAwait(false);
            if (listaProdutoTamanho.Any()) await _tamanhoProdutosRepository.DeletarLista(listaProdutoTamanho).ConfigureAwait(false);

            if (produtoViewModel.TamanhosSelecionados.Any())
                await AdicionarTamanhos(produtoViewModel, produto).ConfigureAwait(false);
            else
                produto.ExisteTamanho = false;

            var listProdutoComplemento = await _complementoProdutoRepository.ObterComplementoPorProdutoId(produto.Id).ConfigureAwait(false);
            if(listProdutoComplemento.Any()) await _complementoProdutoRepository.DeletarLista(listProdutoComplemento).ConfigureAwait(false);
            
            await AdicionarComplementos(produtoViewModel, produto.Id).ConfigureAwait(false);
            await _produtoRepository.Atualizar(produto).ConfigureAwait(false);
        }

        public async Task DeletarProduto(Guid id) =>
            await _produtoRepository.Deletar(id).ConfigureAwait(false);

        public async Task<bool> VerificaNomeExiste(ProdutoViewModel produto) =>
            await _produtoRepository.NomeExiste(produto).ConfigureAwait(false);

        private async Task AdicionarComplementos(ProdutoViewModel produtoViewModel, Guid produtoId)
        {
            await _complementoProdutoRepository.AdicionarLista(new List<ComplementoProduto>(
                produtoViewModel.ComplementosSelecionados.Select(complementoSelecionadoId => new ComplementoProduto(Guid.Parse(complementoSelecionadoId), produtoId)))).ConfigureAwait(false);
        }

        private async Task AdicionarTamanhos(ProdutoViewModel produtoViewModel, Produto produto)
        {
            produto.ExisteTamanho = true;
            await _tamanhoProdutosRepository.AdicionarLista(new List<TamanhoProduto>(
                produtoViewModel.TamanhosSelecionados.Select(tamanhoSelecionadoId => new TamanhoProduto(Guid.Parse(tamanhoSelecionadoId), produto.Id)))).ConfigureAwait(false);
        }
    }
}