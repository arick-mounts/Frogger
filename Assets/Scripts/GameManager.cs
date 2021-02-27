using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    private static int currentLives = 3;
    public static int Lives = 3;
    public static string Name = "DefaultName";

    bool isPause = false;

    public GameObject frog;
    public Text NameText;
    public Text LivesText;
    public GameObject PauseMenu;
    public CarSpawner Spawner;

    // Start is called before the first frame update
    void Start()
    {
        if (NameText)
            NameText.text = GameManager.Name;
    }

    // Update is called once per frame
    void Update()
    {
        if (LivesText)
            LivesText.text = "Lives: " + currentLives.ToString();

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Input.GetKeyDown("escape"))
            {
                pause();
            }
        }
    }

    public void pause()
    {
        if (!isPause)
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true); ;
            isPause = true;
        }
        else
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false); ;
            isPause = false;
        }

    }


    public void LoadIntroScene()
    {
        Score.CurrentScore = 0;
        GameManager.Name = "DefaultName";
        GameManager.Lives = 3;
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void spawnSpeed(float f)
    {
        Car.StaticSpeed = f;
        CarSpawner.spawnDelay = .6f - (f / 45);
    }

    public void setLives(int l)
    {
        Lives = l + 1;
        currentLives = Lives;
    }

    public void setName(string n)
    {
        Name = n;
        Debug.Log(name);
    }

    public void loseLife()
    {
        currentLives -= 1;
        CarSpawner.ClearList();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (currentLives < 0)
        {

            GameManager.LoadNextScene();
        }
    }
    public static void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        foreach (GameObject car in CarSpawner.cars)
        {
            CarData CD = new CarData();
            CD.CarX = car.transform.position.x;
            CD.CarY = car.transform.position.y;
            CD.Speed = car.GetComponent<Car>().speed;
            CD.Facing = car.transform.rotation.z;
            save.Cars.Add(CD);
        }

        save.FrogPositionX = frog.transform.position.x;
        save.FrogPositionY = frog.transform.position.y;

        save.Lives = currentLives;
        save.Name = Name;
        save.Score = Score.CurrentScore;

        return save;
    }

    public void SaveGame()
    {
        // 1
        Save save = CreateSaveGameObject();

        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        // 3
        Score.CurrentScore = 0;
        currentLives = Lives;
        NameText.text = Name;
        LivesText.text = currentLives.ToString();

        CarSpawner.ClearList();
        frog.transform.position = (new Vector2(0f, -4f));

        
        Debug.Log("Game Saved");
    }
    public void LoadGame()
    {
        // 1
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            CarSpawner.ClearList();

            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            // 3
            for (int i = 0; i < save.Cars.Count; i++)
            {
                Spawner.SpawnCar(save.Cars[i].CarX, save.Cars[i].CarY, save.Cars[i].Facing, save.Cars[i].Speed);
            }

            // 4 
            Score.CurrentScore = save.Score;
            currentLives = save.Lives;
            NameText.text = save.Name;
            LivesText.text = currentLives.ToString();
            frog.transform.position = new Vector2( save.FrogPositionX,save.FrogPositionY);
           

            Debug.Log("Game Loaded");

        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    public void SaveAsJSON()
    {
        
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        Debug.Log("Saving as JSON: " + json);
        
    }

}
