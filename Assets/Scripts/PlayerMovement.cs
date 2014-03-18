using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public float moveX = 1.00f;
	public float moveY = 1.00f;

	public int moveFrames = 10;

	private Transform playerPosition;
	private int prevUpdateFrame = 0;

    private HostilityDisplay hostilityDisplay;

    public GameObject terrain;
	private Grid grid;

	public int currentHostility = 0;
	public int randomHostilityMax = 40;
	public int randomHostilityMin = 20;

	private int minHostility = 0;


	// Use this for initialization
	void Start ()
	{
		grid = terrain.GetComponent<Grid>();
        hostilityDisplay = GameObject.Find("statusDisplay").GetComponent<HostilityDisplay>();

		UpdateHostility ();
	}

	void UpdateHostility()
	{
		int curX = (int)transform.position.x;
		int curY = (int)transform.position.y;

		int hostilityIncrement = grid.getHostilityData () [curX, curY];
		int hostilityThreshhold = grid.getHostilityThreshhold () [curX, curY];
		List<PokemonData> pokemonList = grid.getPokemonData () [curX, curY];

		currentHostility += hostilityIncrement;

		currentHostility = Mathf.Max (currentHostility, minHostility);

		// check for a random battle
		bool battleOccurred = false;

		if (currentHostility >= hostilityThreshhold) {
			TriggerBattle(pokemonList);
			battleOccurred = true;
				}

		hostilityDisplay.UpdateDisplay (currentHostility, grid.getHostilityData () [curX, curY], hostilityThreshhold, minHostility, battleOccurred, pokemonList);
	}

	void TriggerBattle(List<PokemonData> pokemonList)
	{
        // compute the total weight of the pokemon list
        uint totalWeight = 0;
        foreach (PokemonData data in pokemonList)
        {
            totalWeight += data.weight;        
        }
        
        uint randomWeight = (uint)Random.Range(0, (int)totalWeight);
        
        uint currentWeight = 0;
        
        int selectedPokemon = 0;
        for (; selectedPokemon < pokemonList.Count; ++selectedPokemon)
        {
            currentWeight += pokemonList[selectedPokemon].weight;
            
            if (currentWeight >= randomWeight)
            {
                break;
            }
        }
        
		//Pokemon pokemon = pokemonList [Random.Range (0, pokemonList.Count)].pokemon;
        Pokemon pokemon = pokemonList[selectedPokemon].pokemon;
		hostilityDisplay.UpdateBattleContext (pokemon);

		// reset the hostility
		currentHostility = Random.Range (0, randomHostilityMax);

		// randomize the minimum
		minHostility = Random.Range (0, randomHostilityMin);
	}

	// Update is called once per frame
	void Update ()
	{
        if (Time.frameCount - prevUpdateFrame >= moveFrames)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

			bool didMove = false;

            // check to make sure movement needs to be updated this window
            if ((h != 0) || (v != 0))
            {
                // make sure movement won't get checked again until the next window
                prevUpdateFrame = Time.frameCount;

                if (h > 0)
                {
                    if (transform.position.x < grid.x - 1)
                    {
                        transform.Translate(new Vector3(moveX, 0.0f, 0.0f));
						didMove = true;
                    }
                }
                else if (h < 0)
                {
                    if (transform.position.x > 0)
                    {
                        transform.Translate (new Vector3(-moveX, 0.0f, 0.0f));
						didMove = true;
                    }
                }

                if (v > 0)
                {
                    if (transform.position.y < grid.y - 1)
                    {
                        transform.Translate (new Vector3(0.0f, moveY, 0.0f));
						didMove = true;
                    }
                }
                else if (v < 0)
                {
                    if (transform.position.y > 0)
                    {
                        transform.Translate(new Vector3(0.0f, -moveY, 0.0f));
						didMove = true;
                    }
                }

				if (didMove)
				{
					UpdateHostility ();
				}
            }
        }
	}
}
