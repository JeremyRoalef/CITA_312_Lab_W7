using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatKeys : MonoBehaviour
{
    CapsuleCollider playerCollider;

    bool boolCollisionEnables = true;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCollisions();
        LoadNextLevel();
    }

    //Method to update collisions for player
    void UpdateCollisions()
    {
        if (Input.GetKey(KeyCode.C) && boolCollisionEnables)
        {
            playerCollider.enabled = false;
            boolCollisionEnables = false;
        }
        else if (Input.GetKey(KeyCode.C) && !boolCollisionEnables)
        {
            playerCollider.enabled = true;
            boolCollisionEnables = true;
        }
    }

    //Method to load next level for player
    void LoadNextLevel()
    {
        if (Input.GetKey(KeyCode.L))
        {
            int intNextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            //Determine if there is a next scene in build
            bool boolThereIsNextScene = intNextSceneIndex < SceneManager.sceneCountInBuildSettings;

            //If there's a next scene in build, load it
            if (boolThereIsNextScene)
            {
                SceneManager.LoadScene(intNextSceneIndex);
            }
            //Otherwise, restart game (scene at index 0)
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
