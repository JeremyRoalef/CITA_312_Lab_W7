using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //Serialized fields
    [SerializeField] float fltReloadDelay = 1f;
    [SerializeField] float fltNextLevelDelay = 1f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;

    //Cashe references
    AudioSource audioSource;

    //private attributes
    bool boolIsTransitioning = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag) {
            case "Dangerous":
                StartCrashSequence();
                break;
            case "Fuel":
                RefuelPlayer();
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                //Do nothing
                break;
        }
    }

    //Method to run when player crashes into dangerous obstacles
    void StartCrashSequence()
    {
        if (boolIsTransitioning) {return;}

        audioSource.Stop();
        boolIsTransitioning = true;
        GetComponent<Movement>().enabled = false; //Disable player movement ability
        PlayCrashSound();
        ReloadScene();
        KillPlayer();
    }

    //Method to run when the player finishes the level
    void StartFinishSequence()
    {
        if (boolIsTransitioning) {return;}

        audioSource.Stop();
        boolIsTransitioning = true;
        GetComponent<Movement>().enabled = false; //Disable player movement ability
        PlaySuccessSound();
        LoadNextScene();
    }

    //Method to kill player
    void KillPlayer()
    {
        Debug.Log("Collision should kill player");
    }

    //Method to refuel player's fuel value
    void RefuelPlayer()
    {
        Debug.Log("Collision should refuel player");
    }

    //Method to load the next scene
    void LoadNextScene()
    {
        Debug.Log("Collision should load next scene");
        StartCoroutine(WaitToLoadNextScene());
    }

    //Method to load the current scene
    void ReloadScene()
    {
        //Start the coroutine to reload the current scene
        StartCoroutine(WaitToReloadScene());
    }

    //Method to play the crash sfx
    void PlayCrashSound()
    {
        audioSource.PlayOneShot(crashSFX);
    }

    //Method to play the success sfx
    void PlaySuccessSound()
    {
        audioSource.PlayOneShot(successSFX);
    }

    //IEnumerator to wait to reload the current scene
    IEnumerator WaitToReloadScene()
    {
        int intCurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //Wait to reload current scene
        yield return new WaitForSeconds(fltReloadDelay);
        //Reload the current scene
        SceneManager.LoadScene(intCurrentSceneIndex);
    }

    //IEnumerator to wait to load the next scene
    IEnumerator WaitToLoadNextScene()
    {
        int intNextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        //Wait to load next scene
        yield return new WaitForSeconds(fltNextLevelDelay);

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
