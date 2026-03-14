using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using AppCompras.Helpers;
using Microsoft.Maui.Controls;

namespace AppCompras;

public partial class MainPage : ContentPage
{
    // Lista principal de produtos
    public ObservableCollection<Produto> Produtos { get; } = new();

    // Lista filtrada que aparece na tela
    public ObservableCollection<Produto> ProdutosFiltrados { get; } = new();

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "MAUI BindingContext is used cross-platform in this app.")]
    public MainPage()
    {
        InitializeComponent();
        // BindingContext do MAUI é usado em todas as plataformas no app; suprimimos o aviso do analisador para evitar ruído.
        BindingContext = this;
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "MAUI Entry.Text is used cross-platform in this app.")]
    private void OnSalvarClicked(object sender, EventArgs e)
    {
        // Acessa os membros gerados pelo XAML (evita duplicação/ambiguidade)
        var descricao = DescricaoEntry?.Text;
        var quantidadeText = QuantidadeEntry?.Text;
        var precoText = PrecoEntry?.Text;

        if (!string.IsNullOrWhiteSpace(descricao) &&
            int.TryParse(quantidadeText, out int quantidade) &&
            double.TryParse(precoText, out double preco))
        {
            var produto = new Produto
            {
                Descricao = descricao!,
                Quantidade = quantidade,
                Preco = preco
            };

            Produtos.Add(produto);
            ProdutosFiltrados.Add(produto);

            if (DescricaoEntry != null)
                DescricaoEntry.Text = string.Empty;

            if (QuantidadeEntry != null)
                QuantidadeEntry.Text = string.Empty;

            if (PrecoEntry != null)
                PrecoEntry.Text = string.Empty;
        }
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "MAUI TextChangedEventArgs is used cross-platform in this app.")]
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        // Evita ToLower/ToUpper e usa comparação explícita ignorando maiúsculas/minúsculas
        var texto = e.NewTextValue ?? string.Empty;

        ProdutosFiltrados.Clear();

        foreach (var produto in Produtos)
        {
            if (!string.IsNullOrEmpty(produto.Descricao) &&
                produto.Descricao.Contains(texto, StringComparison.OrdinalIgnoreCase))
            {
                ProdutosFiltrados.Add(produto);
            }
        }
    }
}