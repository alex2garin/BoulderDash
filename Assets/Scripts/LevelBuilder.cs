using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Runtime.InteropServices;


public class Tile
{
    public int x;
    public int y;
    public char tileType;

    public Tile(int x, int y, char tileType)
    {
        this.x = x;
        this.y = y;
        this.tileType = tileType;
    }
}

public static class Constants
{
    public struct TileType
    {
        public const char Border = 'X';
        public const char Wall = 'W';
        public const char Ground = 'G';
        public const char Portal = 'O';
        public const char Stone = 'S';
        public const char ActiveBomb = 'A';
        public const char DeactiveBomb = 'D';
        public const char Crystal = 'K';
        public const char Mineral = 'M';
        public const char Enemy = 'E';
        public const char Player = 'P';
        public const char Ballon = 'B';
    }
}

public class StartingParameters
{
    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder value, int size, string filePath);

    public string filePath = Path.Combine(Application.dataPath, "config.ini");

    public struct LevelGeneratorParams
    {
        public int xMax;
        public int yMax;
        public int stoneMax;
        public int groundMax;
        public int aBombMax;
        public int dBombMax;
        public int crystalMax;
    }
    public struct BackgroundParams
    {
        public float depth;
        public float speed;
    }
    public struct BallonParams
    {
        public float moveTime;
        public bool canRoll;
        public float rotationSpeed;
    }
    public struct BombParams
    {
        public float moveTime;
        public bool canRoll;
        public float rotationSpeed;
        public float explosionLenghtTime;
        public float destroyDelayTime;
    }
    public struct CrystalParams
    {
        public float moveTime;
        public bool canRoll;
        public float rotationSpeed;
    }
    public struct MineralParams
    {
        public float moveTime;
        public bool canRoll;
        public float rotationSpeed;
    }
    public struct PlayerParams
    {
        public float moveTime;
        public float secondsForOxygen;
    }
    public struct StoneParams
    {
        public float moveTime;
        public bool canRoll;
        public float rotationSpeed;
    }

    public LevelGeneratorParams levelGenerator;
    public void SetLevelGeneratorValue()//string line)
    {
        var lineValue = new StringBuilder(255);

        int length = GetPrivateProfileString("LevelGenerator", "xMax", "", lineValue, lineValue.Capacity, filePath);
        Debug.Log(length);
        Debug.Log(lineValue.ToString());
        Debug.Log(filePath);
        int.TryParse(lineValue.ToString(), out levelGenerator.xMax);

        GetPrivateProfileString("LevelGenerator", "yMax", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out levelGenerator.yMax);

        GetPrivateProfileString("LevelGenerator", "stoneMax", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out levelGenerator.stoneMax);

        GetPrivateProfileString("LevelGenerator", "groundMax", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out levelGenerator.groundMax);

        GetPrivateProfileString("LevelGenerator", "aBombMax", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out levelGenerator.aBombMax);

        GetPrivateProfileString("LevelGenerator", "dBombMax", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out levelGenerator.dBombMax);

        GetPrivateProfileString("LevelGenerator", "crystalMax", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out levelGenerator.crystalMax);

        /*
        if (line.Contains("xMax")) int.TryParse(line.TrimStart("xMax=".ToCharArray()), out levelGenerator.xMax);
        if (line.Contains("yMax")) int.TryParse(line.TrimStart("yMax=".ToCharArray()), out levelGenerator.yMax);
        if (line.Contains("stoneMax")) int.TryParse(line.TrimStart("stoneMax=".ToCharArray()), out levelGenerator.stoneMax);
        if (line.Contains("groundMax")) int.TryParse(line.TrimStart("groundMax=".ToCharArray()), out levelGenerator.groundMax);
        if (line.Contains("aBombMax")) int.TryParse(line.TrimStart("aBombMax=".ToCharArray()), out levelGenerator.aBombMax);
        if (line.Contains("dBombMax")) int.TryParse(line.TrimStart("dBombMax=".ToCharArray()), out levelGenerator.dBombMax);
        if (line.Contains("crystalMax")) int.TryParse(line.TrimStart("crystalMax=".ToCharArray()), out levelGenerator.crystalMax);
        */
    }

    public BackgroundParams background;
    public void SetBackgroundValue()//string line)
    {
        var lineValue = new StringBuilder(255);

        GetPrivateProfileString("Background", "depth", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out background.depth);

        GetPrivateProfileString("Background", "speed", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out background.speed);
        
        //if (line.Contains("depth")) float.TryParse(line.TrimStart("depth=".ToCharArray()), out background.depth);
        //if (line.Contains("speed")) float.TryParse(line.TrimStart("speed=".ToCharArray()), out background.speed);
    }

    public BallonParams ballon;
    public void SetBallonValue()//string line)
    {
        var lineValue = new StringBuilder(255);

        GetPrivateProfileString("Ballon", "moveTime", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out ballon.moveTime);

        GetPrivateProfileString("Ballon", "canRoll", "", lineValue, lineValue.Capacity, filePath);
        bool.TryParse(lineValue.ToString(), out ballon.canRoll);

        GetPrivateProfileString("Ballon", "rotationSpeed", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out ballon.rotationSpeed);

        //if (line.Contains("moveTime")) float.TryParse(line.TrimStart("moveTime=".ToCharArray()), out ballon.moveTime);
        //if (line.Contains("canRoll")) bool.TryParse(line.TrimStart("canRoll=".ToCharArray()), out ballon.canRoll);
        //if (line.Contains("rotationSpeed")) float.TryParse(line.TrimStart("rotationSpeed=".ToCharArray()), out ballon.rotationSpeed);
    }

    public BombParams bomb;
    public void SetBombValue()//string line)
    {
        var lineValue = new StringBuilder(255);

        GetPrivateProfileString("Bomb", "moveTime", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out bomb.moveTime);

        GetPrivateProfileString("Bomb", "canRoll", "", lineValue, lineValue.Capacity, filePath);
        bool.TryParse(lineValue.ToString(), out bomb.canRoll);

        GetPrivateProfileString("Bomb", "rotationSpeed", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out bomb.rotationSpeed);

        GetPrivateProfileString("Bomb", "explosionLenghtTime", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out bomb.explosionLenghtTime);

        GetPrivateProfileString("Bomb", "destroyDelayTime", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out bomb.destroyDelayTime);

        //if (line.Contains("moveTime")) float.TryParse(line.TrimStart("moveTime=".ToCharArray()), out bomb.moveTime);
        //if (line.Contains("canRoll")) bool.TryParse(line.TrimStart("canRoll=".ToCharArray()), out bomb.canRoll);
        //if (line.Contains("rotationSpeed")) float.TryParse(line.TrimStart("rotationSpeed=".ToCharArray()), out bomb.rotationSpeed);
        //if (line.Contains("explosionLenghtTime")) float.TryParse(line.TrimStart("explosionLenghtTime=".ToCharArray()), out bomb.explosionLenghtTime);
        //if (line.Contains("destroyDelayTime")) float.TryParse(line.TrimStart("destroyDelayTime=".ToCharArray()), out bomb.destroyDelayTime);
    }

    public CrystalParams crystal;
    public void SetCrystalValue()//string line)
    {
        var lineValue = new StringBuilder(255);

        GetPrivateProfileString("Crystal", "moveTime", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out crystal.moveTime);

        GetPrivateProfileString("Crystal", "canRoll", "", lineValue, lineValue.Capacity, filePath);
        bool.TryParse(lineValue.ToString(), out crystal.canRoll);

        GetPrivateProfileString("Crystal", "rotationSpeed", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out crystal.rotationSpeed);

        //if (line.Contains("moveTime")) float.TryParse(line.TrimStart("moveTime=".ToCharArray()), out crystal.moveTime);
        //if (line.Contains("canRoll")) bool.TryParse(line.TrimStart("canRoll=".ToCharArray()), out crystal.canRoll);
        //if (line.Contains("rotationSpeed")) float.TryParse(line.TrimStart("rotationSpeed=".ToCharArray()), out crystal.rotationSpeed);
    }

    public MineralParams mineral;
    public void SetMineralValue()//string line)
    {
        var lineValue = new StringBuilder(255);

        GetPrivateProfileString("Mineral", "moveTime", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out mineral.moveTime);

        GetPrivateProfileString("Mineral", "canRoll", "", lineValue, lineValue.Capacity, filePath);
        bool.TryParse(lineValue.ToString(), out mineral.canRoll);

        GetPrivateProfileString("Mineral", "rotationSpeed", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out mineral.rotationSpeed);

        //if (line.Contains("moveTime")) float.TryParse(line.TrimStart("moveTime=".ToCharArray()), out mineral.moveTime);
        //if (line.Contains("canRoll")) bool.TryParse(line.TrimStart("canRoll=".ToCharArray()), out mineral.canRoll);
        //if (line.Contains("rotationSpeed")) float.TryParse(line.TrimStart("rotationSpeed=".ToCharArray()), out mineral.rotationSpeed);
    }

    public PlayerParams player;
    public void SetPlayerValue()//string line)
    {
        var lineValue = new StringBuilder(255);

        GetPrivateProfileString("Player", "moveTime", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out player.moveTime);

        GetPrivateProfileString("Player", "secondsForOxygen", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out player.secondsForOxygen);

        //if (line.Contains("moveTime")) float.TryParse(line.TrimStart("moveTime=".ToCharArray()), out player.moveTime);
        //if (line.Contains("secondsForOxygen")) float.TryParse(line.TrimStart("secondsForOxygen=".ToCharArray()), out player.secondsForOxygen);
    }

    public StoneParams stone;
    public void SetStoneValue()//string line)
    {
        var lineValue = new StringBuilder(255);

        GetPrivateProfileString("Stone", "moveTime", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out stone.moveTime);

        GetPrivateProfileString("Stone", "canRoll", "", lineValue, lineValue.Capacity, filePath);
        bool.TryParse(lineValue.ToString(), out stone.canRoll);

        GetPrivateProfileString("Stone", "rotationSpeed", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out stone.rotationSpeed);

        //if (line.Contains("moveTime")) float.TryParse(line.TrimStart("moveTime=".ToCharArray()), out stone.moveTime);
        //if (line.Contains("canRoll")) bool.TryParse(line.TrimStart("canRoll=".ToCharArray()), out stone.canRoll);
        //if (line.Contains("rotationSpeed")) float.TryParse(line.TrimStart("rotationSpeed=".ToCharArray()), out stone.rotationSpeed);
    }
}



