<template>
  <div class="p-2">
    <div class="card meterological-condition-card">
      <div class="card-content">
        <div>
          <i :class="icon"></i>
          <p>{{value.toFixed(1)}}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MeterologicalConditionCard",
  props: {
    icon: String,
    value: Number
  }
};
</script>
<style scoped>
.meterological-condition-card {
  color: #343d4b;
}
</style>