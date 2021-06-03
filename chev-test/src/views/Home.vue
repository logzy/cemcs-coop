<template>
  <div class="home">
    <!-- <Header></Header> -->
    <Home-Index></Home-Index>
  </div>  
</template>

<script>
// @ is an alias to /src

// import Header from "../components/layout/headers/headerDashboard.vue";
import HomeIndex from "../components/HomeIndex.vue";
export default {
  name: "Home",
  components: {
    // Header,
    HomeIndex
  },
  mounted () {
        document.title = "Login | Chevron CEMCS Corporative"
    },
};
</script>
