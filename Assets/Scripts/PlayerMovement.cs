using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveX = 1.00f;
		public float moveY = 1.00f;

	public int moveFrames = 10;

	private Transform playerPosition;
	private int prevUpdateFrame = 0;

    private HostilityDisplay hostility;

    private Grid grid;

    public GameObject player;

	// Use this for initialization
	void Start ()
	{
        grid = GetComponent<Grid>();
        hostility = GameObject.Find("statusDisplay").GetComponent<HostilityDisplay>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (Time.frameCount - prevUpdateFrame >= moveFrames)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // check to make sure movement needs to be updated this window
            if ((h != 0) || (v != 0))
            {
                // make sure movement won't get checked again until the next window
                prevUpdateFrame = Time.frameCount;

                if (h > 0)
                {
                    if (player.transform.position.x < grid.x - 1)
                    {
                        player.transform.Translate(new Vector3(moveX, 0.0f, 0.0f));
                    }
                }
                else if (h < 0)
                {
                    if (player.transform.position.x > 0)
                    {
                        player.transform.Translate (new Vector3(-moveX, 0.0f, 0.0f));
                    }
                }

                if (v > 0)
                {
                    if (player.transform.position.y < grid.y - 1)
                    {
                        player.transform.Translate (new Vector3(0.0f, moveY, 0.0f));
                    }
                }
                else if (v < 0)
                {
                    if (player.transform.position.y > 0)
                    {
                        player.transform.Translate(new Vector3(0.0f, -moveY, 0.0f));
                    }
                }

                hostility.hostility = grid.getHostilityData()[(int)player.transform.position.x, (int)player.transform.position.y];
            }
        }
	}
}
