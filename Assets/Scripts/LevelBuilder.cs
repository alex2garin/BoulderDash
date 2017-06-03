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
    public GameObject[] borders;
    public GameObject[] walls;
    public GameObject[] grounds;
    public GameObject[] portals;
    //moving objects
    public GameObject[] stones;
    public GameObject[] activeBombs;
    public GameObject[] deactiveBombs;
    public GameObject[] crystals;
    public GameObject[] minerals;
    public GameObject[] ballons;
    //enemies
    public GameObject[] enemies;

    public GameObject[] players;


    private List<Tile> tiles;
    
    public List<Tile> ReadFile()
    {
        List<Tile> newTiles = new List<Tile>();
        string[] fileEntries = Directory.GetFiles("", "*.xls");

        if (fileEntries == null) return newTiles;

        string[] lines = File.ReadAllLines(fileEntries[0], Encoding.UTF8);

        int x = 0;
        int y = 0;

        foreach(var line in lines)
        {
            var symbols = line.Split(',');
            foreach(var symbol in symbols)
            {
                newTiles.Add(new Tile(x, -y, symbol.ToCharArray()[0]));
                    
                y++;
            }
            x++;
        }


        return newTiles;
    }

    public List<Tile> GenerateTestTileList()
    {
        List<Tile> testList = new List<Tile>();
        /*
        XXXXXX
        XESMAX
        XWWWXX
        X   KX
        XPDGOX
        XXXXXX
        */
        

        testList.Add(new Tile(0, 0, Constants.TileType.Border));
        testList.Add(new Tile(1, 0, Constants.TileType.Border));
        testList.Add(new Tile(2, 0, Constants.TileType.Border));
        testList.Add(new Tile(3, 0, Constants.TileType.Border));
        testList.Add(new Tile(4, 0, Constants.TileType.Border));
        testList.Add(new Tile(5, 0, Constants.TileType.Border));

        testList.Add(new Tile(0, 1, Constants.TileType.Border));
        testList.Add(new Tile(2, 2, Constants.TileType.Player));
        /*
        testList.Add(new Tile(2, 1, Constants.TileType.DeactiveBomb));
        testList.Add(new Tile(3, 1, Constants.TileType.Ground));
        testList.Add(new Tile(4, 1, Constants.TileType.Portal));*/
        testList.Add(new Tile(5, 1, Constants.TileType.Border));

        testList.Add(new Tile(0, 2, Constants.TileType.Border));
        //        testList.Add(new Tile(1, 2, player));
        //        testList.Add(new Tile(2, 2, deactiveBomb));
        //        testList.Add(new Tile(3, 2, ground));
      //  testList.Add(new Tile(4, 2, Constants.TileType.Crystal));
        testList.Add(new Tile(5, 2, Constants.TileType.Border));

        testList.Add(new Tile(0, 3, Constants.TileType.Border));
    //    testList.Add(new Tile(1, 3, Constants.TileType.Wall));
    //    testList.Add(new Tile(2, 3, Constants.TileType.Wall));
    //    testList.Add(new Tile(3, 3, Constants.TileType.Wall));
    //    testList.Add(new Tile(4, 3, Constants.TileType.Border));
        testList.Add(new Tile(5, 3, Constants.TileType.Border));

        testList.Add(new Tile(0, 4, Constants.TileType.Border));
    //    testList.Add(new Tile(1, 4, Constants.TileType.Enemy));
    //    testList.Add(new Tile(2, 4, Constants.TileType.Stone));
    //    testList.Add(new Tile(3, 4, Constants.TileType.Mineral));
     //   testList.Add(new Tile(4, 4, Constants.TileType.ActiveBomb));
        testList.Add(new Tile(5, 4, Constants.TileType.Border));

        testList.Add(new Tile(0, 5, Constants.TileType.Border));
        testList.Add(new Tile(1, 5, Constants.TileType.Border));
        testList.Add(new Tile(2, 5, Constants.TileType.Border));
        testList.Add(new Tile(3, 5, Constants.TileType.Border));
        testList.Add(new Tile(4, 5, Constants.TileType.Border));
        testList.Add(new Tile(5, 5, Constants.TileType.Border));

        tiles = testList;
        return testList;
    }

    public void Build()
    {
        GameObject GOTile = grounds[0];
        foreach(Tile tile in tiles)
        {
            switch(tile.tileType)
            {
                case Constants.TileType.ActiveBomb:
                    GOTile = activeBombs[Random.Range(0, activeBombs.Length)];
                    break;
                case Constants.TileType.Border:
                    GOTile = borders[Random.Range(0, borders.Length)];
                    break;
                case Constants.TileType.Crystal:
                    GOTile = crystals[Random.Range(0, crystals.Length)];
                    break;
                case Constants.TileType.DeactiveBomb:
                    GOTile = deactiveBombs[Random.Range(0, deactiveBombs.Length)];
                    break;
                case Constants.TileType.Enemy:
                    GOTile = enemies[Random.Range(0, enemies.Length)];
                    break;
                case Constants.TileType.Ground:
                    GOTile = grounds[Random.Range(0, grounds.Length)];
                    break;
                case Constants.TileType.Mineral:
                    GOTile = minerals[Random.Range(0, minerals.Length)];
                    break;
                case Constants.TileType.Player:
                    GOTile = players[Random.Range(0, players.Length)];
                    break;
                case Constants.TileType.Portal:
                    GOTile = portals[Random.Range(0, portals.Length)];
                    break;
                case Constants.TileType.Stone:
                    GOTile = stones[Random.Range(0, stones.Length)];
                    break;
                case Constants.TileType.Wall:
                    GOTile = walls[Random.Range(0, walls.Length)];
                    break;
            }

            Instantiate(GOTile, new Vector3(tile.x, tile.y, 0f), Quaternion.identity);

        }
    }
    private void Start()
    {
        GenerateTestTileList();
        Build();
    }

}
