using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 200f;
    [SerializeField] float rotationSpeed = 200f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoostParticles;
    [SerializeField] ParticleSystem leftBoostParticles;
    [SerializeField] ParticleSystem rightBoostParticles;

    Rigidbody rb;
    AudioSource ad;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ad = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!ad.isPlaying)
            {
                ad.PlayOneShot(mainEngine);
            }
            if (!mainBoostParticles.isPlaying)
            {
                mainBoostParticles.Play();
            }
        }
        else
        {
            if (ad.isPlaying)
            {
                ad.Stop();
            }
            if (mainBoostParticles.isPlaying)
            {
                mainBoostParticles.Stop();
            }
        }
    }

    void ProcessRotation()
    {

        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
            if (!rightBoostParticles.isPlaying)
            {
                rightBoostParticles.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
            if (rightBoostParticles.isPlaying)
            {
                rightBoostParticles.Stop();
            }
            if (!leftBoostParticles.isPlaying)
            {
                leftBoostParticles.Play();
            }
        }
        else
        {
            if (leftBoostParticles.isPlaying)
            {
                leftBoostParticles.Stop();
            }
        }
    }

    void ApplyRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        rb.freezeRotation = false;
    }
}