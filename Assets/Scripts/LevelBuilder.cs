using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Linq;






public class Tile
{
	public int x;
	public int y;
	public string tileType;
    public int value;

	public Tile (int x, int y, string tileType, int value = 0)
	{
		this.x = x;
		this.y = y;
		this.tileType = tileType;
        this.value = value;
	}
}

public static class Constants
{
	public struct TileType
	{
		public const string Border = "X";
		public const string Wall = "W";
		public const string Ground = "G";
		public const string Portal = "O";
		public const string Stone = "S";
		public const string ActiveBomb = "A";
		public const string DeactiveBomb = "D";
		public const string Crystal = "K";
		public const string Mineral = "M";
		public const string Enemy = "E";
		public const string Player = "P";
        public const string Ballon = "B";
        public const string Switcher = "C";
    }

	public struct BiomType
	{
		public const string Earth = "E";
		public const string Spaceship = "S";
		public const string Asteroid = "A";
	}
}


[System.Serializable]
public class CornerSprites
{
	public Sprite borderUpLeftCorner;
	public Sprite borderUpRightCorner;
	public Sprite borderDownLeftCorner;
	public Sprite borderDownRightCorner;
	public Sprite wallUpLeftCorner;
	public Sprite wallUpRightCorner;
	public Sprite wallDownLeftCorner;
	public Sprite wallDownRightCorner;
	public Sprite groundUpLeftCorner;
	public Sprite groundUpRightCorner;
	public Sprite groundDownLeftCorner;
	public Sprite groundDownRightCorner;
}

[System.Serializable]
public class BorderSprites
{
	public Sprite borderBorderLeft;
	public Sprite borderBorderRight;
	public Sprite borderBorderUp;
	public Sprite borderBorderDown;
	public Sprite borderBorderLeftGR;
	public Sprite borderBorderRightGR;
	public Sprite borderBorderUpGR;
	public Sprite borderBorderDownGR;
	public Sprite wallBorderLeft;
	public Sprite wallBorderRight;
	public Sprite wallBorderUp;
	public Sprite wallBorderDown;
	public Sprite wallBorderLeftGR;
	public Sprite wallBorderRightGR;
	public Sprite wallBorderUpGR;
	public Sprite wallBorderDownGR;
	public Sprite groundBorderLeft;
	public Sprite groundBorderRight;
	public Sprite groundBorderUp;
	public Sprite groundBorderDown;
	public Sprite groundBorderLeftGR;
	public Sprite groundBorderRightGR;
	public Sprite groundBorderUpGR;
	public Sprite groundBorderDownGR;
}


[System.Serializable]
public class RandomLevelGenerator
{
	public int xMax = 7;
	public int yMax = 7;
	public int maxStones = 3;
	public int maxGround = 7;
	public int maxActiveBomb = 1;
	public int maxDeactiveBomb = 2;
	public int maxCrystal = 3;
	public int maxBallon = 3;
}

[System.Serializable]
public class Biom
{
	public Sprite[] borders;
	public Sprite[] walls;
	public Sprite[] grounds;
	public Sprite[] portals;
	public CornerSprites cornerSprites;
	public BorderSprites borderSprites;
	public Sprite[] stones;
	public Sprite[] activeBombs;
	public Sprite[] deactiveBombs;
	public Sprite[] crystals;
	public Sprite[] minerals;
	public Sprite[] ballons;
	public Sprite[] enemies;
	public Sprite[] players;
	public Sprite[] backgrounds;
}


public class LevelBuilder : MonoBehaviour
{
	public Biom earth;
	public Biom spaceship;
	public Biom asteroid;



	//public BorderSprites borderSprites;


	//tiles;
	public GameObject border;
	public GameObject wall;
	public GameObject ground;
	public GameObject portal;
	public GameObject tileBorderLeft;
	public GameObject tileBorderRight;
	public GameObject tileBorderUp;
	public GameObject tileBorderDown;
	public GameObject tileCornerUpLeft;
	public GameObject tileCornerUpRight;
	public GameObject tileCornerDownLeft;
	public GameObject tileCornerDownRight;
    public GameObject gravitySwitcher;
	//moving objects
	public GameObject stone;
	public GameObject activeBomb;
	public GameObject deactiveBomb;
	public GameObject crystal;
	public GameObject mineral;
	public GameObject ballon;
	//enemies
	public GameObject enemy;
	//player
	public GameObject player;

	public GameObject background;

	//public TextAsset fileLevel;
	//public TextAsset ini;

	private RandomLevelGenerator randomLevelGenerator;
    

	private List<Tile> tiles;
	private Biom currentBiom;
	private int numOfCrystalsToExit = 0;

    public Biom GetCurrentBiom()
    {
        return currentBiom;
    }
    

