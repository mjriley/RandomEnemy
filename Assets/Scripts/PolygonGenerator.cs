using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolygonGenerator : MonoBehaviour {

	// this list contains every vertex of the mesh that we are going to render
	public List<Vector3> newVertices = new List<Vector3>();

	// the triangles tell Unity how to build each section of the mesh joining the triangles
	public List<int> newTriangles = new List<int>();

	// the UV list is unimportant right now but it tells Unity how the texture is
	// aligned to each polygon
	public List<Vector2> newUV = new List<Vector2>();

	// A mesh is made up of the vertices, triangles, and UVs we are going to define
	// after we make them up we'll save them as this mesh
	private Mesh mesh;

    private Grid grid;

	private float tUnit = 0.25f;
	private Vector2 tStone = new Vector2(1, 0);
	private Vector2 tGrass = new Vector2(0, 1);
    private Vector2 tBrick = new Vector2(1, 1);

	private int squareCount;

	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter> ().mesh;
        grid = GetComponent<Grid>();

		//GenTerrain ();
		BuildMesh ();
		UpdateMesh ();
	}

	void UpdateMesh()
	{
		mesh.Clear ();
		mesh.vertices = newVertices.ToArray ();
		mesh.triangles = newTriangles.ToArray ();
		mesh.uv = newUV.ToArray ();
		mesh.Optimize ();
		mesh.RecalculateNormals ();

		squareCount = 0;
		newVertices.Clear ();
		newTriangles.Clear ();
		newUV.Clear ();
	}

	void GenSquare(int x, int y, Vector2 texture)
	{
		newVertices.Add (new Vector3 (x, y, 0));
		newVertices.Add (new Vector3 (x + 1, y, 0));
//		newVertices.Add (new Vector3 (x + 1, y - 1, 0));
//		newVertices.Add (new Vector3 (x, y - 1, 0));
		newVertices.Add (new Vector3 (x + 1, y + 1, 0));
		newVertices.Add (new Vector3 (x, y + 1, 0));

		int numVerts = squareCount * 4;

//		newTriangles.Add (numVerts);
//		newTriangles.Add (numVerts + 1);
//		newTriangles.Add (numVerts + 3);
//		newTriangles.Add (numVerts + 1);
//		newTriangles.Add (numVerts + 2);
//		newTriangles.Add (numVerts + 3);

		newTriangles.Add (numVerts);
		newTriangles.Add (numVerts + 2);
		newTriangles.Add (numVerts + 1);
		newTriangles.Add (numVerts);
		newTriangles.Add (numVerts + 3);
		newTriangles.Add (numVerts + 2);

		newUV.Add (new Vector2 (tUnit * texture.x, tUnit * texture.y + tUnit));
		newUV.Add (new Vector2 (tUnit * texture.x + tUnit, tUnit * texture.y + tUnit));
		newUV.Add (new Vector2 (tUnit * texture.x + tUnit, tUnit * texture.y));
		newUV.Add (new Vector2 (tUnit * texture.x, tUnit * texture.y));

		++squareCount;
	}

//	void GenTerrain()
//	{
//		blocks = new byte[grid.x, grid.y];
//
//		for (int px = 0; px < blocks.GetLength(0); ++px)
//		{
//			for (int py = 0; py < blocks.GetLength(1); ++py)
//			{
//				if (py == 5)
//				{
//					blocks[px, py] = 2;
//				}
//				else if (py < 5)
//				{
//					blocks[px, py] = 1;
//				}
//                else
//                {
//                    blocks[px, py] = 3;
//                }
//			}
//		}
//	}

	void BuildMesh()
	{
        byte[,] blocks = grid.getTerrainData();

		for (int px = 0; px < blocks.GetLength (0); ++px)
		{
			for (int py = 0; py < blocks.GetLength(1); ++py)
			{
				if (blocks[px, py] == 1)
				{
					GenSquare (px, py, tStone);
				}
				else if (blocks[px, py] == 2)
				{
					GenSquare (px, py, tGrass);
				}
                else if (blocks[px, py] == 3)
                {
                    GenSquare(px, py, tBrick);
                }
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
