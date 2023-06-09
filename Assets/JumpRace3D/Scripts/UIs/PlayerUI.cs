﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MoveUI
{
    [Header("Player UI Properties")]
    [SerializeField]
    private TextMeshProUGUI _stageNumberCurrent; // For showing the
                                                 // current stage
                                                 // number

    [SerializeField]
    private TextMeshProUGUI _stageNumberNext; // For showing the next
                                              // stage number

    [SerializeField]
    private Image _bar; // For implementing the bar

    [SerializeField]
    private GameObject[] _posBacks; // Array of position backgrounds

    [SerializeField]
    private TextMeshProUGUI[] _posTexts; // Array of position texts

    private int _posIndex; // Index of the positions

    /// <summary>
    /// Setting the stage numbers of the Player UI
    /// </summary>
    /// <param name="current">The current stage number, 
    ///                       of type int</param>
    public void SetStageNumbers(int current)
    {
        // Setting the current stage number
        _stageNumberCurrent.text = current.ToString();

        // Setting the next stage number
        _stageNumberNext.text = (current + 1).ToString();
    }

    /// <summary>
    /// This method sets bar fill amount.
    /// </summary>
    /// <param name="percentage">The bar fill amount in percentage,
    ///                          of type float</param>
    public void SetBar(float percentage)
    {
        // Fixing any error values
        percentage = percentage > 1 ? 1 : 
                     percentage < 0 ? 0 : 
                     percentage;

        _bar.fillAmount = percentage; // Setting the bar
    }

    /// <summary>
    /// This method sets the race position text.
    /// </summary>
    /// <param name="racePosition">The race position of the character,
    ///                            of type int</param>
    /// <param name="isPlayer">Flag to check if the character is the 
    ///                        player, of type bool</param>
    /// <param name="name">The name of the character, of type string</param>
    public void SetInGameRacePosition(int racePosition, bool isPlayer, string name)
    {
        // Fixing any errors in race position
        _posIndex = racePosition >= _posBacks.Length ?
                                       _posBacks.Length - 1 : 
                                       racePosition;

        // Renaming the race position text
        _posTexts[_posIndex].text 
            = (racePosition + 1).ToString() + ". " + name;

        if (isPlayer) // Checking if it is player
        {
            // Loop for highlighting the player
            for(int i = 0; i < _posBacks.Length; i++)
            {
                // Checking if the position is player's
                if (i == _posIndex)
                {
                    // Showing player's highlight
                    _posBacks[i].SetActive(true);
                }
                // Hiding others highlight
                else _posBacks[i].SetActive(false);
            }
        }
    }
}
