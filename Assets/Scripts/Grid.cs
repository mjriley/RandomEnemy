using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    public int x = 10;
    public int y = 10;

    private TileInfo[,] m_tileInfo;
    private int[,] hostilityData;
    private TerrainType[,] terrainData;
	private int[,] hostilityThreshhold;
	private List<PokemonData>[,] pokemonData;

	private List<PokemonData> type1 = new List<PokemonData>(new PokemonData[] {
        new PokemonData(Pokemon.Charizard, 10), 
        new PokemonData(Pokemon.Oddish, 20), 
        new PokemonData(Pokemon.Pichu, 30), 
        new PokemonData(Pokemon.Psyduck, 5)
    });
    
	private List<PokemonData> type2 = new List<PokemonData>(new PokemonData[] {
        new PokemonData(Pokemon.Charizard, 10), 
        new PokemonData(Pokemon.Chespin, 10), 
        new PokemonData(Pokemon.Froakie, 10),
		new PokemonData(Pokemon.Jiggypuff, 10),
        new PokemonData(Pokemon.Lilteo, 20),
        new PokemonData(Pokemon.Oddish, 20),
        new PokemonData(Pokemon.Squirtle, 20)
    });
    
	private List<PokemonData> type3 = new List<PokemonData>(new PokemonData[] {
        new PokemonData(Pokemon.Jiggypuff, 5), 
        new PokemonData(Pokemon.Froakie, 10),
        new PokemonData(Pokemon.Chespin, 15),
        new PokemonData(Pokemon.Pikachu, 20)
    });

//
//    private const int TERRAIN_STONE = 1;
//    private const int TERRAIN_GRASS = 2;
//    private const int TERRAIN_BRICK = 3;
//	private const int TERRAIN_WATER = 4;
//	private const int TERRAIN_DEEP_WATER = 5;

	// Use this for initialization
	void Start ()
    {
        initTerrain();
	}

    private void initTerrain()
    {
        m_tileInfo = new TileInfo[x, y];
        terrainData = new TerrainType[x, y];
        hostilityData = new int[x, y];
		hostilityThreshhold = new int[x, y];
		pokemonData = new List<PokemonData>[x, y];

        for (int i=0; i < x; ++i)
        {
            for (int j=0; j < y; ++j)
            {
				if (i > 5)
				{
					terrainData[i, j] = TerrainType.DeepWater;
					hostilityData[i, j] = -1;
					hostilityThreshhold[i, j] = 100;
					pokemonData[i, j] = null;
                    
                    m_tileInfo[i, j] = new TileInfo(TerrainType.DeepWater, 100, -1, null);
				}
				else if (i == 5)
				{
					terrainData[i, j] = TerrainType.Water;
					hostilityData[i, j] = 0;
					hostilityThreshhold[i, j] = 100;
					pokemonData[i, j] = null;
                    
                    m_tileInfo[i, j] = new TileInfo(TerrainType.Water, 100, 0, null);
				}
                else if (j == 5)
                {
                    terrainData[i, j] = TerrainType.Grass;
                    hostilityData[i, j] = 5;
					hostilityThreshhold[i, j] = 100;
					pokemonData[i, j] = type1;
                    
                    m_tileInfo[i, j] = new TileInfo(TerrainType.Grass, 100, 5, type1);
                }
                else if (j < 5)
                {
                    terrainData[i, j] = TerrainType.Stone;
                    hostilityData[i, j] = 10;
					hostilityThreshhold[i, j] = 90;
					pokemonData[i, j] = type2;
                    
                    m_tileInfo[i, j] = new TileInfo(TerrainType.Stone, 90, 10, type2);
                }
                else
                {
                    terrainData[i, j] = TerrainType.Brick;
                    hostilityData[i, j] = 1;
					hostilityThreshhold[i, j] = 200;
					pokemonData[i, j] = type3;
                    
                    m_tileInfo[i, j] = new TileInfo(TerrainType.Brick, 200, 1, type3);
                }
            }
        }
    }

    public int[,] getHostilityData()
    {
        return hostilityData;
    }

	public int[,] getHostilityThreshhold()
	{
		return hostilityThreshhold;
	}

    public TerrainType[,] getTerrainData()
    {
        return terrainData;
    }

	public List<PokemonData>[,] getPokemonData()
	{
		return pokemonData;
	}

    public TileInfo[,] getTileInfo()
    {
        return m_tileInfo;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
