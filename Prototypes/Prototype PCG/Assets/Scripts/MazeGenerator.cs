using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width = 31;
    public int height = 17;
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public Transform mazeParent;
    public GameObject goalPrefab;

    private bool[,] maze;

    void Start()
    {
        GenerateMaze();
        SpawnMaze();
        Timer.instance.StartTimer();
    }




    void GenerateMaze()
    {
        maze = new bool[width, height];
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                maze[x, y] = false; // wall by default

        RecursiveBacktrack(1, 1);
    }

    void RecursiveBacktrack(int x, int y)
    {
        maze[x, y] = true;

        int[] dirs = { 0, 1, 2, 3 }; // up, right, down, left
        Shuffle(dirs);

        foreach (int dir in dirs)
        {
            int dx = 0, dy = 0;
            switch (dir)
            {
                case 0: dy = 2; break;
                case 1: dx = 2; break;
                case 2: dy = -2; break;
                case 3: dx = -2; break;
            }

            int nx = x + dx, ny = y + dy;
            if (nx > 0 && ny > 0 && nx < width - 1 && ny < height - 1 && !maze[nx, ny])
            {
                maze[x + dx / 2, y + dy / 2] = true; // carve path
                RecursiveBacktrack(nx, ny);
            }
        }
    }

    void Shuffle(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int rnd = Random.Range(i, array.Length);
            int temp = array[i];
            array[i] = array[rnd];
            array[rnd] = temp;
        }
    }

    void SpawnMaze()
    {
        Vector3 offset = new Vector3(-width / 2f + 1f, -height / 2f + 0.5f, 0f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x, y, 0) + offset;
                GameObject tile = Instantiate(maze[x, y] ? floorPrefab : wallPrefab, pos, Quaternion.identity, mazeParent);
                tile.name = $"Tile_{x}_{y}";
            }
        }

        // Position player at start
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 startPos = new Vector3(1, 1, 0) + offset; // or find an actual floor tile
            player.transform.position = startPos;
        }


        // === Step 1: Collect valid floor positions ===
        List<Vector2Int> validGoalSpots = new List<Vector2Int>();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (maze[x, y]) // false = floor
                {
                    validGoalSpots.Add(new Vector2Int(x, y));
                }
            }
        }

        // === Step 2: Shuffle and place up to 4 goals ===
        System.Random rand = new System.Random();
        int goalCount = Mathf.Min(4, validGoalSpots.Count);
        for (int i = validGoalSpots.Count - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            (validGoalSpots[i], validGoalSpots[j]) = (validGoalSpots[j], validGoalSpots[i]);
        }

        for (int i = 0; i < goalCount; i++)
        {
            Vector2Int gridPos = validGoalSpots[i];
            Vector3 worldPos = new Vector3(gridPos.x, gridPos.y, -1f) + offset;

            GameObject goal = Instantiate(goalPrefab, worldPos, Quaternion.identity);
            goal.tag = "Goal"; // Redundant if already tagged
        }



    }
}



