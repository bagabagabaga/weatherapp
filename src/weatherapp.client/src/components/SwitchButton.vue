// Based on https://codepen.io/iamturner/pen/GZVoXo

<template>
  <div class="switchToggle">
    <input type="checkbox" id="switch" v-on:change="changeSign" />
    <label for="switch">Toggle</label>
  </div>
</template>


<script>
export default {
  name: "SwitchButton",
  methods: {
    changeSign() {
      this.$emit("temperatureSignChanged");
    }
  }
};
</script>


<style scoped>
/* https://bootsnipp.com/snippets/j6k6b */
.switchToggle input[type="checkbox"] {
  height: 0;
  width: 0;
  visibility: hidden;
  position: absolute;
  margin: 0;
}

.switchToggle label {
  cursor: pointer;
  text-indent: -9999px;
  width: 70px;
  max-width: 70px;
  height: 30px;
  background: #222831;
  border-radius: 100px;
  position: relative;
}

.switchToggle label:after {
  content: "";
  position: absolute;
  top: 2px;
  left: 2px;
  width: 26px;
  height: 26px;
  background: #343d4b;
  border-radius: 90px;
  transition: 0.3s;
}

.switchToggle input:checked + label,
.switchToggle input:checked + input + label {
  background: #fff;
}

.switchToggle input + label:before,
.switchToggle input + input + label:before {
  content: "°F";
  position: absolute;
  top: 5px;
  left: 35px;
  width: 26px;
  height: 26px;
  border-radius: 90px;
  transition: 0.3s;
  text-indent: 0;
  color: #fff;
}
.switchToggle input:checked + label:before,
.switchToggle input:checked + input + label:before {
  content: "°C";
  position: absolute;
  top: 5px;
  left: 10px;
  width: 26px;
  height: 26px;
  border-radius: 90px;
  transition: 0.3s;
  text-indent: 0;
  color: #222831;
}
.switchToggle input:checked + label:after,
.switchToggle input:checked + input + label:after {
  left: calc(100% - 2px);
  transform: translateX(-100%);
}
.switchToggle label:active:after {
  width: 60px;
}
.toggle-switchArea {
  margin: 10px 0 10px 0;
}
</style>