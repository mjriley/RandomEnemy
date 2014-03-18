using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    public int x = 10;
    public int y = 10;

    private int[,] hostilityData;
    private byte[,] terrainData;
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


    private const int TERRAIN_STONE = 1;
    private const int TERRAIN_GRASS = 2;
    private const int TERRAIN_BRICK = 3;
	private const int TERRAIN_WATER = 4;
	private const int TERRAIN_DEEP_WATER = 5;

	// Use this for initialization
	void Start ()
    {
        initTerrain();
	}

    private void initTerrain()
    {
        terrainData = new byte[x, y];
        hostilityData = new int[x, y];
		hostilityThreshhold = new int[x, y];
		pokemonData = new List<PokemonData>[x, y];

        for (int i=0; i < x; ++i)
        {
            for (int j=0; j < y; ++j)
            {
				if (i > 5)
				{
					terrainData[i, j] = TERRAIN_DEEP_WATER;
					hostilityData[i, j] = -1;
					hostilityThreshhold[i, j] = 100;
					pokemonData[i, j] = null;
				}
				else if (i == 5)
				{
					terrainData[i, j] = TERRAIN_WATER;
					hostilityData[i, j] = 0;
					hostilityThreshhold[i, j] = 100;
					pokemonData[i, j] = null;
				}
                else if (j == 5)
                {
                    terrainData[i, j] = TERRAIN_GRASS;
                    hostilityData[i, j] = 5;
					hostilityThreshhold[i, j] = 100;
					pokemonData[i, j] = type1;
                }
                else if (j < 5)
                {
                    terrainData[i, j] = TERRAIN_STONE;
                    hostilityData[i, j] = 10;
					hostilityThreshhold[i, j] = 90;
					pokemonData[i, j] = type2;
                }
                else
                {
                    terrainData[i, j] = TERRAIN_BRICK;
                    hostilityData[i, j] = 1;
					hostilityThreshhold[i, j] = 200;
					pokemonData[i, j] = type3;
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

    public byte[,] getTerrainData()
    {
        return terrainData;
    }

	public List<PokemonData>[,] getPokemonData()
	{
		return pokemonData;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
