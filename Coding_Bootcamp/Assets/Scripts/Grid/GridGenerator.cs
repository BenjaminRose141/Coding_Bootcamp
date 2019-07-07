using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class GridGenerator : EditorWindow
{
    
    int rows = 5;
    int columns = 5;
    float paddingFactor = 1;
    Transform gridAnchor = null;
    Material material = null;
    public List<GameObject> cubes = new List<GameObject>();

    
    [MenuItem("Window/GridGenerator")]
    static void OpenWindow()
    {
        GridGenerator window = (GridGenerator)GetWindow(typeof(GridGenerator));
        window.minSize = new Vector3(200,100);
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Parameters", EditorStyles.boldLabel);
        rows = EditorGUILayout.IntField("Rows",rows);
        columns = EditorGUILayout.IntField("Columns",columns);
        paddingFactor = EditorGUILayout.FloatField("Padding Factor", paddingFactor);
        gridAnchor = EditorGUILayout.ObjectField("Grid Anchor", gridAnchor, typeof(Transform), true) as Transform;
        material = (Material)EditorGUILayout.ObjectField("Material", material, typeof(Material));

        GUILayout.BeginHorizontal();

        if(GUILayout.Button("Generate Grid"))
        {
            if(gridAnchor == null)
            {
                Debug.Log("Please set a grid Anchor!");
                return;
            }

            GenerateGrid(); 
            ChangeMaterial();
        }

        if(GUILayout.Button("Remove"))
        {
           DeleteGrid();
        }

        GUILayout.EndHorizontal();

        if(GUILayout.Button("Finalize"))
        {
           DetachGrid();
        }
    }

    private void GenerateGrid()
    {
        DeleteGrid();

        for(int row = 0; row < rows; row++)
        {
            for(int column = 0; column < columns; column++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                
                float posX = column * paddingFactor + gridAnchor.position.x;
                float posZ = row * paddingFactor + gridAnchor.position.z;
                
                if(column == 0)
                {
                    posX = gridAnchor.position.x;
                }


                cube.transform.position = new Vector3(posX, gridAnchor.position.y, posZ);    
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
            if(material == null)
            {
                material = new Material(Shader.Find("Unlit/Texture"));
                material.color = Color.white;
            }
            mat.material = material;
        }
        Debug.Log("ChangedMat");
    }

    void DeleteGrid()
    {
        Debug.Log("DeleteGridCalled with " + cubes.Count + " cubes");

        if(cubes.Count>0)
        {
            for(int i=0; i<cubes.Count; i++)
            {
                DestroyImmediate(cubes[i]);
            } 
            cubes.Clear();
            Debug.Log("Grid Deleted");
        }
        else
        {
            Debug.Log("Nothing to Remove");
        }
    }

    void DetachGrid()
    {
        if(cubes.Count == 0) return;

        GameObject parent = new GameObject();
        parent.transform.position = gridAnchor.position;
        parent.name = "Grid";

        for(int i = 0; i < cubes.Count; i++)
        {
           cubes[i].transform.SetParent(parent.transform);
        }

        cubes.Clear();
    }
}
