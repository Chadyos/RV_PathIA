using UnityEngine;
using System;
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
{
    // Lancer la coroutine englobante
    StartCoroutine(ExecutePathfindingSequence());
}

private IEnumerator ExecutePathfindingSequence()
{
    if (gridGenerator != null && PLAYER != null)
    {
        List<List<Cell>> saveOpen;
        List<List<Cell>> saveClose;
        List<Cell> path;
        (path, saveOpen, saveClose) = PLAYER.AStar(gridGenerator.GRID.goal);

        // Exécuter RevealSearch et attendre qu'elle soit terminée
        yield return StartCoroutine(RevealSearch(saveOpen, saveClose));

        // Ensuite, exécuter RevealPath
        yield return StartCoroutine(RevealPath(path));
    }
}
IEnumerator RevealSearch(List<List<Cell>> saveOpen, List<List<Cell>> saveClose)
{
    for (int i = 0; i < saveOpen.Count; i++)
    { Debug.Log("i : " + i);
        int maxSize = Math.Max(saveOpen[i].Count, saveClose[i].Count);

        for (int j = 0; j < maxSize; j++)
        {
            Debug.Log("j : " + j);
            if (j < saveOpen[i].Count)
            {
                Cell cellOpen = saveOpen[i][j];
                if (cellOpen != PLAYER.cell && cellOpen != GRID.goal)
                {
                    cellOpen.HighlightPath(Color.blue);
                }
            }

            if (j < saveClose[i].Count)
            {
                Cell cellClose = saveClose[i][j];
                if (cellClose != PLAYER.cell && cellClose != GRID.goal)
                {
                    cellClose.HighlightPath(Color.cyan);
                }
            }

            // Optionnel : pause entre les itérations
        }
        yield return new WaitForSeconds(0.05f);

    }
    
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

