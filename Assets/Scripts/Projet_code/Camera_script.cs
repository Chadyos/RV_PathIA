using System.Collections;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic; 
using System.Linq;


public class CameraAdjuster : MonoBehaviour
{
    public GridGenerator gridGenerator;
    public Camera mainCamera;

    void Start()
    {   
        gridGenerator = FindObjectOfType<GridGenerator>();
        AdjustCamera();
    }

    void AdjustCamera()
    {
        if (gridGenerator == null || mainCamera == null) return;

        // Calculez le centre de la grille
        Vector2 gridCenter = new Vector2((gridGenerator.gridWidth - 1) * gridGenerator.spacing / 2, 
                                         (gridGenerator.gridHeight - 1) * gridGenerator.spacing / 2);

        // Positionnez la caméra au-dessus du centre de la grille
        mainCamera.transform.position = new Vector3(gridCenter.x, gridCenter.y, -10);

        // Ajustez la taille orthographique de la caméra
        float maxDimension = Mathf.Max(gridGenerator.gridWidth, gridGenerator.gridHeight) * gridGenerator.spacing;
        mainCamera.orthographicSize = maxDimension / 2;
    }
}
