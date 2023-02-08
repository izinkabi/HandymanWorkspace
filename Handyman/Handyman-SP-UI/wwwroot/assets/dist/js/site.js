


    var xValues = ["Italy", "France", "Spain", "USA", "Argentina"];
    var yValues = [55, 49, 44, 24, 15];
    var barColors = [
    "#b91d47",
    "#00aba9",
    "#2b5797",
    "#e8c3b9",
    "#1e7145"
    ];

    new Chart("myChart", {
        type: "doughnut",
    data: {
        labels: xValues,
    datasets: [{
        backgroundColor: barColors,
    data: yValues
    }]
  },
    options: {
        title: {
        display: true,
    text: "World Wide Wine Production 2018"
    }
  }
});
google.charts.load('current', { packages: ['corechart'] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {
    // Set Data
    var data = google.visualization.arrayToDataTable([
        ['Price', 'Size'],
        [50, 7], [60, 8], [70, 8], [80, 9], [90, 9],
        [100, 9], [110, 10], [120, 11],
        [130, 14], [140, 14], [150, 15]
    ]);
    // Set Options
    var options = {
        title: 'House Prices vs. Size',
        hAxis: { title: 'Square Meters' },
        vAxis: { title: 'Price in Millions' },
        legend: 'none'
    };
    // Draw
    var chart = new google.visualization.LineChart(document.getElementById('myChart'));
    chart.draw(data, options);
}