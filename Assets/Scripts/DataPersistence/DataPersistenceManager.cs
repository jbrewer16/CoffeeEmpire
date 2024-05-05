using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    // TODO - temp field for testing
    [SerializeField] private bool startNewGame;
    [SerializeField] private bool restartGame;

    [SerializeField] private GameObject beanManagerObject;
    private GameData gameData;
    private BeanManager beanManager;
    private List<IDataPersistence> dataPersistenceObjs;
    
    public static DataPersistenceManager instance { get; private set; }
    public FileDataHandler dataHandler;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        beanManager = beanManagerObject.GetComponent<BeanManager>();
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjs = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void RestartGame()
    {
        restartGame = true;
        LoadGame();
        beanManager.ResetBeansUI();
    }

    public void LoadGame()
    {
        // Load any saved data from file handler
        gameData = dataHandler.Load();

        // If no data to load, initialize gameData;
        if(this.gameData == null || startNewGame || restartGame)
        {
            Debug.Log("No data was found or restart was clicked, initializing data to defaults");
            NewGame();
            restartGame = false;
        }
        // Push the loaded data to all scripts that need it
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjs)
        {
            dataPersistenceObj.LoadData(gameData);
        }

    }

    public void SaveGame()
    {
        // pass the data to other scripts so they can update it
        if(dataPersistenceObjs != null)
        {
            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjs)
            {
                dataPersistenceObj.SaveData(ref gameData);
            }

            // Save the data to file
            dataHandler.Save(gameData);
        }

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void OnApplicationPause(bool pause)
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjs = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjs);
    }

}
