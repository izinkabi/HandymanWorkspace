// Write your Javascript code.
//TODO - Animations
//TODO -    FORM VALIDATION
//TODO - Chart display

//* This how we pass data and chart set up to 
const ctx = document.getElementById('bar-chart');

  new Chart(ctx, {
    type: 'bar',
    data: {
      labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec' ],
      datasets: [{
        label: 'Workshop',
        data: [1, 9, 3, 5, 2, 3, 4, 7, 3, 5, 2, 3],
        borderWidth: 1,
        backgroundColor: '#849BA0',
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

  console.log(Chart);