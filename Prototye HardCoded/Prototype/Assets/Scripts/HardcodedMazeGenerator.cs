using UnityEngine;
using System.Collections.Generic;

public class HardcodedMazeGenerator : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject goalPrefab;
    public Transform mazeParent;
    public float tileSize = 1f;

    private int[,] maze = new int[,]
    {
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,1},
        {1,0,1,0,1,0,1,1,1,1,1,0,1,0,1,1,1,1,1,1,1,1,1,1,0,1},
        {1,0,1,0,1,0,1,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,1,0,1},
        {1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,0,1,0,1},
        {1,0,0,0,1,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,1,0,1,0,1},
        {1,1,1,0,1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,0,1,0,1,0,1},
        {1,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,1,0,1,0,1,0,1},
        {1,0,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,0,1,0,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    };

    private void Start()
    {
        int height = maze.GetLength(0);
        int width = maze.GetLength(1);

        Timer.instance.StartTimer();



        // Align maze to top-left of screen
        Vector3 offset = new Vector3(-width / 2.08f, height / 2.2f, 0);

        // Keep track of all floor positions
        List<Vector2Int> floorPositions = new List<Vector2Int>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 pos = (new Vector3(x, -y, 0) + offset) * tileSize;

                if (maze[y, x] == 0)
                {
                    Instantiate(floorPrefab, pos, Quaternion.identity, mazeParent);
                    floorPositions.Add(new Vector2Int(x, y));
                }
                else
                {
                    Instantiate(wallPrefab, pos, Quaternion.identity, mazeParent);
                }
            }
        }

        // Shuffle floor positions and pick 4 goal positions
        ShuffleList(floorPositions);
        int goalsToPlace = Mathf.Min(4, floorPositions.Count);

        //for (int i = 0; i < goalsToPlace; i++)
        //{
        //    Vector2Int pos = floorPositions[i];
        //    Vector3 spawnPos = (new Vector3(pos.x, -pos.y, 0) + offset) * tileSize;
        //    Instantiate(goalPrefab, spawnPos, Quaternion.identity, mazeParent);
        //}
    }

    private void ShuffleList(List<Vector2Int> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = rng.Next(n--);
            Vector2Int temp = list[n];
            list[n] = list[k];
            list[k] = temp;
        }
    }
}
