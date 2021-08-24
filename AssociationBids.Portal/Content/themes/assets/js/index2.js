$(function(e) {
	'use strict'
	var ctx = document.getElementById("AreaChart4");
	var myChart = new Chart(ctx, {
		type: 'line',
		data: {
			labels: ['Date 1', 'Date 2', 'Date 3', 'Date 4', 'Date 5', 'Date 6', 'Date 7', 'Date 8', 'Date 9', 'Date 10', 'Date 11', 'Date 12', 'Date 13', 'Date 14', 'Date 15', 'Date 16', 'Date 17', 'Date 18', 'Date 19', 'Date 20', 'Date 21', 'Date 22', 'Date 23', 'Date 24', 'Date 25', 'Date 26', 'Date 27', 'Date 28', 'Date 29', 'Date 30'],
			type: 'line',
			datasets: [{
				data: [45, 0, 32, 67, 49, 72, 52, 55, 46, 54, 32, 74, 88, 96, 36, 32, 48, 54, 87, 88, 96, 53, 21, 24, 14, 58, 78, 55, 41, 21, 45, 54, 51, 52, 48],
				label: 'Admissions',
				backgroundColor: 'transparent',
				borderColor: '#ff6666',
				borderWidth: '3',
				pointBorderColor: 'transparent',
				pointBackgroundColor: 'transparent',
			}]
		},
		options: {
			maintainAspectRatio: false,
			legend: {
				display: false
			},
			responsive: true,
			tooltips: {
				mode: 'index',
				titleFontSize: 12,
				titleFontColor: '#7886a0',
				bodyFontColor: '#7886a0',
				backgroundColor: '#fff',
				titleFontFamily: 'Montserrat',
				bodyFontFamily: 'Montserrat',
				cornerRadius: 3,
				intersect: false,
			},
			scales: {
				xAxes: [{
					gridLines: {
						color: 'transparent',
						zeroLineColor: 'transparent'
					},
					ticks: {
						fontSize: 2,
						fontColor: 'transparent'
					}
				}],
				yAxes: [{
					display: false,
					ticks: {
						display: false,
					}
				}]
			},
			title: {
				display: false,
			},
			elements: {
				line: {
					borderWidth: 1
				},
				point: {
					radius: 4,
					hitRadius: 10,
					hoverRadius: 4
				}
			}
		}
	});
	var ctx = document.getElementById("AreaChart5");
	var myChart = new Chart(ctx, {
		type: 'line',
		data: {
			labels: ['Date 1', 'Date 2', 'Date 3', 'Date 4', 'Date 5', 'Date 6', 'Date 7', 'Date 8', 'Date 9', 'Date 10', 'Date 11', 'Date 12', 'Date 13', 'Date 14', 'Date 15', 'Date 16', 'Date 17', 'Date 18', 'Date 19', 'Date 20', 'Date 21', 'Date 22', 'Date 23', 'Date 24', 'Date 25', 'Date 26', 'Date 27', 'Date 28', 'Date 29', 'Date 30'],
			type: 'line',
			datasets: [{
				data: [88, 96, 36, 32, 48, 54, 87, 88, 96, 53, 21, 24, 14, 45, 0, 32, 67, 49, 72, 52, 55, 46, 54, 32, 74, 58, 78, 55, 41, 21, 45, 54, 51, 52, 48],
				label: 'Admissions',
				backgroundColor: 'transparent',
				borderColor: '#467fcf',
				borderWidth: '3',
				pointBorderColor: 'transparent',
				pointBackgroundColor: 'transparent',
			}]
		},
		options: {
			maintainAspectRatio: false,
			legend: {
				display: false
			},
			responsive: true,
			tooltips: {
				mode: 'index',
				titleFontSize: 12,
				titleFontColor: '#7886a0',
				bodyFontColor: '#7886a0',
				backgroundColor: '#fff',
				titleFontFamily: 'Montserrat',
				bodyFontFamily: 'Montserrat',
				cornerRadius: 3,
				intersect: false,
			},
			scales: {
				xAxes: [{
					gridLines: {
						color: 'transparent',
						zeroLineColor: 'transparent'
					},
					ticks: {
						fontSize: 2,
						fontColor: 'transparent'
					}
				}],
				yAxes: [{
					display: false,
					ticks: {
						display: false,
					}
				}]
			},
			title: {
				display: false,
			},
			elements: {
				line: {
					borderWidth: 1
				},
				point: {
					radius: 4,
					hitRadius: 10,
					hoverRadius: 4
				}
			}
		}
	});
	var ctx = document.getElementById("AreaChart6");
	var myChart = new Chart(ctx, {
		type: 'line',
		data: {
			labels: ['Date 1', 'Date 2', 'Date 3', 'Date 4', 'Date 5', 'Date 6', 'Date 7', 'Date 8', 'Date 9', 'Date 10', 'Date 11', 'Date 12', 'Date 13', 'Date 14', 'Date 15', 'Date 16', 'Date 17', 'Date 18', 'Date 19', 'Date 20', 'Date 21', 'Date 22', 'Date 23', 'Date 24', 'Date 25', 'Date 26', 'Date 27', 'Date 28', 'Date 29', 'Date 30'],
			type: 'line',
			datasets: [{
				data: [58, 78, 55, 41, 21, 45, 54, 51, 52, 48, 88, 96, 36, 32, 48, 24, 14, 45, 0, 32, 67, 49, 54, 87, 88, 96, 53, 21, 72, 52, 55, 46, 54, 32, 74],
				label: 'Admissions',
				backgroundColor: 'transparent',
				borderColor: '#fff',
				borderWidth: '3',
				pointBorderColor: 'transparent',
				pointBackgroundColor: 'transparent',
			}]
		},
		options: {
			maintainAspectRatio: false,
			legend: {
				display: false
			},
			responsive: true,
			tooltips: {
				mode: 'index',
				titleFontSize: 12,
				titleFontColor: '#7886a0',
				bodyFontColor: '#7886a0',
				backgroundColor: '#fff',
				titleFontFamily: 'Montserrat',
				bodyFontFamily: 'Montserrat',
				cornerRadius: 3,
				intersect: false,
			},
			scales: {
				xAxes: [{
					gridLines: {
						color: 'transparent',
						zeroLineColor: 'transparent'
					},
					ticks: {
						fontSize: 2,
						fontColor: 'transparent'
					}
				}],
				yAxes: [{
					display: false,
					ticks: {
						display: false,
					}
				}]
			},
			title: {
				display: false,
			},
			elements: {
				line: {
					borderWidth: 1
				},
				point: {
					radius: 4,
					hitRadius: 10,
					hoverRadius: 4
				}
			}
		}
	});
	var ctx = document.getElementById("AreaChart7");
	var myChart = new Chart(ctx, {
		type: 'line',
		data: {
			labels: ['Date 1', 'Date 2', 'Date 3', 'Date 4', 'Date 5', 'Date 6', 'Date 7', 'Date 8', 'Date 9', 'Date 10', 'Date 11', 'Date 12', 'Date 13', 'Date 14', 'Date 15', 'Date 16', 'Date 17', 'Date 18', 'Date 19', 'Date 20', 'Date 21', 'Date 22', 'Date 23', 'Date 24', 'Date 25', 'Date 26', 'Date 27', 'Date 28', 'Date 29', 'Date 30'],
			type: 'line',
			datasets: [{
				data: [88, 96, 36, 32, 48, 24, 14, 45, 0, 32, 45, 54, 51, 52, 48, 54, 67, 49, 58, 78, 55, 41, 21, 87, 88, 96, 53, 21, 72, 52, 55, 46, 54, 32, 74],
				label: 'Admissions',
				backgroundColor: 'transparent',
				borderColor: '#ffca4a',
				borderWidth: '3',
				pointBorderColor: 'transparent',
				pointBackgroundColor: 'transparent',
			}]
		},
		options: {
			maintainAspectRatio: false,
			legend: {
				display: false
			},
			responsive: true,
			tooltips: {
				mode: 'index',
				titleFontSize: 12,
				titleFontColor: '#7886a0',
				bodyFontColor: '#7886a0',
				backgroundColor: '#fff',
				titleFontFamily: 'Montserrat',
				bodyFontFamily: 'Montserrat',
				cornerRadius: 3,
				intersect: false,
			},
			scales: {
				xAxes: [{
					gridLines: {
						color: 'transparent',
						zeroLineColor: 'transparent'
					},
					ticks: {
						fontSize: 2,
						fontColor: 'transparent'
					}
				}],
				yAxes: [{
					display: false,
					ticks: {
						display: false,
					}
				}]
			},
			title: {
				display: false,
			},
			elements: {
				line: {
					borderWidth: 1
				},
				point: {
					radius: 4,
					hitRadius: 10,
					hoverRadius: 4
				}
			}
		}
	});
	/* Chartjs (#resolved-complaints) */
	var ctx = document.getElementById("resolved-complaints");
	var myChart = new Chart(ctx, {
		type: 'line',
		data: {
			labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct'],
			type: 'line',
			datasets: [{
				data: [1, 7, 3, 9, 4, 5, 2, 4, 1, 0],
				label: 'Resolved-complaints',
				backgroundColor: 'rgba(70, 127, 207, 0.8)',
				borderColor: 'rgba(70, 127, 207)',
			}, ]
		},
		options: {
			maintainAspectRatio: true,
			legend: {
				display: false
			},
			responsive: true,
			tooltips: {
				mode: 'index',
				titleFontSize: 12,
				titleFontColor: '#000',
				bodyFontColor: '#000',
				backgroundColor: '#fff',
				cornerRadius: 0,
				intersect: false,
			},
			scales: {
				xAxes: [{
					gridLines: {
						color: 'transparent',
						zeroLineColor: 'transparent'
					},
					ticks: {
						fontSize: 2,
						fontColor: 'transparent'
					}
				}],
				yAxes: [{
					display: false,
					ticks: {
						display: false,
					}
				}]
			},
			title: {
				display: false,
			},
			elements: {
				line: {
					borderWidth: 2
				},
				point: {
					radius: 0,
					hitRadius: 10,
					hoverRadius: 4
				}
			}
		}
	});
	/* Chartjs (#resolved-complaints) */
	var ctx = document.getElementById("resolved-complaints2");
	var myChart = new Chart(ctx, {
		type: 'line',
		data: {
			labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct'],
			type: 'line',
			datasets: [{
				data: [5, 2, 4, 1, 0, 1, 7, 3, 9, 4, ],
				label: 'Resolved-complaints',
				backgroundColor: 'rgba(94, 186, 0, 0.8)',
				borderColor: 'rgb(94, 186, 0)',
			}, ]
		},
		options: {
			maintainAspectRatio: true,
			legend: {
				display: false
			},
			responsive: true,
			tooltips: {
				mode: 'index',
				titleFontSize: 12,
				titleFontColor: '#000',
				bodyFontColor: '#000',
				backgroundColor: '#fff',
				cornerRadius: 0,
				intersect: false,
			},
			scales: {
				xAxes: [{
					gridLines: {
						color: 'transparent',
						zeroLineColor: 'transparent'
					},
					ticks: {
						fontSize: 2,
						fontColor: 'transparent'
					}
				}],
				yAxes: [{
					display: false,
					ticks: {
						display: false,
					}
				}]
			},
			title: {
				display: false,
			},
			elements: {
				line: {
					borderWidth: 2
				},
				point: {
					radius: 0,
					hitRadius: 10,
					hoverRadius: 4
				}
			}
		}
	});
	
	//Chart12
        var options = {
            chart: {
                height: 350,
                type: 'line',
                shadow: {
                    enabled: true,
                    color: '#000',
                    top: 18,
                    left: 7,
                    blur: 10,
                    opacity: 1
                },
                toolbar: {
                    show: false
                }
            },
            colors: ['#77B6EA', '#545454'],
            dataLabels: {
                enabled: true,
            },
            stroke: {
                curve: 'smooth'
            },
            series: [{
                    name: "High - 2013",
                    data: [28, 29, 33, 36, 32, 32, 33]
                },
                {
                    name: "Low - 2013",
                    data: [12, 11, 14, 18, 17, 13, 13]
                }
            ],
            title: {
                text: 'Average High & Low Temperature',
                align: 'left'
            },
            grid: {
                borderColor: '#e7e7e7',
                row: {
                    colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
                    opacity: 0.5
                },
            },
            markers: {
                
                size: 6
            },
            xaxis: {
                categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'],
                title: {
                    text: 'Month'
                }
            },
            yaxis: {
                title: {
                    text: 'Temperature'
                },
                min: 5,
                max: 40
            },
            legend: {
                position: 'top',
                horizontalAlign: 'right',
                floating: true,
                offsetY: -25,
                offsetX: -5
            }
        }

        var chart = new ApexCharts(
            document.querySelector("#chart12"),
            options
        );

        chart.render();

	//Radial chart
	var options = {
		chart: {
			type: 'radialBar',
			background: 'transparent',
			stroke: "#fff",
			color: "#fff"
		},
		plotOptions: {
			responsive: [{
				breakpoint: undefined,
				options: {},
			}],
			radialBar: {
				size: undefined,
				inverseOrder: false,
				startAngle: 0,
				endAngle: 360,
				offsetX: 0,
				offsetY: 0,
				hollow: {
					margin: 5,
					size: '50%',
					background: 'transparent',
					image: undefined,
					imageWidth: 150,
					imageHeight: 150,
					imageOffsetX: 0,
					imageOffsetY: 0,
					imageClipped: true,
					position: 'front',
					dropShadow: {
						enabled: false,
						top: 0,
						left: 0,
						blur: 3,
						opacity: 0.5
					}
				},
				track: {
					show: true,
					startAngle: undefined,
					endAngle: undefined,
					background: '#f9f9f9',
					strokeWidth: '97%',
					opacity: 1,
					margin: 5,
					dropShadow: {
						enabled: false,
						top: 0,
						left: 0,
						blur: 3,
						opacity: 0.5
					}
				},
				dataLabels: {
					show: true,
					name: {
						show: true,
						fontSize: '18px',
						fontFamily: undefined,
						color: undefined,
						offsetY: -10
					},
					value: {
						show: true,
						fontSize: '16px',
						fontFamily: undefined,
						color: undefined,
						offsetY: 16,
						formatter: function(val) {
							return val + '%'
						}
					},
					total: {
						show: false,
						label: 'Total',
						color: '#373d3f',
						formatter: function(w) {
							return w.globals.seriesTotals.reduce((a, b) => {
								return a + b
							}, 0) / w.globals.series.length + '%'
						}
					}
				}
			}
		},
		stroke: {
			lineCap: "round"
		},
		series: [44, 55, 67, 83],
		labels: ['Existing Customers', 'New Customers', 'Visiting Customers', 'Employes'],
		colors: ['#467fcf', '#5eba00', '#ffca4a', '#ff6666'],
	}
	var chart = new ApexCharts(document.querySelector("#pieChart"), options);
	chart.render();
	//Radial chart


	/*--Apex charts--*/
	var options = {
		chart: {
			height: 300,
			type: 'bar',
		},
		plotOptions: {
			bar: {
				horizontal: true,
			}
		},
		dataLabels: {
			enabled: false
		},
		series: [{
			name: 'Defect Rate',
			data: [48, 68, 57, 48, 79, 84, 85, 89, 158, 102, 325, 78]
		}],
		xaxis: {
			categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'June', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
		},
		yaxis: {},
		colors: ['#467fcf'],
		tooltip: {}
	}
	var chart = new ApexCharts(document.querySelector("#chart"), options);
	chart.render();
});