public class LevelBuilder : MonoBehaviour {
    //tiles;
    public Sprite[] borders;
    public Sprite[] walls;
    public Sprite[] grounds;
    public Sprite[] portals;
    //moving objects
    public Sprite[] stones;
    public Sprite[] activeBombs;
    public Sprite[] deactiveBombs;
    public Sprite[] crystals;
    public Sprite[] minerals;
    public Sprite[] ballons;
    //enemies
    public Sprite[] enemies;
    //player
    public Sprite[] players;

    public Sprite[] backgrounds;

    //tiles;
    public GameObject border;
    public GameObject wall;
    public GameObject ground;
    public GameObject portal;
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

    public TextAsset fileLevel;
    //public TextAsset ini;

    public int xMax = 7;
    public int yMax = 7;
    public int maxStones = 3;
    public int maxGround = 7;
    public int maxActiveBomb = 1;
    public int maxDeactiveBomb = 2;
    public int maxCrystal = 3;
    

    private List<Tile> tiles;
    public StartingParameters startingParams;

    private StartingParameters ReadIni()
    {

        /*if (ini == null) return null;
        string[] lines = ini.text.Split('\n');
        string objectType = null;
        StartingParameters param = new StartingParameters();

        for (int i=0;i<lines.Length;i++)
        {
            if (lines[i] == "LevelGenerator" || lines[i] == "Background" || lines[i] == "Ballon" || lines[i] == "Bomb" || lines[i] == "Crystal" || lines[i] == "Mineral" || lines[i] == "Player" || lines[i] == "Stone") objectType = lines[i];
            else if (objectType == "LevelGenerator") param.SetLevelGeneratorValue(lines[i]);
            else if (objectType == "Background") param.SetBackgroundValue(lines[i]);
            else if (objectType == "Ballon") param.SetBallonValue(lines[i]);
            else if (objectType == "Bomb") param.SetBombValue(lines[i]);
            else if (objectType == "Crystal") param.SetCrystalValue(lines[i]);
            else if (objectType == "Mineral") param.SetMineralValue(lines[i]);
            else if (objectType == "Player") param.SetPlayerValue(lines[i]);
            else if (objectType == "Stone") param.SetStoneValue(lines[i]);
        }
        return param;*/

     
            StartingParameters param = new StartingParameters();
            param.SetLevelGeneratorValue();
            param.SetBackgroundValue();
            param.SetBallonValue();
            param.SetBombValue();
            param.SetCrystalValue();
            param.SetMineralValue();
            param.SetPlayerValue();
            param.SetStoneValue();

        //Debug.Log(param);
        return param;


    }

