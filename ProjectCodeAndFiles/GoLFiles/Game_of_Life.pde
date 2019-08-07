import javax.swing.*;

Cell[][] grid;
Cell[][] next;
int cols;
int rows;
int resolution; //3 is the lowest we can go here
int generation = 0;
final int NUM_STATES = 75;
int resurectionCount;
int maxAlive;
int minAlive;
int heatMap;
int drawing = 0;
int start = 0;

class stateChange {
  int state;
  int generation;

  stateChange(int thisState, int thisGeneration) {
    state = thisState;
    generation = thisGeneration;
  }
}

class Cell {
  Cell(int thisValue, stateChange[] thisStateChanges) {
    value = thisValue;
    stateChanges = thisStateChanges;
  }
  Cell(int thisValue, stateChange[] thisStateChanges, int thisIterator) {
    value = thisValue;
    stateChanges = thisStateChanges;
    iterator = thisIterator;
  }  
  Cell(int thisValue) {
    value = thisValue;
  }
  int iterator;  
  int value;
  stateChange[] stateChanges;
}

void reset() {
  frameCount = -1;
  start = 0;
  String[] arr = {""};
  String op1s = JOptionPane.showInputDialog(frame, "Please re-enter your: \nSize of each cell in pixels(Min 3) *Larger size has better performance*\nCell rebirth neighbor count\nMaximum allowed alive neighbors\nMinimum allowed alive neighbors\nIf you want a heatmap of game 1-Yes 0-No\nWould you like to draw your own shapes to start? 1-Yes 0-No\nALL IN THE TEXTBOX SEPERATED BY A SINGLE COMMA WITH NO SPACES\nDefault values are given for Conway's Game of Life", "10,3,3,2,0,0");
  if (op1s != null) 
    arr = op1s.split(",", 6); 
  if (Integer.valueOf(arr[0]) < 3) {
    arr[0] = "3";
  }
  resolution = Integer.valueOf(arr[0]);    
  resurectionCount = Integer.valueOf(arr[1]);
  maxAlive = Integer.valueOf(arr[2]);
  minAlive = Integer.valueOf(arr[3]);
  heatMap = Integer.valueOf(arr[4]);
  drawing = Integer.valueOf(arr[5]);
  if (drawing == 1) {
    JOptionPane.showMessageDialog(null, "While the animation plays you may drag the mouse\nto revive cells.\n\nPress any key on the keyboard while\nmoused over a cell to view that cell's history.\n\nWhile drawing you can select one cell at a time\n or drag the mouse to set cells to the \"alive\" state.\n\nPress ENTER when you are ready\nto start the animation.", "Drawing Instructions", JOptionPane.INFORMATION_MESSAGE);
  } else {
    JOptionPane.showMessageDialog(null, "While the animation plays you may drag the mouse\nto revive cells.\n\nPress any key on the keyboard while\nmoused over a cell to view that cell's history.\n\n Press BACKSPACE to reset the \nboard and enter new options.", "Instructions", JOptionPane.INFORMATION_MESSAGE);
  }
    noLoop();
}

void settings() {
  String[] arr = {""};
  String op1s = JOptionPane.showInputDialog(frame, "Please enter your: \nDesired game width(Min 600)\nDesired game height(Min 600)\nSize of each cell in pixels(Min 3) *Larger size has better performance*\nCell rebirth neighbor count\nMaximum allowed alive neighbors\nMinimum allowed alive neighbors\nIf you want a heatmap of game 1-Yes 0-No\nWould you like to draw your own shapes to start? 1-Yes 0-No\nALL IN THE TEXTBOX SEPERATED BY A SINGLE COMMA WITH NO SPACES\nDefault values are given for Conway's Game of Life", "1000,1000,10,3,3,2,0,0");
  if (op1s != null) 
    arr = op1s.split(",", 8); 
  if (Integer.valueOf(arr[0]) < 600) {
    arr[0] = "600";
  }
  if (Integer.valueOf(arr[1]) < 600) {
    arr[1] = "600";
  }
  if (Integer.valueOf(arr[2]) < 3) {
    arr[2] = "3";
  }
  size(Integer.valueOf(arr[0])+200, Integer.valueOf(arr[1]));
  resolution = Integer.valueOf(arr[2]);    
  resurectionCount = Integer.valueOf(arr[3]);
  maxAlive = Integer.valueOf(arr[4]);
  minAlive = Integer.valueOf(arr[5]);
  heatMap = Integer.valueOf(arr[6]);
  drawing = Integer.valueOf(arr[7]);
  if (drawing == 1) {
    JOptionPane.showMessageDialog(null, "While the animation plays you may drag the mouse\nto revive cells.\n\nPress any key on the keyboard while\nmoused over a cell to view that cell's history.\n\nWhile drawing you can select one cell at a time\n or drag the mouse to set cells to the \"alive\" state.\n\nPress ENTER when you are ready\nto start the animation.", "Drawing Instructions", JOptionPane.INFORMATION_MESSAGE);
  } else {
    JOptionPane.showMessageDialog(null, "While the animation plays you may drag the mouse\nto revive cells.\n\nPress any key on the keyboard while\nmoused over a cell to view that cell's history.\n\n Press BACKSPACE to reset the \nboard and enter new options.", "Instructions", JOptionPane.INFORMATION_MESSAGE);
  }
}

