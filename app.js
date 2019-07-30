// Write your JS in here
pics = [
	"imgs/Casino.PNG",
	"imgs/FullDB.PNG", 
	"imgs/GameOfLife.PNG",
	"imgs/JumpingJim.PNG",
	"imgs/Launcher.PNG",
	"imgs/RGBHerring.PNG",	
	"imgs/SmallDB.PNG",
	"imgs/TripTracker.jpg",
]
alts = [
	"A Casino game suite with BlackJack, Roulette, and Poker. Written in C# with WPF.",
	"A full database project with frontend HTML and backend written in Java. Database stored in Postgres and SQL.", 
	"A cellular automata project written in Java using Processing. Allows the user to control the parameters of Conways Game of Life in an interactive and visually pleasing environment.",
	"A graphsearch project. Uses Depth and Breadth first searches to solve the puzzle in the Jumping Jim problem.",
	"A unified video game launcher. Allows the user to launch games from the seven major game launchers. Uses APIs from all seven launchers to allow easy access and statistics tracking for games.",
	"An image stegonagrapy project written in MIPS using MARS. Includes six diffentent encoding methods for hiding plain text messages in bitmap images and their respective decoding methods.",	
	"An In-Memory database project used for query practice. Uses publicly available data from basketballconference.com",
	"An Android application written in Java. Provides the user with full control of a Google Map, allows for private and public waypoint creation, allows the user to track trips including the start, endtimes, speed and distance of the trip, as well as provides the current local weather conditions.",
]

var btn = document.querySelector('#next');
var img = document.querySelector("img");
var desc = document.querySelector("#desc");
var itr = 1;

btn.addEventListener("click", function(){
	img.src = pics[itr];
	img.alt = alts[itr];
	desc.textContent = alts[itr];
	itr = (itr+1) % pics.length;
});

function myFunction(x){
	x.classList.toggle("change");
}