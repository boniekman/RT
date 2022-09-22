using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    AudioSource audioSource;

    bool isTransitioning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(UnityEngine.Collision collision)
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
    void FinishSequence()
    {

        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        //todo animation
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", loadDelay);

    }
    void CrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        //todo animation
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
