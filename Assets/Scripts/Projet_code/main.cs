using System.Collections;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic; 
using System.Linq;

public class GridGenerator : MonoBehaviour
{
    public GameObject squarePrefab; // Assignez votre prefab de carré ici dans l'inspecteur
    public int gridWidth;
    public int gridHeight;
    public Grid GRID;
    public Player PLAYER;
    public float spacing;
    public GameObject[,] gridArray;
    private ClickableSquare selectedSquare;
    private ClickableSquare redSquare;
    private ClickableSquare greenSquare;




    void Start()
    {   GRID = new Grid(gridWidth, gridHeight);
        GenerateGrid();

    }

   void Update()
   {
       if (Input.GetKeyDown(KeyCode.P))
       {
           if (redSquare != null) redSquare.ChangeColor(Color.white); // Réinitialise la couleur précédente
           if (selectedSquare != null)
           {
               selectedSquare.ChangeColor(Color.red);
               PLAYER = new Player(selectedSquare.cell, GRID);
               redSquare = selectedSquare;
           }
       }
       else if (Input.GetKeyDown(KeyCode.G))
       {
           if (greenSquare != null) greenSquare.ChangeColor(Color.white); // Réinitialise la couleur précédente
           if (selectedSquare != null)
           {
               selectedSquare.ChangeColor(Color.green);
               GRID.setGoalPosition(selectedSquare.cell.x, selectedSquare.cell.y);
               greenSquare = selectedSquare;
           }
       }
   }
    public void SetSelectedSquare(ClickableSquare square)
   {
       selectedSquare = square;
   }
void GenerateGrid()
{   
    gridArray = new GameObject[gridWidth, gridHeight];
    Vector2 gridCenter = new Vector2((gridWidth - 1) * spacing / 2, (gridHeight - 1) * spacing / 2);

    for (int x = 0; x < gridWidth; x++)
    {
        for (int y = 0; y < gridHeight; y++)
        {
            Vector2 position = new Vector2(x * spacing, y * spacing) ;
            GameObject squareInstance = Instantiate(squarePrefab, position, Quaternion.identity);
            if (GRID.grid[x,y] == null) Debug.Log("GRID.grid[x,y] est NULL ????");
            //ClickableSquare clickableSquare = squareInstance.AddComponent<ClickableSquare>();
            //ICI en fait on a déjà un clickablesquare lié au squareInstance
            ClickableSquare clickableSquare = squareInstance.GetComponent<ClickableSquare>();

            clickableSquare.Initialize(GRID.grid[x, y]);
            if (clickableSquare.cell == null) Debug.Log("clickableSquare.cell est NULL ????");  
            GRID.grid[x,y].gameObject = squareInstance;
        }
    }
}


IEnumerator ChangeColorAfterDelay(float delay, int x, int y, Color color)
{
    yield return new WaitForSeconds(delay);
    MeshRenderer meshRenderer = gridArray[x, y].GetComponent<MeshRenderer>();
    if (meshRenderer != null)
    {
        // Création d'une instance unique du matériau
        Material matInstance = new Material(meshRenderer.material);
        meshRenderer.material = matInstance;

        // Modification de la couleur
        meshRenderer.material.color = color;
    }
    else
    {
        Debug.LogError("MeshRenderer not found on the object at position [" + x + ", " + y + "]");
    }
}

}