	public List<Tile> ReadFile ()
	{
		List<Tile> newTiles;
		if (ApplicationController.SelectedFile == null)
			return null;

		string[] lines = ApplicationController.SelectedFile.text.Split ('\n');
        
		int x = 0;
		int y = 0;

		newTiles = new List<Tile> ();

		if (lines.Length > 0) {
			var line = lines [0].Split (';', ',', '\n');

			switch (line [0]) {
			case Constants.BiomType.Asteroid:
				currentBiom = asteroid;
				break;
			case Constants.BiomType.Earth:
				currentBiom = earth;
				break;
			case Constants.BiomType.Spaceship:
				currentBiom = spaceship;
				break;
			default:
				currentBiom = earth;
				break;
			}

			if (!int.TryParse (line [1], out numOfCrystalsToExit))
				numOfCrystalsToExit = 0;

			switch (line [2]) {
			case "up":
				ApplicationController.gravity = Vector3.up;
				break;
			case "down":
				ApplicationController.gravity = Vector3.down;
				break;
			case "left":
				ApplicationController.gravity = Vector3.left;
				break;
			case "right":
				ApplicationController.gravity = Vector3.right;
				break;
			default:
				ApplicationController.gravity = Vector3.down;
				break;
			}

		}

		var tileLines = lines.Skip (1);//skip first line



		foreach (var line in tileLines) {
            //Debug.Log(line);
            var symbols = line.Split (';', ',');
			foreach (var symbol in symbols) {
                //  Debug.Log(symbol);
                if (symbol != "")
                {
                    if (string.Compare(symbol[0].ToString(),Constants.TileType.Mineral)==0)
                    {
                        int value;
                        int.TryParse(new string(symbol.ToCharArray().Where(char.IsDigit).ToArray()), out value);
                        newTiles.Add(new Tile(x, -y, symbol[0].ToString(),value));
                    }
                    else  newTiles.Add(new Tile(x, -y, symbol[0].ToString()));

                }
				x++;
			}
			y++;
			x = 0;
		}
		tiles = newTiles;
            
		return newTiles;
	}

	public List<Tile> GenerateRandom ()
	{

		List<Tile> freeTiles = new List<Tile> ();
		List<Tile> newTiles = new List<Tile> ();
		Tile newTile;
		Tile player;

		currentBiom = earth;
		ApplicationController.gravity = Vector3.down;

		for (int x = 1; x <= randomLevelGenerator.xMax; x++) {
			for (int y = 1; y <= randomLevelGenerator.yMax; y++) {
				if (x == 1 || x == randomLevelGenerator.xMax || y == 1 || y == randomLevelGenerator.yMax) {
					newTile = new Tile (x, y, Constants.TileType.Border);
					newTiles.Add (newTile);
				} else
					freeTiles.Add (new Tile (x, y, " "));
			}
		}

        
		int index = Random.Range (0, freeTiles.Count);
		player = freeTiles [index];
		player.tileType = Constants.TileType.Player;
		newTiles.Add (player);
		freeTiles.RemoveAt (index);
		if (player.y != randomLevelGenerator.yMax - 1) {

			newTile = freeTiles.Find (tile => tile.x == player.x && tile.y == player.y + 1);
            
			freeTiles.Remove (newTile);

			newTile.tileType = Constants.TileType.Ground;
			newTiles.Add (newTile);
			randomLevelGenerator.maxGround--;
		}
        
		while (randomLevelGenerator.maxStones > 0) {
			if (freeTiles.Count == 0)
				break; 
			index = Random.Range (0, freeTiles.Count - 1);
			newTile = freeTiles [index];
			newTile.tileType = Constants.TileType.Stone;
			newTiles.Add (newTile);
			freeTiles.RemoveAt (index);
			randomLevelGenerator.maxStones--;
		}

		while (randomLevelGenerator.maxGround > 0) {

			if (freeTiles.Count == 0)
				break;
			index = Random.Range (0, freeTiles.Count - 1);
			newTile = freeTiles [index];
			newTile.tileType = Constants.TileType.Ground;
			newTiles.Add (newTile);
			freeTiles.RemoveAt (index);
			randomLevelGenerator.maxGround--;
		}

		while (randomLevelGenerator.maxCrystal > 0) {
			if (freeTiles.Count == 0)
				break;
			index = Random.Range (0, freeTiles.Count - 1);
			newTile = freeTiles [index];
			newTile.tileType = Constants.TileType.Crystal;
			newTiles.Add (newTile);
			freeTiles.RemoveAt (index);
			randomLevelGenerator.maxCrystal--;
			numOfCrystalsToExit++;
		}

		while (randomLevelGenerator.maxActiveBomb > 0) {
			if (freeTiles.Count == 0)
				break;
			index = Random.Range (0, freeTiles.Count - 1);
			newTile = freeTiles [index];
			newTile.tileType = Constants.TileType.ActiveBomb;
			newTiles.Add (newTile);
			freeTiles.RemoveAt (index);
			randomLevelGenerator.maxActiveBomb--;
		}

		while (randomLevelGenerator.maxDeactiveBomb > 0) {
			if (freeTiles.Count == 0)
				break;
			index = Random.Range (0, freeTiles.Count - 1);
			newTile = freeTiles [index];
			newTile.tileType = Constants.TileType.DeactiveBomb;
			newTiles.Add (newTile);
			freeTiles.RemoveAt (index);
			randomLevelGenerator.maxDeactiveBomb--;
		}
        
		while (randomLevelGenerator.maxBallon > 0) {
			if (freeTiles.Count == 0)
				break;
			index = Random.Range (0, freeTiles.Count - 1);
			newTile = freeTiles [index];
			newTile.tileType = Constants.TileType.Ballon;
			newTiles.Add (newTile);
			freeTiles.RemoveAt (index);
			randomLevelGenerator.maxBallon--;
		}

		tiles = newTiles;
		return newTiles;

	}
    
