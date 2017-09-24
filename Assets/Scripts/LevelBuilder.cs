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
        public bool canKill;
    }
    public struct BombParams
    {
        public float moveTime;
        public bool canRoll;
        public float rotationSpeed;
        public float explosionLenghtTime;
        public float destroyDelayTime;
        public bool canKill;
    }
    public struct CrystalParams
    {
        public float moveTime;
        public bool canRoll;
        public float rotationSpeed;
        public bool canKill;
    }
    public struct MineralParams
    {
        public float moveTime;
        public bool canRoll;
        public float rotationSpeed;
        public bool canKill;
    }
    public struct PlayerParams
    {
        public float moveTime;
        public int secondsForBallon;
        public int startingSecondsOfOxygen;
    }
    public struct StoneParams
    {
        public float moveTime;
        public bool canRoll;
        public float rotationSpeed;
        public bool canKill;
    }
    /*
    public LevelGeneratorParams levelGenerator;
    public void SetLevelGeneratorValue()//string line)
    {
        var lineValue = new StringBuilder(255);

        //int length = 
        GetPrivateProfileString("LevelGenerator", "xMax", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out levelGenerator.xMax);
        //Debug.Log(length);
        //Debug.Log(lineValue.ToString());
        //Debug.Log(filePath);
        //Debug.Log(levelGenerator.xMax);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("LevelGenerator", "yMax", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out levelGenerator.yMax);
        //Debug.Log(lineValue.ToString());
        //Debug.Log(levelGenerator.yMax);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("LevelGenerator", "stoneMax", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out levelGenerator.stoneMax);
        //Debug.Log(lineValue.ToString());
        //Debug.Log(levelGenerator.stoneMax);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("LevelGenerator", "groundMax", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out levelGenerator.groundMax);
        // Debug.Log(lineValue.ToString());
        //Debug.Log(levelGenerator.groundMax);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("LevelGenerator", "aBombMax", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out levelGenerator.aBombMax);
        //Debug.Log(lineValue.ToString());
        //Debug.Log(levelGenerator.aBombMax);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("LevelGenerator", "dBombMax", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out levelGenerator.dBombMax);
        //Debug.Log(lineValue.ToString());
        //Debug.Log(levelGenerator.dBombMax);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("LevelGenerator", "crystalMax", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out levelGenerator.crystalMax);
        //Debug.Log(lineValue.ToString());
        //Debug.Log(levelGenerator.crystalMax);

    
        
    }
    */
    public BackgroundParams background;
    public void SetBackgroundValue()//string line)
    {
        var lineValue = new StringBuilder(255);

        //GetPrivateProfileString("Background", "depth", "", lineValue, lineValue.Capacity, filePath);
        //float.TryParse(lineValue.ToString(), out background.depth);

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

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Ballon", "canRoll", "", lineValue, lineValue.Capacity, filePath);
        bool.TryParse(lineValue.ToString(), out ballon.canRoll);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Ballon", "rotationSpeed", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out ballon.rotationSpeed);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Ballon", "canKill", "", lineValue, lineValue.Capacity, filePath);
        bool.TryParse(lineValue.ToString(), out ballon.canKill);

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

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Bomb", "canRoll", "", lineValue, lineValue.Capacity, filePath);
        bool.TryParse(lineValue.ToString(), out bomb.canRoll);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Bomb", "rotationSpeed", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out bomb.rotationSpeed);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Bomb", "explosionLenghtTime", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out bomb.explosionLenghtTime);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Bomb", "destroyDelayTime", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out bomb.destroyDelayTime);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Bomb", "canKill", "", lineValue, lineValue.Capacity, filePath);
        bool.TryParse(lineValue.ToString(), out bomb.canKill);

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

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Crystal", "canRoll", "", lineValue, lineValue.Capacity, filePath);
        bool.TryParse(lineValue.ToString(), out crystal.canRoll);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Crystal", "rotationSpeed", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out crystal.rotationSpeed);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Crystal", "canKill", "", lineValue, lineValue.Capacity, filePath);
        bool.TryParse(lineValue.ToString(), out crystal.canKill);

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

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Mineral", "canRoll", "", lineValue, lineValue.Capacity, filePath);
        bool.TryParse(lineValue.ToString(), out mineral.canRoll);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Mineral", "rotationSpeed", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out mineral.rotationSpeed);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Mineral", "canKill", "", lineValue, lineValue.Capacity, filePath);
        bool.TryParse(lineValue.ToString(), out mineral.canKill);

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

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Player", "secondsForBallon", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out player.secondsForBallon);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Player", "startingSecondsOfOxygen", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out player.startingSecondsOfOxygen);

        //if (line.Contains("moveTime")) float.TryParse(line.TrimStart("moveTime=".ToCharArray()), out player.moveTime);
        //if (line.Contains("secondsForOxygen")) float.TryParse(line.TrimStart("secondsForOxygen=".ToCharArray()), out player.secondsForOxygen);
    }

    public StoneParams stone;
    public void SetStoneValue()//string line)
    {
        var lineValue = new StringBuilder(255);

        GetPrivateProfileString("Stone", "moveTime", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out stone.moveTime);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Stone", "canRoll", "", lineValue, lineValue.Capacity, filePath);
        bool.TryParse(lineValue.ToString(), out stone.canRoll);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Stone", "rotationSpeed", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out stone.rotationSpeed);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Stone", "canKill", "", lineValue, lineValue.Capacity, filePath);
        bool.TryParse(lineValue.ToString(), out stone.canKill);

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
    public GameObject tileBorderLeft;
    public GameObject tileBorderRight;
    public GameObject tileBorderUp;
    public GameObject tileBorderDown;
    public GameObject tileCornerUpLeft;
    public GameObject tileCornerUpRight;
    public GameObject tileCornerDownLeft;
    public GameObject tileCornerDownRight;
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
            //param.SetLevelGeneratorValue();
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

    private void SetBordersAndAngles(Tile thisTile, GameObject thisGO)
    {

        //if (thisTile.tileType == Constants.TileType.Border)
        //{
        //    tileBorderLeft.GetComponent<SpriteRenderer>().sprite = borderBorderLeft;
        //    tileBorderRight.GetComponent<SpriteRenderer>().sprite = borderBorderRight;
        //    tileBorderUp.GetComponent<SpriteRenderer>().sprite = borderBorderUp;
        //    tileBorderDown.GetComponent<SpriteRenderer>().sprite = borderBorderDown;
        //    tileBorderLeftGR.GetComponent<SpriteRenderer>().sprite = borderBorderLeftGR;
        //    tileBorderRightGR.GetComponent<SpriteRenderer>().sprite = borderBorderRightGR;
        //    tileBorderUpGR.GetComponent<SpriteRenderer>().sprite = borderBorderUpGR;
        //    tileBorderDownGR.GetComponent<SpriteRenderer>().sprite = borderBorderDownGR;

        //    tileCornerUpLeft.GetComponent<SpriteRenderer>().sprite = borderUpLeftCorner;
        //    tileCornerUpRight.GetComponent<SpriteRenderer>().sprite = borderUpRightCorner;
        //    tileCornerDownLeft.GetComponent<SpriteRenderer>().sprite = borderDownLeftCorner;
        //    tileCornerDownRight.GetComponent<SpriteRenderer>().sprite = borderDownRightCorner;
        //}
        //else if (thisTile.tileType == Constants.TileType.Wall)
        //{
        //    tileBorderLeft.GetComponent<SpriteRenderer>().sprite = wallBorderLeft;
        //    tileBorderRight.GetComponent<SpriteRenderer>().sprite = wallBorderRight;
        //    tileBorderUp.GetComponent<SpriteRenderer>().sprite = wallBorderUp;
        //    tileBorderDown.GetComponent<SpriteRenderer>().sprite = wallBorderDown;
        //    tileBorderLeftGR.GetComponent<SpriteRenderer>().sprite = wallBorderLeftGR;
        //    tileBorderRightGR.GetComponent<SpriteRenderer>().sprite = wallBorderRightGR;
        //    tileBorderUpGR.GetComponent<SpriteRenderer>().sprite = wallBorderUpGR;
        //    tileBorderDownGR.GetComponent<SpriteRenderer>().sprite = wallBorderDownGR;

        //    tileCornerUpLeft.GetComponent<SpriteRenderer>().sprite = wallUpLeftCorner;
        //    tileCornerUpRight.GetComponent<SpriteRenderer>().sprite = wallUpRightCorner;
        //    tileCornerDownLeft.GetComponent<SpriteRenderer>().sprite = wallDownLeftCorner;
        //    tileCornerDownRight.GetComponent<SpriteRenderer>().sprite = wallDownRightCorner;
        //}
        //else if (thisTile.tileType == Constants.TileType.Ground)
        //{
        //    tileBorderLeft.GetComponent<SpriteRenderer>().sprite = groundBorderLeft;
        //    tileBorderRight.GetComponent<SpriteRenderer>().sprite = groundBorderRight;
        //    tileBorderUp.GetComponent<SpriteRenderer>().sprite = groundBorderUp;
        //    tileBorderDown.GetComponent<SpriteRenderer>().sprite = groundBorderDown;
        //    tileBorderLeftGR.GetComponent<SpriteRenderer>().sprite = groundBorderLeftGR;
        //    tileBorderRightGR.GetComponent<SpriteRenderer>().sprite = groundBorderRightGR;
        //    tileBorderUpGR.GetComponent<SpriteRenderer>().sprite = groundBorderUpGR;
        //    tileBorderDownGR.GetComponent<SpriteRenderer>().sprite = groundBorderDownGR;

        //    tileCornerUpLeft.GetComponent<SpriteRenderer>().sprite = groundUpLeftCorner;
        //    tileCornerUpRight.GetComponent<SpriteRenderer>().sprite = groundUpRightCorner;
        //    tileCornerDownLeft.GetComponent<SpriteRenderer>().sprite = groundDownLeftCorner;
        //    tileCornerDownRight.GetComponent<SpriteRenderer>().sprite = groundDownRightCorner;
        //}
        


        /////////////////////////////////////////////////////
        var leftNeighbour = tiles.Find(item =>  (item.x == thisTile.x - 1) && (item.y == thisTile.y ) );
        if (leftNeighbour == null)
        {
            if (thisTile.tileType == Constants.TileType.Border)
            {
                tileBorderLeft.GetComponent<SpriteRenderer>().sprite = borderBorderLeft;
            }
            else if (thisTile.tileType == Constants.TileType.Wall)
            {
                tileBorderLeft.GetComponent<SpriteRenderer>().sprite = wallBorderLeft;
            }
            else if (thisTile.tileType == Constants.TileType.Ground)
            {
                tileBorderLeft.GetComponent<SpriteRenderer>().sprite = groundBorderLeft;
            }
            Instantiate(tileBorderLeft, thisGO.transform.position + new Vector3(-0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
        }
        else if (leftNeighbour.tileType == Constants.TileType.Border)
        {

        }
        else if (leftNeighbour.tileType == Constants.TileType.Wall)
        {
            if (thisTile.tileType == Constants.TileType.Border)
            {
                tileBorderLeft.GetComponent<SpriteRenderer>().sprite = borderBorderLeftGR;
                Instantiate(tileBorderLeft, thisGO.transform.position + new Vector3(-0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
            }
        }
        else if (leftNeighbour.tileType == Constants.TileType.Ground)
        {
            if (thisTile.tileType == Constants.TileType.Wall || thisTile.tileType == Constants.TileType.Border)
            {
                if (thisTile.tileType == Constants.TileType.Border)
                {
                    tileBorderLeft.GetComponent<SpriteRenderer>().sprite = borderBorderLeftGR;
                }
                else if (thisTile.tileType == Constants.TileType.Wall)
                {
                    tileBorderLeft.GetComponent<SpriteRenderer>().sprite = wallBorderLeftGR;
                }
                Instantiate(tileBorderLeft, thisGO.transform.position + new Vector3(-0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
            }
        }
        else
        {
            if (thisTile.tileType == Constants.TileType.Border)
            {
                tileBorderLeft.GetComponent<SpriteRenderer>().sprite = borderBorderLeft;
            }
            else if (thisTile.tileType == Constants.TileType.Wall)
            {
                tileBorderLeft.GetComponent<SpriteRenderer>().sprite = wallBorderLeft;
            }
            else if (thisTile.tileType == Constants.TileType.Ground)
            {
                tileBorderLeft.GetComponent<SpriteRenderer>().sprite = groundBorderLeft;
            }
            Instantiate(tileBorderLeft, thisGO.transform.position + new Vector3(-0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
        }

        /////////////////////////////////////////////////////
        var rightNeighbour = tiles.Find(item => (item.x == thisTile.x + 1) && (item.y == thisTile.y));
        if (rightNeighbour == null)
        {
            if (thisTile.tileType == Constants.TileType.Border)
            {
                tileBorderRight.GetComponent<SpriteRenderer>().sprite = borderBorderRight;
            }
            else if (thisTile.tileType == Constants.TileType.Wall)
            {
                tileBorderRight.GetComponent<SpriteRenderer>().sprite = wallBorderRight;
            }
            else if (thisTile.tileType == Constants.TileType.Ground)
            {
                tileBorderRight.GetComponent<SpriteRenderer>().sprite = groundBorderRight;
            }
            Instantiate(tileBorderRight, thisGO.transform.position + new Vector3(0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
        }
        else if (rightNeighbour.tileType == Constants.TileType.Border)
        {

        }
        else if (rightNeighbour.tileType == Constants.TileType.Wall)
        {
            if (thisTile.tileType == Constants.TileType.Border)
            {
                tileBorderRight.GetComponent<SpriteRenderer>().sprite = borderBorderRightGR;
                Instantiate(tileBorderRight, thisGO.transform.position + new Vector3(0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
            }
        }
        else if (rightNeighbour.tileType == Constants.TileType.Ground)
        {
            if (thisTile.tileType == Constants.TileType.Wall || thisTile.tileType == Constants.TileType.Border)
            {
                if (thisTile.tileType == Constants.TileType.Border)
                {
                    tileBorderRight.GetComponent<SpriteRenderer>().sprite = borderBorderRightGR;
                }
                else if (thisTile.tileType == Constants.TileType.Wall)
                {
                    tileBorderRight.GetComponent<SpriteRenderer>().sprite = wallBorderRightGR;
                }
                Instantiate(tileBorderRight, thisGO.transform.position + new Vector3(0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
            }
        }
        else
        {
            if (thisTile.tileType == Constants.TileType.Border)
            {
                tileBorderRight.GetComponent<SpriteRenderer>().sprite = borderBorderRight;
            }
            else if (thisTile.tileType == Constants.TileType.Wall)
            {
                tileBorderRight.GetComponent<SpriteRenderer>().sprite = wallBorderRight;
            }
            else if (thisTile.tileType == Constants.TileType.Ground)
            {
                tileBorderRight.GetComponent<SpriteRenderer>().sprite = groundBorderRight;
            }
            Instantiate(tileBorderRight, thisGO.transform.position + new Vector3(0.5f, 0f, 0f), Quaternion.identity, thisGO.transform);
        }

        /////////////////////////////////////////////////////
        var upNeighbour = tiles.Find(item => (item.x == thisTile.x ) && (item.y == thisTile.y + 1));
        if (upNeighbour == null)
        {
            if (thisTile.tileType == Constants.TileType.Border)
            {
                tileBorderUp.GetComponent<SpriteRenderer>().sprite = borderBorderUp;
            }
            else if (thisTile.tileType == Constants.TileType.Wall)
            {
                tileBorderUp.GetComponent<SpriteRenderer>().sprite = wallBorderUp;
            }
            else if (thisTile.tileType == Constants.TileType.Ground)
            {
                tileBorderUp.GetComponent<SpriteRenderer>().sprite = groundBorderUp;
            }
            Instantiate(tileBorderUp, thisGO.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
        }
        else if (upNeighbour.tileType == Constants.TileType.Border)
        {

        }
        else if (upNeighbour.tileType == Constants.TileType.Wall)
        {
            if (thisTile.tileType == Constants.TileType.Border)
            {
                tileBorderUp.GetComponent<SpriteRenderer>().sprite = borderBorderUpGR;
                Instantiate(tileBorderUp, thisGO.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
            }
        }
        else if (upNeighbour.tileType == Constants.TileType.Ground)
        {
            if (thisTile.tileType == Constants.TileType.Wall || thisTile.tileType == Constants.TileType.Border)
            {
                if (thisTile.tileType == Constants.TileType.Border)
                {
                    tileBorderUp.GetComponent<SpriteRenderer>().sprite = borderBorderUpGR;
                }
                else if (thisTile.tileType == Constants.TileType.Wall)
                {
                    tileBorderUp.GetComponent<SpriteRenderer>().sprite = wallBorderUpGR;
                }
                Instantiate(tileBorderUp, thisGO.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
            }
        }
        else
        {
            if (thisTile.tileType == Constants.TileType.Border)
            {
                tileBorderUp.GetComponent<SpriteRenderer>().sprite = borderBorderUp;
            }
            else if (thisTile.tileType == Constants.TileType.Wall)
            {
                tileBorderUp.GetComponent<SpriteRenderer>().sprite = wallBorderUp;
            }
            else if (thisTile.tileType == Constants.TileType.Ground)
            {
                tileBorderUp.GetComponent<SpriteRenderer>().sprite = groundBorderUp;
            }
            Instantiate(tileBorderUp, thisGO.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
        }

        /////////////////////////////////////////////////////
        var downNeighbour = tiles.Find(item => (item.x == thisTile.x) && (item.y == thisTile.y - 1));
        if (downNeighbour == null)
        {
            if (thisTile.tileType == Constants.TileType.Border)
            {
                tileBorderDown.GetComponent<SpriteRenderer>().sprite = borderBorderDown;
            }
            else if (thisTile.tileType == Constants.TileType.Wall)
            {
                tileBorderDown.GetComponent<SpriteRenderer>().sprite = wallBorderDown;
            }
            else if (thisTile.tileType == Constants.TileType.Ground)
            {
                tileBorderDown.GetComponent<SpriteRenderer>().sprite = groundBorderDown;
            }
            Instantiate(tileBorderDown, thisGO.transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
        }
        else if (downNeighbour.tileType == Constants.TileType.Border)
        {

        }
        else if (downNeighbour.tileType == Constants.TileType.Wall)
        {
            if (thisTile.tileType == Constants.TileType.Border)
            {
                tileBorderDown.GetComponent<SpriteRenderer>().sprite = borderBorderDownGR;
                Instantiate(tileBorderDown, thisGO.transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
            }
        }
        else if (downNeighbour.tileType == Constants.TileType.Ground)
        {
            if (thisTile.tileType == Constants.TileType.Wall || thisTile.tileType == Constants.TileType.Border)
            {
                if (thisTile.tileType == Constants.TileType.Border)
                {
                    tileBorderDown.GetComponent<SpriteRenderer>().sprite = borderBorderDownGR;
                }
                else if (thisTile.tileType == Constants.TileType.Wall)
                {
                    tileBorderDown.GetComponent<SpriteRenderer>().sprite = wallBorderDownGR;
                }
                Instantiate(tileBorderDown, thisGO.transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
            }
        }
        else
        {
            if (thisTile.tileType == Constants.TileType.Border)
            {
                tileBorderDown.GetComponent<SpriteRenderer>().sprite = borderBorderDown;
            }
            else if (thisTile.tileType == Constants.TileType.Wall)
            {
                tileBorderDown.GetComponent<SpriteRenderer>().sprite = wallBorderDown;
            }
            else if (thisTile.tileType == Constants.TileType.Ground)
            {
                tileBorderDown.GetComponent<SpriteRenderer>().sprite = groundBorderDown;
            }
            Instantiate(tileBorderDown, thisGO.transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
        }

        /*********************************************************************************/

        if (thisTile.tileType == Constants.TileType.Border)
        {
            tileCornerUpLeft.GetComponent<SpriteRenderer>().sprite = borderUpLeftCorner;
        }
        else if (thisTile.tileType == Constants.TileType.Wall)
        {
            tileCornerUpLeft.GetComponent<SpriteRenderer>().sprite = wallUpLeftCorner;
        }
        else if (thisTile.tileType == Constants.TileType.Ground)
        {
            tileCornerUpLeft.GetComponent<SpriteRenderer>().sprite = groundUpLeftCorner;
        }
        if (leftNeighbour != null && upNeighbour != null && leftNeighbour.tileType == thisTile.tileType && upNeighbour.tileType == thisTile.tileType)
        {
            var upLeftNeighbour = tiles.Find(item => (item.x == thisTile.x - 1) && (item.y == thisTile.y + 1));
            if (thisTile.tileType == Constants.TileType.Border && (upLeftNeighbour == null || upLeftNeighbour.tileType != Constants.TileType.Border))
                Instantiate(tileCornerUpLeft, thisGO.transform.position + new Vector3(-0.5f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
            else if (thisTile.tileType == Constants.TileType.Wall && (upLeftNeighbour == null || (upLeftNeighbour.tileType != Constants.TileType.Border && upLeftNeighbour.tileType != Constants.TileType.Wall)))
                Instantiate(tileCornerUpLeft, thisGO.transform.position + new Vector3(-0.5f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
            else if (thisTile.tileType == Constants.TileType.Ground && (upLeftNeighbour == null || (upLeftNeighbour.tileType != Constants.TileType.Border && upLeftNeighbour.tileType != Constants.TileType.Wall && upLeftNeighbour.tileType != Constants.TileType.Ground)))
                Instantiate(tileCornerUpLeft, thisGO.transform.position + new Vector3(-0.5f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
        }

        /////////////////////////////////////////////////////////////
        if (thisTile.tileType == Constants.TileType.Border)
        {
            tileCornerUpRight.GetComponent<SpriteRenderer>().sprite = borderUpRightCorner;
        }
        else if (thisTile.tileType == Constants.TileType.Wall)
        {
            tileCornerUpRight.GetComponent<SpriteRenderer>().sprite = wallUpRightCorner;
        }
        else if (thisTile.tileType == Constants.TileType.Ground)
        {
            tileCornerUpRight.GetComponent<SpriteRenderer>().sprite = groundUpRightCorner;
        }
        if (rightNeighbour != null && upNeighbour != null && rightNeighbour.tileType == thisTile.tileType && upNeighbour.tileType == thisTile.tileType)
        {
            var upRightNeighbour = tiles.Find(item => (item.x == thisTile.x + 1) && (item.y == thisTile.y + 1));
            if (thisTile.tileType == Constants.TileType.Border && (upRightNeighbour == null || upRightNeighbour.tileType != Constants.TileType.Border))
                Instantiate(tileCornerUpRight, thisGO.transform.position + new Vector3(0.5f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
            else if (thisTile.tileType == Constants.TileType.Wall && (upRightNeighbour == null || (upRightNeighbour.tileType != Constants.TileType.Border && upRightNeighbour.tileType != Constants.TileType.Wall)))
                Instantiate(tileCornerUpRight, thisGO.transform.position + new Vector3(0.5f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
            else if (thisTile.tileType == Constants.TileType.Ground && (upRightNeighbour == null || (upRightNeighbour.tileType != Constants.TileType.Border && upRightNeighbour.tileType != Constants.TileType.Wall && upRightNeighbour.tileType != Constants.TileType.Ground)))
                Instantiate(tileCornerUpRight, thisGO.transform.position + new Vector3(0.5f, 0.5f, 0f), Quaternion.identity, thisGO.transform);
        }

        /////////////////////////////////////////////////////////////
        if (thisTile.tileType == Constants.TileType.Border)
        {
            tileCornerDownLeft.GetComponent<SpriteRenderer>().sprite = borderDownLeftCorner;
        }
        else if (thisTile.tileType == Constants.TileType.Wall)
        {
            tileCornerDownLeft.GetComponent<SpriteRenderer>().sprite = wallDownLeftCorner;
        }
        else if (thisTile.tileType == Constants.TileType.Ground)
        {
            tileCornerDownLeft.GetComponent<SpriteRenderer>().sprite = groundDownLeftCorner;
        }
        if (leftNeighbour != null && downNeighbour != null && leftNeighbour.tileType == thisTile.tileType && downNeighbour.tileType == thisTile.tileType)
        {
            var downLeftNeighbour = tiles.Find(item => (item.x == thisTile.x - 1) && (item.y == thisTile.y - 1));
            if (thisTile.tileType == Constants.TileType.Border && (downLeftNeighbour == null || downLeftNeighbour.tileType != Constants.TileType.Border))
                Instantiate(tileCornerDownLeft, thisGO.transform.position + new Vector3(-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
            else if (thisTile.tileType == Constants.TileType.Wall && (downLeftNeighbour == null || (downLeftNeighbour.tileType != Constants.TileType.Border && downLeftNeighbour.tileType != Constants.TileType.Wall)))
                Instantiate(tileCornerDownLeft, thisGO.transform.position + new Vector3(-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
            else if (thisTile.tileType == Constants.TileType.Ground && (downLeftNeighbour == null || (downLeftNeighbour.tileType != Constants.TileType.Border && downLeftNeighbour.tileType != Constants.TileType.Wall && downLeftNeighbour.tileType != Constants.TileType.Ground)))
                Instantiate(tileCornerDownLeft, thisGO.transform.position + new Vector3(-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
        }

        /////////////////////////////////////////////////////////////
        if (thisTile.tileType == Constants.TileType.Border)
        {
            tileCornerDownRight.GetComponent<SpriteRenderer>().sprite = borderDownRightCorner;
        }
        else if (thisTile.tileType == Constants.TileType.Wall)
        {
            tileCornerDownRight.GetComponent<SpriteRenderer>().sprite = wallDownRightCorner;
        }
        else if (thisTile.tileType == Constants.TileType.Ground)
        {
            tileCornerDownRight.GetComponent<SpriteRenderer>().sprite = groundDownRightCorner;
        }
        if (rightNeighbour != null && downNeighbour != null && rightNeighbour.tileType == thisTile.tileType && downNeighbour.tileType == thisTile.tileType)
        {
            var downRightNeighbour = tiles.Find(item => (item.x == thisTile.x + 1) && (item.y == thisTile.y - 1));
            if (thisTile.tileType == Constants.TileType.Border && (downRightNeighbour == null || downRightNeighbour.tileType != Constants.TileType.Border))
                Instantiate(tileCornerDownRight, thisGO.transform.position + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
            else if (thisTile.tileType == Constants.TileType.Wall && (downRightNeighbour == null || (downRightNeighbour.tileType != Constants.TileType.Border && downRightNeighbour.tileType != Constants.TileType.Wall)))
                Instantiate(tileCornerDownRight, thisGO.transform.position + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
            else if (thisTile.tileType == Constants.TileType.Ground && (downRightNeighbour == null || (downRightNeighbour.tileType != Constants.TileType.Border && downRightNeighbour.tileType != Constants.TileType.Wall && downRightNeighbour.tileType != Constants.TileType.Ground)))
                Instantiate(tileCornerDownRight, thisGO.transform.position + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if ((rightNeighbour == null || rightNeighbour.tileType != thisTile.tileType) && (downNeighbour == null || downNeighbour.tileType != thisTile.tileType))
        {
            var downRightNeighbour = tiles.Find(item => (item.x == thisTile.x + 1) && (item.y == thisTile.y - 1));
            if (thisTile.tileType == Constants.TileType.Border && downRightNeighbour != null && downRightNeighbour.tileType == thisTile.tileType)
            {
                tileCornerUpRight.GetComponent<SpriteRenderer>().sprite = borderUpRightCorner;
                Instantiate(tileCornerUpRight, thisGO.transform.position + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);

                tileCornerDownLeft.GetComponent<SpriteRenderer>().sprite = borderDownLeftCorner;
                Instantiate(tileCornerDownLeft, thisGO.transform.position + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
            }
            else if (thisTile.tileType == Constants.TileType.Wall && downRightNeighbour != null && downRightNeighbour.tileType == thisTile.tileType)
            {
                if (rightNeighbour == null || rightNeighbour.tileType != Constants.TileType.Border)
                {
                    tileCornerUpRight.GetComponent<SpriteRenderer>().sprite = wallUpRightCorner;
                    Instantiate(tileCornerUpRight, thisGO.transform.position + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
                }

                if (downNeighbour == null || downNeighbour.tileType != Constants.TileType.Border)
                {
                    tileCornerDownLeft.GetComponent<SpriteRenderer>().sprite = wallDownLeftCorner;
                    Instantiate(tileCornerDownLeft, thisGO.transform.position + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
                }
            }
            else if (thisTile.tileType == Constants.TileType.Ground && downRightNeighbour != null && downRightNeighbour.tileType == thisTile.tileType)
            {
                if (rightNeighbour == null || (rightNeighbour.tileType != Constants.TileType.Border && rightNeighbour.tileType != Constants.TileType.Wall))
                {
                    tileCornerUpRight.GetComponent<SpriteRenderer>().sprite = groundUpRightCorner;
                    Instantiate(tileCornerUpRight, thisGO.transform.position + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
                }

                if (downNeighbour == null || (downNeighbour.tileType != Constants.TileType.Border && downNeighbour.tileType != Constants.TileType.Wall))
                {
                    tileCornerDownLeft.GetComponent<SpriteRenderer>().sprite = groundDownLeftCorner;
                    Instantiate(tileCornerDownLeft, thisGO.transform.position + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
                }
            }

        }

        if ((leftNeighbour == null || leftNeighbour.tileType != thisTile.tileType) && (downNeighbour == null || downNeighbour.tileType != thisTile.tileType))
        {
            var downLeftNeighbour = tiles.Find(item => (item.x == thisTile.x - 1) && (item.y == thisTile.y - 1));
            if (thisTile.tileType == Constants.TileType.Border && downLeftNeighbour != null && downLeftNeighbour.tileType == thisTile.tileType)
            {
                tileCornerUpLeft.GetComponent<SpriteRenderer>().sprite = borderUpLeftCorner;
                Instantiate(tileCornerUpLeft, thisGO.transform.position + new Vector3(-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);

                tileCornerDownRight.GetComponent<SpriteRenderer>().sprite = borderDownRightCorner;
                Instantiate(tileCornerDownRight, thisGO.transform.position + new Vector3(-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
            }
            else if (thisTile.tileType == Constants.TileType.Wall && downLeftNeighbour != null && downLeftNeighbour.tileType == thisTile.tileType)
            {
                if (leftNeighbour == null || leftNeighbour.tileType != Constants.TileType.Border)
                {
                    tileCornerUpLeft.GetComponent<SpriteRenderer>().sprite = wallUpLeftCorner;
                    Instantiate(tileCornerUpLeft, thisGO.transform.position + new Vector3(-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
                }

                if (downNeighbour == null || downNeighbour.tileType != Constants.TileType.Border)
                {
                    tileCornerDownRight.GetComponent<SpriteRenderer>().sprite = wallDownRightCorner;
                    Instantiate(tileCornerDownRight, thisGO.transform.position + new Vector3(-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
                }
            }
            else if (thisTile.tileType == Constants.TileType.Ground && downLeftNeighbour != null && downLeftNeighbour.tileType == thisTile.tileType)
            {
                if (leftNeighbour == null || (leftNeighbour.tileType != Constants.TileType.Border && leftNeighbour.tileType != Constants.TileType.Wall))
                {
                    tileCornerUpLeft.GetComponent<SpriteRenderer>().sprite = groundUpLeftCorner;
                    Instantiate(tileCornerUpLeft, thisGO.transform.position + new Vector3(-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
                }

                if (downNeighbour == null || (downNeighbour.tileType != Constants.TileType.Border && downNeighbour.tileType != Constants.TileType.Wall))
                {
                    tileCornerDownRight.GetComponent<SpriteRenderer>().sprite = groundDownRightCorner;
                    Instantiate(tileCornerDownRight, thisGO.transform.position + new Vector3(-0.5f, -0.5f, 0f), Quaternion.identity, thisGO.transform);
                }
            }

        }


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
                    //GOTile.GetComponent<BombController>().IsActive = true;
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
                if (tile.tileType == Constants.TileType.Ground || tile.tileType == Constants.TileType.Border || tile.tileType == Constants.TileType.Wall)
                    SetBordersAndAngles(tile, newObject);
            }
        }
    }
    private void Awake()
    {
        
        
        startingParams = ReadIni();
        if (startingParams !=null)
        {
            /*
            xMax = startingParams.levelGenerator.xMax;
            yMax = startingParams.levelGenerator.yMax;
            maxDeactiveBomb = startingParams.levelGenerator.dBombMax;
            maxActiveBomb = startingParams.levelGenerator.aBombMax;
            maxCrystal = startingParams.levelGenerator.crystalMax;
            maxGround = startingParams.levelGenerator.groundMax;
            maxStones = startingParams.levelGenerator.stoneMax;
            BackgroundController bc = background.GetComponent<BackgroundController>();
            Debug.Log(bc);
            */

            /*
            xMax = startingParams.levelGenerator.xMax;
            yMax = startingParams.levelGenerator.yMax;
            maxDeactiveBomb = startingParams.levelGenerator.dBombMax;
            maxActiveBomb = startingParams.levelGenerator.aBombMax;
            maxCrystal = startingParams.levelGenerator.crystalMax;
            maxGround = startingParams.levelGenerator.groundMax;
            maxStones = startingParams.levelGenerator.stoneMax;
            */

            BackgroundController bc = background.GetComponent<BackgroundController>();
            //bc.depth = startingParams.background.depth;
            bc.speed = startingParams.background.speed;

            MovingObjectController ballonMOC = ballon.GetComponent<MovingObjectController>();
            ballonMOC.canRoll = startingParams.ballon.canRoll;
            ballonMOC.rotationSpeed = startingParams.ballon.rotationSpeed;
            ballonMOC.sideMoveTime = startingParams.ballon.moveTime;
            ballonMOC.canKill = startingParams.ballon.canKill;

            MovingObjectController BombMOC = activeBomb.GetComponent<MovingObjectController>();
            BombMOC.canRoll = startingParams.bomb.canRoll;
            BombMOC.rotationSpeed = startingParams.bomb.rotationSpeed;
            BombMOC.sideMoveTime = startingParams.bomb.moveTime;
            BombMOC.canKill = startingParams.bomb.canKill;
            BombController bombCtrl = activeBomb.GetComponent<BombController>();
            bombCtrl.destroyDelayTime = startingParams.bomb.destroyDelayTime;
            bombCtrl.explosionLengthTime = startingParams.bomb.explosionLenghtTime;
            //Debug.Log(bombCtrl.explosionLengthTime);
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
            crystalMOC.canKill = startingParams.crystal.canKill;

            MovingObjectController mineralMOC = mineral.GetComponent<MovingObjectController>();
            mineralMOC.canRoll = startingParams.mineral.canRoll;
            mineralMOC.rotationSpeed = startingParams.mineral.rotationSpeed;
            mineralMOC.sideMoveTime = startingParams.mineral.moveTime;
            mineralMOC.canKill = startingParams.mineral.canKill;

            PlayerController playerCtrl = player.GetComponent<PlayerController>();
            playerCtrl.moveTime = startingParams.player.moveTime;
            playerCtrl.secondsForBallon = startingParams.player.secondsForBallon;
            playerCtrl.startingSecondsOfOxygen = startingParams.player.startingSecondsOfOxygen;

            MovingObjectController stoneMOC = stone.GetComponent<MovingObjectController>();
            stoneMOC.canRoll = startingParams.stone.canRoll;
            stoneMOC.rotationSpeed = startingParams.stone.rotationSpeed;
            stoneMOC.sideMoveTime = startingParams.stone.moveTime;
            stoneMOC.canKill = startingParams.stone.canKill;

        }
        if (ApplicationController.levelToLoad == ApplicationController.Level.level1) ReadFile();
        else if (ApplicationController.levelToLoad == ApplicationController.Level.random) GenerateRandom();
 //       if (ReadFile() == null) GenerateRandom();
        Build();
    }

}
