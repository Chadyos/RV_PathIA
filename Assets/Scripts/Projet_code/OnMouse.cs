using UnityEngine;

public class ClickableSquare : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Cell cell;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

 public void Initialize(Cell cell)
    {
        this.cell = cell;
    }

void OnMouseOver()
{       
    FindObjectOfType<GridGenerator>().SetSelectedSquare(this);


    if (!IsRedOrGreen())
    {
        if (Input.GetMouseButton(0)) // Bouton gauche de la souris enfoncé
        {   if (this.cell == null) Debug.Log("Cell est NULL ????");
            if(this.cell!= null) Debug.Log($"Cell position: x = {cell.x}, y = {cell.y}");
            this.cell.setWall();
            ChangeColor(Color.black);
        }
        else if (Input.GetMouseButton(1)) // Bouton droit de la souris enfoncé
        {   this.cell.setNotWall();
            ChangeColor(Color.white);
        }
    }
}

private bool IsRedOrGreen()
{
    return meshRenderer.material.color == Color.red || meshRenderer.material.color == Color.green;
}


    public void ChangeColor(Color targetColor)
    {
        if (meshRenderer != null)
        {
            Material matInstance = new Material(meshRenderer.material);
            meshRenderer.material = matInstance;
            meshRenderer.material.color = targetColor;
        }
        else
        {
            Debug.LogError("MeshRenderer component not found on " + gameObject.name);
        }
    }
}



