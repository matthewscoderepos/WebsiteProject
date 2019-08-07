#include <iostream>
#include <fstream>
#include <queue>
#include <vector>
using namespace std;

class Graph{
  public:
	~Graph();
	int graphSize;
	int sizeSquared;	//helpful for calculations
	queue<int> bQue;	//queue used for the breadth first search
	vector<int> spaces; //vector of actual square values
	vector<int> previous;
	vector<int> position;
	vector<int> condition;	 //undiscoverd, discovered, explored
	vector<vector<int>> graph; //our graph

	//Sets up the sizes and variables required for all of the functions
	Graph(int size){
		graphSize = size;
		sizeSquared = graphSize * graphSize;
		previous.resize(sizeSquared);
		position.resize(sizeSquared);
		condition.resize(sizeSquared);
	}
	void setUpGraph(vector<vector<int>> a){
		graph = a;
	}
};

//Destructor
Graph::~Graph(){
	spaces.clear();
	graph.clear();
	previous.clear();
	position.clear();
	condition.clear();
}

void PopulateGraph(Graph &jimGraph, istream &input){
	//Stores each box's value into the "spaces" vector, representing all of the verticies in the graph

	for (int i = 0; i < jimGraph.sizeSquared; i++){
		int squareValue;
		input >> squareValue;
		jimGraph.spaces.push_back(squareValue);
	}
}

void GraphtoAdjList(Graph &jimGraph){
	//This function stores the boxes that each box can jump to, from 0 to (sizeSquared - 1)
	//There is a max of 4 options for the adjacency list to hold, the up, right, down, and left spaces, if they are available
	//The jumpable boxes are stored in the "inner" vector belonging to each spaces
	
	cout << "Adjacency Lists" << endl;

	for (int i = 0; i < jimGraph.sizeSquared; i++){
		int squareValue = jimGraph.spaces[i];
		int width = i % jimGraph.graphSize;

		if (squareValue > 0){

			if (i % jimGraph.graphSize == 0){

			//	cout << "____________________" << endl;
			}
			cout << "Square Index " << i << ": ";
			if (width + squareValue <= jimGraph.graphSize - 1){

				//insert the index of the space rightward to the adjacency list
				//cout << "Right to ";
				jimGraph.graph[i].push_back(squareValue + i);
				cout << (squareValue + i) << " -> ";
			}

			if ((squareValue * jimGraph.graphSize + i) <= (jimGraph.sizeSquared - 1)){

				//insert the index of the space downward to the adjacency list
				//cout << "Down to ";
				jimGraph.graph[i].push_back(squareValue * jimGraph.graphSize + i);
				cout << (squareValue * jimGraph.graphSize + i) <<  " -> ";
			}

			if (width - squareValue >= 0){

				//insert the index of the space leftward to the adjacency list
				//cout << "Left to ";
				jimGraph.graph[i].push_back(i - squareValue);
				cout << (i - squareValue) << " -> ";
			}

			if ((i - squareValue * jimGraph.graphSize) >= 0){

				//insert the index of the space upward to the adjacency list
				//cout << "Up to ";
				jimGraph.graph[i].push_back(i - squareValue * jimGraph.graphSize);
				cout << (i - squareValue * jimGraph.graphSize) << " -> ";
			}
			cout << "---" << endl;
		}
	}
}
void Traverse(Graph &jimGraph){
	//Run of the mill Breadth First Search on the inner vectors in our graph (the adjacency lists)
	//undiscovered = -5, discovered = -4, explored = -3


	//mark all vertices as undiscovered
	for (int i = 0; i < jimGraph.sizeSquared; i++){

		jimGraph.previous[i] = -1;
		jimGraph.position[i] = -1;
		jimGraph.condition[i] = -5;
	}

	//mark the first vertex as discovered
	jimGraph.previous[0] = -1;
	jimGraph.position[0] = 0;
	jimGraph.condition[0] = -4;
	jimGraph.bQue.push(0);

	//Traverse while the queue is not empty
	while (jimGraph.bQue.size() != 0){

		int vertex = jimGraph.bQue.front();
		jimGraph.bQue.pop();
		
		//the graph at position vertex holds said vertex's adjacency list, so we iterate through each adjacency list here
		for (vector<int>::iterator current = jimGraph.graph[vertex].begin(); current != jimGraph.graph[vertex].end(); current++){

			if (jimGraph.condition[*current] == -5){ //if condition of the current adjaceny list member is undiscovered

				jimGraph.condition[*current] = -4; //set current to discovered
				jimGraph.position[*current] = jimGraph.position[vertex] + 1; //position of current is the position of the vertex + 1
				jimGraph.previous[*current] = vertex; //the current vertex in the adj list's previous is the current vertex 
				jimGraph.bQue.push(*current); //push the current vertex
			}
		}
		jimGraph.condition[vertex] = -3; //mark the vertex we just dealt with as explored
	}
}
void getJimsPath(Graph &jimGraph, ostream &out){
	//We will push jims path onto a vector, path
	//The path that jim takes largley depends on the verctor path at location path.size(), this allows us to look through previous and push specific squares from it into the path

	vector<int> path;
	path.push_back(jimGraph.sizeSquared - 1); //start out with the longest possible path to the end
	
	while (path[path.size() - 1] != 0){ //while path at path.size-1 is not 0

		path.push_back(jimGraph.previous[path[path.size() - 1]]); //push the "previous" at path of path.size-1
	}
	//pushing of the directions, based on path at path.size
	while (path.size() != 1){
		int i = path.size();
		int size = jimGraph.graphSize;
		//moving left and right uses regular division, path at path.size divided by the graphSize
		if (path[i-2] / size == path[i-1] / size){
			if (path[i-2] > path[i-1]){

				cout << "E ";
				out << "E ";
			}
			else{

				cout << "W ";
				out << "W ";
			}
		}
		//moving up and down uses the modulus of path at path.size % graphSize
		else if (path[i-2] % size == path[i-1] % size){
			if (path[i-2] > path[i-1]){

				cout << "S ";
				out << "S ";
			}
			else{

				cout << "N ";
				out << "N ";
			}
		}
		path.pop_back();
		i--;
	}
	out << endl;
}

int main(){

	ifstream jumpingIN("input.txt");
	ofstream jumpingOUT("output.txt");

	//Read in the first line, the dimensions of the graph (stored in same variable because its a square)
	int inputSize;
	jumpingIN >> inputSize;
	jumpingIN >> inputSize;

	//Creating our graph object using the side size
	Graph JumpingJim(inputSize);

	//creating the 2 dim vector graph
	vector<vector<int>> emptyGraph(inputSize * inputSize, vector<int>(4, 0));

	//assigning said graph to our graph object
	JumpingJim.setUpGraph(emptyGraph);

	//reading in squares
	PopulateGraph(JumpingJim, jumpingIN);

	//creating adjacency lists based on the read squares
	GraphtoAdjList(JumpingJim);

	//traverse the graph using JumpingJim's adjacency lists
	Traverse(JumpingJim);

	//get the final path that Jim needs to take
	getJimsPath(JumpingJim, jumpingOUT);

	return 0;
}