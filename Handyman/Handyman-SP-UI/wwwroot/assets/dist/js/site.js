import 'https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.4/Chart.js'

var xValues = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"];
export function InitPieChart() {
    var yValues = [55, 49, 44, 24, 15];
    var barColors = [
        "#b91d47",
        "#00aba9",
        "#2b5797",
        "#e8c3b9",
        "#1e7145"
    ];

    new Chart("myChart", {
        type: "pie",
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
                text: "Jobs statistics"
            }
        }
    });
}


export function HideBagde() {
    $(document).ready(function () {
       
        $("badge").hide("slow", function () {
                
        });
        
    });
}


