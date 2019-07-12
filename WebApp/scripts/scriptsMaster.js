 //datatables
    $(document).ready(function () {
        var handleDataTableButtons = function () {
            if ($("#datatableUser").length) {
                $("#datatableUser").DataTable({
                    dom: "Bfrtip",
                    buttons: [
                      {
                          extend: "copy",
                          className: "btn-sm btn-info",
                          text: "Copiar"
                      },
                      {
                          extend: "excel",
                          className: "btn-sm btn-success",
                          text: "Exportar excel"
                      },
                      {
                          extend: "pdfHtml5",
                          className: "btn-sm btn-warning",
                          text: "Gerar PDF"
                      },
                      {
                          extend: "print",
                          className: "btn-sm btn-primary",
                          text: " Imprimir"
                      },
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

        $('#datatable').dataTable();

        $('#datatable-keytable').DataTable({
            keys: true
        });

        $('#datatable-responsive').DataTable();

        $('#datatable-scroller').DataTable({
            ajax: "js/datatables/json/scroller-demo.json",
            deferRender: true,
            scrollY: 380,
            scrollCollapse: true,
            scroller: true
        });

        $('#datatable-fixed-header').DataTable({
            fixedHeader: true
        });

        var $datatable = $('#datatable-checkbox');

        $datatable.dataTable({
            'order': [[1, 'asc']],
            'columnDefs': [
              { orderable: false, targets: [0] }
            ]
        });
        $datatable.on('draw.dt', function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_flat-green'
            });
        });

        TableManageButtons.init();
    });

//Datatable dashboard
$(document).ready(function () {
    var handleDataTableButtons = function () {
        if ($("#tableDash").length) {
            $("#tableDash").DataTable({
                dom: "Bfrtip",
                buttons: [
                  {
                      extend: "copy",
                      className: "btn-sm btn-info",
                      text: "Copiar"
                  },
                  {
                      extend: "excel",
                      className: "btn-sm btn-success",
                      text: "Exportar excel"
                  },
                  {
                      extend: "pdfHtml5",
                      className: "btn-sm btn-warning",
                      text: "Gerar PDF"
                  },
                  {
                      extend: "print",
                      className: "btn-sm btn-dark",
                      text: " Imprimir"
                  },
                ],
                responsive: true,                    
                'iDisplayLength': 4
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

    $('#datatable').dataTable({
        responsive: true,            
    });

    $('#datatable-keytable').DataTable({
        keys: true
    });

    $('#datatable-responsive').DataTable();

    $('#datatable-scroller').DataTable({
        ajax: "js/datatables/json/scroller-demo.json",
        deferRender: true,
        scrollY: 380,
        scrollCollapse: true,
        scroller: true
    });

    $('#datatable-fixed-header').DataTable({
        fixedHeader: true
    });

    var $datatable = $('#datatable-checkbox');

    $datatable.dataTable({
        'order': [[1, 'asc']],
        'columnDefs': [
          { orderable: false, targets: [0] }
        ]         
    });
    $datatable.on('draw.dt', function () {
        $('input').iCheck({
            checkboxClass: 'icheckbox_flat-green'
        });
    });

    TableManageButtons.init();
});

//Datatable Pesquisas
$(document).ready(function () {
    var handleDataTableButtons = function () {
        if ($("#tablePesquisa").length) {
            $("#tablePesquisa").DataTable({
                dom: "Bfrtip",
                buttons: [
                  {
                      extend: "copy",
                      className: "btn-sm btn-info",
                      text: "Copiar"
                  },
                  {
                      extend: "excel",
                      className: "btn-sm btn-success",
                      text: "Exportar excel"
                  },
                  {
                      extend: "pdfHtml5",
                      className: "btn-sm btn-warning",
                      text: "Gerar PDF"
                  },                     
                ],
                responsive: true,
                'iDisplayLength': 20,
                'aLengthMenu': [[25, 50, 75, -1], [25, 50, 75, "All"]]
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

    $('#datatable').dataTable({
        responsive: true,
    });

    $('#datatable-keytable').DataTable({
        keys: true
    });

    $('#datatable-responsive').DataTable();

    $('#datatable-scroller').DataTable({
        ajax: "js/datatables/json/scroller-demo.json",
        deferRender: true,
        scrollY: 380,
        scrollCollapse: true,
        scroller: true
    });

    $('#datatable-fixed-header').DataTable({
        fixedHeader: true
    });

    var $datatable = $('#datatable-checkbox');

    $datatable.dataTable({
        'order': [[1, 'asc']],
        'columnDefs': [
          { orderable: false, targets: [0] }
        ]
    });
    $datatable.on('draw.dt', function () {
        $('input').iCheck({
            checkboxClass: 'icheckbox_flat-green'
        });
    });

    TableManageButtons.init();
});

//Datatable projetos em aberto
$(document).ready(function () {
    var handleDataTableButtons = function () {
        if ($("#tabAbertos").length) {
            $("#tabAbertos").DataTable({
                dom: "Bfrtip",
                buttons: [
                  {
                      extend: "copy",
                      className: "btn-sm btn-info",
                      text: "Copiar"
                  },
                  {
                      extend: "excel",
                      className: "btn-sm btn-success",
                      text: "Exportar excel"
                  },
                  {
                      extend: "pdfHtml5",
                      className: "btn-sm btn-warning",
                      text: "Gerar PDF"
                  },
                  {
                      extend: "print",
                      className: "btn-sm btn-primary",
                      text: " Imprimir"
                  },
                ],
                responsive: true,
                'iDisplayLength': 5
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

    $('#datatable').dataTable();

    $('#datatable-keytable').DataTable({
        keys: true
    });

    $('#datatable-responsive').DataTable();

    $('#datatable-scroller').DataTable({
        ajax: "js/datatables/json/scroller-demo.json",
        deferRender: true,
        scrollY: 380,
        scrollCollapse: true,
        scroller: true
    });

    $('#datatable-fixed-header').DataTable({
        fixedHeader: true
    });

    var $datatable = $('#datatable-checkbox');

    $datatable.dataTable({
        'order': [[1, 'asc']],
        'columnDefs': [
          { orderable: false, targets: [0] }
        ]
    });
    $datatable.on('draw.dt', function () {
        $('input').iCheck({
            checkboxClass: 'icheckbox_flat-green'
        });
    });

    TableManageButtons.init();
});

//Datatable projetos encerrados
$(document).ready(function () {
    var handleDataTableButtons = function () {
        if ($("#tabEncerrados").length) {
            $("#tabEncerrados").DataTable({
                dom: "Bfrtip",
                buttons: [
                  {
                      extend: "copy",
                      className: "btn-sm btn-info",
                      text: "Copiar"
                  },
                  {
                      extend: "excel",
                      className: "btn-sm btn-success",
                      text: "Exportar excel"
                  },
                  {
                      extend: "pdfHtml5",
                      className: "btn-sm btn-warning",
                      text: "Gerar PDF"
                  },
                  {
                      extend: "print",
                      className: "btn-sm btn-primary",
                      text: " Imprimir"
                  },
                ],
                responsive: true,
                'iDisplayLength': 5
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

    $('#datatable').dataTable();

    $('#datatable-keytable').DataTable({
        keys: true
    });

    $('#datatable-responsive').DataTable();

    $('#datatable-scroller').DataTable({
        ajax: "js/datatables/json/scroller-demo.json",
        deferRender: true,
        scrollY: 380,
        scrollCollapse: true,
        scroller: true
    });

    $('#datatable-fixed-header').DataTable({
        fixedHeader: true
    });

    var $datatable = $('#datatable-checkbox');

    $datatable.dataTable({
        'order': [[1, 'asc']],
        'columnDefs': [
          { orderable: false, targets: [0] }
        ]
    });
    $datatable.on('draw.dt', function () {
        $('input').iCheck({
            checkboxClass: 'icheckbox_flat-green'
        });
    });

    TableManageButtons.init();
});

//Adiciona anexo ao sistema de OS
$(document).ready(function () {
    $('#file').MultiFile({
        accept: 'gif|jpg|png|jpeg',
        max: 3,
        STRING: {
            remove: 'Remover',
            denied: 'Tipo do arquivo inválido $ext!',
            duplicate: 'Esse arquivo já foi selecionado:\n$file!'
        }
    });
});