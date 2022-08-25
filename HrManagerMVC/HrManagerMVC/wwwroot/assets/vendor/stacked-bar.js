// start stacked bar
new Chart(document.getElementById("bar-chart-grouped"), {
    type: 'bar',
    data: {
      labels: ["Q1", "Q2", "Q3", "Q4"],
      datasets: [
        {
          label: "Developer",
          backgroundColor: "#f68c1f",
          data: [55,221,783,2478]
        }, {
          label: "Marketing",
          backgroundColor: "#583D72",
          data: [408,547,675,734]
        },
        {
            label: "Sales",
            backgroundColor: "#ded5c4",
            data: [408,547,675,734]
          }
      ]
    },
    options: {
      title: {
        display: true,
        text: 'Salary Statistics'
      }
    }
});
// end stacked bar