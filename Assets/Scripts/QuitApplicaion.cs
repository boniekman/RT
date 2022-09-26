using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplicaion : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape pushed");
            Application.Quit();
        }
    }
}
