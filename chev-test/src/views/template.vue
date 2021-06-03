<template>
            <div class="app-admin-section">
            <div class="app-admin-col-1">
            <Leftbar/>
            </div>
            <div class="app-admin-col-2">
            <div class="admin-top-bar">
        <div class="admin-top-bar-left">
          <div class="settings-icon"></div>
        </div>
        <div class="admin-top-bar-right">
          <div class="admin-topbar-date">{{new Date().toLocaleString() | humanize}}</div>
        </div>
      </div>
            <div class="content-header">Page Name</div>
              <div class="content-sub2"> Page Description</div>

            </div>
            <div class="app-admin-col-3">
              <Rightbar />
            </div>
          </div>
</template>


<script>
import Leftbar from '../../components/leftbar/leftbar'
import Rightbar from '../../components/rightbar/rightbar'
export default {
  components: {
    Leftbar,
    Rightbar
  },
}

</script>

<style src=".././global.css"></style>