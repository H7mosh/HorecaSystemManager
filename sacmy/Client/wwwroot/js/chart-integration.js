// wwwroot/js/chart-integration.js
let salesChart = null;

window.initializeChart = function (canvasId) {
    try {
        const ctx = document.getElementById(canvasId);
        if (!ctx) {
            console.error('Canvas element not found:', canvasId);
            return;
        }

        // Destroy existing chart if it exists
        if (salesChart) {
            salesChart.destroy();
        }

        // Create new chart
        salesChart = new Chart(ctx, {
            type: 'pie',
            data: {
                labels: [],
                datasets: [{
                    data: [],
                    backgroundColor: [],
                    borderWidth: 1,
                    borderColor: 'rgba(255, 255, 255, 0.8)'
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        position: 'right',
                        labels: {
                            padding: 20,
                            font: {
                                size: 14
                            },
                            color: '#333'
                        }
                    },
                    tooltip: {
                        backgroundColor: 'rgba(0, 0, 0, 0.8)',
                        padding: 12,
                        titleFont: {
                            size: 14
                        },
                        bodyFont: {
                            size: 13
                        },
                        callbacks: {
                            label: function (context) {
                                let label = context.label || '';
                                let value = context.parsed || 0;
                                return ` ${label}: $${value.toLocaleString(undefined, {
                                    minimumFractionDigits: 2,
                                    maximumFractionDigits: 2
                                })}`;
                            }
                        }
                    }
                },
                animation: {
                    animateScale: true,
                    animateRotate: true
                }
            }
        });

        console.log('Chart initialized successfully');
    } catch (error) {
        console.error('Error initializing chart:', error);
    }
};

window.updateChart = function (canvasId, chartData) {
    try {
        if (!salesChart) {
            initializeChart(canvasId);
        }

        if (salesChart) {
            salesChart.data = chartData;
            salesChart.update('show');
            console.log('Chart updated with data:', chartData);
        }
    } catch (error) {
        console.error('Error updating chart:', error);
    }
};