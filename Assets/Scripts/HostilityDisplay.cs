﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HostilityDisplay : MonoBehaviour 
{
	public GUIStyle testStyle;

	public int playerHostility = 0;
    public int hostility = 0;
	public int hostilityThreshhold = 0;
	public bool battleFlag = false;

	public int width = 240;
	public int border = 25;
	public int padding = 10;
	public int startY = 25;
	public int textHeight = 22;
	public int textWidth = 200;

	public int pokemonPerRow = 4;

	public Texture2D spriteSheet;

	private int containerX;
	private int containerY;

	private int PLAYER_HOSTILITY_INDEX = 0;
	private int TILE_HOSTILITY_INDEX = 1;
	private int HOSTILITY_THRESHHOLD_INDEX = 2;
	private int MIN_HOSTILITY_INDEX = 3;
	private int BATTLE_INDEX = 4;

	private string PLAYER_HOSTILITY_PREFIX = "Player Hostility: ";
	private string HOSTILITY_PREFIX = "Tile Hostility: ";
	private string HOSTILITY_THRESHHOLD_PREFIX = "Hostility Threshhold: ";
	private string MIN_HOSTILITY_PREFIX = "Minimum Hostility: ";
	private string BATTLE_MESSAGE_PREFIX = "Battle Occurred! Fought ";
	
	private List<string> labels = new List<string> ();
	private List<Pokemon> pokemonList = new List<Pokemon> ();

	public int iconColumn = 37;
	public int iconWidth = 43;
	public int iconHeight = 44;
	private Texture2D myTexture;

	private Dictionary<Pokemon, Point> coords = new Dictionary<Pokemon, Point>();
	private Dictionary<Pokemon, Texture2D> textures = new Dictionary<Pokemon, Texture2D>();

	// Use this for initialization
	void Start () {
		containerX = Screen.width - width - border;
		containerY = border;

		coords[Pokemon.Pikachu] = new Point(171, 44);
		coords [Pokemon.Charizard] = new Point (171, 0);
		coords [Pokemon.Squirtle] = new Point (384, 0);
		coords [Pokemon.Chespin] = new Point (768, 1349);
		coords [Pokemon.Psyduck] = new Point (384, 87);
		coords [Pokemon.Jiggypuff] = new Point (768, 44);
		coords [Pokemon.Pichu] = new Point (640, 305);
		coords [Pokemon.Froakie] = new Point (0, 1393);
		coords [Pokemon.Oddish] = new Point (938, 44);
		coords [Pokemon.Lilteo] = new Point (256, 1436);

		addDisplayText (PLAYER_HOSTILITY_PREFIX);
		addDisplayText (HOSTILITY_PREFIX);
		addDisplayText (HOSTILITY_THRESHHOLD_PREFIX);
		addDisplayText (MIN_HOSTILITY_PREFIX);
		addDisplayText (BATTLE_MESSAGE_PREFIX);

		addIcon (Pokemon.Pikachu);
		addIcon (Pokemon.Charizard);
		addIcon (Pokemon.Squirtle);
		addIcon (Pokemon.Chespin);
		addIcon (Pokemon.Psyduck);
		addIcon (Pokemon.Jiggypuff);
		addIcon (Pokemon.Pichu);
		addIcon (Pokemon.Froakie);
		addIcon (Pokemon.Oddish);
		addIcon (Pokemon.Lilteo);
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

	public void UpdateDisplay(int currentHostility, int tileHostility, int tileThreshhold, int minHostility, bool isBattle, List<Pokemon> pokemon)
	{
		playerHostility = currentHostility;
		hostility = tileHostility;
		hostilityThreshhold = tileThreshhold;
		battleFlag = isBattle;

		labels [PLAYER_HOSTILITY_INDEX] = PLAYER_HOSTILITY_PREFIX + playerHostility;
		labels [TILE_HOSTILITY_INDEX] = HOSTILITY_PREFIX + hostility;
		labels [HOSTILITY_THRESHHOLD_INDEX] = HOSTILITY_THRESHHOLD_PREFIX + hostilityThreshhold;
		labels [MIN_HOSTILITY_INDEX] = MIN_HOSTILITY_PREFIX + minHostility;
		if (!battleFlag)
		{
			labels[BATTLE_INDEX] = "";
		}

		pokemonList = pokemon;
	}

	public void UpdateBattleContext(Pokemon pokemon)
	{
		labels[BATTLE_INDEX] = BATTLE_MESSAGE_PREFIX + pokemon.ToString();
	}

	void addDisplayText(string text)
	{
		labels.Add (text);
	}

	void addIcon(Pokemon pokemon)
	{
		//pokemonList.Add (pokemon);

		textures [pokemon] = createIcon (pokemon);
	}

	Texture2D createIcon(Pokemon pokemon)
	{
		Texture2D texture = new Texture2D (iconWidth, iconHeight);

		for (int x = 0; x < iconWidth; ++x) {
			for (int y = 0; y < iconHeight; ++y)
			{
				texture.SetPixel(x, y, spriteSheet.GetPixel(coords[pokemon].x + x, 1611 - coords[pokemon].y - 44 + y));
			}
		}
		texture.Apply ();

		return texture;
	}

	void OnGUI()
	{
		Rect parentRect = new Rect (containerX, containerY, width, Screen.height - border * 2);
		GUI.BeginGroup (parentRect);
			GUI.Box (new Rect (0, 0, parentRect.width, parentRect.height), "Loader Menu", testStyle);
			
		for (int i = 0; i < labels.Count; ++i)
        {
            GUI.Label(new Rect(testStyle.border.left, testStyle.border.top + i * textHeight, textWidth, textHeight), labels[i]);
        }

        if (pokemonList != null)
        {
            for (int i=0; i < pokemonList.Count; ++i)
            {
                int column = i % pokemonPerRow;
                int row = i / pokemonPerRow;
                GUI.Label (new Rect(testStyle.border.left + iconWidth * column, startY + 150 + iconHeight * row, iconWidth, iconHeight), textures[pokemonList[i]]);
            }
        }

		GUI.EndGroup ();
		//GUI.Box (new Rect(containerX, containerY, width, Screen.height - border * 2), "Loader Menu", testStyle);



//		for (int i = 0; i < labels.Count; ++i)
//		{
//			GUI.Label (new Rect(containerX + padding, containerY + startY + i * textHeight, textWidth, textHeight), labels[i]);
//		}

//		if (pokemonList != null) {
//			for (int i = 0; i < pokemonList.Count; ++i) {
//				int column = i % pokemonPerRow;
//				int row = i / pokemonPerRow;
//				GUI.Label (new Rect (containerX + padding + iconWidth * column, containerY + startY + 150 + iconHeight * row, iconWidth, iconHeight), textures [pokemonList [i]]);
//			}
//		}
	}

}
