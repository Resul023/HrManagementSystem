
let btnArrow = document.querySelector(".btn-arrow")
btnArrow.addEventListener("click",function() {
    let sideBar = document.querySelector(".side-bar")
    let dashboard = document.querySelector(".dashboard")
    sideBar.classList.toggle("active-sidebar")
    dashboard.classList.toggle("width")
    btnArrow.classList.toggle("rotate-arrow")
    
})
let toogleMenu = document.querySelector(".toggle-menu")
toogleMenu.addEventListener("click",function() {
    let sideBar = document.querySelector(".side-bar")
    let dashboard = document.querySelector(".dashboard")
    sideBar.classList.toggle("active-sidebar")
    dashboard.classList.toggle("width")
    btnArrow.classList.toggle("rotate-arrow")
    
})
let checkBox = document.querySelectorAll(".form-check-input")
let lineThroughLabel= document.querySelectorAll(".form-check-label")
checkBox.forEach(ele => {
    ele.addEventListener("click",function() {
        ele.classList.toggle("toggle-checkbox")
        lineThroughLabel.forEach(elem => {
            if(elem.getAttribute("for") == ele.getAttribute("id")){
                elem.classList.toggle("toggle-label")
            }
        });
        
    })
});

let listItemSideBar= document.querySelectorAll(".list-hr .list-item")
listItemSideBar.forEach(ele => {
  ele.addEventListener("click",function() {
    let currentActiveItem = document.querySelector(".active-itemlist")
    currentActiveItem.classList.remove("active-itemlist")
    ele.classList.add("active-itemlist")
    
  })
});











