using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    private int[,] grid; //0 empty, 1 path, 2 tower
    [SerializeField] private int gridSize = 20;
    [SerializeField] private GameObject waypointPrefab;
    [SerializeField] private GameObject towerpointPrefab;
    [SerializeField] private GameObject towerLocationButton;
    [SerializeField] private Transform mapCanvas;
    public static List<GameObject> path;
    // Start is called before the first frame update
    void Awake()
    {
        grid = new int[gridSize, gridSize];
        path = new List<GameObject>();
        createGrid();
        createTowerLocations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void createTowerLocations()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                if (grid[x, z] == 0 && CanPlaceTower(x, z))
                {
                    grid[x, z] = 2;
                    Instantiate(towerpointPrefab, new Vector3(x, FindTerrainHeight(x,z), z), Quaternion.identity, mapCanvas);
                    Instantiate(towerLocationButton, new Vector3(x, FindTerrainHeight(x, z), z), Quaternion.Euler(90,0,0), mapCanvas);
                }
            }
        }

    }
    float FindTerrainHeight(int x, int z)
    {
        RaycastHit hit;
        if(Physics.Raycast(new Vector3(x,0,z), Vector3.down, out hit))
            return hit.point.y + 1.0f;
        else
            return 0;
    }
    private void createGrid()
    {
        int nextMove;
        int prevMove = -1;
        int x = 0, z = Random.Range(0, gridSize);
        grid[x, z] = 1;
      Instantiate(waypointPrefab, new Vector3(x, 0, z), Quaternion.identity);
        while (x < gridSize-1)
        {
            //0-up, 1-down, 2-right (movement)
            nextMove = Random.Range(0, 3);
            while((nextMove == 0 && prevMove == 1) || (nextMove == 1 && prevMove == 0))
                    nextMove = Random.Range(0, 3);
            prevMove = nextMove;
            switch (nextMove)
            {
                case 0:
                    if(z<gridSize-1)
                    z++;
                    else
                    {
                        x++;
                        prevMove = 2;
                    }
                    break;
                case 1:
                    if(z>0)
                    z--;
                    else
                    {
                        x++;
                        prevMove = 2;
                    }
                    break;
                case 2:
                    x++;
                    break;
            }
            FillGridWithPath(x, z);
        }
    }

    private bool CanPlaceTower(int x, int z)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <=1 ; j++)
            {
                int a = x + i;
                int b = z + j;
                if (a >= 0 && a < gridSize && b >= 0 && b < gridSize && grid[a, b] == 1)
                    return true;
            }
        }
        return false;
    }
    void FillGridWithPath(int x, int z)
    {
        grid[x, z] = 1;
        path.Add(Instantiate(waypointPrefab, new Vector3(x, FindTerrainHeight(x, z), z), Quaternion.identity, transform));
        path[path.Count - 1].name = (path.Count - 1).ToString();
    }
}
