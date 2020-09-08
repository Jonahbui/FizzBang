/* Author: Jonah Bui
 * Date: September 8, 2020
 * Purpose: Create a game called FizzBang, which increments numerically until it reaches a number
 * determined by the player.
 * 
 * Unity Version: 2019.4.18f1
 */
using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class FizzBang : MonoBehaviour
{
    public TMP_InputField setNumber;    // Used to allow the user to set the number to increment to
    public TextMeshProUGUI display;     // Displays the current increment, or "FizzBang" words
    public TextMeshProUGUI gameOver;    // Inidicates the game is over to the player
    public TextMeshProUGUI arrayNumbers;// Displays numbers in array at end.
    
    // Text for "START" and "Next Number" manually inputted in scene
    public GameObject startButton;      // Used to put game in play mode
    public GameObject incrementButton;  // Used to increment the current number

    private int currentIncrement;       // Current number the user is on
    private int targetNumber;           // Number to increment up to

    private int[] multiples;            // Store the multiples of 3, 5
    private int index;                  // Used to keep track of current array element to store numbers

    private bool isPlaying;             // Used to end the game to prevent extra coroutine calls

    private void Awake()
    {
        isPlaying = false;

        // Only show game start UI elements
        display.enabled = false;
        gameOver.enabled = false;
        arrayNumbers.enabled = false;
        incrementButton.SetActive(false);
        gameOver.text = "Game Over";

        currentIncrement = 0;
        targetNumber = 0;
    }

    void Update()
    {
        if (isPlaying)
        {
            // Target number reached, end game
            if (currentIncrement == targetNumber)
                StartCoroutine(GameOver(3f));
        }
    }

    /// <summary>
    /// Starts the game and shows the appropriate UI elements to the user. Also allocates values to
    /// store multiplicities and sets the text of TextMeshPros.
    /// </summary>
    public void StartGame()
    {
        // Set the target value
        try
        {
            targetNumber = Int32.Parse(setNumber.text);
            multiples = new int[(targetNumber / 3) + (targetNumber / 5)];

            // Disable all UI elements except the increment text and button
            setNumber.gameObject.SetActive(false);
            startButton.SetActive(false);

            incrementButton.SetActive(true);
            display.enabled = true;
            display.text = "0";

            isPlaying = true;
        }
        catch
        {
            Debug.LogWarning("Empty input! Please enter correct input!");
        }
    }

    /// <summary>
    /// To be used by a button when the game is in play mode. Increments the current number and
    /// displays the appropriate text to the user. Also stores numbers with multiples 3,
    /// </summary>
    public void Increment()
    {
        currentIncrement++;
        if (currentIncrement % 15 == 0)
        {
            display.text = "FizzBuzz";
            multiples[index++] = currentIncrement;
        }
        else if (currentIncrement % 3 == 0)
        {
            display.text = "Fizz";
            multiples[index++] = currentIncrement;
        }
        else if (currentIncrement % 5 == 0)
        {
            display.text = "Buzz";
            multiples[index++] = currentIncrement;
        }
        else
        {
            display.text = $"{currentIncrement}";
        }
    }

    /// <summary>
    /// Ends the game showing the multiples of 3 and 5.
    /// </summary>
    /// <param name="time">Time to wait before ending the game. Provides user enough time to see
    /// the last increment before the "Game Over" text pops up.</param>
    /// <returns></returns>
    private IEnumerator GameOver(float time)
    {
        // Prevent player from incrementing further after game finishes
        incrementButton.SetActive(false);

        // Set multiplicities to show at the end
        String numbers = "";
        foreach (int num in multiples)
            if (num != 0) numbers += $"{num}, ";
        arrayNumbers.text = numbers;

        yield return new WaitForSeconds(time);

        // Show and hide UI elements for game over
        display.enabled = false;

        arrayNumbers.enabled = true;
        gameOver.enabled = true;

        isPlaying = false;
    }
}
