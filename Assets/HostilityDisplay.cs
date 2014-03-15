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

	public Texture2D spriteSheet;

	private int containerX;
	private int containerY;
	
	private List<string> labels = new List<string> ();

	public int iconColumn = 37;
	public int iconWidth = 43;
	public int iconHeight = 44;
	private Texture2D myTexture;

	// Use this for initialization
	void Start () {
		containerX = Screen.width - width - border;
		containerY = border;

		addDisplayText ("Test");
		addDisplayText ("Next Test");
		addDisplayText ("Final Test");

		//myTexture = new Texture2D (iconWidth, iconHeight);


		//createIcon ();
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

	void createIcon()
	{
		myTexture = new Texture2D (iconWidth, iconHeight);

		for (int x = 0; x < iconWidth; ++x) {
			for (int y = 0; y < iconHeight; ++y)
			{
				myTexture.SetPixel(x, y, spriteSheet.GetPixel(171 + x, 1611 - 88 + y));
			}
		}
		myTexture.Apply ();
	}

	void OnGUI()
	{
		GUI.Box (new Rect(containerX, containerY, width, Screen.height - border * 2), "Loader Menu");

		for (int i = 0; i < labels.Count; ++i)
		{
			GUI.Label (new Rect(containerX + padding, containerY + startY + i * textHeight, textWidth, textHeight), labels[i]);
		}

		createIcon ();
		GUI.Label (new Rect (containerX + padding, containerY + startY + 100, iconWidth, iconHeight), myTexture);
	}

}
