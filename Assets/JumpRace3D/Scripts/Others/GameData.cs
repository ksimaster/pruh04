﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>GameData</c> contains data that will be used by other classes.
/// </summary>
public class GameData : MonoBehaviour
{
    [Header("Game Data Properties")]
    [Tooltip("The y-axis value limit for objects to fall to.")]
    public float FallHeightLimit; // The limit of fall distance

    [Header("Simulation Properties")]
    [Range(0f, 0.999f)]
    public float SimulationSpeedMin; // The minimum simulation speed
                                     // which is the start value of
                                     // simulation speed when
                                     // activated, range is
                                     // 0 <= min < 1

    public float SimulationSpeedAcceleration; // The acceleration of
                                              // simulation speed
                                              // over time
    
    private float _simulationSpeed = 1; // The main simulation speed
                                        // for the game and default
                                        // value is 1, range is
                                        // 0 <= s <= 1

    /// <summary>
    /// Getting the current simulation speed value, of type float
    /// </summary>
    public float SimulationSpeed { get { return _simulationSpeed; } }

    /// <summary>
    /// Getting the normalized value of the simulation speed,
    /// of type float
    /// </summary>
    public float SimulationSpeedNormalized
    {
        get { return (_simulationSpeed - SimulationSpeedMin) / 
                     (1 - SimulationSpeedMin); }
    }

    // Flag to check if simulation speed effect is activated
    private bool _isSimulationActive
    { get { return _simulationSpeed < 1; } }

    /// <summary>
    /// For getting the acceleration value for the simulation speed, 
    /// of type float
    /// </summary>
    private float _simulationAccelerationValue
    { get { return _simulationSpeed + (SimulationSpeedAcceleration * _fps); } }

    private float _fps; // For storing Time.deltaTime, needed for
                        // smooth evenly transition where ever
                        // needed

    private string _playerName = "Вы"; // Name of the player

    /// <summary>
    /// Returns the name of the player, of type string
    /// </summary>
    public string PlayerName { get { return _playerName; } }

    private int _level; // For storing the loaded value of level
    private int _levelNumberCurrent; // For storing the loaded
                                     // value of stage number

    public static GameData Instance; // Singleton

    void Awake()
    {
        if (Instance == null) // NOT initialized
        {
            Instance = this; // Initializing the instance

            DontDestroyOnLoad(gameObject); // Making it available
                                           // throughout the game
        }
        else Destroy(gameObject); // Already initialized and
                                  // destroying duplicate
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadData(); // Loading the game
    }

    // Update is called once per frame
    void Update()
    {
        // Condition to check if simulation speed activated
        if (_isSimulationActive) SimulationSpeedEffect();
    }

    /// <summary>
    /// This method updates the simulation speed which means activating
    /// the simulation speed effect
    /// </summary>
    private void SimulationSpeedEffect()
    {
        /*Hint: _fps maybe needs to be placed in the Update() method 
         *      if other effects needs to use the _fps value in
         *      future.
         */
        _fps = Time.deltaTime; // Storing the Time.deltaTime value.

        // Accelerating simulation speed
        _simulationSpeed = _simulationAccelerationValue >= 1 ?
                            1 :
                            _simulationAccelerationValue;
    }
    
    /// <summary>
    /// This method loads the game data.
    /// </summary>
    private void LoadData()
    {
        // Getting the level number
        _level = PlayerPrefs.GetInt("level", 1);

        // Gettng the stage number
        _levelNumberCurrent = PlayerPrefs.GetInt("levelNumberCurrent", 1);

        // Loading the stage data
        StageGenerator.Instance.LoadStage(_level, _levelNumberCurrent);
    }

    /// <summary>
    /// This method saves the data from the game.
    /// </summary>
    /// <param name="level">Current level to store, of type int</param>
    /// <param name="levelNumberCurrent">Current level number to store,
    ///                                  of type int</param>
    public void SaveData(int level, int levelNumberCurrent)
    {
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("levelNumberCurrent", levelNumberCurrent);
    }

    /// <summary>
    /// This method starts the simulation effect.
    /// </summary>
    public void StartSimulationSpeedEffect()
    {
        _simulationSpeed = SimulationSpeedMin;
    }
}