void setup() {
  cols = (width-200) / resolution;
  rows = height / resolution;
  grid = new Cell[cols][rows];

  for (int i = 0; i < cols; i++) {
    for (int j = 0; j < rows; j++) {
      stateChange[] states = new stateChange[NUM_STATES];
      for (int k = 0; k<NUM_STATES; k++) {
        stateChange stateChange = new stateChange(2, 0);
        states[k] = stateChange;
      }
      //if setting up a random game
      if (drawing == 0) {
        if (floor(random(2)) == 1) {
          grid[i][j] = new Cell(floor(random(2)), states, 0);
        } else {
          grid[i][j] = new Cell(0, states, 0);
        }
      } else {   
        grid[i][j] = new Cell(0, states, 0);
      }
    }
  }
}



void draw() {
  background(0);
  stroke(30);
  if ( start == 1 || drawing == 0) {
    for (int i = 0; i < cols; i++) {
      for (int j = 0; j < rows; j++) {
        int x = i * resolution;
        int y = j * resolution;
        fill(0);
        if (grid[i][j].value == 1) {
          fill(255);
        } else if (heatMap == 1) {
          if (grid[i][j].iterator <= 10) {
            fill(  0, 9, 229);
          } else
            if (grid[i][j].iterator <=20 && grid[i][j].iterator > 10) {
              fill(  0, 182, 221);
            } else
              if (grid[i][j].iterator <=30 && grid[i][j].iterator > 20) {
                fill(  0, 217, 169);
              } else
                if (grid[i][j].iterator <=40 && grid[i][j].iterator > 30) {
                  fill(  1, 210, 0);
                } else
                  if (grid[i][j].iterator <=50 && grid[i][j].iterator > 40) {
                    fill(  160, 202, 0);
                  } else
                    if (grid[i][j].iterator <=60 && grid[i][j].iterator > 50) {
                      fill(  198, 161, 0);
                    } else
                      if (grid[i][j].iterator <=70 && grid[i][j].iterator > 60) {
                        fill(194, 81, 0);
                      } else
                        if (grid[i][j].iterator > 70) {
                          fill(194, 4, 0);
                        }
        }
        rect(x, y, resolution - 1, resolution - 1);
        fill(255);
      }
    }

    Cell[][] next = new Cell[cols][rows];
    for (int i = 0; i < cols; i++) {
      for (int j = 0; j < rows; j++) {
        next[i][j] = new Cell(grid[i][j].value, grid[i][j].stateChanges, grid[i][j].iterator++);
      }
    }


    // Compute next based on grid
    for (int i = 0; i < cols; i++) {
      for (int j = 0; j < rows; j++) {
        int state = grid[i][j].value;
        // Count live neighbors!
        int neighbors = countNeighbors(grid, i, j);

        if (state == 0 && neighbors == resurectionCount) {      
          next[i][j].value = 1;
          if ( grid[i][j].value != next[i][j].value) {
            logChange(next, i, j, state, next[i][j].iterator);
            next[i][j].iterator++;
          }
        } else if (state == 1 && (neighbors < minAlive || neighbors > maxAlive)) {
          next[i][j].value = 0;
          if ( grid[i][j].value != next[i][j].value) {
            logChange(next, i, j, state, next[i][j].iterator);
            next[i][j].iterator++;
          }
        } else {     
          next[i][j].value = state;
        }
      }
    }
    grid = next;
    generation++;
  } else {
    //draw a single frame
    for (int i = 0; i < cols; i++) {
      for (int j = 0; j < rows; j++) {
        int x = i * resolution;
        int y = j * resolution;
        if (grid[i][j].value == 1) {
          fill(255);
        }
        rect(x, y, resolution - 1, resolution - 1);
        fill(0);
      }
    }
  }
}



int countNeighbors(Cell[][] grid, int x, int y) {
  int sum = 0;
  for (int i = -1; i < 2; i++) {
    for (int j = -1; j < 2; j++) {
      int col = (x + i + cols) % cols;
      int row = (y + j + rows) % rows;
      sum += grid[col][row].value;
    }
  }
  sum -= grid[x][y].value;
  return sum;
}

void logChange(Cell[][] next, int x, int y, int state, int itr) {
  stateChange stateChange = new stateChange(state, generation);  
  next[x][y].stateChanges[itr%NUM_STATES] = stateChange;
}

void keyPressed() {
  if (key == BACKSPACE) {
    reset();
  } else if (key != ENTER) {
    noLoop(); 
    int x = mouseX/ resolution;
    int y = mouseY/ resolution;
    if (x <= (width-200) / resolution) {
      for (int i = 0; i<NUM_STATES; i++) {
        textSize(18);
        text("State", width-170, height/100+5);
        text("Generation", width-110, height/100+5);
        textSize(14);
        if (grid[x][y].stateChanges[i].state != 2) {
          text(grid[x][y].stateChanges[i].state, width-150, floor((height/100)+20)+i*13);
          text(grid[x][y].stateChanges[i].generation, width-80, floor((height/100)+20)+i*13);
        }
      }
    }
  } else if (key == ENTER) {
    start = 1;
  }
}

void keyReleased() {
  loop();
}

void mouseDragged() {

  int x = mouseX/ resolution;
  int y = mouseY/ resolution;
  if (x < (width-200) / resolution && x > 0 && y > 0 && y<height/resolution) {
    grid[x][y].value = 1;
  }
}

void mouseClicked() {
  if (drawing == 1 && start == 0) {
    fill(255);
    int x = mouseX / resolution;
    int y = mouseY / resolution;
    if (x <= (width-200) / resolution && x > 0 && y > 0 && y<height) {
      if (grid[x][y].value == 0)
        grid[x][y].value = 1;
      else if (grid[x][y].value == 1)
        grid[x][y].value = 0;
      redraw();
    }
  }
}
