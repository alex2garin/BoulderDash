using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


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

    public TextAsset fileLevel;

    public int xMax = 7;
    public int yMax = 7;
    public int maxStones = 3;
    public int maxGround = 7;
    public int maxActiveBomb = 1;
    public int maxDeactiveBomb = 2;
    public int maxCrystal = 3;
    

    private List<Tile> tiles;
    
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
        GameObject GOTile = ground;
        foreach(Tile tile in tiles)
        {
            switch(tile.tileType)
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
          //  if (GOTile == player) playerGO = Instantiate(GOTile, new Vector3(tile.x, tile.y, 0f), Quaternion.identity);
         //   else    
            Instantiate(GOTile, new Vector3(tile.x, tile.y, 0f), Quaternion.identity);

        }
    }
    private void Start()
    {
        if (ReadFile() == null) GenerateRandom();//GenerateTestTileList();
        Build();
    }

}
