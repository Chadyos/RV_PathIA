using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player
{

    public Cell cell;
    public Grid grid;
    public Player(Cell cell, Grid grid)
    {
        this.cell = cell;
        this.grid = grid;
        this.initScores();
    }
    //
    // public Cell findBestNeighbour(Cell cell)
    // {
    //     // Compare H score of neighbours, return bests ones
    //     // If there is more than one best neighbour, choose the one with the lowest H score
    //     Cell bestNeighbour = cell;
    //
    //     foreach (Edge edge in cell.edges)
    //     {
    //         if (edge.cell2.isVisited == false)
    //         {
    //             edge.cell2.isVisited = true;
    //             if (bestNeighbour == cell)
    //             {
    //                 bestNeighbour = edge.cell2;
    //             }
    //             else if (edge.cell2.HScore < bestNeighbour.HScore)
    //             {
    //                 bestNeighbour = edge.cell2;
    //             }
    //         }
    //
    //     }
    //     bestNeighbour.print();
    //     if (bestNeighbour == cell)
    //     {
    //         Console.WriteLine("No best neighbour found");
    //     }
    //     if (bestNeighbour == grid.grid[grid.goalX, grid.goalY])
    //     {
    //         Console.WriteLine("Goal found");
    //     }
    //     return bestNeighbour;
    //
    // }

    public void initScores()
    {
        Grid grid = this.grid;
        grid.initEdges();
        // Init scores
        for (int i = 0; i < grid.width; i++)
        {
            for (int j = 0; j < grid.height; j++)
            {
                // Calculate distance from start to current cell
                grid.grid[i, j].GScore = grid.getDistance(this.cell, this.grid.grid[i, j]);
                // Calculate distance from current cell to goal
                grid.grid[i, j].HScore = grid.getDistance(this.grid.grid[i, j], grid.goal);
                // Calculate FScore
                grid.grid[i, j].FScore = grid.grid[i, j].GScore + grid.grid[i, j].HScore;

            }
        }
    }


    public List<Cell> Go2Goal(Cell Goal, Cell? cell = null, List<Cell>? List_cell = null)
    {
        // Si les deux derniers paramètres ne sont pas mentionnés, on vient les chercher
        cell ??= this.cell;
        List_cell ??= new List<Cell>();


        // On vient trier les voisins en fonction de leurs score
        List<Cell> Sorted_List_Neighboors = cell.Sort_non_visited_Neighboors();

        // S'il n'y a aucun voisin non visité, on s'arrête là et on retourne la liste de cellules
        if (Sorted_List_Neighboors.Count == 0)
        {
            return List_cell;
        }

        // On vient vient ensuite parcourir les voisins non visités
        foreach (Cell new_cell in Sorted_List_Neighboors)
        {
            // On vient créer une copie de la liste de cellules
            List<Cell> new_list_cell = new List<Cell>(List_cell);

            // On vient ajouter la cellule à cette nouvelle liste et on marque la cellule comme visitée
            new_list_cell.Add(new_cell);
            new_cell.setVisited();

            // Si la cellule est le goal, on retourne la liste de cellules et on a terminé
            if (new_cell == Goal)
            {
                return new_list_cell;
            }

            // Sinon, on vient appeler la fonction récursivement
            else if (!new_list_cell.Contains(Goal))
            {
                List<Cell> result = Go2Goal(Goal, new_cell, new_list_cell);
                if (result.Contains(Goal))
                {
                    return result;
                }
            }
        }
        return List_cell;

    }


    public List<Cell> AStar(Cell Goal)
    {

        // *** INITIALISATION *** //
        // Création de deux listes
        List<Cell> OpenList = new List<Cell>();
        List<Cell> ClosedList = new List<Cell>();
        double successorCurrentCost;
        // On ajoute la cellule de départ à la liste ouverte
        OpenList.Add(this.cell);
        //////////////////////////////

        // *** BOUCLE *** //
        // Tant que la liste OpenList n'est pas vide : 
        while (OpenList.Count != 0)
        {
            // On vient trier la liste OpenList en fonction de la valeur de FScore
            OpenList.Sort((x, y) => x.FScore.CompareTo(y.FScore));
            // On vient récupérer la cellule avec le plus petit FScore
            Cell current_cell = OpenList[0];
            // On vient retirer la cellule de la liste OpenList
            OpenList.Remove(current_cell);

            // On vérifie si la cellule est le goal
            if (current_cell == Goal)
            {   
                Debug.Log("Chemin trouvé");
                List<Cell> path = returnPath(current_cell);
                return path;
            }

            // On vient chercher les voisins de la cellule
            List<Cell> Neighboors = current_cell.getNeighboors();

            // On vient parcourir les voisins
            foreach (Cell successor in Neighboors)
            {
                // On calcule le coût réel de la cellule, en ajoutant 1 vu que les diagonales ne sont pas autorisées
                
                successorCurrentCost = current_cell.GScore + 1;

                // On vérifie si la cellule est déjà dans la liste OpenList 
                if (OpenList.Contains(successor)){
                    if (successor.GScore <= successorCurrentCost)
                    {
                        continue; // Attention à bien vérifier ce que fait le continue
                    }

                }
                    // On vérifie si la cellule est déjà dans la liste ClosedList
                else if (ClosedList.Contains(successor)){
                    if (successor.GScore <= successorCurrentCost)
                    {
                        continue; // Attention à bien vérifier ce que fait le continue
                    }
                    // On vient retirer la cellule de la liste ClosedList
                    ClosedList.Remove(successor);
                    // On vient ajouter la cellule à la liste OpenList
                    OpenList.Add(successor);
                }


                // Comme la cellule n'est pas dans les deux listes, on vient l'ajouter à la liste OpenList
                else
                {
                    // On vient ajouter la cellule à la liste OpenList
                    OpenList.Add(successor);
                }

            // On update le score de la cellule
            successor.GScore = successorCurrentCost;
            // On met à jour le parent de la cellule
            successor.parent = current_cell;
            // On calcule le FScore de la cellule
            successor.FScore = successor.GScore + successor.HScore;
            }
            // FIN DE BOUCLE FOREACH
            ClosedList.Add(current_cell);
        }   
        // FIN DE BOUCLE WHILE
        
        Debug.Log("Aucun chemin trouvé");
        List<Cell> emptyList = new List<Cell>();
        return emptyList;
        

    }

    private List<Cell> returnPath(Cell Goal){
        List<Cell> path = new List<Cell>();
        Cell current_cell = Goal;
        while (current_cell != this.cell){
            path.Add(current_cell);
            current_cell = current_cell.parent;
        }
        path.Reverse();
        return path;
    }

}





