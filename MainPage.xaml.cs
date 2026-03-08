using System;
using System.Collections.ObjectModel;
using AppCompras.Models;

namespace AppCompras;

public partial class MainPage : ContentPage
{
    // Coleção explicitamente instanciada para evitar aviso do analisador (IDE0028)
    public ObservableCollection<Produto> Produtos { get; } = new ObservableCollection<Produto>();

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void OnSalvarClicked(object sender, EventArgs e)
    {
        // Ler de forma segura os textos das Entry (podem ser null antes do InitializeComponent)
        var descricao = DescricaoEntry?.Text;
        var quantidadeText = QuantidadeEntry?.Text;
        var precoText = PrecoEntry?.Text;

        // Validar e converter: quantidade -> int, preco -> double (Produto.Preco é double)
        if (!string.IsNullOrWhiteSpace(descricao) &&
            int.TryParse(quantidadeText, out int quantidade) &&
            double.TryParse(precoText, out double preco))
        {
            Produtos.Add(new Produto
            {
                Descricao = descricao,
                Quantidade = quantidade,
                Preco = preco
            });

            // Limpar campos se não nulos
            if (DescricaoEntry is not null)
                DescricaoEntry.Text = string.Empty;
            if (QuantidadeEntry is not null)
                QuantidadeEntry.Text = string.Empty;
            if (PrecoEntry is not null)
                PrecoEntry.Text = string.Empty;
        }
    }
}