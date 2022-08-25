// start Line chart
new Chart(document.getElementById("line-chart"), {
    type: 'line',
    data: {
      labels: ["Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct",],
      datasets: [{ 
          data: [2000,114,106,106,107,111,133,221,783,2478],
          label: "Sales",
          borderColor: "#f68c1f",
          fill: false
        }, { 
          data: [4000,3500,411,502,635,809,947,1402,3700,5267],
          label: "Marketing",
          borderColor: "#8e5ea2",
          fill: false
        }, { 
          data: [168,170,178,190,203,276,408,547,675,734],
          label: "Design",
          borderColor: "#3cba9f",
          fill: false
        }, { 
          data: [40,20,10,16,24,38,74,167,508,784],
          label: "Support",
          borderColor: "#e8c3b9",
          fill: false
        }, { 
          data: [6,3,2,2,7,26,82,172,312,433],
          label: "Development",
          borderColor: "#c45850",
          fill: false
        }
      ]
    },
    options: {
      title: {
        display: true,
        text: 'Total Salary by Unit'
      }
    }
  });
// end line chart