using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InputController
{

}

class IniFileReader
{
    Dictionary<string, Dictionary<string,string>> iniParams;
    string filePath;

    public bool ReadFile(string filePath)
    {
        if (File.Exists(filePath)) return false;

        this.filePath = filePath;
        var lines = File.ReadAllLines(this.filePath);

        iniParams = new Dictionary<string, Dictionary<string, string>>();
        Dictionary<string, string> dictionary = null;

        foreach (var line in lines)
        {

            if (line.Contains("[") && line.Contains("]"))
            {
                dictionary = new Dictionary<string, string>();
                iniParams.Add(line.Trim('[', ']'), dictionary);
            }
            else
            {
                var pair = line.Split('=');
                if (pair.Length == 2 && dictionary != null)
                {
                    dictionary.Add(pair[0], pair[1]);
                }
            }
        }
        return true;
    }
    public bool GetParameter(string partition, string parameter, out string value)
    {
        value = null;
        if (iniParams == null) return false;

        Dictionary<string, string> dictionary;
        if (iniParams.TryGetValue(partition, out dictionary))
        {
            return dictionary.TryGetValue(parameter, out value);
        }
        return false;
    }
}


public class StartingParameters
{
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

    public BackgroundParams background;
    public void SetBackgroundValue()//string line)
    {
        var lineValue = new StringBuilder(255);

        GetPrivateProfileString("Background", "speed", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out background.speed);
        //
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

        GetPrivateProfileString("Player", "moveTime", "", lineValue, lineValue.Capacity, filePath);
        float.TryParse(lineValue.ToString(), out player.moveTime);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Player", "secondsForBallon", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out player.secondsForBallon);

        lineValue = new StringBuilder(255);
        GetPrivateProfileString("Player", "startingSecondsOfOxygen", "", lineValue, lineValue.Capacity, filePath);
        int.TryParse(lineValue.ToString(), out player.startingSecondsOfOxygen);

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

    }
}


public class ApplicationController : MonoBehaviour
{

    public static InputController inputCTRL;
    public enum Level { random, level1 };
    public static Vector3 gravity = Vector2.down;
    public static Vector3 Left
    {
        get
        {
            return new Vector3(gravity.y, -gravity.x);
        }
    }
    public static Vector3 UpLeft
    {
        get
        {
            return new Vector3(-gravity.x + gravity.y, -gravity.x - gravity.y);
        }
    }
    public static Vector3 DownLeft
    {
        get
        {
            return new Vector3(gravity.x + gravity.y, -gravity.x + gravity.y);
        }
    }
    public static Vector3 Right
    {
        get
        {
            return new Vector3(-gravity.y, gravity.x);
        }
    }
    public static Vector3 UpRight
    {
        get
        {
            return new Vector3(-gravity.x - gravity.y, gravity.x - gravity.y);
        }
    }
    public static Vector3 DownRight
    {
        get
        {
            return new Vector3(gravity.x - gravity.y, gravity.x + gravity.y);
        }
    }

    public static Level levelToLoad = Level.random;

    private static int bombExplosionSortValue = 0;
    public static int BombExplosionSortValue
    {
        get { return bombExplosionSortValue++; }
    }
    public string iniFilePath = "config.ini";

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
        inputCTRL = new InputController();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ReadIniFile(string filePath)
    {
        IniFileReader iniFR = new IniFileReader();
        if (iniFR.ReadFile(Path.Combine(Application.dataPath, filePath)))
        {
            string value = null;
            iniFR.GetParameter("Background", "speed", out value);
            float.TryParse(value, out background.speed);
            //
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

            GetPrivateProfileString("Player", "moveTime", "", lineValue, lineValue.Capacity, filePath);
            float.TryParse(lineValue.ToString(), out player.moveTime);

            lineValue = new StringBuilder(255);
            GetPrivateProfileString("Player", "secondsForBallon", "", lineValue, lineValue.Capacity, filePath);
            int.TryParse(lineValue.ToString(), out player.secondsForBallon);

            lineValue = new StringBuilder(255);
            GetPrivateProfileString("Player", "startingSecondsOfOxygen", "", lineValue, lineValue.Capacity, filePath);
            int.TryParse(lineValue.ToString(), out player.startingSecondsOfOxygen);

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
        }
    }
}