    public List<Tile> ReadFile()
    {
        List<Tile> newTiles;
        if (fileLevel == null) return null;

        string[] lines = fileLevel.text.Split('\n');
        
        int x = 0;
        int y = 0;

        newTiles = new List<Tile>();

        foreach (var line in lines)
        {
            var symbols = line.Split(';',',');
            foreach(var symbol in symbols)
            {
                if (symbol!="")
                newTiles.Add(new Tile(x, -y, symbol.ToCharArray()[0]));
                    
                x++;
            }
            y++;
            x = 0;
        }

        tiles = newTiles;
        return newTiles;
    }

    public List<Tile> GenerateRandom()
    {

        List<Tile> freeTiles = new List<Tile>();
        List<Tile> newTiles = new List<Tile>();
        Tile newTile;
        Tile player;

        for (int x = 1; x <= xMax; x++)
        {
            for (int y = 1; y <= yMax; y++)
            {
                if (x == 1 || x == xMax || y == 1 || y == yMax)
                {
                    newTile = new Tile(x, y, Constants.TileType.Border);
                    newTiles.Add(newTile);
                }
                else freeTiles.Add(new Tile(x, y, ' '));
            }
        }

        
        int index = Random.Range(0, freeTiles.Count);
        player = freeTiles[index];
        player.tileType = Constants.TileType.Player;
        newTiles.Add(player);
        freeTiles.RemoveAt(index);
        if (player.y != yMax - 1)
        {

            newTile = freeTiles.Find(tile => tile.x == player.x && tile.y == player.y+1);
            
            freeTiles.Remove(newTile);

            newTile.tileType = Constants.TileType.Ground;
            newTiles.Add(newTile);
            maxGround--;
        }
        
        while(maxStones>0)
        {
            if (freeTiles.Count == 0) break; 
            index = Random.Range(0, freeTiles.Count-1);
            newTile = freeTiles[index];
            newTile.tileType = Constants.TileType.Stone;
            newTiles.Add(newTile);
            freeTiles.RemoveAt(index);
            maxStones--;
        }

        while (maxGround > 0)
        {

            if (freeTiles.Count == 0) break;
            index = Random.Range(0, freeTiles.Count-1);
            newTile = freeTiles[index];
            newTile.tileType = Constants.TileType.Ground;
            newTiles.Add(newTile);
            freeTiles.RemoveAt(index);
            maxGround--;
        }

        while( maxCrystal > 0)
        {
            if (freeTiles.Count == 0) break;
            index = Random.Range(0, freeTiles.Count - 1);
            newTile = freeTiles[index];
            newTile.tileType = Constants.TileType.Crystal;
            newTiles.Add(newTile);
            freeTiles.RemoveAt(index);
            maxCrystal--;
        }

        while (maxActiveBomb > 0)
        {
            if (freeTiles.Count == 0) break;
            index = Random.Range(0, freeTiles.Count - 1);
            newTile = freeTiles[index];
            newTile.tileType = Constants.TileType.ActiveBomb;
            newTiles.Add(newTile);
            freeTiles.RemoveAt(index);
            maxActiveBomb--;
        }

        while (maxDeactiveBomb > 0)
        {
            if (freeTiles.Count == 0) break;
            index = Random.Range(0, freeTiles.Count - 1);
            newTile = freeTiles[index];
            newTile.tileType = Constants.TileType.DeactiveBomb;
            newTiles.Add(newTile);
            freeTiles.RemoveAt(index);
            maxDeactiveBomb--;
        }

        tiles = newTiles;
        return newTiles;

    }
    
