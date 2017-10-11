
var dataIn, dataFm, dataIn2, dataFm2;
function init_charts(rote) {
    var theme = {
        color: [
            '#26B99A', '#34495E', '#BDC3C7', '#3498DB',
            '#9B59B6', '#8abb6f', '#759c6a', '#bfd3b7'
        ],
        title: {
            itemGap: 8,
            textStyle: {
                fontWeight: 'normal',
                color: '#408829'
            }
        },
        dataRange: {
            color: ['#1f610a', '#97b58d']
        },
        toolbox: {
            color: ['#408829', '#408829', '#408829', '#408829']
        },
        tooltip: {
            backgroundColor: 'rgba(0,0,0,0.5)',
            axisPointer: {
                type: 'line',
                lineStyle: {
                    color: '#408829',
                    type: 'dashed'
                },
                crossStyle: {
                    color: '#408829'
                },
                shadowStyle: {
                    color: 'rgba(200,200,200,0.3)'
                }
            }
        },
        dataZoom: {
            dataBackgroundColor: '#eee',
            fillerColor: 'rgba(64,136,41,0.2)',
            handleColor: '#408829'
        },
        grid: {
            borderWidth: 0
        },
        categoryAxis: {
            axisLine: {
                lineStyle: {
                    color: '#408829'
                }
            },
            splitLine: {
                lineStyle: {
                    color: ['#eee']
                }
            }
        },
        valueAxis: {
            axisLine: {
                lineStyle: {
                    color: '#408829'
                }
            },
            splitArea: {
                show: true,
                areaStyle: {
                    color: ['rgba(250,250,250,0.1)', 'rgba(200,200,200,0.1)']
                }
            },
            splitLine: {
                lineStyle: {
                    color: ['#eee']
                }
            }
        },
        timeline: {
            lineStyle: {
                color: '#408829'
            },
            controlStyle: {
                normal: { color: '#408829' },
                emphasis: { color: '#408829' }
            }
        },
        k: {
            itemStyle: {
                normal: {
                    color: '#68a54a',
                    color0: '#a9cba2',
                    lineStyle: {
                        width: 1,
                        color: '#408829',
                        color0: '#86b379'
                    }
                }
            }
        },
        map: {
            itemStyle: {
                normal: {
                    areaStyle: {
                        color: '#ddd'
                    },
                    label: {
                        textStyle: {
                            color: '#c12e34'
                        }
                    }
                },
                emphasis: {
                    areaStyle: {
                        color: '#99d2dd'
                    },
                    label: {
                        textStyle: {
                            color: '#c12e34'
                        }
                    }
                }
            }
        },
        force: {
            itemStyle: {
                normal: {
                    linkStyle: {
                        strokeColor: '#408829'
                    }
                }
            }
        },
        chord: {
            padding: 4,
            itemStyle: {
                normal: {
                    lineStyle: {
                        width: 1,
                        color: 'rgba(128, 128, 128, 0.5)'
                    },
                    chordStyle: {
                        lineStyle: {
                            width: 1,
                            color: 'rgba(128, 128, 128, 0.5)'
                        }
                    }
                },
                emphasis: {
                    lineStyle: {
                        width: 1,
                        color: 'rgba(128, 128, 128, 0.5)'
                    },
                    chordStyle: {
                        lineStyle: {
                            width: 1,
                            color: 'rgba(128, 128, 128, 0.5)'
                        }
                    }
                }
            }
        },
        gauge: {
            startAngle: 225,
            endAngle: -45,
            axisLine: {
                show: true,
                lineStyle: {
                    color: [[0.2, '#86b379'], [0.8, '#68a54a'], [1, '#408829']],
                    width: 8
                }
            },
            axisTick: {
                splitNumber: 10,
                length: 12,
                lineStyle: {
                    color: 'auto'
                }
            },
            axisLabel: {
                textStyle: {
                    color: 'auto'
                }
            },
            splitLine: {
                length: 18,
                lineStyle: {
                    color: 'auto'
                }
            },
            pointer: {
                length: '90%',
                color: 'auto'
            },
            title: {
                textStyle: {
                    color: '#333'
                }
            },
            detail: {
                textStyle: {
                    color: 'auto'
                }
            }
        },
        textStyle: {
            fontFamily: 'Arial, Verdana, sans-serif'
        }
    };

    if ($('#echart_pie_top_produto').length) {

        var topProdutos = [];
        var ProdutosName = [];
        $.ajax({
            url: "/api/TopProduto?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                return true;
            },
            success: function (data) {

                for (var index in data) {
                    ProdutosName.push(data[index].NomeProduto);
                    topProdutos.push({ value: data[index].QuantidadeProduto, name: data[index].NomeProduto });
                }
            },
            complete: function () {
                var echartPie = echarts.init(document.getElementById('echart_pie_top_produto'), theme);

                echartPie.setOption({
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} unidades ({d}%)"
                    },
                    legend: {
                        x: 'center',
                        y: 'bottom',
                        data: ProdutosName
                    },
                    toolbox: {
                        show: true,
                        feature: {
                            magicType: {
                                show: true,
                                type: ['pie', 'funnel'],
                                option: {
                                    funnel: {
                                        x: '25%',
                                        width: '50%',
                                        funnelAlign: 'left',
                                        max: 1548
                                    }
                                }
                            },
                            restore: {
                                show: true,
                                title: "Restore"
                            },
                            saveAsImage: {
                                show: true,
                                title: "Save Image"
                            }
                        }
                    },
                    calculable: true,
                    series: [{
                        name: 'Produto',
                        type: 'pie',
                        radius: '55%',
                        center: ['50%', '48%'],
                        data: topProdutos
                    }]
                });

                var dataStyle = {
                    normal: {
                        label: {
                            show: false
                        },
                        labelLine: {
                            show: false
                        }
                    }
                };

                var placeHolderStyle = {
                    normal: {
                        color: 'rgba(0,0,0,0)',
                        label: {
                            show: false
                        },
                        labelLine: {
                            show: false
                        }
                    },
                    emphasis: {
                        color: 'rgba(0,0,0,0)'
                    }
                };
            }

        });



    }

    if ($('#echart_pie_lucroArtigo').length) {

        var topProdutos = [];
        var ProdutosName = [];
        $.ajax({
            url: "/api/LucrosPorProduto?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                return true;
            },
            success: function (data) {

                for (var index in data) {
                    ProdutosName.push(data[index].NomeProduto);
                    topProdutos.push({ value: data[index].Lucro.toFixed(2), name: data[index].NomeProduto });
                }


            },
            complete: function () {
                var echartPie = echarts.init(document.getElementById('echart_pie_lucroArtigo'), theme);

                echartPie.setOption({
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} € ({d}%)"
                    },
                    legend: {
                        x: 'center',
                        y: 'bottom',
                        data: ProdutosName
                    },
                    toolbox: {
                        show: true,
                        feature: {
                            magicType: {
                                show: true,
                                type: ['pie', 'funnel'],
                                option: {
                                    funnel: {
                                        x: '25%',
                                        width: '50%',
                                        funnelAlign: 'left',
                                        max: 1548
                                    }
                                }
                            },
                            restore: {
                                show: true,
                                title: "Restore"
                            },
                            saveAsImage: {
                                show: true,
                                title: "Save Image"
                            }
                        }
                    },
                    calculable: true,
                    series: [{
                        name: 'Artigo',
                        type: 'pie',
                        radius: '55%',
                        center: ['50%', '48%'],
                        data: topProdutos
                    }]
                });

                var dataStyle = {
                    normal: {
                        label: {
                            show: false
                        },
                        labelLine: {
                            show: false
                        }
                    }
                };

                var placeHolderStyle = {
                    normal: {
                        color: 'rgba(0,0,0,0)',
                        label: {
                            show: false
                        },
                        labelLine: {
                            show: false
                        }
                    },
                    emphasis: {
                        color: 'rgba(0,0,0,0)'
                    }
                };
            }

        });



    }

    if ($('#echart_pie_custoProduto').length) {

        var topProdutos = [];
        var ProdutosName = [];
        $.ajax({
            url: "/api/custosPorProduto?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                return true;
            },
            success: function (data) {

                for (var index in data) {
                    ProdutosName.push(data[index].NomeProduto);
                    topProdutos.push({ value: data[index].Total.toFixed(2), name: data[index].NomeProduto });
                }


            },
            complete: function () {
                var echartPie = echarts.init(document.getElementById('echart_pie_custoProduto'), theme);

                echartPie.setOption({
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} € ({d}%)"
                    },
                    legend: {
                        x: 'center',
                        y: 'bottom',
                        data: ProdutosName
                    },
                    toolbox: {
                        show: true,
                        feature: {
                            magicType: {
                                show: true,
                                type: ['pie', 'funnel'],
                                option: {
                                    funnel: {
                                        x: '25%',
                                        width: '50%',
                                        funnelAlign: 'left',
                                        max: 1548
                                    }
                                }
                            },
                            restore: {
                                show: true,
                                title: "Restore"
                            },
                            saveAsImage: {
                                show: true,
                                title: "Save Image"
                            }
                        }
                    },
                    calculable: true,
                    series: [{
                        name: 'Produto',
                        type: 'pie',
                        radius: '55%',
                        center: ['50%', '48%'],
                        data: topProdutos
                    }]
                });

                var dataStyle = {
                    normal: {
                        label: {
                            show: false
                        },
                        labelLine: {
                            show: false
                        }
                    }
                };

                var placeHolderStyle = {
                    normal: {
                        color: 'rgba(0,0,0,0)',
                        label: {
                            show: false
                        },
                        labelLine: {
                            show: false
                        }
                    },
                    emphasis: {
                        color: 'rgba(0,0,0,0)'
                    }
                };
            }

        });



    }

    if ($('#echart_pie_quantidadeArtigo').length) {

        var topProdutos = [];
        var ProdutosName = [];
        $.ajax({
            url: "/api/quantidadeProdutos?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                return true;
            },
            success: function (data) {

                for (var index in data) {
                    ProdutosName.push(data[index].NomeProduto);
                    topProdutos.push({ value: data[index].Quantidade, name: data[index].NomeProduto });
                }


            },
            complete: function () {
                var echartPie = echarts.init(document.getElementById('echart_pie_quantidadeArtigo'), theme);

                echartPie.setOption({
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} unidades ({d}%)"
                    },
                    legend: {
                        x: 'center',
                        y: 'bottom',
                        data: ProdutosName
                    },
                    toolbox: {
                        show: true,
                        feature: {
                            magicType: {
                                show: true,
                                type: ['pie', 'funnel'],
                                option: {
                                    funnel: {
                                        x: '25%',
                                        width: '50%',
                                        funnelAlign: 'left',
                                        max: 1548
                                    }
                                }
                            },
                            restore: {
                                show: true,
                                title: "Restore"
                            },
                            saveAsImage: {
                                show: true,
                                title: "Save Image"
                            }
                        }
                    },
                    calculable: true,
                    series: [{
                        name: 'Artigo',
                        type: 'pie',
                        radius: '55%',
                        center: ['50%', '48%'],
                        data: topProdutos
                    }]
                });

                var dataStyle = {
                    normal: {
                        label: {
                            show: false
                        },
                        labelLine: {
                            show: false
                        }
                    }
                };

                var placeHolderStyle = {
                    normal: {
                        color: 'rgba(0,0,0,0)',
                        label: {
                            show: false
                        },
                        labelLine: {
                            show: false
                        }
                    },
                    emphasis: {
                        color: 'rgba(0,0,0,0)'
                    }
                };
            }

        });



    }

    if ($('#echart_pie2_top_cliente').length) {

        var topClientes = [];
        var ClientesName = [];
        $.ajax({
            url: "/api/TopClientes?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                return true;
            },
            success: function (data) {

                for (var index in data) {
                    ClientesName.push(data[index].NomeCliente);
                    topClientes.push({ value: data[index].QuantidadeVendida.toFixed(2), name: data[index].NomeCliente });

                }
            },
            complete: function () {
                var echartPieCollapse = echarts.init(document.getElementById('echart_pie2_top_cliente'), theme);

                echartPieCollapse.setOption({
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} € ({d}%)"
                    },
                    legend: {
                        x: 'center',
                        y: 'bottom',
                        data: ClientesName
                    },
                    toolbox: {
                        show: true,
                        feature: {
                            magicType: {
                                show: true,
                                type: ['pie', 'funnel']
                            },
                            restore: {
                                show: true,
                                title: "Restore"
                            },
                            saveAsImage: {
                                show: true,
                                title: "Save Image"
                            }
                        }
                    },
                    calculable: true,
                    series: [{
                        name: 'Cliente',
                        type: 'pie',
                        radius: [25, 90],
                        center: ['50%', 170],
                        roseType: 'area',
                        x: '50%',
                        max: 40,
                        sort: 'ascending',
                        data: topClientes
                    }]
                });
            }

        });
    }

    if ($('#echart_pie2_lucroCliente').length) {

        var topClientes = [];
        var ClientesName = [];
        $.ajax({
            url: "/api/valorGanhoCliente?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                return true;
            },
            success: function (data) {

                for (var index in data) {
                    ClientesName.push(data[index].NomeCliente);
                    topClientes.push({ value: data[index].Valor.toFixed(2), name: data[index].NomeCliente });

                }
            },
            complete: function () {
                var echartPieCollapse = echarts.init(document.getElementById('echart_pie2_lucroCliente'), theme);

                echartPieCollapse.setOption({
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} € ({d}%)"
                    },
                    legend: {
                        x: 'center',
                        y: 'bottom',
                        data: ClientesName
                    },
                    toolbox: {
                        show: true,
                        feature: {
                            magicType: {
                                show: true,
                                type: ['pie', 'funnel']
                            },
                            restore: {
                                show: true,
                                title: "Restore"
                            },
                            saveAsImage: {
                                show: true,
                                title: "Save Image"
                            }
                        }
                    },
                    calculable: true,
                    series: [{
                        name: 'Cliente',
                        type: 'pie',
                        radius: [25, 90],
                        center: ['50%', 170],
                        roseType: 'area',
                        x: '50%',
                        max: 40,
                        sort: 'ascending',
                        data: topClientes
                    }]
                });
            }

        });
    }

    if ($('#echart_pie2_gastoFornecedor').length) {

        var topClientes = [];
        var ClientesName = [];
        $.ajax({
            url: "/api/valorGastoFornecedor?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                return true;
            },
            success: function (data) {

                for (var index in data) {
                    ClientesName.push(data[index].NomeFornecedor);
                    topClientes.push({ value: data[index].ValorGasto.toFixed(2), name: data[index].NomeFornecedor });

                }
            },
            complete: function () {
                var echartPieCollapse = echarts.init(document.getElementById('echart_pie2_gastoFornecedor'), theme);

                echartPieCollapse.setOption({
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} € ({d}%)"
                    },
                    legend: {
                        x: 'center',
                        y: 'bottom',
                        data: ClientesName
                    },
                    toolbox: {
                        show: true,
                        feature: {
                            magicType: {
                                show: true,
                                type: ['pie', 'funnel']
                            },
                            restore: {
                                show: true,
                                title: "Restore"
                            },
                            saveAsImage: {
                                show: true,
                                title: "Save Image"
                            }
                        }
                    },
                    calculable: true,
                    series: [{
                        name: 'Fornecedor',
                        type: 'pie',
                        radius: [25, 90],
                        center: ['50%', 170],
                        roseType: 'area',
                        x: '50%',
                        max: 40,
                        sort: 'ascending',
                        data: topClientes
                    }]
                });
            }

        });
    }

    if ($('#echart_pie2_valorArtigo').length) {

        var topClientes = [];
        var ClientesName = [];
        $.ajax({
            url: "/api/valorProdutos?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                return true;
            },
            success: function (data) {

                for (var index in data) {
                    ClientesName.push(data[index].NomeProduto);
                    topClientes.push({ value: data[index].Valor.toFixed(2), name: data[index].NomeProduto });

                }
            },
            complete: function () {
                var echartPieCollapse = echarts.init(document.getElementById('echart_pie2_valorArtigo'), theme);

                echartPieCollapse.setOption({
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} € ({d}%)"
                    },
                    legend: {
                        x: 'center',
                        y: 'bottom',
                        data: ClientesName
                    },
                    toolbox: {
                        show: true,
                        feature: {
                            magicType: {
                                show: true,
                                type: ['pie', 'funnel']
                            },
                            restore: {
                                show: true,
                                title: "Restore"
                            },
                            saveAsImage: {
                                show: true,
                                title: "Save Image"
                            }
                        }
                    },
                    calculable: true,
                    series: [{
                        name: 'Artigo',
                        type: 'pie',
                        radius: [25, 90],
                        center: ['50%', 170],
                        roseType: 'area',
                        x: '50%',
                        max: 40,
                        sort: 'ascending',
                        data: topClientes
                    }]
                });
            }

        });
    }

    if ($('#echart_donut_top_fornecedor').length) {

        var topFornecedores = [];
        var FornecedoresName = [];
        $.ajax({
            url: "/api/TopFornecedores?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                return true;
            },
            success: function (data) {

                for (var index in data) {
                    FornecedoresName.push(data[index].NomeFornecedor);
                    topFornecedores.push({ value: data[index].QuantidadeComprada.toFixed(2), name: data[index].NomeFornecedor });

                }

            },
            complete: function () {

                var echartDonut = echarts.init(document.getElementById('echart_donut_top_fornecedor'), theme);

                echartDonut.setOption({
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} € ({d}%)"
                    },
                    calculable: true,
                    legend: {
                        x: 'center',
                        y: 'bottom',
                        data: FornecedoresName
                    },
                    toolbox: {
                        show: true,
                        feature: {
                            magicType: {
                                show: true,
                                type: ['pie', 'funnel'],
                                option: {
                                    funnel: {
                                        x: '25%',
                                        width: '50%',
                                        funnelAlign: 'center',
                                        max: 1548
                                    }
                                }
                            },
                            restore: {
                                show: true,
                                title: "Restore"
                            },
                            saveAsImage: {
                                show: true,
                                title: "Save Image"
                            }
                        }
                    },
                    series: [{
                        name: 'Fornecedor',
                        type: 'pie',
                        radius: ['35%', '55%'],
                        itemStyle: {
                            normal: {
                                label: {
                                    show: true
                                },
                                labelLine: {
                                    show: true
                                }
                            },
                            emphasis: {
                                label: {
                                    show: true,
                                    position: 'center',
                                    textStyle: {
                                        fontSize: '14',
                                        fontWeight: 'normal'
                                    }
                                }
                            }
                        },
                        data: topFornecedores
                    }]
                });
            }

        });




    }

    if ($('#echart_line').length) {

        var year1 = dataIn + "-" + dataFm;

        var year2 = dataIn2 + "-" + dataFm2;
        var labelInferior = [];
        var valores = [];
        $.ajax({
            url: "/api/" + rote + "?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                return true;
            },
            success: function (data) {
                var a = []
                for (var index in data) {
                    labelInferior.push(data[index].Data);
                    a.push(data[index].Total.toFixed(2));

                }
                valores.push(a);
            },
            complete: function () {
                $.ajax({
                    url: "/api/" + rote + "?dataIn='" + dataIn2 + "'&dataFm='" + dataFm2 + "'",
                    type: 'GET',
                    async: true,
                    cache: false,
                    timeout: 30000,
                    error: function () {
                        return true;
                    },
                    success: function (data) {
                        var a = []
                        for (var index in data) {
                            labelInferior.push(data[index].Data);
                            a.push(data[index].Total.toFixed(2));

                        }
                        valores.push(a);
                    },
                    complete: function () {
                        var echartLine = echarts.init(document.getElementById('echart_line'), theme);

                        echartLine.setOption({

                            tooltip: {
                                trigger: 'axis'
                            },
                            legend: {
                                x: 'center',
                                y: 'bottom',
                                data: [year1, year2]
                            },
                            toolbox: {
                                show: true,
                                feature: {
                                    magicType: {
                                        show: true,
                                        title: {
                                            line: 'Line',
                                            bar: 'Bar',
                                            stack: 'Stack',
                                            tiled: 'Tiled'
                                        },
                                        type: ['line', 'bar', 'stack', 'tiled']
                                    },
                                    restore: {
                                        show: true,
                                        title: "Restore"
                                    },
                                    saveAsImage: {
                                        show: true,
                                        title: "Save Image"
                                    }
                                }
                            },
                            calculable: true,
                            xAxis: [{
                                type: 'category',
                                boundaryGap: false,
                                data: labelInferior
                            }],
                            yAxis: [{
                                type: 'value'
                            }],
                            series: [{
                                name: year1,
                                type: 'line',
                                smooth: true,
                                itemStyle: {
                                    normal: {
                                        areaStyle: {
                                            type: 'default'
                                        }
                                    }
                                },
                                data: valores[0]
                            }, {
                                name: year2,
                                type: 'line',
                                smooth: true,
                                itemStyle: {
                                    normal: {
                                        areaStyle: {
                                            type: 'default'
                                        }
                                    }
                                },
                                data: valores[1]
                            }]
                        });


                    }
                });
            }
        });
    }

}