	private void SetBordersAndAngles (Tile thisTile, GameObject thisGO)
	{
        return;



		/////////////////////////////////////////////////////
		var leftNeighbour = tiles.Find (item => (item.x == thisTile.x - 1) && (item.y == thisTile.y));
		if (leftNeighbour == null) {
			if (thisTile.tileType == Constants.TileType.Border) {
				tileBorderLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderLeft;
			} else if (thisTile.tileType == Constants.TileType.Wall) {
				tileBorderLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.wallBorderLeft;
			} else if (thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral)
            {
				tileBorderLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.groundBorderLeft;
			}
			Instantiate (tileBorderLeft, thisGO.transform.position + new Vector3 (-0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
		} else if (leftNeighbour.tileType == Constants.TileType.Border) {

		} else if (leftNeighbour.tileType == Constants.TileType.Wall) {
			if (thisTile.tileType == Constants.TileType.Border) {
				tileBorderLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderLeftGR;
				Instantiate (tileBorderLeft, thisGO.transform.position + new Vector3 (-0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
			}
		} else if (leftNeighbour.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) {
			if (thisTile.tileType == Constants.TileType.Wall || thisTile.tileType == Constants.TileType.Border) {
				if (thisTile.tileType == Constants.TileType.Border) {
					tileBorderLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderLeftGR;
				} else if (thisTile.tileType == Constants.TileType.Wall) {
					tileBorderLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.wallBorderLeftGR;
				}
				Instantiate (tileBorderLeft, thisGO.transform.position + new Vector3 (-0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
			}
		} else {
			if (thisTile.tileType == Constants.TileType.Border) {
				tileBorderLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderLeft;
			} else if (thisTile.tileType == Constants.TileType.Wall) {
				tileBorderLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.wallBorderLeft;
			} else if (thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) {
				tileBorderLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.groundBorderLeft;
			}
			Instantiate (tileBorderLeft, thisGO.transform.position + new Vector3 (-0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
		}

		/////////////////////////////////////////////////////
		var rightNeighbour = tiles.Find (item => (item.x == thisTile.x + 1) && (item.y == thisTile.y));
		if (rightNeighbour == null) {
			if (thisTile.tileType == Constants.TileType.Border) {
				tileBorderRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderRight;
			} else if (thisTile.tileType == Constants.TileType.Wall) {
				tileBorderRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.wallBorderRight;
			} else if (thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) {
				tileBorderRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.groundBorderRight;
			}
			Instantiate (tileBorderRight, thisGO.transform.position + new Vector3 (0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
		} else if (rightNeighbour.tileType == Constants.TileType.Border) {

		} else if (rightNeighbour.tileType == Constants.TileType.Wall) {
			if (thisTile.tileType == Constants.TileType.Border) {
				tileBorderRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderRightGR;
				Instantiate (tileBorderRight, thisGO.transform.position + new Vector3 (0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
			}
		} else if (rightNeighbour.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) {
			if (thisTile.tileType == Constants.TileType.Wall || thisTile.tileType == Constants.TileType.Border) {
				if (thisTile.tileType == Constants.TileType.Border) {
					tileBorderRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderRightGR;
				} else if (thisTile.tileType == Constants.TileType.Wall) {
					tileBorderRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.wallBorderRightGR;
				}
				Instantiate (tileBorderRight, thisGO.transform.position + new Vector3 (0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
			}
		} else {
			if (thisTile.tileType == Constants.TileType.Border) {
				tileBorderRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderRight;
			} else if (thisTile.tileType == Constants.TileType.Wall) {
				tileBorderRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.wallBorderRight;
			} else if (thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) {
				tileBorderRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.groundBorderRight;
			}
			Instantiate (tileBorderRight, thisGO.transform.position + new Vector3 (0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
		}

		/////////////////////////////////////////////////////
		var upNeighbour = tiles.Find (item => (item.x == thisTile.x) && (item.y == thisTile.y + 1));
		if (upNeighbour == null) {
			if (thisTile.tileType == Constants.TileType.Border) {
				tileBorderUp.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderUp;
			} else if (thisTile.tileType == Constants.TileType.Wall) {
				tileBorderUp.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.wallBorderUp;
			} else if (thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) {
				tileBorderUp.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.groundBorderUp;
			}
			Instantiate (tileBorderUp, thisGO.transform.position + new Vector3 (0f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
		} else if (upNeighbour.tileType == Constants.TileType.Border) {

		} else if (upNeighbour.tileType == Constants.TileType.Wall) {
			if (thisTile.tileType == Constants.TileType.Border) {
				tileBorderUp.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderUpGR;
				Instantiate (tileBorderUp, thisGO.transform.position + new Vector3 (0f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
			}
		} else if (upNeighbour.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) {
			if (thisTile.tileType == Constants.TileType.Wall || thisTile.tileType == Constants.TileType.Border) {
				if (thisTile.tileType == Constants.TileType.Border) {
					tileBorderUp.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderUpGR;
				} else if (thisTile.tileType == Constants.TileType.Wall) {
					tileBorderUp.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.wallBorderUpGR;
				}
				Instantiate (tileBorderUp, thisGO.transform.position + new Vector3 (0f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
			}
		} else {
			if (thisTile.tileType == Constants.TileType.Border) {
				tileBorderUp.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderUp;
			} else if (thisTile.tileType == Constants.TileType.Wall) {
				tileBorderUp.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.wallBorderUp;
			} else if (thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) {
				tileBorderUp.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.groundBorderUp;
			}
			Instantiate (tileBorderUp, thisGO.transform.position + new Vector3 (0f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
		}

		/////////////////////////////////////////////////////
		var downNeighbour = tiles.Find (item => (item.x == thisTile.x) && (item.y == thisTile.y - 1));
		if (downNeighbour == null) {
			if (thisTile.tileType == Constants.TileType.Border) {
				tileBorderDown.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderDown;
			} else if (thisTile.tileType == Constants.TileType.Wall) {
				tileBorderDown.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.wallBorderDown;
			} else if (thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) {
				tileBorderDown.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.groundBorderDown;
			}
			Instantiate (tileBorderDown, thisGO.transform.position + new Vector3 (0f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
		} else if (downNeighbour.tileType == Constants.TileType.Border) {

		} else if (downNeighbour.tileType == Constants.TileType.Wall) {
			if (thisTile.tileType == Constants.TileType.Border) {
				tileBorderDown.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderDownGR;
				Instantiate (tileBorderDown, thisGO.transform.position + new Vector3 (0f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
			}
		} else if (downNeighbour.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) {
			if (thisTile.tileType == Constants.TileType.Wall || thisTile.tileType == Constants.TileType.Border) {
				if (thisTile.tileType == Constants.TileType.Border) {
					tileBorderDown.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderDownGR;
				} else if (thisTile.tileType == Constants.TileType.Wall) {
					tileBorderDown.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.wallBorderDownGR;
				}
				Instantiate (tileBorderDown, thisGO.transform.position + new Vector3 (0f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
			}
		} else {
			if (thisTile.tileType == Constants.TileType.Border) {
				tileBorderDown.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.borderBorderDown;
			} else if (thisTile.tileType == Constants.TileType.Wall) {
				tileBorderDown.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.wallBorderDown;
			} else if (thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) {
				tileBorderDown.GetComponent<SpriteRenderer> ().sprite = currentBiom.borderSprites.groundBorderDown;
			}
			Instantiate (tileBorderDown, thisGO.transform.position + new Vector3 (0f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
		}

		/*********************************************************************************/

		if (thisTile.tileType == Constants.TileType.Border) {
			tileCornerUpLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.borderUpLeftCorner;
		} else if (thisTile.tileType == Constants.TileType.Wall) {
			tileCornerUpLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.wallUpLeftCorner;
		} else if (thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) {
			tileCornerUpLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.groundUpLeftCorner;
		}
		if (leftNeighbour != null && upNeighbour != null && leftNeighbour.tileType == thisTile.tileType && upNeighbour.tileType == thisTile.tileType) {
			var upLeftNeighbour = tiles.Find (item => (item.x == thisTile.x - 1) && (item.y == thisTile.y + 1));
			if (thisTile.tileType == Constants.TileType.Border && (upLeftNeighbour == null || upLeftNeighbour.tileType != Constants.TileType.Border))
				Instantiate (tileCornerUpLeft, thisGO.transform.position + new Vector3 (-0.5f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
			else if (thisTile.tileType == Constants.TileType.Wall && (upLeftNeighbour == null || (upLeftNeighbour.tileType != Constants.TileType.Border && upLeftNeighbour.tileType != Constants.TileType.Wall)))
				Instantiate (tileCornerUpLeft, thisGO.transform.position + new Vector3 (-0.5f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
			else if ((thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) && (upLeftNeighbour == null || (upLeftNeighbour.tileType != Constants.TileType.Border && upLeftNeighbour.tileType != Constants.TileType.Wall && upLeftNeighbour.tileType != Constants.TileType.Ground && upLeftNeighbour.tileType != Constants.TileType.Mineral)))
				Instantiate (tileCornerUpLeft, thisGO.transform.position + new Vector3 (-0.5f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
		}

		/////////////////////////////////////////////////////////////
		if (thisTile.tileType == Constants.TileType.Border) {
			tileCornerUpRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.borderUpRightCorner;
		} else if (thisTile.tileType == Constants.TileType.Wall) {
			tileCornerUpRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.wallUpRightCorner;
		} else if (thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) {
			tileCornerUpRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.groundUpRightCorner;
		}
		if (rightNeighbour != null && upNeighbour != null && rightNeighbour.tileType == thisTile.tileType && upNeighbour.tileType == thisTile.tileType) {
			var upRightNeighbour = tiles.Find (item => (item.x == thisTile.x + 1) && (item.y == thisTile.y + 1));
			if (thisTile.tileType == Constants.TileType.Border && (upRightNeighbour == null || upRightNeighbour.tileType != Constants.TileType.Border))
				Instantiate (tileCornerUpRight, thisGO.transform.position + new Vector3 (0.5f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
			else if (thisTile.tileType == Constants.TileType.Wall && (upRightNeighbour == null || (upRightNeighbour.tileType != Constants.TileType.Border && upRightNeighbour.tileType != Constants.TileType.Wall)))
				Instantiate (tileCornerUpRight, thisGO.transform.position + new Vector3 (0.5f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
			else if ((thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) && (upRightNeighbour == null || (upRightNeighbour.tileType != Constants.TileType.Border && upRightNeighbour.tileType != Constants.TileType.Wall && upRightNeighbour.tileType != Constants.TileType.Ground && upRightNeighbour.tileType != Constants.TileType.Mineral)))
				Instantiate (tileCornerUpRight, thisGO.transform.position + new Vector3 (0.5f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
		}

		/////////////////////////////////////////////////////////////
		if (thisTile.tileType == Constants.TileType.Border) {
			tileCornerDownLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.borderDownLeftCorner;
		} else if (thisTile.tileType == Constants.TileType.Wall) {
			tileCornerDownLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.wallDownLeftCorner;
		} else if (thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) {
			tileCornerDownLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.groundDownLeftCorner;
		}
		if (leftNeighbour != null && downNeighbour != null && leftNeighbour.tileType == thisTile.tileType && downNeighbour.tileType == thisTile.tileType) {
			var downLeftNeighbour = tiles.Find (item => (item.x == thisTile.x - 1) && (item.y == thisTile.y - 1));
			if (thisTile.tileType == Constants.TileType.Border && (downLeftNeighbour == null || downLeftNeighbour.tileType != Constants.TileType.Border))
				Instantiate (tileCornerDownLeft, thisGO.transform.position + new Vector3 (-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
			else if (thisTile.tileType == Constants.TileType.Wall && (downLeftNeighbour == null || (downLeftNeighbour.tileType != Constants.TileType.Border && downLeftNeighbour.tileType != Constants.TileType.Wall)))
				Instantiate (tileCornerDownLeft, thisGO.transform.position + new Vector3 (-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
			else if ((thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) && (downLeftNeighbour == null || (downLeftNeighbour.tileType != Constants.TileType.Border && downLeftNeighbour.tileType != Constants.TileType.Wall && downLeftNeighbour.tileType != Constants.TileType.Ground && downLeftNeighbour.tileType != Constants.TileType.Mineral)))
				Instantiate (tileCornerDownLeft, thisGO.transform.position + new Vector3 (-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
		}

		/////////////////////////////////////////////////////////////
		if (thisTile.tileType == Constants.TileType.Border) {
			tileCornerDownRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.borderDownRightCorner;
		} else if (thisTile.tileType == Constants.TileType.Wall) {
			tileCornerDownRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.wallDownRightCorner;
		} else if (thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) {
			tileCornerDownRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.groundDownRightCorner;
		}
		if (rightNeighbour != null && downNeighbour != null && rightNeighbour.tileType == thisTile.tileType && downNeighbour.tileType == thisTile.tileType) {
			var downRightNeighbour = tiles.Find (item => (item.x == thisTile.x + 1) && (item.y == thisTile.y - 1));
			if (thisTile.tileType == Constants.TileType.Border && (downRightNeighbour == null || downRightNeighbour.tileType != Constants.TileType.Border))
				Instantiate (tileCornerDownRight, thisGO.transform.position + new Vector3 (0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
			else if (thisTile.tileType == Constants.TileType.Wall && (downRightNeighbour == null || (downRightNeighbour.tileType != Constants.TileType.Border && downRightNeighbour.tileType != Constants.TileType.Wall)))
				Instantiate (tileCornerDownRight, thisGO.transform.position + new Vector3 (0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
			else if ((thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) && (downRightNeighbour == null || (downRightNeighbour.tileType != Constants.TileType.Border && downRightNeighbour.tileType != Constants.TileType.Wall && downRightNeighbour.tileType != Constants.TileType.Ground && downRightNeighbour.tileType != Constants.TileType.Mineral)))
				Instantiate (tileCornerDownRight, thisGO.transform.position + new Vector3 (0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
		}
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		if ((rightNeighbour == null || rightNeighbour.tileType != thisTile.tileType) && (downNeighbour == null || downNeighbour.tileType != thisTile.tileType)) {
			var downRightNeighbour = tiles.Find (item => (item.x == thisTile.x + 1) && (item.y == thisTile.y - 1));
			if (thisTile.tileType == Constants.TileType.Border && downRightNeighbour != null && downRightNeighbour.tileType == thisTile.tileType) {
				tileCornerUpRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.borderUpRightCorner;
				Instantiate (tileCornerUpRight, thisGO.transform.position + new Vector3 (0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);

				tileCornerDownLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.borderDownLeftCorner;
				Instantiate (tileCornerDownLeft, thisGO.transform.position + new Vector3 (0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
			} else if (thisTile.tileType == Constants.TileType.Wall && downRightNeighbour != null && downRightNeighbour.tileType == thisTile.tileType) {
				if (rightNeighbour == null || rightNeighbour.tileType != Constants.TileType.Border) {
					tileCornerUpRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.wallUpRightCorner;
					Instantiate (tileCornerUpRight, thisGO.transform.position + new Vector3 (0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
				}

				if (downNeighbour == null || downNeighbour.tileType != Constants.TileType.Border) {
					tileCornerDownLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.wallDownLeftCorner;
					Instantiate (tileCornerDownLeft, thisGO.transform.position + new Vector3 (0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
				}
			} else if ((thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) && downRightNeighbour != null && downRightNeighbour.tileType == thisTile.tileType) {
				if (rightNeighbour == null || (rightNeighbour.tileType != Constants.TileType.Border && rightNeighbour.tileType != Constants.TileType.Wall)) {
					tileCornerUpRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.groundUpRightCorner;
					Instantiate (tileCornerUpRight, thisGO.transform.position + new Vector3 (0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
				}

				if (downNeighbour == null || (downNeighbour.tileType != Constants.TileType.Border && downNeighbour.tileType != Constants.TileType.Wall)) {
					tileCornerDownLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.groundDownLeftCorner;
					Instantiate (tileCornerDownLeft, thisGO.transform.position + new Vector3 (0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
				}
			}

		}

		if ((leftNeighbour == null || leftNeighbour.tileType != thisTile.tileType) && (downNeighbour == null || downNeighbour.tileType != thisTile.tileType)) {
			var downLeftNeighbour = tiles.Find (item => (item.x == thisTile.x - 1) && (item.y == thisTile.y - 1));
			if (thisTile.tileType == Constants.TileType.Border && downLeftNeighbour != null && downLeftNeighbour.tileType == thisTile.tileType) {
				tileCornerUpLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.borderUpLeftCorner;
				Instantiate (tileCornerUpLeft, thisGO.transform.position + new Vector3 (-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);

				tileCornerDownRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.borderDownRightCorner;
				Instantiate (tileCornerDownRight, thisGO.transform.position + new Vector3 (-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
			} else if (thisTile.tileType == Constants.TileType.Wall && downLeftNeighbour != null && downLeftNeighbour.tileType == thisTile.tileType) {
				if (leftNeighbour == null || leftNeighbour.tileType != Constants.TileType.Border) {
					tileCornerUpLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.wallUpLeftCorner;
					Instantiate (tileCornerUpLeft, thisGO.transform.position + new Vector3 (-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
				}

				if (downNeighbour == null || downNeighbour.tileType != Constants.TileType.Border) {
					tileCornerDownRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.wallDownRightCorner;
					Instantiate (tileCornerDownRight, thisGO.transform.position + new Vector3 (-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
				}
			} else if ((thisTile.tileType == Constants.TileType.Ground || thisTile.tileType == Constants.TileType.Mineral) && downLeftNeighbour != null && downLeftNeighbour.tileType == thisTile.tileType) {
				if (leftNeighbour == null || (leftNeighbour.tileType != Constants.TileType.Border && leftNeighbour.tileType != Constants.TileType.Wall)) {
					tileCornerUpLeft.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.groundUpLeftCorner;
					Instantiate (tileCornerUpLeft, thisGO.transform.position + new Vector3 (-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
				}

				if (downNeighbour == null || (downNeighbour.tileType != Constants.TileType.Border && downNeighbour.tileType != Constants.TileType.Wall)) {
					tileCornerDownRight.GetComponent<SpriteRenderer> ().sprite = currentBiom.cornerSprites.groundDownRightCorner;
					Instantiate (tileCornerDownRight, thisGO.transform.position + new Vector3 (-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
				}
			}

		}


	}

	public void Build ()
	{
		GameObject GOTile = background;
		GOTile.GetComponent<SpriteRenderer> ().sprite = currentBiom.backgrounds [Random.Range (0, currentBiom.backgrounds.Length)];
		GameObject newObject = Instantiate (GOTile, new Vector3 (0f, 0f, 0f), Quaternion.identity);
		newObject.transform.SetParent (gameObject.transform);

		foreach (Tile tile in tiles) {
            
//            Debug.Log(tile.tileType + " " + tile.x + " " + tile.y);
         //   if (tile.tileType == Constants.TileType.Border) Debug.Log(true);
            GOTile = null;
			switch (tile.tileType) {
			case Constants.TileType.ActiveBomb:
				GOTile = activeBomb;
				GOTile.GetComponentInChildren<SpriteRenderer> ().sprite = currentBiom.activeBombs [Random.Range (0, currentBiom.activeBombs.Length)];
				break;
			case Constants.TileType.Ballon:
				GOTile = ballon;
				GOTile.GetComponentInChildren<SpriteRenderer> ().sprite = currentBiom.ballons [Random.Range (0, currentBiom.ballons.Length)];
				break;
			case Constants.TileType.Border:
				GOTile = border;
				GOTile.GetComponent<SpriteRenderer> ().sprite = currentBiom.borders [Random.Range (0, currentBiom.borders.Length)];
				break;
			case Constants.TileType.Crystal:
				GOTile = crystal;
				GOTile.GetComponentInChildren<SpriteRenderer> ().sprite = currentBiom.crystals [Random.Range (0, currentBiom.crystals.Length)];
				break;
			case Constants.TileType.DeactiveBomb:
				GOTile = deactiveBomb;
				GOTile.GetComponentInChildren<SpriteRenderer> ().sprite = currentBiom.deactiveBombs [Random.Range (0, currentBiom.deactiveBombs.Length)];
				break;
			case Constants.TileType.Enemy:
				GOTile = enemy;
				GOTile.GetComponentInChildren<SpriteRenderer> ().sprite = currentBiom.enemies [Random.Range (0, currentBiom.enemies.Length)];
				break;
			case Constants.TileType.Ground:
				GOTile = ground;
				GOTile.GetComponent<SpriteRenderer> ().sprite = currentBiom.grounds [Random.Range (0, currentBiom.grounds.Length)];
				break;
			case Constants.TileType.Mineral:
				GOTile = mineral;
                    GOTile.GetComponentInChildren<SpriteRenderer>().sprite = currentBiom.minerals[Random.Range(0, currentBiom.minerals.Length)];
                    GOTile.GetComponent<SpriteRenderer>().sprite = currentBiom.grounds[Random.Range(0, currentBiom.grounds.Length)];
                    //var SRs = GOTile.GetComponentInChildren<SpriteRenderer>();
                    //foreach(var SR in SRs)
                    //{
                    //    if (SR.gameObject.CompareTag("MineralSprite")) SR.sprite = currentBiom.minerals[Random.Range(0, currentBiom.minerals.Length)];
                    //    else SR.sprite = currentBiom.grounds[Random.Range(0, currentBiom.grounds.Length)];
                    //}
                    //Debug.Log(tile.value);
                    GOTile.GetComponent<GroundController>().SetValue(tile.value);

                        //.sprite = currentBiom.minerals [Random.Range (0, currentBiom.minerals.Length)];
                    break;
			case Constants.TileType.Player:
				GOTile = player;
				GOTile.GetComponentInChildren<SpriteRenderer> ().sprite = currentBiom.players [Random.Range (0, currentBiom.players.Length)];
				GOTile.GetComponent<PlayerController> ().SetNumCrystalsToExit (numOfCrystalsToExit);            
				break;
			case Constants.TileType.Portal:
				GOTile = portal;
				GOTile.GetComponent<SpriteRenderer> ().sprite = currentBiom.portals [Random.Range (0, currentBiom.portals.Length)];
				break;
			case Constants.TileType.Stone:
				GOTile = stone;
				GOTile.GetComponentInChildren<SpriteRenderer> ().sprite = currentBiom.stones [Random.Range (0, currentBiom.stones.Length)];
				break;
			case Constants.TileType.Wall:
				GOTile = wall;
				GOTile.GetComponent<SpriteRenderer> ().sprite = currentBiom.walls [Random.Range (0, currentBiom.walls.Length)];
				break;
            case Constants.TileType.Switcher:
                    GOTile = gravitySwitcher;
                    //GOTile.GetComponent<SpriteRenderer>().sprite = currentBiom.walls[Random.Range(0, currentBiom.walls.Length)];
                    break;


            }
			if (GOTile != null) {
				newObject = Instantiate (GOTile, new Vector3 (tile.x, tile.y, 0f), Quaternion.identity);
				newObject.transform.SetParent (gameObject.transform);
				//if (tile.tileType == Constants.TileType.Ground || tile.tileType == Constants.TileType.Border || tile.tileType == Constants.TileType.Wall || tile.tileType == Constants.TileType.Mineral)
				//	SetBordersAndAngles (tile, newObject);
			}
		}
	}

	private void Awake ()
	{

        ApplicationController.levelBuilder = this;
        randomLevelGenerator = ApplicationController.instance.randomLevelGenerator;


		BackgroundController bc = background.GetComponent<BackgroundController> ();
		bc.speed = ApplicationController.startingParams.background.speed;

		MovingObjectController ballonMOC = ballon.GetComponent<MovingObjectController> ();
		ballonMOC.canRoll = ApplicationController.startingParams.ballon.canRoll;
		ballonMOC.rotationSpeed = ApplicationController.startingParams.ballon.rotationSpeed;
		ballonMOC.moveTime = ApplicationController.startingParams.ballon.moveTime;
		ballonMOC.canKill = ApplicationController.startingParams.ballon.canKill;

		MovingObjectController BombMOC = activeBomb.GetComponent<MovingObjectController> ();
		BombMOC.canRoll = ApplicationController.startingParams.bomb.canRoll;
		BombMOC.rotationSpeed = ApplicationController.startingParams.bomb.rotationSpeed;
		BombMOC.moveTime = ApplicationController.startingParams.bomb.moveTime;
		BombMOC.canKill = ApplicationController.startingParams.bomb.canKill;
		BombController bombCtrl = activeBomb.GetComponent<BombController> ();
		bombCtrl.destroyDelayTime = ApplicationController.startingParams.bomb.destroyDelayTime;
		bombCtrl.explosionLengthTime = ApplicationController.startingParams.bomb.explosionLenghtTime;

        BombMOC = deactiveBomb.GetComponent<MovingObjectController>();
        BombMOC.canRoll = ApplicationController.startingParams.bomb.canRoll;
        BombMOC.rotationSpeed = ApplicationController.startingParams.bomb.rotationSpeed;
        BombMOC.moveTime = ApplicationController.startingParams.bomb.moveTime;
        BombMOC.canKill = ApplicationController.startingParams.bomb.canKill;
        bombCtrl = deactiveBomb.GetComponent<BombController>();
        bombCtrl.destroyDelayTime = ApplicationController.startingParams.bomb.destroyDelayTime;
        bombCtrl.explosionLengthTime = ApplicationController.startingParams.bomb.explosionLenghtTime;

        MovingObjectController crystalMOC = crystal.GetComponent<MovingObjectController> ();
		crystalMOC.canRoll = ApplicationController.startingParams.crystal.canRoll;
		crystalMOC.rotationSpeed = ApplicationController.startingParams.crystal.rotationSpeed;
		crystalMOC.moveTime = ApplicationController.startingParams.crystal.moveTime;
		crystalMOC.canKill = ApplicationController.startingParams.crystal.canKill;

		//MovingObjectController mineralMOC = mineral.GetComponent<MovingObjectController> ();
		//mineralMOC.canRoll = ApplicationController.startingParams.mineral.canRoll;
		//mineralMOC.rotationSpeed = ApplicationController.startingParams.mineral.rotationSpeed;
		//mineralMOC.moveTime = ApplicationController.startingParams.mineral.moveTime;
		//mineralMOC.canKill = ApplicationController.startingParams.mineral.canKill;

		PlayerController playerCtrl = player.GetComponent<PlayerController> ();
		playerCtrl.moveTime = ApplicationController.startingParams.player.moveTime;
		playerCtrl.secondsForBallon = ApplicationController.startingParams.player.secondsForBallon;
		playerCtrl.startingSecondsOfOxygen = ApplicationController.startingParams.player.startingSecondsOfOxygen;

		MovingObjectController stoneMOC = stone.GetComponent<MovingObjectController> ();
		stoneMOC.canRoll = ApplicationController.startingParams.stone.canRoll;
		stoneMOC.rotationSpeed = ApplicationController.startingParams.stone.rotationSpeed;
		//stoneMOC.sideMoveTime = ApplicationController.startingParams.stone.moveTime;
        stoneMOC.moveTime = ApplicationController.startingParams.stone.moveTime;
        stoneMOC.canKill = ApplicationController.startingParams.stone.canKill;

        
		if (ApplicationController.levelToLoad == ApplicationController.Level.selectedLevel)
			ReadFile ();
		else if (ApplicationController.levelToLoad == ApplicationController.Level.random)
			GenerateRandom ();

		Build ();
	}

}
