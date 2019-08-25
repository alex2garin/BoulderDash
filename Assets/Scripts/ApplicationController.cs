using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class InputController
{
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
    //public struct MineralParams
    //{
    //    public float moveTime;
    //    public bool canRoll;
    //    public float rotationSpeed;
    //    public bool canKill;
    //}
    public struct PlayerParams
    {
        public float moveTime;
        public int secondsForBallon;
        public int startingSecondsOfOxygen;
        public int deathDelay;
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
    //public MineralParams mineral;
    public PlayerParams player;
    public StoneParams stone;

}


public class ApplicationController : MonoBehaviour
{
    public static ApplicationController instance;

    public static LevelBuilder levelBuilder;
    public static PlayerController playerController;
    public static TextAsset[] fileLevels;
    public TextAsset[] FileLevels;
    public static TextAsset SelectedFile;

    public static InputController inputCTRL;
    public enum Level { random, selectedLevel };
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
    public RandomLevelGenerator randomLevelGenerator;


    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
        inputCTRL = new InputController();
		ReadIniFile (iniFilePath);
        fileLevels = FileLevels;
        instance = this;
    }


    void ReadIniFile(string filePath)
    {
        IniFileReader iniFR = new IniFileReader();
        if (iniFR.ReadFile(Path.Combine(Application.dataPath, filePath)))
        {
            string value = null;
            iniFR.GetParameter("Background", "speed", out value);
			float.TryParse(value, out startingParams.background.speed);

			value = null;
			iniFR.GetParameter("Ballon", "moveTime", out value);
			float.TryParse(value, out startingParams.ballon.moveTime);

			value = null;
			iniFR.GetParameter("Ballon", "canRoll", out value);
			bool.TryParse(value, out startingParams.ballon.canRoll);

			value = null;
			iniFR.GetParameter("Ballon", "rotationSpeed", out value);
			float.TryParse(value, out startingParams.ballon.rotationSpeed);

			value = null;
			iniFR.GetParameter("Ballon", "canKill", out value);
			bool.TryParse(value, out startingParams.ballon.canKill);

			value = null;
			iniFR.GetParameter("Bomb", "moveTime", out value);
			float.TryParse(value, out startingParams.bomb.moveTime);

			value = null;
			iniFR.GetParameter("Bomb", "canRoll", out value);
			bool.TryParse(value, out startingParams.bomb.canRoll);

			value = null;
			iniFR.GetParameter("Bomb", "rotationSpeed", out value);
			float.TryParse(value, out startingParams.bomb.rotationSpeed);

			value = null;
			iniFR.GetParameter("Bomb", "explosionLenghtTime", out value);
			float.TryParse(value, out startingParams.bomb.explosionLenghtTime);

			value = null;
			iniFR.GetParameter("Bomb", "destroyDelayTime", out value);
			float.TryParse(value, out startingParams.bomb.destroyDelayTime);

			value = null;
			iniFR.GetParameter("Bomb", "canKill", out value);
			bool.TryParse(value, out startingParams.bomb.canKill);

			value = null;
			iniFR.GetParameter("Crystal", "moveTime", out value);
			float.TryParse(value, out startingParams.crystal.moveTime);

			value = null;
			iniFR.GetParameter("Crystal", "canRoll", out value);
			bool.TryParse(value, out startingParams.crystal.canRoll);

			value = null;
			iniFR.GetParameter("Crystal", "rotationSpeed", out value);
			float.TryParse(value, out startingParams.crystal.rotationSpeed);

			value = null;
			iniFR.GetParameter("Crystal", "canKill", out value);
			bool.TryParse(value, out startingParams.crystal.canKill);

			//value = null;
			//iniFR.GetParameter("Mineral", "moveTime", out value);
			//float.TryParse(value, out startingParams.mineral.moveTime);

			//value = null;
			//iniFR.GetParameter("Mineral", "canRoll", out value);
			//bool.TryParse(value, out startingParams.mineral.canRoll);

			//value = null;
			//iniFR.GetParameter("Mineral", "rotationSpeed", out value);
			//float.TryParse(value, out startingParams.mineral.rotationSpeed);

			//value = null;
			//iniFR.GetParameter("Mineral", "canKill", out value);
			//bool.TryParse(value, out startingParams.mineral.canKill);

			value = null;
			iniFR.GetParameter("Player", "moveTime", out value);
			float.TryParse(value, out startingParams.player.moveTime);

			value = null;
			iniFR.GetParameter("Player", "secondsForBallon", out value);
			int.TryParse(value, out startingParams.player.secondsForBallon);

			value = null;
			iniFR.GetParameter("Player", "startingSecondsOfOxygen", out value);
			int.TryParse(value, out startingParams.player.startingSecondsOfOxygen);

            value = null;
            iniFR.GetParameter("Player", "deathDelaySeconds", out value);
            int.TryParse(value, out startingParams.player.deathDelay);

            value = null;
			iniFR.GetParameter("Stone", "moveTime", out value);
			float.TryParse(value, out startingParams.stone.moveTime);

			value = null;
			iniFR.GetParameter("Stone", "canRoll", out value);
			bool.TryParse(value, out startingParams.stone.canRoll);

			value = null;
			iniFR.GetParameter("Stone", "rotationSpeed", out value);
			float.TryParse(value, out startingParams.stone.rotationSpeed);

			value = null;
			iniFR.GetParameter("Stone", "canKill", out value);
			bool.TryParse(value, out startingParams.stone.canKill);
        }
    }

    private static IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(startingParams.player.deathDelay);
        SceneManager.LoadScene("Menu");
    }
    public static void PlayerDeathDelay()
    {
        if (instance!=null)
        instance.StartCoroutine(DeathDelay());
    }
}