function init_datePicker() {

    if (typeof ($.fn.daterangepicker) === 'undefined') {
        return;
    }
    console.log('init_daterangepicker_right');

    var cb = function (start, end, label) {
        console.log(start.toISOString(), end.toISOString(), label);
        $('#reportrange_right span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
    };
    dataIn = moment().subtract(29, 'days').format('YYYY/MM/DD');
    dataFm = moment().format('YYYY/MM/DD');
    dataIn2 = moment().subtract(29, 'days').subtract(1, 'year').format('YYYY/MM/DD');
    dataFm2 = moment().subtract(1, 'year').format('YYYY/MM/DD');
    var optionSet1 = {
        startDate: moment().subtract(29, 'days'),
        endDate: moment(),
        minDate: '01/01/2012',
        maxDate: '12/31/2050',
        dateLimit: {
            days: 60
        },
        showDropdowns: true,
        showWeekNumbers: true,
        timePicker: false,
        timePickerIncrement: 1,
        timePicker12Hour: true,
        ranges: {
            'Este Mês': [moment().startOf('month'), moment().endOf('month')],
            'Ultimo Mês': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
            'Este Ano': [moment().startOf('year'), moment().endOf('year')],
            'Ultimo Ano': [moment().subtract(1, 'year').startOf('year'), moment().subtract(1, 'year').endOf('year')]
        },
        opens: 'right',
        buttonClasses: ['btn btn-default'],
        applyClass: 'btn-small btn-primary',
        cancelClass: 'btn-small',
        format: 'YYYY/MM/DD',
        separator: ' to ',
        locale: {
            applyLabel: 'Confirmar',
            cancelLabel: 'Limpar',
            fromLabel: 'From',
            toLabel: 'To',
            customRangeLabel: 'Custom',
            daysOfWeek: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S'],
            monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            firstDay: 1
        }
    };

    $('#reportrange_right span').html(moment().subtract(29, 'days').format('MMMM D, YYYY') + ' - ' + moment().format('MMMM D, YYYY'));
    $('#reportrange_right').daterangepicker(optionSet1, cb);

    $('#reportrange_right').on('show.daterangepicker', function () {
        console.log("show event fired");
    });
    $('#reportrange_right').on('hide.daterangepicker', function () {
        console.log("hide event fired");
    });
    $('#reportrange_right').on('apply.daterangepicker', function (ev, picker) {
        console.log("apply event fired, start/end dates are " + picker.startDate.format('YYYY/MM/DD') + " to " + picker.endDate.format('YYYY/MM/DD'));

        dataIn = picker.startDate.format('YYYY/MM/DD');
        dataFm = picker.endDate.format('YYYY/MM/DD');
        dataIn2 = picker.startDate.subtract(1, 'year').format('YYYY/MM/DD');
        dataFm2 = picker.endDate.subtract(1, 'year').format('YYYY/MM/DD');
        init_component();
    });
    $('#reportrange_right').on('cancel.daterangepicker', function (ev, picker) {
        console.log("cancel event fired");
    });

    $('#options1').click(function () {
        $('#reportrange_right').data('daterangepicker').setOptions(optionSet1, cb);
    });

    $('#options2').click(function () {
        $('#reportrange_right').data('daterangepicker').setOptions(optionSet2, cb);
    });

    $('#destroy').click(function () {
        $('#reportrange_right').data('daterangepicker').remove();
    });

}

function init_table(str, rote) {

    if (str == "#datatable-buttons_dividas_cliente" || str == "#datatable-buttons_dividas_fornecedor") {

        var tbody = document.createElement("tbody");

        $.ajax({
            url: "/api/" + rote + "?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                return true;
            },
            success: function (data) {

                for (var index in data) {

                    var tr = document.createElement("tr");
                    var nome = document.createElement("th");
                    nome.innerHTML = data[index].Nome;
                    tr.appendChild(nome);

                    var serie = document.createElement("th");
                    serie.innerHTML = data[index].Serie;
                    tr.appendChild(serie);

                    var numDoc = document.createElement("th");
                    numDoc.innerHTML = data[index].NumDocumento;
                    tr.appendChild(numDoc);

                    var valor = document.createElement("th");
                    valor.innerHTML = data[index].Valor.toFixed(2);
                    tr.appendChild(valor);

                    tbody.appendChild(tr);
                }

                $(str).find('tbody').replaceWith(tbody);
            },
            complete: function () {
                console.log('run_datatables');

                if ($.fn.dataTable.isDataTable(str)) {
                    var table = $(str).DataTable();
                    table.destroy();
                }
                console.log('init_DataTables');

                var handleDataTableButtons = function () {
                    if ($(str).length) {
                        $(str).DataTable({
                            dom: "Bfrtip",
                            buttons: [
                                {
                                    extend: "copy",
                                    className: "btn-sm"
                                },
                                {
                                    extend: "csv",
                                    className: "btn-sm"
                                },
                                {
                                    extend: "excel",
                                    className: "btn-sm"
                                },
                                {
                                    extend: "pdfHtml5",
                                    className: "btn-sm"
                                },
                                {
                                    extend: "print",
                                    className: "btn-sm"
                                }
                            ],
                            responsive: true
                        });
                    }
                };

                TableManageButtons = function () {
                    "use strict";
                    return {
                        init: function () {
                            handleDataTableButtons();
                        }
                    };
                }();

                TableManageButtons.init();
            }
        });
    }

    if (str == "#datatable-buttons_ProdutosCliente" || str == "#datatable-buttons_ProdutosFornecedor") {


        var tbody = document.createElement("tbody");

        $.ajax({
            url: "/api/" + rote + "?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                return true;
            },
            success: function (data) {

                for (var index in data) {

                    var tr = document.createElement("tr");
                    var nome = document.createElement("th");
                    nome.innerHTML = data[index].Nome;
                    tr.appendChild(nome);

                    var nomeProduto = document.createElement("th");
                    nomeProduto.innerHTML = data[index].NomeProduto;
                    tr.appendChild(nomeProduto);

                    var quantidade = document.createElement("th");
                    quantidade.innerHTML = data[index].Quantidade;
                    tr.appendChild(quantidade);

                    tbody.appendChild(tr);
                }

                $(str).find('tbody').replaceWith(tbody);
            },
            complete: function () {
                console.log('run_datatables');

                if ($.fn.dataTable.isDataTable(str)) {
                    var table = $(str).DataTable();
                    table.destroy();
                }
                console.log('init_DataTables');

                var handleDataTableButtons = function () {
                    if ($(str).length) {
                        $(str).DataTable({
                            dom: "Bfrtip",
                            buttons: [
                                {
                                    extend: "copy",
                                    className: "btn-sm"
                                },
                                {
                                    extend: "csv",
                                    className: "btn-sm"
                                },
                                {
                                    extend: "excel",
                                    className: "btn-sm"
                                },
                                {
                                    extend: "pdfHtml5",
                                    className: "btn-sm"
                                },
                                {
                                    extend: "print",
                                    className: "btn-sm"
                                }
                            ],
                            responsive: true
                        });
                    }
                };

                TableManageButtons = function () {
                    "use strict";
                    return {
                        init: function () {
                            handleDataTableButtons();
                        }
                    };
                }();

                TableManageButtons.init();
            }
        });
    }



}

