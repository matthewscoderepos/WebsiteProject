// Write your JS in here
pics = [
	"imgs/Casino.PNG",
	"imgs/FullDB.PNG", 
	"imgs/GameOfLife.PNG",
	"imgs/JumpingJim.PNG",
	"imgs/Launcher.PNG",
	"imgs/RGBHerring.PNG",	
	"imgs/SmallDB.PNG",
	"imgs/TripTracker.PNG",
]

var btn = document.querySelector('#next');
var img = document.querySelector("img");
var itr = 1;

btn.addEventListener("click", function(){
	img.src = pics[itr];
	itr = (itr+1) % pics.length;
});

function myFunction(x){
	x.classList.toggle("change");
}