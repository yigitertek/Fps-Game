using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ak47 : MonoBehaviour
{
    public bool canShoot;
    float shootFrequency;
    public float fireRate;
    public float range;
    public Camera myCam;
    public AudioSource FireSound;
    public ParticleSystem FireEffect;
    public ParticleSystem BulletHoleEffect;
    public ParticleSystem BloodEffect;
    Animator animator;
   
    
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && canShoot && Time.time>shootFrequency)
        {
            Fire();
            shootFrequency = Time.time + fireRate;
        }

        if (Input.GetKey(KeyCode.R))
        {
            animator.Play("Reload_animation");
        }


    }

    void Fire()
    {
        FireSound.Play();
        FireEffect.Play();
        animator.Play("Fire");

        RaycastHit hit;
        if (Physics.Raycast(myCam.transform.position,myCam.transform.forward,out hit,range))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Instantiate(BloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if (hit.transform.gameObject.CompareTag("OvertumObjects"))
            {
                Rigidbody rg = hit.transform.gameObject.GetComponent<Rigidbody>();
                
                rg.AddForce(-hit.normal * 50f);
                
               Instantiate(BulletHoleEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                Instantiate(BulletHoleEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
           
        }

       // Instantiate(BulletHoleEffect, hit.point, Quaternion.LookRotation(hit.normal));
    }
}