function init_financial_indicators() {
    var valor;
    if ($("#liquidezGeral").length) {
        $.ajax({
            url: "/api/liquidezGeral?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                valor = "Não foi possivel carregar";
                return true;
            },
            success: function (data) {
                if (data != null) {
                    if (data.Sucess == true) {

                        valor = data.ValorLiquidezGeral;

                    } else {

                        valor = "Não foi possivel carregar";
                    }
                } else {

                    valor = "Não foi possivel carregar";
                }


            },
            complete: function () {
                if (valor != "Não foi possivel carregar")
                    $("#liquidezGeral").find('div').html(valor + "€").css("font-size", "");
                else {
                    $("#liquidezGeral").find('div').html(valor).css("font-size", "12px");
                }
            }
        });

    }

    if ($("#faturacao").length) {


        $.ajax({
            url: "/api/faturacao?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                valor = "Não foi possivel carregar";
                return true;
            },
            success: function (data) {
                if (data != null) {
                    if (data.Sucess == true) {

                        valor = data.ValorFaturacao;

                    } else {

                        valor = "Não foi possivel carregar";
                    }
                } else {

                    valor = "Não foi possivel carregar";
                }


            },
            complete: function () {
                if (valor != "Não foi possivel carregar")
                    $("#faturacao").find('div').html(valor + "€").css("font-size", "");
                else {
                    $("#faturacao").find('div').html(valor).css("font-size", "12px");
                }
            }
        });

    }

    if ($("#roa").length) {
        $.ajax({
            url: "/api/roa?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                valor = "Não foi possivel carregar";
                return true;
            },
            success: function (data) {
                if (data != null) {
                    if (data.Sucess == true) {

                        valor = data.ValorRoa;

                    } else {

                        valor = "Não foi possivel carregar";
                    }
                } else {

                    valor = "Não foi possivel carregar";
                }


            },
            complete: function () {
                if (valor != "Não foi possivel carregar")
                    $("#roa").find('div').html(valor + "%").css("font-size", "");
                else {
                    $("#roa").find('div').html(valor).css("font-size", "12px");
                }
            }
        });

    }

    if ($("#autoFinanceira").length) {
        $.ajax({
            url: "/api/AutoFinanceira?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                valor = "Não foi possivel carregar";
                return true;
            },
            success: function (data) {
                if (data != null) {
                    if (data.Sucess == true) {

                        valor = data.ValorAutoFinanceira;

                    } else {

                        valor = "Não foi possivel carregar";
                    }
                } else {

                    valor = "Não foi possivel carregar";
                }


            },
            complete: function () {
                if (valor != "Não foi possivel carregar")
                    $("#autoFinanceira").find('div').html(valor + "%").css("font-size", "");
                else {
                    $("#autoFinanceira").find('div').html(valor).css("font-size", "12px");
                }
            }
        });

    }

    if ($("#roe").length) {
        $.ajax({
            url: "/api/Roe?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                valor = "Não foi possivel carregar";
                return true;
            },
            success: function (data) {
                if (data != null) {
                    if (data.Sucess == true) {

                        valor = data.ValorRoe;

                    } else {

                        valor = "Não foi possivel carregar";
                    }
                } else {

                    valor = "Não foi possivel carregar";
                }


            },
            complete: function () {
                if (valor != "Não foi possivel carregar")
                    $("#roe").find('div').html(valor + "%").css("font-size", "");
                else {
                    $("#roe").find('div').html(valor).css("font-size", "12px");
                }
            }
        });

    }
}

