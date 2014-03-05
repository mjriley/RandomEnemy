using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    public int x = 10;
    public int y = 10;

    private int[,] hostilityData;
    private byte[,] terrainData;


    private const int TERRAIN_STONE = 1;
    private const int TERRAIN_GRASS = 2;
    private const int TERRAIN_BRICK = 3;

	// Use this for initialization
	void Start ()
    {
        initTerrain();
	}

    private void initTerrain()
    {
        terrainData = new byte[x, y];
        hostilityData = new int[x, y];

        for (int i=0; i < x; ++i)
        {
            for (int j=0; j < y; ++j)
            {
                if (j == 5)
                {
                    terrainData[i, j] = TERRAIN_GRASS;
                    hostilityData[i, j] = 5;
                }
                else if (j < 5)
                {
                    terrainData[i, j] = TERRAIN_STONE;
                    hostilityData[i, j] = 10;
                }
                else
                {
                    terrainData[i, j] = TERRAIN_BRICK;
                    hostilityData[i, j] = 1;
                }
            }
        }
    }

    public int[,] getHostilityData()
    {
        return hostilityData;
    }

    public byte[,] getTerrainData()
    {
        return terrainData;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
