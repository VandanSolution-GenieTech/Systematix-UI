/*
 |--------------------------------------------------------------------------
 | Shards Dashboards: Charts Values
 |--------------------------------------------------------------------------
 */

'use strict';

(function ($) {
  $(document).ready(function () {

    // Blog overview date range init.
    $('#blog-overview-date-range').datepicker({});

    //
    // Small Stats
    //

    // Datasets
    var boSmallStatsDatasets = [
      {
        backgroundColor: 'rgba(0, 184, 216, 0.1)',
        borderColor: 'rgb(0, 184, 216)',
        data: [1, 2, 1, 3, 5, 4, 7],
      },
      {
        backgroundColor: 'rgba(23,198,113,0.1)',
        borderColor: 'rgb(23,198,113)',
        data: [1, 2, 3, 3, 3, 4, 4]
      },
      {
        backgroundColor: 'rgba(255,180,0,0.1)',
        borderColor: 'rgb(255,180,0)',
        data: [2, 3, 3, 3, 4, 3, 3]
      },
      {
        backgroundColor: 'rgba(255,65,105,0.1)',
        borderColor: 'rgb(255,65,105)',
        data: [1, 7, 1, 3, 1, 4, 8]
      },
      {
        backgroundColor: 'rgb(0,123,255,0.1)',
        borderColor: 'rgb(0,123,255)',
        data: [3, 2, 3, 2, 4, 5, 4]
      }
    ];

    // Options
    function boSmallStatsOptions(max) {
      return {
        maintainAspectRatio: true,
        responsive: true,
        // Uncomment the following line in order to disable the animations.
        // animation: false,
        legend: {
          display: false
        },
        tooltips: {
          enabled: false,
          custom: false
        },
        elements: {
          point: {
            radius: 0
          },
          line: {
            tension: 0.3
          }
        },
        scales: {
          xAxes: [{
            gridLines: false,
            scaleLabel: false,
            ticks: {
              display: false
            }
          }],
          yAxes: [{
            gridLines: false,
            scaleLabel: false,
            ticks: {
              display: false,
              // Avoid getting the graph line cut of at the top of the canvas.
              // Chart.js bug link: https://github.com/chartjs/Chart.js/issues/4790
              suggestedMax: max
            }
          }],
        },
      };
    }

    // Generate the small charts
    boSmallStatsDatasets.map(function (el, index) {
      var chartOptions = boSmallStatsOptions(Math.max.apply(Math, el.data) + 1);
      var ctx = document.getElementsByClassName('blog-overview-stats-small-' + (index + 1));
      new Chart(ctx, {
        type: 'line',
        data: {
          labels: ["Label 1", "Label 2", "Label 3", "Label 4", "Label 5", "Label 6", "Label 7"],
          datasets: [{
            label: 'Today',
            fill: 'start',
            data: el.data,
            backgroundColor: el.backgroundColor,
            borderColor: el.borderColor,
            borderWidth: 1.5,
          }]
        },
        options: chartOptions
      });
    });


    //
    // Blog Overview Users
    //

//    var bouCtx = document.getElementsByClassName('blog-overview-users')[0];

//    // Data
//    var bouData = {
//      // Generate the days labels on the X axis.
//      labels: Array.from(new Array(30), function (_, i) {
//        return i === 0 ? 1 : i;
//      }),
//      datasets: [{
//        label: 'PNL',
//        fill: 'start',
//        data: [500, 800, 320, 180, 240, 320, 230, 650, 590, 1200, 750, 940, 1420, 1200, 960, 1450, 1820, 2800, 2102, 1920, 3920, 3202, 3140, 2800, 3200, 3200, 3400, 2910, 3100, 4250],
//        backgroundColor: 'rgba(0,123,255,0.1)',
//        borderColor: 'rgba(0,123,255,1)',
//        pointBackgroundColor: '#ffffff',
//        pointHoverBackgroundColor: 'rgb(0,123,255)',
//        borderWidth: 1.5,
//        pointRadius: 0,
//        pointHoverRadius: 3
//      }]
//    };

//    // Options
//    var bouOptions = {
//      responsive: true,
//      legend: {
//        position: 'top'
//      },
//      elements: {
//        line: {
//          // A higher value makes the line look skewed at this ratio.
//          tension: 0.3
//        },
//        point: {
//          radius: 0
//        }
//      },
//      scales: {
//        xAxes: [{
//          gridLines: false,
//          ticks: {
//            callback: function (tick, index) {
//              // Jump every 7 values on the X axis labels to avoid clutter.
//              return index % 7 !== 0 ? '' : tick;
//            }
//          }
//        }],
//        yAxes: [{
//          ticks: {
//            suggestedMax: 45,
//            callback: function (tick, index, ticks) {
//              if (tick === 0) {
//                return tick;
//              }
//              // Format the amounts using Ks for thousands.
//              return tick > 999 ? (tick/ 1000).toFixed(1) + 'K' : tick;
//            }
//          }
//        }]
//      },
//      // Uncomment the next lines in order to disable the animations.
//      // animation: {
//      //   duration: 0
//      // },
//      hover: {
//        mode: 'nearest',
//        intersect: false
//      },
//      tooltips: {
//        custom: false,
//        mode: 'nearest',
//        intersect: false
//      }
//    };

//    // Generate the Analytics Overview chart.
//    window.BlogOverviewUsers = new Chart(bouCtx, {
//      type: 'LineWithLine',
//      data: bouData,
//      options: bouOptions
//    });

//    // Hide initially the first and last analytics overview chart points.
//    // They can still be triggered on hover.
//    var aocMeta = BlogOverviewUsers.getDatasetMeta(0);
//    aocMeta.data[0]._model.radius = 0;
//    aocMeta.data[bouData.datasets[0].data.length - 1]._model.radius = 0;

//    // Render the chart.
//    window.BlogOverviewUsers.render();

    //
    // Users by device pie chart
    //

    // Data
     var SecotorEXP = document.getElementById("SecotorEXP1").value;
     var SecotorEXPV = document.getElementById("SecotorEXPV1").value;

//     var SecotorPNL = document.getElementById("SecotorPNL").value;
//     var SecotorPNLV = document.getElementById("SecotorPNLV").value;

//     var IndustryEXP = document.getElementById("IndustryEXP").value;
//     var IndustryEXPV = document.getElementById("IndustryEXPV").value;

//     var IndustryPNL = document.getElementById("IndustryPNL").value;
//     var IndustryPNLV = document.getElementById("IndustryPNLV").value;

    //alert(strValue);
    var ubdData = {
      datasets: [{
        hoverBorderColor: '#ffffff',
        data: [document.getElementById("SecotorEXPV1").value, document.getElementById("SecotorEXPV2").value, document.getElementById("SecotorEXPV3").value,document.getElementById("SecotorEXPV4").value,document.getElementById("SecotorEXPV5").value,document.getElementById("SecotorEXPV6").value,document.getElementById("SecotorEXPV7").value],
        backgroundColor: [
          'rgba(0,123,255,0.9)',
          'rgba(0,123,255,0.7)',
          'rgba(0,123,255,0.5)',
          'rgba(0,123,255,0.3)'
        ]
      }],
      labels: [document.getElementById("SecotorEXP1").value, document.getElementById("SecotorEXP2").value, document.getElementById("SecotorEXP3").value,document.getElementById("SecotorEXP4").value,document.getElementById("SecotorEXP5").value,document.getElementById("SecotorEXP6").value,document.getElementById("SecotorEXP7").value]
    };


     var ubdData2 = {
      datasets: [{
        hoverBorderColor: '#ffffff',
        data: [document.getElementById("SecotorPNLV1").value, document.getElementById("SecotorPNLV2").value, document.getElementById("SecotorPNLV3").value,document.getElementById("SecotorPNLV4").value,document.getElementById("SecotorPNLV5").value,document.getElementById("SecotorPNLV6").value,document.getElementById("SecotorPNLV7").value],
        backgroundColor: [
          'rgba(0,123,255,0.9)',
          'rgba(0,123,255,0.7)',
          'rgba(0,123,255,0.5)',
          'rgba(0,123,255,0.3)'
        ]
      }],
      labels: [document.getElementById("SecotorPNL1").value, document.getElementById("SecotorPNL2").value, document.getElementById("SecotorPNL3").value,document.getElementById("SecotorPNL4").value,document.getElementById("SecotorPNL5").value,document.getElementById("SecotorPNL6").value,document.getElementById("SecotorPNL7").value]
    };


     var ubdData3 = {
      datasets: [{
        hoverBorderColor: '#ffffff',
        data: [document.getElementById("IndustryEXPV1").value, document.getElementById("IndustryEXPV2").value, document.getElementById("IndustryEXPV3").value,document.getElementById("IndustryEXPV4").value],
        backgroundColor: [
          'rgba(0,123,255,0.9)',
          'rgba(0,123,255,0.7)',
          'rgba(0,123,255,0.5)',
          'rgba(0,123,255,0.3)'
        ]
      }],
	labels: [document.getElementById("IndustryEXP1").value, document.getElementById("IndustryEXP2").value, document.getElementById("IndustryEXP3").value,document.getElementById("IndustryEXP4").value]
    };


     var ubdData4 = {
      datasets: [{
        hoverBorderColor: '#ffffff',
        data: [document.getElementById("IndustryPNLV1").value, document.getElementById("IndustryPNLV2").value, document.getElementById("IndustryPNLV3").value,document.getElementById("IndustryPNLV4").value],
        backgroundColor: [
          'rgba(0,123,255,0.9)',
          'rgba(0,123,255,0.7)',
          'rgba(0,123,255,0.5)',
          'rgba(0,123,255,0.3)'
        ]
      }],
	labels: [document.getElementById("IndustryPNL1").value, document.getElementById("IndustryPNL2").value, document.getElementById("IndustryPNL3").value,document.getElementById("IndustryPNL4").value]
    };







     var ubdData5 = {
      datasets: [{
        hoverBorderColor: '#ffffff',
        data: [document.getElementById("ClassEXPV1").value, document.getElementById("ClassEXPV2").value, document.getElementById("ClassEXPV3").value,document.getElementById("ClassEXPV4").value,document.getElementById("ClassEXPV5").value],
        backgroundColor: [
          'rgba(0,123,255,0.9)',
          'rgba(0,123,255,0.7)',
          'rgba(0,123,255,0.5)',
          'rgba(0,123,255,0.3)'
        ]
      }],
	labels: [document.getElementById("ClassEXP1").value, document.getElementById("ClassEXP2").value, document.getElementById("ClassEXP3").value,document.getElementById("ClassEXP4").value,document.getElementById("ClassEXP5").value]
    };


     var ubdData6 = {
      datasets: [{
        hoverBorderColor: '#ffffff',
        data: [document.getElementById("ClassPNLV1").value, document.getElementById("ClassPNLV2").value, document.getElementById("ClassPNLV3").value,document.getElementById("ClassPNLV4").value,document.getElementById("ClassPNLV5").value],
        backgroundColor: [
          'rgba(0,123,255,0.9)',
          'rgba(0,123,255,0.7)',
          'rgba(0,123,255,0.5)',
          'rgba(0,123,255,0.3)'
        ]
      }],
	labels: [document.getElementById("ClassPNL1").value, document.getElementById("ClassPNL2").value, document.getElementById("ClassPNL3").value,document.getElementById("ClassPNL4").value,document.getElementById("ClassPNL5").value]
    };





    // Options
    var ubdOptions = {
      legend: {
        display: false,
        labels: {
          padding: 25,
          boxWidth: 20
        }
      },
      cutoutPercentage: 0,
      // Uncomment the following line in order to disable the animations.
      // animation: false,
      tooltips: {
        custom: false,
        mode: 'index',
        position: 'nearest'
      }
    };

    


    var ctx = document.getElementById('myChart').getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: [document.getElementById("HoldingExp1").value, document.getElementById("HoldingExp2").value, document.getElementById("HoldingExp3").value, document.getElementById("HoldingExp4").value, document.getElementById("HoldingExp5").value, document.getElementById("HoldingExp6").value, document.getElementById("HoldingExp7").value, document.getElementById("HoldingExp8").value, document.getElementById("HoldingExp9").value, document.getElementById("HoldingExp10").value],
            datasets: [{
                label: '',
                data: [document.getElementById("HoldingExpV1").value, document.getElementById("HoldingExpV2").value, document.getElementById("HoldingExpV3").value, document.getElementById("HoldingExpV4").value, document.getElementById("HoldingExpV5").value, document.getElementById("HoldingExpV6").value, document.getElementById("HoldingExpV7").value, document.getElementById("HoldingExpV8").value, document.getElementById("HoldingExpV9").value, document.getElementById("HoldingExpV10").value],
                backgroundColor: [
                'rgba(255, 99, 132, 0.9)',
                'rgba(54, 162, 235, 0.9)',
                'rgba(255, 206, 86, 0.9)',
                'rgba(75, 192, 192, 0.9)',
                'rgba(153, 102, 255, 0.9)',
                'rgba(255, 159, 64, 0.9)',
                'rgba(255, 99, 132, 0.9)',
                'rgba(54, 162, 235, 0.9)',
                'rgba(255, 206, 86, 0.9)',
                'rgba(75, 192, 192, 0.9)'

            ],

                borderWidth: 1
            }]
        },

        options: {
        plugins: {
             legend: {
                 display: false,
                      },
                      },


            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }],
                 xAxes: [{
                    display: false
                    }]
            }
        }
    });


    var ubdCtx = document.getElementsByClassName('blog-users-by-device')[0];
    var ubdCty = document.getElementsByClassName('blog-users-by-device-1')[0];
    var ubdCty1 = document.getElementsByClassName('blog-users-by-device-2')[0];
    var ubdCty2 = document.getElementsByClassName('blog-users-by-device-3')[0];

    var ubdCty3 = document.getElementsByClassName('blog-users-by-device-4')[0];
    var ubdCty4 = document.getElementsByClassName('blog-users-by-device-5')[0];

    // Generate the users by device chart.
    window.ubdChart = new Chart(ubdCtx, {
      type: 'pie',
      data: ubdData,
      options: ubdOptions
    });

    window.ubdChart = new Chart(ubdCty, {
      type: 'pie',
      data: ubdData2,
      options: ubdOptions,
     });


    window.ubdChart = new Chart(ubdCty1, {
      type: 'pie',
      data: ubdData3,
      options: ubdOptions,
     });

    window.ubdChart = new Chart(ubdCty2, {
      type: 'pie',
      data: ubdData4,
      options: ubdOptions,
     });


    window.ubdChart = new Chart(ubdCty3, {
      type: 'pie',
      data: ubdData5,
      options: ubdOptions,
     });

    window.ubdChart = new Chart(ubdCty4, {
      type: 'pie',
      data: ubdData6,
      options: ubdOptions,
     });


  });
})(jQuery);