function init_sales_indicators() {
    var valor;

    if ($("#totalVendas").length) {
        $.ajax({
            url: "/api/TotalVendas?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                valor = "Não foi possivel carregar";
                return true;
            },
            success: function (data) {
                if (data != null) {
                    if (data.Sucess == true) {

                        valor = data.ValorTotalVendas;

                    } else {

                        valor = "Não foi possivel carregar";
                    }
                } else {

                    valor = "Não foi possivel carregar";
                }


            },
            complete: function () {
                if (valor != "Não foi possivel carregar")
                    $("#totalVendas").find('div').html(valor.toFixed(2) + " €").css("font-size", "");
                else {
                    $("#totalVendas").find('div').html(valor).css("font-size", "12px");
                }
            }
        });

    }

    if ($("#pmRecebimentos").length) {


        $.ajax({
            url: "/api/PmRecebimentos?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                valor = "Não foi possivel carregar";
                return true;
            },
            success: function (data) {
                if (data != null) {
                    if (data.Sucess == true && !isNaN(data.ValorPmRecebimentos)) {

                        valor = data.ValorPmRecebimentos;

                    } else {

                        valor = "Não foi possivel carregar";
                    }
                } else {

                    valor = "Não foi possivel carregar";
                }


            },
            complete: function () {
                if (valor != "Não foi possivel carregar") {
                    $("#pmRecebimentos").find('div').html(valor.toFixed(2) + " Dias").css("font-size", "");
                } else {
                    $("#pmRecebimentos").find('div').html(valor).css("font-size", "12px");
                }
            }
        });

    }

    if ($("#rentabilidadeVendas").length) {
        $.ajax({
            url: "/api/RentabilidadeVendas?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                valor = "Não foi possivel carregar";
                return true;
            },
            success: function (data) {
                if (data != null) {
                    if (data.Sucess == true && !isNaN(data.ValorRentabilidadeVendas)) {

                        valor = data.ValorRentabilidadeVendas;

                    } else {

                        valor = "Não foi possivel carregar";
                    }
                } else {

                    valor = "Não foi possivel carregar";
                }


            },
            complete: function () {
                if (valor != "Não foi possivel carregar")
                    $("#rentabilidadeVendas").find('div').html(valor.toFixed(2) + " %").css("font-size", "");
                else {
                    $("#rentabilidadeVendas").find('div').html(valor).css("font-size", "12px");
                }
            }
        });

    }
}