    public void Build()
    {
        GameObject GOTile = background;
        GOTile.GetComponent<SpriteRenderer>().sprite = backgrounds[Random.Range(0, backgrounds.Length)];
        GameObject newObject = Instantiate(GOTile, new Vector3(0f, 0f, 0f), Quaternion.identity);
        newObject.transform.SetParent(gameObject.transform);

        foreach (Tile tile in tiles)
        {
            GOTile = null;
            switch (tile.tileType)
            {
                case Constants.TileType.ActiveBomb:
                    GOTile = activeBomb;
                    GOTile.GetComponent<SpriteRenderer>().sprite = activeBombs[Random.Range(0, activeBombs.Length)];
                    //GOTile.GetComponent<BombController>().SetActive(true);
                    break;
                case Constants.TileType.Ballon:
                    GOTile = ballon;
                    GOTile.GetComponent<SpriteRenderer>().sprite = ballons[Random.Range(0, ballons.Length)];
                    break;
                case Constants.TileType.Border:
                    GOTile = border;
                    GOTile.GetComponent<SpriteRenderer>().sprite = borders[Random.Range(0, borders.Length)];
                    break;
                case Constants.TileType.Crystal:
                    GOTile = crystal;
                    GOTile.GetComponent<SpriteRenderer>().sprite = crystals[Random.Range(0, crystals.Length)];
                    break;
                case Constants.TileType.DeactiveBomb:
                    GOTile = deactiveBomb;
                    GOTile.GetComponent<SpriteRenderer>().sprite = deactiveBombs[Random.Range(0, deactiveBombs.Length)];
                    //GOTile.GetComponent<BombController>().SetActive(false);
                    break;
                case Constants.TileType.Enemy:
                    GOTile = enemy;
                    GOTile.GetComponent<SpriteRenderer>().sprite = enemies[Random.Range(0, enemies.Length)];
                    break;
                case Constants.TileType.Ground:
                    GOTile = ground;
                    GOTile.GetComponent<SpriteRenderer>().sprite = grounds[Random.Range(0, grounds.Length)];
                    break;
                case Constants.TileType.Mineral:
                    GOTile = mineral;
                    GOTile.GetComponent<SpriteRenderer>().sprite = minerals[Random.Range(0, minerals.Length)];
                    break;
                case Constants.TileType.Player:
                    GOTile = player;
                    GOTile.GetComponent<SpriteRenderer>().sprite = players[Random.Range(0, players.Length)];
                    break;
                case Constants.TileType.Portal:
                    GOTile = portal;
                    GOTile.GetComponent<SpriteRenderer>().sprite = portals[Random.Range(0, portals.Length)];
                    break;
                case Constants.TileType.Stone:
                    GOTile = stone;
                    GOTile.GetComponentInChildren<SpriteRenderer>().sprite = stones[Random.Range(0, stones.Length)];
                    //GOTile.GetComponent<SpriteRenderer>().sprite = stones[Random.Range(0, stones.Length)];
                    break;
                case Constants.TileType.Wall:
                    GOTile = wall;
                    GOTile.GetComponent<SpriteRenderer>().sprite = walls[Random.Range(0, walls.Length)];
                    break;
       
                
            }
            if (GOTile != null)
            {
                newObject = Instantiate(GOTile, new Vector3(tile.x, tile.y, 0f), Quaternion.identity);
                newObject.transform.SetParent(gameObject.transform);
            }
        }
    }
    private void Awake()
    {
        
        
        startingParams = ReadIni();
        if (startingParams !=null)
        {/*
            
            xMax = startingParams.levelGenerator.xMax;
            yMax = startingParams.levelGenerator.yMax;
            maxDeactiveBomb = startingParams.levelGenerator.dBombMax;
            maxActiveBomb = startingParams.levelGenerator.aBombMax;
            maxCrystal = startingParams.levelGenerator.crystalMax;
            maxGround = startingParams.levelGenerator.groundMax;
            maxStones = startingParams.levelGenerator.stoneMax;
            BackgroundController bc = background.GetComponent<BackgroundController>();
            Debug.Log(bc);*/

            xMax = startingParams.levelGenerator.xMax;
            yMax = startingParams.levelGenerator.yMax;
            maxDeactiveBomb = startingParams.levelGenerator.dBombMax;
            maxActiveBomb = startingParams.levelGenerator.aBombMax;
            maxCrystal = startingParams.levelGenerator.crystalMax;
            maxGround = startingParams.levelGenerator.groundMax;
            maxStones = startingParams.levelGenerator.stoneMax;

            BackgroundController bc = background.GetComponent<BackgroundController>();
            bc.depth = startingParams.background.depth;
            bc.speed = startingParams.background.speed;

            MovingObjectController ballonMOC = ballon.GetComponent<MovingObjectController>();
            ballonMOC.canRoll = startingParams.ballon.canRoll;
            ballonMOC.rotationSpeed = startingParams.ballon.rotationSpeed;
            ballonMOC.sideMoveTime = startingParams.ballon.moveTime;

            MovingObjectController BombMOC = activeBomb.GetComponent<MovingObjectController>();
            BombMOC.canRoll = startingParams.bomb.canRoll;
            BombMOC.rotationSpeed = startingParams.bomb.rotationSpeed;
            BombMOC.sideMoveTime = startingParams.bomb.moveTime;
            BombController bombCtrl = activeBomb.GetComponent<BombController>();
            bombCtrl.destroyDelayTime = startingParams.bomb.destroyDelayTime;
            bombCtrl.explosionLengthTime = startingParams.bomb.explosionLenghtTime;
            /*
			BombMOC = deactiveBomb.GetComponent<MovingObjectController> ();
			BombMOC.canRoll = startingParams.bomb.canRoll;
			BombMOC.rotationSpeed = startingParams.bomb.rotationSpeed;
			BombMOC.sideMoveTime = startingParams.bomb.moveTime;
*/
            MovingObjectController crystalMOC = crystal.GetComponent<MovingObjectController>();
            crystalMOC.canRoll = startingParams.crystal.canRoll;
            crystalMOC.rotationSpeed = startingParams.crystal.rotationSpeed;
            crystalMOC.sideMoveTime = startingParams.crystal.moveTime;

            MovingObjectController mineralMOC = mineral.GetComponent<MovingObjectController>();
            mineralMOC.canRoll = startingParams.mineral.canRoll;
            mineralMOC.rotationSpeed = startingParams.mineral.rotationSpeed;
            mineralMOC.sideMoveTime = startingParams.mineral.moveTime;

            PlayerController playerCtrl = player.GetComponent<PlayerController>();
            playerCtrl.moveTime = startingParams.player.moveTime;
            playerCtrl.secondsForOxygenBallon = startingParams.player.secondsForOxygen;

            MovingObjectController stoneMOC = stone.GetComponent<MovingObjectController>();
            stoneMOC.canRoll = startingParams.stone.canRoll;
            stoneMOC.rotationSpeed = startingParams.stone.rotationSpeed;
            stoneMOC.sideMoveTime = startingParams.stone.moveTime;

        }
        if (ReadFile() == null) GenerateRandom();
        Build();
    }

}
