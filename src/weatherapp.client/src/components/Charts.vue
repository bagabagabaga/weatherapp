<template>
  <apexcharts id="forecast-graph" height="350" type="line" :options="chartOptions" :series="series"></apexcharts>
</template>

<script>
import VueApexCharts from "vue-apexcharts";
import ApexCharts from "apexcharts";

export default {
  components: {
    apexcharts: VueApexCharts
  },
  name: "Charts",
  props: {
    isFahrenheit: Boolean
  },
  methods: {
    scaleTemperature: function(temperature, isFahrenheit) {
      if (isFahrenheit) {
        return (((temperature - 273.15) * 9) / 5 + 32).toFixed(1);
      } else {
        return (temperature - 273.15).toFixed(1);
      }
    },
    async updateGraph(temperatures, humidities, dates, isFahrenheit) {
      this.series = [
        {
          name: "Temperature",
          data: temperatures.map(x => this.scaleTemperature(x, isFahrenheit))
        },
        {
          name: "Humidity",
          data: humidities.map(x => x.toFixed(1))
        }
      ];
      ApexCharts.exec("forecast-graph", "updateOptions", {
        xaxis: {
          categories: dates.map(x =>
            new Date(x).toLocaleDateString("en", {
              year: "numeric",
              month: "short",
              day: "numeric"
            })
          )
        }
      });
    }
  },
  data: function() {
    return {
      chartOptions: {
        chart: {
          id: "forecast-graph",
          width: "100%",
          toolbar: {
            show: false
          }
        },
        colors: ["#fff", "#41b883"],
        stroke: {
          curve: "smooth"
        },
        tooltip: {
          enabled: true,
          followCursor: true,
          theme: "dark"
        },
        legend: {
          show: false
        }
      },
      series: [],
      tooltip: {
        enabled: false
      }
    };
  }
};
</script>
<style scoped>
.apexcharts-tooltip {
  background: #223344;
  color: orange;
}
</style>