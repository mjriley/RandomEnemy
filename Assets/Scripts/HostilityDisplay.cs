using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HostilityDisplay : MonoBehaviour 
{
	public GUIStyle testStyle;
    public GUIStyle iconStyle;
    public GUIStyle statStyle;
    public GUIStyle sectionHeaderCaption;
    public GUIStyle sectionHeaderStyle;

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

		labels [PLAYER_HOSTILITY_INDEX] = PLAYER_HOSTILITY_PREFIX + "<color=darkblue>" + playerHostility + "</color>";
		labels [TILE_HOSTILITY_INDEX] = HOSTILITY_PREFIX + "<color=darkblue>" + hostility + "</color>";
		labels [HOSTILITY_THRESHHOLD_INDEX] = HOSTILITY_THRESHHOLD_PREFIX + "<color=darkblue>" + hostilityThreshhold + "</color>";
		labels [MIN_HOSTILITY_INDEX] = MIN_HOSTILITY_PREFIX + "<color=darkblue>" + minHostility + "</color>";
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
		GUILayout.BeginArea(parentRect, "Diagnostics", testStyle);

        
                    Rect textRect = new Rect(testStyle.border.left + testStyle.padding.left, 
                                            testStyle.border.top + testStyle.padding.top, 
                                         parentRect.width - testStyle.border.left - testStyle.border.right - testStyle.padding.left - testStyle.padding.right, 
                                         parentRect.height - testStyle.border.top - testStyle.border.bottom - testStyle.padding.top - testStyle.padding.bottom);
        
        GUILayout.BeginArea(textRect);
        GUILayout.BeginVertical();

                for (int i = 0; i < labels.Count; ++i)
                {
                    //GUI.Label(new Rect(0, i * textHeight, textWidth, textHeight), labels[i]);
                    GUILayout.Label(labels[i], statStyle);
                }
                

                
                GUILayout.FlexibleSpace();
                
                GUILayout.Label("Native Pokemon", sectionHeaderCaption);
                
                GUILayout.BeginVertical(sectionHeaderStyle, GUILayout.MinHeight(106));
                
                // This should be a conditional group, but height isn't respected if the vertical group contains no elements
                GUILayout.BeginHorizontal();
                
                if (pokemonList != null)
                {   
                    for (int i = 0; i < pokemonList.Count; ++i)
                    {
                        if (i % pokemonPerRow == 0 && i != 0)
                        {
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                        }
                        
                        GUILayout.Label(textures[pokemonList[i]], iconStyle, GUILayout.Width(textures[pokemonList[i]].width));
                    }
                }
                
                GUILayout.EndHorizontal();
                
                GUILayout.EndVertical();
                
        GUILayout.EndVertical();
        GUILayout.EndArea();
        GUILayout.EndArea();
	}

}
