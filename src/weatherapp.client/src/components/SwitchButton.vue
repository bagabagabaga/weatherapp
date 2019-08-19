// Based on https://codepen.io/iamturner/pen/GZVoXo

<template>
 
    
        <div class="switch-button" id="button-16">
          <input type="checkbox" class="checkbox">
          <div class="knobs"></div>
        </div>


</template>


<script>
export default {
  name: "SwitchButton",
  props: {
    day: String
  }
};
</script>


<style scoped>
.switch-button
{
    position: relative;
    top: 50%;
    width: 74px;
    height: 36px;
    margin: -20px auto 0 auto;
    overflow: hidden;
}

.checkbox
{
    position: relative;
    width: 100%;
    height: 100%;
    padding: 0;
    margin: 0;
    opacity: 0;
    cursor: pointer;
    z-index: 3;
}

/* Button 16 */
#button-16 .knobs:before
{
    content: '°C';
    position: absolute;
    top: 4px;
    left: 4px;
    width: 30px;
    height: 10px;
    color: #fff;
    font-size: 14px;
    font-weight: bold;
    text-align: center;
    color:white;
    line-height: 1;
    height:100%;
    padding: 9px 4px;
    background-color: #9ed3df;
    border-radius: 2px;
    transition: 0.3s ease all, left 0.3s cubic-bezier(0.18, 0.89, 0.35, 1.15);
}

#button-16 .checkbox:active + .knobs:before
{
    width: 46px;
}

#button-16 .checkbox:checked:active + .knobs:before
{
    margin-left: -26px;
}

#button-16 .checkbox:checked + .knobs:before
{
    content: '°F';
    left: 42px;
    color:white;
    background-color: #5eb5c9;
    height: 100%;
}
</style>