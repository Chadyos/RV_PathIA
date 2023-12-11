using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic; 
using System.Linq;

    public class Cell {
        // Définition de la classe Cellule qui compose la grille
        public int x;
        public int y;
        public double GScore; 
        public double HScore; 
        public GameObject gameObject;
        public double FScore;
        public List<Edge> edges;
        public string type;
        public bool isWall;
        public bool isVisited;

        public Cell parent;
        public Cell(int x, int y) {
            this.x = x;
            this.y = y;
            this.isWall = false;
            this.isVisited = false;
            this.edges = new List<Edge>();
        }
        public void print() {
            Console.WriteLine("x: " + this.x + " y: " + this.y);
        }
        public void setWall() {
            this.isWall = true;
        }
        public void setNotWall() {
            this.isWall = false;
        }
        public void setVisited() {
            this.isVisited = true;
        }
        public void setUnvisited() {
            this.isVisited = false;
        }

        public void afficher() {
            Console.Write(this.type + " ");
        }


    public List<Cell> Sort_non_visited_Neighboors(){
        List<Cell> nonVisitedNeighboors = new List<Cell>();
        foreach (Edge edge in edges){
            if (!edge.cell2.isVisited){
                nonVisitedNeighboors.Add(edge.cell2);
            }
        }
         return nonVisitedNeighboors.OrderBy(cell => cell.HScore)
                                    .ThenBy(cell => cell.FScore)
                                    .ToList();


    }

 public List<Cell> getNeighboors(){
        List<Cell> Neighboors = new List<Cell>();
        foreach (Edge edge in edges){
            Neighboors.Add(edge.cell2);
        }
         return Neighboors;
    }


 public void HighlightPath(Color color)
    { // ajout d'un wait pour voir le chemin
        
        if (gameObject != null) // Assurez-vous que gameObject est assigné
        {
            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material.color = color;
            }
        }
    }



public void reset(){
        this.isVisited = false;
        this.GScore = 0;
        this.HScore = 0;
        this.FScore = 0;
        this.isWall = false;
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material.color = Color.white;
    }


}



