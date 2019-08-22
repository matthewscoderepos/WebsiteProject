pics = [
  "../imgs/MainImages/Casino.PNG",
  "../imgs/MainImages/FullDB.png",
  "../imgs/MainImages/GameOfLife.PNG",
  "../imgs/MainImages/JumpingJim.PNG",
  "../imgs/MainImages/Launcher.PNG",
  "../imgs/MainImages/RGBHerring.PNG",
  "../imgs/MainImages/SmallDB.PNG",
  "../imgs/MainImages/TripTracker.jpg",
];
//extend these descriptions
alts = [
  "A Casino game suite with Roulette, Blackjack, and Poker. Written in C# with WPF.",
  "A full database project with frontend HTML and backend written in Java. \n\nConsisting of 9 total tables, the database models a library system. \n\nMembers are able to view books based on many criteria and are shown which books are available based on the criteria chosen. \n\nMembers are also able to see a list of their past and active transactions including times and locations that books were checked out. \n\nLibrarians are able to check in and out books at their home location. \n\nBoth member and librarian username and passwords are authenticated against two tables holding login information. \n\nDatabase stored in Postgres and SQL.",
  "A cellular automata project written in Java using Processing. Allows the user to control the parameters of Conways Game of Life in an interactive and visually pleasing environment.",
  "A graph search project. Uses Depth and Breadth first searches to solve the puzzle in the Jumping Jim problem.",
  "A unified video game launcher. Allows the user to launch games from the seven major game launchers. Uses APIs from all seven launchers to allow easy access and statistics tracking for games.",
  "An image stegonagrapy project written in MIPS using MARS. Includes four diffentent encoding methods for hiding plain text messages in bitmap images and their respective decoding methods.",
  "An In-Memory database project used for query practice. Uses publicly available data from basketballconference.com",
  "An Android application written in Java. Provides the user with full control of a Google Map, private and public waypoint creation, trip tracking including the start and endtimes, speed and distance of the trip, and shows the current local weather conditions."
];

urls = [
  "../ProjectPages/casino.html",
  "../ProjectPages/fullDB.html",
  "../ProjectPages/gameOfLife.html",
  "../ProjectPages/jumpingJim.html",
  "../ProjectPages/launcher.html",
  "../ProjectPages/rgbHerring.html",
  "../ProjectPages/smallDB.html",
  "../ProjectPages/tripTracker.html",

];
var btn = document.querySelector("#next");
var img = document.querySelector("img");
var desc = document.querySelector("#desc");

if(sessionStorage.getItem('itr')!=null){
  itr = sessionStorage.getItem('itr');
}
else{
  var itr = 0;
}
img.src = pics[itr];
img.alt = alts[itr];
desc.textContent = alts[itr];


btn.addEventListener("click", function() {
  itr = (itr + 1) % pics.length;
  img.src = pics[itr];
  img.alt = alts[itr];
  desc.textContent = alts[itr];
  sessionStorage.setItem('itr',itr);
});

img.addEventListener("click", function() {
  // save the value of the itr before moving to next page, eventually
  location.href = urls[itr];
});
