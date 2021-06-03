<template>
           <div class="loader-class">
            <img src="./loader.gif" alt="" class="loader-img" />
        </div> 
</template>

<style scoped>
.loader-class{
    display: flex;
    background: rgba(255, 255, 255, 0.68);
    position: absolute;
    align-items: center;
    justify-content: center;
    width: 100%;
    height: 100%;
    left: 0;
    top: 0;
    z-index: 9999;
}

.loader-img {
 width: 120px;
}
</style>