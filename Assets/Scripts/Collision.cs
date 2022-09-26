using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Collision : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] Text collisionOFF;

    AudioSource audioSource;

    bool isTransitioning;
    bool isCollisionPossible;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isTransitioning = false;
        isCollisionPossible = true;
        collisionOFF.enabled = false;
    }
    private void Update()
    {
        DebugCheats();
    }

    private void DebugCheats()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            NextLevel();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            isCollisionPossible = !isCollisionPossible; // toggle
            collisionOFF.enabled = !collisionOFF.enabled;
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (isCollisionPossible)
        {
            if (!isTransitioning)
            {
                switch (collision.gameObject.tag)
                {
                    case "Finish":
                        FinishSequence();
                        break;
                    case "Respawn":
                        Debug.Log("Begin ignition");
                        break;
                    default:
                        CrashSequence();
                        break;
                }
            }
        }
    }
    void FinishSequence()
    {

        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", loadDelay);

    }
    void CrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);

        
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }


}
