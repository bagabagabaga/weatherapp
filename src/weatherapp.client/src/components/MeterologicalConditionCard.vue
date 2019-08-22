<template>
  <div class="col">
    <div class="card-content">
      <div>
        <div>
          <i :class="icon"></i>
          <p>{{value.toFixed(2)}}</p>
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