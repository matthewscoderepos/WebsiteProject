// Write your JS in here
pics = [
	"imgs/kitty_bed.jpg",
	"imgs/kitty_basket.jpg", 
	"imgs/kitty_laptop.jpg",
	"imgs/kitty_door.jpg",
	"imgs/kitty_sink.jpg",
	"imgs/kitty_wall.jpg"
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