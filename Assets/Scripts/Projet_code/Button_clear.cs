using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Button_clear : MonoBehaviour
{
    public GridGenerator gridGenerator; // R�f�rence � GridGenerator
    public Grid GRID;
    public Player PLAYER; // R�f�rence � votre classe Player
    public MeshRenderer meshRenderer;
    public ClickableSquare square;

    public void OnClearButtonClick()
    {
        GRID = FindObjectOfType<GridGenerator>().GRID;
        PLAYER = FindObjectOfType<GridGenerator>().PLAYER;
        Debug.Log("Clear");

        ClearGrid();
        return;
    }

        // Update is called once per frame
        private void ClearGrid()
    {  
        if (gridGenerator.GRID.goal != null) {
            gridGenerator.GRID.goal = null;
        }
        if (PLAYER.cell != null) {
            PLAYER.cell = null;
        }
        for (int x = 0; x < GRID.height; x++)
        {
            for (int y = 0; y < GRID.width; y++)
            {   
                GRID.grid[x,y].reset(); // On reset les cellules

            }
        }
    }   
}

