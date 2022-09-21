using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("its friendly");
                break;
            case "Fuel":
                Debug.Log("its fuel");
                break ;
            case "Finish":
                Debug.Log("its finish");
                break;
            case "Respawn":
                Debug.Log("Begin ignition");
                break;
            default:
                ReloadLevel();
                break;
        }
            
    }

    private static void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
