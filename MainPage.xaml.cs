using AppCompras.Helpers;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace AppCompras;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Produto> Produtos { get; } = new();
    public ObservableCollection<Produto> ProdutosFiltrados { get; } = new();

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "MAUI BindingContext is used cross-platform in this app.")]
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void OnSalvarClicked(object sender, EventArgs e)
    {
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
                Preco = preco,
                DataCadastro = DataPicker.Date ?? DateTime.Now 
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

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
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

    
    private async void OnRelatorioClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RelatorioPage(Produtos));
    }
}