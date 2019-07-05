using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    [SerializeField] int rows = 0;
    [SerializeField] int columns = 0;
    [SerializeField] float padding = 0;
    [SerializeField] Transform GridAnchor = null;
    [SerializeField] Material material = null;
    public List<GameObject> cubes;


    void Awake()
    {
        GenerateGrid(); 
    }

    void Start()
    {
        ChangeMaterial();
    }

    private void GenerateGrid()
    {
        for(int row = 0; row < rows; row++)
        {
            for(int column = 0; column < columns; column++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                
                float posX = column * padding + GridAnchor.position.x;
                float posZ = row * padding + GridAnchor.position.z;
                
                if(column == 0)
                {
                    posX = GridAnchor.position.x;
                }


                cube.transform.position = new Vector3(posX, GridAnchor.position.y, posZ);    
                cubes.Add(cube);
            }
        }
    }

    void ChangeMaterial()
    {
        for(int i = 0; i < cubes.Count; i++)
        {
            Renderer mat = cubes[i].GetComponent<Renderer>();
            mat.material = new Material(Shader.Find("Standard"));
            mat.material = material;
            Debug.Log("ChangedMat");
        }
    }
}
