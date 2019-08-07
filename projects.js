// Write your JS in here
pics = [
	"../imgs/MainImages/FullDB.png", 
	"../imgs/MainImages/GameOfLife.PNG",
	"../imgs/MainImages/JumpingJim.PNG",
	"../imgs/MainImages/Launcher.PNG",
	"../imgs/MainImages/RGBHerring.PNG",	
	"../imgs/MainImages/SmallDB.PNG",
	"../imgs/MainImages/TripTracker.jpg",
	"../imgs/MainImages/Casino.PNG",
]
alts = [
	"A full database project with frontend HTML and backend written in Java. Database stored in Postgres and SQL.", 
	"A cellular automata project written in Java using Processing. Allows the user to control the parameters of Conways Game of Life in an interactive and visually pleasing environment.",
	"A graph search project. Uses Depth and Breadth first searches to solve the puzzle in the Jumping Jim problem.",
	"A unified video game launcher. Allows the user to launch games from the seven major game launchers. Uses APIs from all seven launchers to allow easy access and statistics tracking for games.",
	"An image stegonagrapy project written in MIPS using MARS. Includes six diffentent encoding methods for hiding plain text messages in bitmap images and their respective decoding methods.",	
	"An In-Memory database project used for query practice. Uses publicly available data from basketballconference.com",
	"An Android application written in Java. Provides the user with full control of a Google Map, private and public waypoint creation, trip tracking including the start and endtimes, speed and distance of the trip, and shows the current local weather conditions.",
	"A Casino game suite with Roulette, Blackjack, and Poker. Written in C# with WPF.",
]

urls = [
	"../ProjectPages/fullDB.html",
	"../ProjectPages/gameOfLife.html",
	"../ProjectPages/jumpingJim.html",
	"../ProjectPages/launcher.html",
	"../ProjectPages/rgbHerring.html",
	"../ProjectPages/smallDB.html",
	"../ProjectPages/tripTracker.html",
	"../ProjectPages/casino.html",
]
var btn = document.querySelector('#next');
var img = document.querySelector("img");
var desc = document.querySelector("#desc");
var itr = 0;

btn.addEventListener("click", function(){
	img.src = pics[itr];
	img.alt = alts[itr];
	desc.textContent = alts[itr];
	itr = (itr+1) % pics.length;
});

img.addEventListener("click", function(){
	// save the value of the itr before moving to next page, eventually
	if(itr>0)
		location.href = urls[itr-1];
	else
		location.href = urls[urls.length-1];
});

