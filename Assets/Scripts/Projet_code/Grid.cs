using System;
using System.Collections;
using System.Collections.Generic; 


    // Grid class
public  class Grid
    {
        public int width;
        public int height;
        public Cell goal;
        public Cell[,] grid;

        public Grid(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.grid = new Cell[width, height];
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    this.grid[i, j] = new Cell(i, j);
                }
            }
            this.initEdges();
        }

        public void setVisited(int x, int y)
        {
            this.grid[x, y].setVisited();
        }
        
        

        public void setGoalPosition(int x, int y)
        {
            this.goal = this.grid[x, y];
        }
        public void setWall(int x, int y)
        {
            this.grid[x, y].setWall();
        }
        public void initEdges()
        {
            // Init edges
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    Cell currentCell = this.grid[i, j];
                    currentCell.edges = new List<Edge>();
                    // if neighbours exist and not a wall : 
                    if (i > 0 && !this.grid[i - 1, j].isWall)
                    {
                        currentCell.edges.Add(new Edge(currentCell, this.grid[i - 1, j]));
                    }
                    if (i < this.width - 1 && !this.grid[i + 1, j].isWall)
                    {
                        currentCell.edges.Add(new Edge(currentCell, this.grid[i + 1, j]));
                    }
                    if (j > 0 && !this.grid[i, j - 1].isWall)
                    {
                        currentCell.edges.Add(new Edge(currentCell, this.grid[i, j - 1]));
                    }
                    if (j < this.height - 1 && !this.grid[i, j + 1].isWall)
                    {
                        currentCell.edges.Add(new Edge(currentCell, this.grid[i, j + 1]));
                    }
                    /* FIX TEMPORAIRE POUR LES DIAGONALES
                    // On instance aussi les diagonales en leur donnant un poids de 1.4
                    if (i > 0 && j > 0 && !this.grid[i - 1, j - 1].isWall)
                    {
                        currentCell.edges.Add(new Edge(currentCell, this.grid[i - 1, j - 1], 1.4));
                    }
                    if (i < this.width - 1 && j > 0 && !this.grid[i + 1, j - 1].isWall)
                    {
                        currentCell.edges.Add(new Edge(currentCell, this.grid[i + 1, j - 1], 1.4));
                    }
                    if (i > 0 && j < this.height - 1 && !this.grid[i - 1, j + 1].isWall)
                    {
                        currentCell.edges.Add(new Edge(currentCell, this.grid[i - 1, j + 1], 1.4));
                    }
                    if (i < this.width - 1 && j < this.height - 1 && !this.grid[i + 1, j + 1].isWall)
                    {
                        currentCell.edges.Add(new Edge(currentCell, this.grid[i + 1, j + 1], 1.4));
                    } */

                }

            }

        }
        public double getDistance(Cell cell1, Cell cell2)
        {
            // Calcul de la distance octile
            int dx = Math.Abs(cell1.x - cell2.x);
            int dy = Math.Abs(cell1.y - cell2.y);

            double straightCost = 1;
            double diagonalCost = 1.4;

            return straightCost * (dx + dy) + (diagonalCost - 2 * straightCost) * Math.Min(dx, dy);
        }

        
    }

