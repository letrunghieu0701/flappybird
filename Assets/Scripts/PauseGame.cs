using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PauseGame : MonoBehaviour
{

    public static bool isPausing;
    GameController gameController = null;
    // Start is called before the first frame update
    void Start()
    {
        isPausing = false;
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.isGameOver == true)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePause();
        }
    }

    public static void HandlePause()
    {
        if (isPausing == false)
        {
            Time.timeScale = 0;
            isPausing = true;
        }
        else
        {
            Time.timeScale = 1;
            isPausing = false;
        }
    }
}
