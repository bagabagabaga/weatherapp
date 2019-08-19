<template>
  <div class="column">
    <div class="card-content">
      <div class="media">
        <div class="media-content">
          <i class="fas fa-poo-storm"></i>
          <p>24th september 2019</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MeterologicalConditionCard",
  props: {
    icon: String
  }
};
</script>