function init_purchasing_indicators() {
    var valor;

    if ($("#totalCompras").length) {
        $.ajax({
            url: "/api/TotalCompras?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                valor = "Não foi possivel carregar";
                return true;
            },
            success: function (data) {
                if (data != null) {
                    if (data.Sucess == true) {

                        valor = data.ValorTotalCompras;

                    } else {

                        valor = "Não foi possivel carregar";
                    }
                } else {

                    valor = "Não foi possivel carregar";
                }


            },
            complete: function () {
                if (valor != "Não foi possivel carregar")
                    $("#totalCompras").find('div').html(valor.toFixed(2) + " €").css("font-size", "");
                else {
                    $("#totalCompras").find('div').html(valor).css("font-size", "12px");
                }
            }
        });

    }

    if ($("#pmPagamentos").length) {


        $.ajax({
            url: "/api/PmPagamentos?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                valor = "Não foi possivel carregar";
                return true;
            },
            success: function (data) {
                if (data != null) {
                    if (data.Sucess == true && !isNaN(data.ValorPmPagamentos)) {

                        valor = data.ValorPmPagamentos;

                    } else {

                        valor = "Não foi possivel carregar";
                    }
                } else {

                    valor = "Não foi possivel carregar";
                }


            },
            complete: function () {
                if (valor != "Não foi possivel carregar")
                    $("#pmPagamentos").find('div').html(valor.toFixed(2) + " Dias").css("font-size", "");
                else {
                    $("#pmPagamentos").find('div').html(valor).css("font-size", "12px");
                }
            }
        });

    }


}

