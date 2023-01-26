/*scroll navbar */

var myjsnavbar =document.querySelector('.jsnavbar')
var myjsnavbarcontent = document.querySelector(".jsnavbar-content"); 
function navbarevent(){
  

  myjsnavbar.classList.toggle("jsnavbar-contentx");
  myjsnavbarcontent.classList.toggle("jsnavbar-contentx");


}








var  navigation = document.querySelector(".navigation ");
var  main = document.querySelector(".main ");

var e = document.getElementById("employers");
var d = document.getElementById("listdemmandes");
var title = document.getElementById("#title")
var df =document.getElementById("demmanderefuser")
var da = document.getElementById("demmandeaccepter")


function toggle(){
  navigation.classList.toggle("activetoggle");
  main.classList.toggle("activetoggle"); 
  d.classList.toggle("activetoggle");  
  df.classList.toggle("activetoggle");  
  da.classList.toggle("activetoggle");  

  
}


function listemployers(){
    e.style.display="block"
    d.style.display="none"
}



function listedemmandes(){
 
    d.style.display="block"
    e.style.display="none"
    df.style.display="none";
   da.style.display="none";
}
function demmandeaccepter(){
    df.style.display="none";
    da.style.display="block";
    e.style.display="none";
   d.style.display="none";

}
function demmanderefuser(){
   da.style.display="none";
   df.style.display="block";
   e.style.display="none";
   d.style.display="none";
}

 
 
    function toggleMobileMenu(menu) {
    menu.classList.toggle('open');
}
 