using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    public int x = 10;
    public int y = 10;

    private int[,] hostilityData;
    private byte[,] terrainData;
	private int[,] hostilityThreshhold;
	private List<Pokemon>[,] pokemonData;

	private List<Pokemon> type1 = new List<Pokemon>(new Pokemon[] {Pokemon.Charizard, Pokemon.Oddish, Pokemon.Pichu, Pokemon.Psyduck});
	private List<Pokemon> type2 = new List<Pokemon>(new Pokemon[] {Pokemon.Charizard, Pokemon.Chespin, Pokemon.Froakie, 
		Pokemon.Jiggypuff, Pokemon.Lilteo, Pokemon.Oddish, Pokemon.Squirtle});
	private List<Pokemon> type3 = new List<Pokemon>(new Pokemon[] {Pokemon.Jiggypuff, Pokemon.Froakie, Pokemon.Chespin, Pokemon.Pikachu});


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
		pokemonData = new List<Pokemon>[x, y];

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

	public List<Pokemon>[,] getPokemonData()
	{
		return pokemonData;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
