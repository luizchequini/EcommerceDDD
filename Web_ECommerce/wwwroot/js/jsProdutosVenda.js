var ObjetoVenda = new Object();


ObjetoVenda.AdicionaNoCarrinho = function (idproduto) {

    var nome = $('#nome_' + idproduto).val();
    var qtd = $('#qtd_' + idproduto).val();

    $.ajax({
        type: 'POST',
        url: "/api/AdicionaProdutoNoCarrinho",
        dataType: "JSON",
        cache: false,
        async: true,
        data: {

            "id": idproduto, "nome": nome, "qtd": qtd
        },
        success: function (data) {

        }
    });

}

ObjetoVenda.CarregaProduto = function () {
    $.ajax({
        type: 'GET',
        url: "/api/ListaProdutoComEstoque",
        dataType: "JSON",
        cache: false,
        async: true,
        success: function (data) {

            var htmlConteudo = "";

            data.forEach(function (entitie) {

                htmlConteudo += "<div class='col-xs-12 col-sm-4 col-md-4 col-lg-4'>";

                var idNome = 'nome_' + entitie.id;
                var idQtd = 'qtd_' + entitie.id;

                htmlConteudo += "<label id:'" + idNome + "'> Produto: " + entitie.nome + "</label></br>";
                htmlConteudo += "<label> Valor: " + entitie.valor + "</label></br>";

                htmlConteudo += "quantidade: <input type='number' value='1'" + idQtd + "'>";

                htmlConteudo += "<input type='button' onclick='ObjetoVenda.AdicionaNoCarrinho'(" + entitie.id + ")' value = 'Comprar' </br>";

                htmlConteudo += "</div>"
            });

            $("#DivVendas").html(htmlConteudo);
        }
    });
}

$(function () {
    ObjetoVenda.CarregaProduto();
});