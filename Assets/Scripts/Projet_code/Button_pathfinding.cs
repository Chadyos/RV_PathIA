using UnityEngine;
using System.Collections.Generic; // Pour utiliser List<>
using System.Collections;
public class PathfindingManager : MonoBehaviour
{
    public GridGenerator gridGenerator; // Référence à GridGenerator
    public Grid GRID;
    public Player PLAYER; // Référence à votre classe Player

    public void OnPathfindingButtonClick()
    {   GRID = FindObjectOfType<GridGenerator>().GRID;
        PLAYER = FindObjectOfType<GridGenerator>().PLAYER;

        ExecutePathfinding();
        return;
    }

    private void ExecutePathfinding()
    {   //Debug.Log("Pathfinding");
        //Debug.Log("Position du joueur : x =" + PLAYER.cell.x + " y = " + PLAYER.cell.y);
        //Debug.Log("Position du goal : x =" + GRID.goal.x + " y = " + GRID.goal.y);
        //Debug.Log("gridGenerator != null"+ gridGenerator != null+ "ET player != null"+ PLAYER != null );  

        if (gridGenerator != null && PLAYER != null)
        {
            PLAYER.initScores();
            List<Cell> path = PLAYER.AStar(gridGenerator.GRID.goal);
            // remove last cell from path
            //path.RemoveAt(path.Count - 1);
            Debug.Log("path : " + path);
            StartCoroutine(RevealPath(path));
        }
        return;
    }

    IEnumerator RevealPath(List<Cell> path)
{
    foreach (Cell cell in path)
    {   
        if (cell == PLAYER.cell) continue;
        if (cell == GRID.goal) continue;
        cell.HighlightPath(Color.yellow);
        yield return new WaitForSeconds(0.2f); // Attendez 1 seconde
    }
}

}