function init_stock_indicators() {
    var valor;

    if ($("#valorStock").length) {
        $.ajax({
            url: "/api/ValorStock?dataIn='" + dataIn + "'&dataFm='" + dataFm + "'",
            type: 'GET',
            async: true,
            cache: false,
            timeout: 30000,
            error: function () {
                valor = "Não foi possivel carregar";
                return true;
            },
            success: function (data) {
                if (data != null) {
                    if (data.Sucess == true) {

                        valor = data.Valor_Stock;

                    } else {

                        valor = "Não foi possivel carregar";
                    }
                } else {

                    valor = "Não foi possivel carregar";
                }


            },
            complete: function () {
                if (valor != "Não foi possivel carregar")
                    $("#valorStock").find('div').html(valor.toFixed(2) + " €").css("font-size", "");
                else {
                    $("#valorStock").find('div').html(valor).css("font-size", "12px");
                }
            }
        });


    }
}

function init_indicators() {
    init_financial_indicators();
    init_sales_indicators();
    init_purchasing_indicators();
    init_stock_indicators();

    $("#dashboard_financas").on('click', function () {
        window.location = "../Financas";
    });
    $("#dashboard_vendas").on('click', function () {
        window.location = "../Vendas";
    });
    $("#dashboard_compras").on('click', function (
    ) {
        window.location = "../Compras";
    });
    $("#dashboard_stock").on('click', function (
    ) {
        window.location = "../Stock";
    });
}

