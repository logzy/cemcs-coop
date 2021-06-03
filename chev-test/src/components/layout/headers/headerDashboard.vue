<template>
  <div class="header-nav">
    <div>
      <a href="/" class="router-link-active">
        <img
          src="../../../assets/images/chevron-cemcs-logo.png"
          class="chevron-logo"
          alt="Chevron_logo"
        />
      </a>
    </div>
    <div class="avatar-logout">
      <router-link to>
        <button @click="logout" class="logout-btn">
          <span>
            <font-awesome-icon icon="power-off" class="angle-left" />
          </span>
          Logout
        </button>
      </router-link>
    </div>
  </div>
</template>
<script>
export default {};
</script>
<style></style>
