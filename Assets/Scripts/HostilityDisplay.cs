using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HostilityDisplay : MonoBehaviour 
{
	public int playerHostility = 0;
    public int hostility = 0;

	public int width = 240;
	public int border = 25;
	public int padding = 10;
	public int startY = 25;
	public int textHeight = 20;
	public int textWidth = 100;

	public int pokemonPerRow = 4;

	public Texture2D spriteSheet;

	private int containerX;
	private int containerY;

	private enum Pokemon
	{
		Pikachu,
		Charizard,
		Squirtle,
		Chespin
	}
	
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

		coords[Pokemon.Pikachu] = new Point(171, 88);
		coords [Pokemon.Charizard] = new Point (171, 44);
		coords [Pokemon.Squirtle] = new Point (384, 44);
		coords [Pokemon.Chespin] = new Point (768, 1393);

		addDisplayText ("Test");
		addDisplayText ("Next Test");
		addDisplayText ("Final Test");

		addIcon (Pokemon.Pikachu);
		addIcon (Pokemon.Charizard);
		addIcon (Pokemon.Squirtle);
		addIcon (Pokemon.Chespin);
	}
	
	// Update is called once per frame
	void Update ()
    {
        guiText.text = "Hostility: " + hostility;
	}

	void addDisplayText(string text)
	{
		labels.Add (text);
	}

	void addIcon(Pokemon pokemon)
	{
		pokemonList.Add (pokemon);

		textures [pokemon] = createIcon (pokemon);
	}

	Texture2D createIcon(Pokemon pokemon)
	{
		Texture2D texture = new Texture2D (iconWidth, iconHeight);

		for (int x = 0; x < iconWidth; ++x) {
			for (int y = 0; y < iconHeight; ++y)
			{
				texture.SetPixel(x, y, spriteSheet.GetPixel(coords[pokemon].x + x, 1611 - coords[pokemon].y + y));
			}
		}
		texture.Apply ();

		return texture;
	}

	void OnGUI()
	{
		GUI.Box (new Rect(containerX, containerY, width, Screen.height - border * 2), "Loader Menu");

		for (int i = 0; i < labels.Count; ++i)
		{
			GUI.Label (new Rect(containerX + padding, containerY + startY + i * textHeight, textWidth, textHeight), labels[i]);
		}

		for (int i = 0; i < pokemonList.Count; ++i)
		{
			int column = i % pokemonPerRow;
			int row = i / pokemonPerRow;
			GUI.Label(new Rect(containerX + padding + iconWidth * column, containerY + startY + 100 + iconHeight * row, iconWidth, iconHeight), textures[pokemonList[i]]);
		}
	}

}
