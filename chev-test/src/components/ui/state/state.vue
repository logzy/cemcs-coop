<template>
      <div style="z-index : 999999" class="app-modal-overlay">
    <div class="app-modal-div success" style="text-align:center">
    <div v-if="state == 'success'"> <div class="alert-icon"></div> <div class="alert-message">Operation Successful</div>  <div style="font-size:12px;margin-top:20px">{{message}}</div>
     <div @click="closeModal" style="margin-top:30px;cursor:pointer" class="app-modal-button">Close</div>
    </div> 
            
     <div v-else> 
            <div class="alert-icon failed"></div>
           <div class="alert-message">Operation failed</div>
     <div style="font-size:12px;margin-top:20px">{{message}}</div>
            <div @click="closeModal" style="margin-top:30px;cursor:pointer" class="app-modal-button">Close</div>
            </div>
    </div>
  </div>
</template>
<script>
export default {
  props: ["state", "closeModal", "message", "resetState"],
  methods: {
  },
};
</script>