function init_geral_dashboard() {

    var text = $("#nome").html();
    text = text.replace(/Nome\sde\sUser/g, sessionStorage.getItem('user'));
    $("#nome").html(text);


    var text = $(".nome").html();
    text = text.replace(/Nome\sde\sUser/g, sessionStorage.getItem('user'));
    $(".nome").html(text);
    init_indicators();
    init_charts();
}

function init_stock_dashboard() {
    var text = $("#nome").html();
    text = text.replace(/Nome\sde\sUser/g, sessionStorage.getItem('user'));
    $("#nome").html(text);


    var text = $(".nome").html();
    text = text.replace(/Nome\sde\sUser/g, sessionStorage.getItem('user'));
    $(".nome").html(text);
    init_charts("ValorStockData");
}

function init_sales_dashboard() {
    var text = $("#nome").html();
    text = text.replace(/Nome\sde\sUser/g, sessionStorage.getItem('user'));
    $("#nome").html(text);


    var text = $(".nome").html();
    text = text.replace(/Nome\sde\sUser/g, sessionStorage.getItem('user'));
    $(".nome").html(text);
    init_charts("TotalVendasData");
    init_table("#datatable-buttons_ProdutosCliente", "ProdutosPorCliente");
}

function init_purchasing_dashboard() {
    var text = $("#nome").html();
    text = text.replace(/Nome\sde\sUser/g, sessionStorage.getItem('user'));
    $("#nome").html(text);


    var text = $(".nome").html();
    text = text.replace(/Nome\sde\sUser/g, sessionStorage.getItem('user'));
    $(".nome").html(text);
    init_charts("totalCustos");
    init_table("#datatable-buttons_ProdutosFornecedor", "ProdutosPorFornecedor");

}

