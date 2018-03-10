using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InputController
{
//	public enum ActionType { moveVertical, moveHorizontal, plantBomb, actionWithoutMoving };
	public float VerticalMovement( )
	{
		return Input.GetAxisRaw ("Vertical");
	}
	public float HorizontalMovement( )
	{
		return Input.GetAxisRaw ("Horizontal");
	}
	public bool PlantBomb( )
	{
		return Input.GetKey (KeyCode.Space);
	}
	public bool ActionWithoutMoving()
	{
		return Input.GetKey (KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftShift);
	}
}

class IniFileReader
{
    Dictionary<string, Dictionary<string,string>> iniParams;
    string filePath;

    public bool ReadFile(string filePath)
	{
		if (!File.Exists(filePath)) return false;

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


public struct StartingParameters
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
    public BallonParams ballon;
    public BombParams bomb;
    public CrystalParams crystal;
    public MineralParams mineral;
    public PlayerParams player;
    public StoneParams stone;

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

	public static StartingParameters startingParams;

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
        inputCTRL = new InputController();
		ReadIniFile (iniFilePath);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    void ReadIniFile(string filePath)
    {
        IniFileReader iniFR = new IniFileReader();
        if (iniFR.ReadFile(Path.Combine(Application.dataPath, filePath)))
        {
            string value = null;
            iniFR.GetParameter("Background", "speed", out value);
			float.TryParse(value, out startingParams.background.speed);

//			value = null;
//			iniFR.GetParameter("Background", "depth", out value);
//			float.TryParse(value, out startingParams.background.depth);
            //
//            GetPrivateProfileString("Ballon", "moveTime", "", lineValue, lineValue.Capacity, filePath);
//            float.TryParse(lineValue.ToString(), out ballon.moveTime);

			value = null;
			iniFR.GetParameter("Ballon", "moveTime", out value);
			float.TryParse(value, out startingParams.ballon.moveTime);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Ballon", "canRoll", "", lineValue, lineValue.Capacity, filePath);
//            bool.TryParse(lineValue.ToString(), out ballon.canRoll);

			value = null;
			iniFR.GetParameter("Ballon", "canRoll", out value);
			bool.TryParse(value, out startingParams.ballon.canRoll);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Ballon", "rotationSpeed", "", lineValue, lineValue.Capacity, filePath);
//            float.TryParse(lineValue.ToString(), out ballon.rotationSpeed);

			value = null;
			iniFR.GetParameter("Ballon", "rotationSpeed", out value);
			float.TryParse(value, out startingParams.ballon.rotationSpeed);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Ballon", "canKill", "", lineValue, lineValue.Capacity, filePath);
//            bool.TryParse(lineValue.ToString(), out ballon.canKill);

			value = null;
			iniFR.GetParameter("Ballon", "canKill", out value);
			bool.TryParse(value, out startingParams.ballon.canKill);
//
//            GetPrivateProfileString("Bomb", "moveTime", "", lineValue, lineValue.Capacity, filePath);
			//            float.TryParse(lineValue.ToString(), out bomb.moveTime);

			value = null;
			iniFR.GetParameter("Bomb", "moveTime", out value);
			float.TryParse(value, out startingParams.bomb.moveTime);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Bomb", "canRoll", "", lineValue, lineValue.Capacity, filePath);
			//            bool.TryParse(lineValue.ToString(), out bomb.canRoll);

			value = null;
			iniFR.GetParameter("Bomb", "canRoll", out value);
			bool.TryParse(value, out startingParams.bomb.canRoll);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Bomb", "rotationSpeed", "", lineValue, lineValue.Capacity, filePath);
			//            float.TryParse(lineValue.ToString(), out bomb.rotationSpeed);

			value = null;
			iniFR.GetParameter("Bomb", "rotationSpeed", out value);
			float.TryParse(value, out startingParams.bomb.rotationSpeed);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Bomb", "explosionLenghtTime", "", lineValue, lineValue.Capacity, filePath);
			//            float.TryParse(lineValue.ToString(), out bomb.explosionLenghtTime);

			value = null;
			iniFR.GetParameter("Bomb", "explosionLenghtTime", out value);
			float.TryParse(value, out startingParams.bomb.explosionLenghtTime);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Bomb", "destroyDelayTime", "", lineValue, lineValue.Capacity, filePath);
			//            float.TryParse(lineValue.ToString(), out bomb.destroyDelayTime);

			value = null;
			iniFR.GetParameter("Bomb", "destroyDelayTime", out value);
			float.TryParse(value, out startingParams.bomb.destroyDelayTime);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Bomb", "canKill", "", lineValue, lineValue.Capacity, filePath);
			//            bool.TryParse(lineValue.ToString(), out bomb.canKill);

			value = null;
			iniFR.GetParameter("Bomb", "canKill", out value);
			bool.TryParse(value, out startingParams.bomb.canKill);
//
//            GetPrivateProfileString("Crystal", "moveTime", "", lineValue, lineValue.Capacity, filePath);
			//            float.TryParse(lineValue.ToString(), out crystal.moveTime);

			value = null;
			iniFR.GetParameter("Crystal", "moveTime", out value);
			float.TryParse(value, out startingParams.crystal.moveTime);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Crystal", "canRoll", "", lineValue, lineValue.Capacity, filePath);
			//            bool.TryParse(lineValue.ToString(), out crystal.canRoll);

			value = null;
			iniFR.GetParameter("Crystal", "canRoll", out value);
			bool.TryParse(value, out startingParams.crystal.canRoll);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Crystal", "rotationSpeed", "", lineValue, lineValue.Capacity, filePath);
			//            float.TryParse(lineValue.ToString(), out crystal.rotationSpeed);

			value = null;
			iniFR.GetParameter("Crystal", "rotationSpeed", out value);
			float.TryParse(value, out startingParams.crystal.rotationSpeed);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Crystal", "canKill", "", lineValue, lineValue.Capacity, filePath);
			//            bool.TryParse(lineValue.ToString(), out crystal.canKill);

			value = null;
			iniFR.GetParameter("Crystal", "canKill", out value);
			bool.TryParse(value, out startingParams.crystal.canKill);
//
//            GetPrivateProfileString("Mineral", "moveTime", "", lineValue, lineValue.Capacity, filePath);
			//            float.TryParse(lineValue.ToString(), out mineral.moveTime);

			value = null;
			iniFR.GetParameter("Mineral", "moveTime", out value);
			float.TryParse(value, out startingParams.mineral.moveTime);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Mineral", "canRoll", "", lineValue, lineValue.Capacity, filePath);
			//            bool.TryParse(lineValue.ToString(), out mineral.canRoll);

			value = null;
			iniFR.GetParameter("Mineral", "canRoll", out value);
			bool.TryParse(value, out startingParams.mineral.canRoll);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Mineral", "rotationSpeed", "", lineValue, lineValue.Capacity, filePath);
			//            float.TryParse(lineValue.ToString(), out mineral.rotationSpeed);

			value = null;
			iniFR.GetParameter("Mineral", "rotationSpeed", out value);
			float.TryParse(value, out startingParams.mineral.rotationSpeed);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Mineral", "canKill", "", lineValue, lineValue.Capacity, filePath);
			//            bool.TryParse(lineValue.ToString(), out mineral.canKill);

			value = null;
			iniFR.GetParameter("Mineral", "canKill", out value);
			bool.TryParse(value, out startingParams.mineral.canKill);
//
//            GetPrivateProfileString("Player", "moveTime", "", lineValue, lineValue.Capacity, filePath);
			//            float.TryParse(lineValue.ToString(), out player.moveTime);

			value = null;
			iniFR.GetParameter("Player", "moveTime", out value);
			float.TryParse(value, out startingParams.player.moveTime);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Player", "secondsForBallon", "", lineValue, lineValue.Capacity, filePath);
			//            int.TryParse(lineValue.ToString(), out player.secondsForBallon);

			value = null;
			iniFR.GetParameter("Player", "secondsForBallon", out value);
			int.TryParse(value, out startingParams.player.secondsForBallon);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Player", "startingSecondsOfOxygen", "", lineValue, lineValue.Capacity, filePath);
			//            int.TryParse(lineValue.ToString(), out player.startingSecondsOfOxygen);

			value = null;
			iniFR.GetParameter("Player", "startingSecondsOfOxygen", out value);
			int.TryParse(value, out startingParams.player.startingSecondsOfOxygen);
//
//            GetPrivateProfileString("Stone", "moveTime", "", lineValue, lineValue.Capacity, filePath);
			//            float.TryParse(lineValue.ToString(), out stone.moveTime);

			value = null;
			iniFR.GetParameter("Stone", "moveTime", out value);
			float.TryParse(value, out startingParams.stone.moveTime);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Stone", "canRoll", "", lineValue, lineValue.Capacity, filePath);
			//            bool.TryParse(lineValue.ToString(), out stone.canRoll);

			value = null;
			iniFR.GetParameter("Stone", "canRoll", out value);
			bool.TryParse(value, out startingParams.stone.canRoll);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Stone", "rotationSpeed", "", lineValue, lineValue.Capacity, filePath);
			//            float.TryParse(lineValue.ToString(), out stone.rotationSpeed);

			value = null;
			iniFR.GetParameter("Stone", "rotationSpeed", out value);
			float.TryParse(value, out startingParams.stone.rotationSpeed);
//
//            lineValue = new StringBuilder(255);
//            GetPrivateProfileString("Stone", "canKill", "", lineValue, lineValue.Capacity, filePath);
			//            bool.TryParse(lineValue.ToString(), out stone.canKill);

			value = null;
			iniFR.GetParameter("Stone", "canKill", out value);
			bool.TryParse(value, out startingParams.stone.canKill);
        }
    }
}
