using AppCompras.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppCompras;

public partial class RelatorioPage : ContentPage
{
    private ObservableCollection<Produto> _produtos;

    public RelatorioPage(ObservableCollection<Produto> produtos)
    {
        InitializeComponent();
        _produtos = produtos;
    }

    private async void OnFiltrarClicked(object sender, EventArgs e)
    {
        try
        {
            // dtInicio.Date e dtFim.Date podem ser DateTime? — usar coalescência para valores seguros
            DateTime inicio = dtInicio?.Date ?? DateTime.MinValue;
            DateTime fim = dtFim?.Date ?? DateTime.MaxValue;

            if (inicio > fim)
            {
                await DisplayAlertAsync("Aviso", "Data de início maior que a data de fim.", "OK");
                return;
            }

            var filtrados = _produtos
                .Where(p => p.DataCadastro >= inicio && p.DataCadastro <= fim)
                .ToList();

            listaRelatorio.ItemsSource = filtrados;
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
    }
}