function init_financial_dashboard() {
    var text = $("#nome").html();
    text = text.replace(/Nome\sde\sUser/g, sessionStorage.getItem('user'));
    $("#nome").html(text);


    var text = $(".nome").html();
    text = text.replace(/Nome\sde\sUser/g, sessionStorage.getItem('user'));
    $(".nome").html(text);
    init_table("#datatable-buttons_dividas_cliente", "DividasCliente");
    init_table("#datatable-buttons_dividas_fornecedor", "DividasFornecedor");
}

function init_login() {
    $("#Button_Login").on('click', function () {

        var password = $("#password").val();
        var username = $("#username").val();


        $.ajax({
            url: "/api/login",
            type: 'POST',
            timeout: 3000,
            data: { "Nome": username, "Pass": password },

            error: function (data) {


                var div = document.createElement('div');
                var strong = document.createElement('strong');


                $(div).addClass('alert alert-danger');
                $(div).text("Username ou Password Incorreta");

                $(div).append(strong);

                $("body").append(div);
                $(div).fadeIn("slow").delay(3000).fadeOut("slow");

            },
            success: function () {
                window.sessionStorage.setItem('user', username);

               
                window.location.href = "../Home/";
                

            }
        });

    });
}

function init_logout() {

    
    $("#logout").on('click', function () {
        sessionStorage.removeItem('user');        
    });
}

function verification_session() {
    var user = sessionStorage.getItem('user');
    if (user == null) {
        window.location.href = "../Login/"
    }
}

function verification_session_login() {
    var user = window.sessionStorage.getItem('user');
    
    if (user != null) {
        window.location.href = "../Home/"
    }
}

function init_component() {
   
    if ($("head>title").text().toLowerCase().indexOf('geral') > -1) {
        verification_session()
        init_geral_dashboard();
        init_logout();
    }

    if ($("head>title").text().toLowerCase().indexOf("vendas") > -1) {
        verification_session()
        init_sales_dashboard();
        init_logout();
    }

    if ($("head>title").text().toLowerCase().indexOf("compras") > -1) {
        verification_session()
        init_purchasing_dashboard();
        init_logout();
    }

    if ($("head>title").text().toLowerCase().indexOf("finanças") > -1) {
        verification_session()
        init_financial_dashboard();
        init_logout();
    }

    if ($("head>title").text().toLowerCase().indexOf("stock") > -1) {
        verification_session()
        init_stock_dashboard();
        init_logout();
    }


    if ($("head>title").text().toLowerCase().indexOf("login") > -1) {
        verification_session_login();
        init_login();
        
    }

}

$(document).ready(function () {
    init_datePicker();
    init_component();
});