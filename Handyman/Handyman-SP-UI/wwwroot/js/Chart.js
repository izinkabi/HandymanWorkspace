

import "https://cdn.jsdelivr.net/npm/chart.js"
//* This how we pass data and chart set up to 
const ctx = document.getElementById('bar-chart');

export new Chart(ctx, {
    type: 'line',
    data: {
        labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        datasets: [{
            label: 'Expenses R',
            data: [4, 2, 3, 5, 6, 7, 8, 9, 10, 11, 12, 2],
            fill: false,
            borderColor: 'Red',
            tension: 0.1,
        },
        {
            label: 'Orders No',
            data: [8, 3, 4, 6, 8, 10, 1, 12, 16, 22, 44, 6],
            fill: false,
            borderColor: 'green',
            tension: 0.3,
        },
        {
            label: 'Money R',
            data: [1, 9, 3, 5, 2, 3, 4, 7, 3, 5, 2, 3],
            fill: false,
            borderColor: 'grey',
            tension: 0.2,
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});


