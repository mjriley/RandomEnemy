using UnityEngine;
using System.Collections;

public class HostilityDisplay : MonoBehaviour 
{

    public int hostility = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        guiText.text = "Hostility: " + hostility;
	
	}

}
