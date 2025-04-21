const prices = [];
const labels = [];

const chartData = {
    labels: labels,
    datasets: [{
        label: 'Stock price',
        data: prices,
        fill: false,
        borderColor: '#0a8ac6',
        tension: 0.1
    }]
};

const chartOptions = {
    scales: {
        x: {
            ticks: {display: false}
        }
    },
    plugins: {legend: {display: false}},
    tooltips: {mode: 'index'}
};

const ctx = document.getElementById('stock-chart').getContext('2d');

const chart = new Chart(ctx, {type: 'line', data: chartData, options: chartOptions});

var d = new Date();
document.querySelector(".date").innerText = d.toLocaleDateString();