using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public float moveX = 1.00f;
	public float moveY = 1.00f;

	public int moveFrames = 10;

	private Transform playerPosition;
	private int prevUpdateFrame = 0;

    //private HostilityDisplay hostilityDisplay;

    public GameObject terrain;
	private Grid grid;

	private int currentHostility = 0;
    public int CurrentHostility
    {
        get
        {
            return currentHostility;
        }
    }
    
	public int randomHostilityMax = 40;
	public int randomHostilityMin = 20;

	private int minHostility = 0;
    public int MinHostility
    {
        get
        {
            return minHostility;
        }
    }
    
    private Animator animator;
    
    private Point currentPosition = new Point(0, 0);
    public Point CurrentPosition
    {
        get
        {
            return currentPosition;
        }
    }
    
    private bool battleOccurred = false;
    public bool BattleOccured
    {
        get
        {
            return battleOccurred;
        }
    }
    
    private Pokemon battlePokemon = Pokemon.Pikachu;
    public Pokemon BattlePokemon
    {
        get
        {
            return battlePokemon;
        }
    }
    
    private Dictionary<string, IMovementListener> m_listeners = new Dictionary<string, IMovementListener>();
    
    private enum Direction
    {
        SOUTH = 0,
        WEST,
        NORTH,
        EAST
    }
    
    public TileInfo GetCurrentTileInfo()
    {
        return grid.getTileInfo()[currentPosition.x, currentPosition.y];
    }


	// Use this for initialization
	void Start ()
	{
		grid = terrain.GetComponent<Grid>();
        //hostilityDisplay = GameObject.Find("statusDisplay").GetComponent<HostilityDisplay>();

		//UpdateHostility ();
        
        animator = this.GetComponentInChildren<Animator>();
	}
    
    
    /***************
     * Pub/Sub stuff
     ***************/
    public void RegisterListener(string registeredName, IMovementListener listener)
    {
        m_listeners[registeredName] = listener;
    }
    
    public void RemoveListener(string registeredName)
    {
        m_listeners.Remove(registeredName);
    }
    
    void NotifyListeners()
    {
        foreach (KeyValuePair<string, IMovementListener> pair in m_listeners)
        {
            pair.Value.HandleMovement();
        }
    }
    /**************/
    
    void UpdatePosition()
    {
        currentPosition.x = (int)transform.position.x;
        currentPosition.y = (int)transform.position.y;
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
		if (currentHostility >= hostilityThreshhold)
        {
			TriggerBattle(pokemonList);
			battleOccurred = true;
		}
        else
        {
            battleOccurred = false;
        }

		//hostilityDisplay.UpdateDisplay (currentHostility, grid.getHostilityData () [curX, curY], hostilityThreshhold, minHostility, battleOccurred, pokemonList);
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
        battlePokemon = pokemonList[selectedPokemon].pokemon;
		//hostilityDisplay.UpdateBattleContext (pokemon);

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
                        animator.SetInteger("Direction", (int)Direction.EAST);
                        transform.Translate(new Vector3(moveX, 0.0f, 0.0f));
						didMove = true;
                    }
                }
                else if (h < 0)
                {
                    if (transform.position.x > 0)
                    {
                        animator.SetInteger("Direction", (int)Direction.WEST);
                        transform.Translate (new Vector3(-moveX, 0.0f, 0.0f));
						didMove = true;
                    }
                }

                if (v > 0)
                {
                    if (transform.position.y < grid.y - 1)
                    {
                        animator.SetInteger("Direction", (int)Direction.NORTH);
                        transform.Translate (new Vector3(0.0f, moveY, 0.0f));
						didMove = true;
                    }
                }
                else if (v < 0)
                {
                    if (transform.position.y > 0)
                    {
                        animator.SetInteger("Direction", (int)Direction.SOUTH);
                        transform.Translate(new Vector3(0.0f, -moveY, 0.0f));
						didMove = true;
                    }
                }

				if (didMove)
				{
                    UpdatePosition();
					UpdateHostility();
                    NotifyListeners();
				}
            }
        }
	}
}
