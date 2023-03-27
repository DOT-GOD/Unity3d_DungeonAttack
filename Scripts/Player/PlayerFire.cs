using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    GameObject _particleObject = null;

    [SerializeField]
    [Range(0.5f, 5.0f)]
    public float lifeTime = 0.8f;
    
    void Start()
    {
        GameObject particle = Instantiate(_particleObject, this.transform);
        //particle.transform.localScale = this.transform.localScale;
        ParticleSystem[] particles = particle.GetComponentsInChildren<ParticleSystem>();
        Invoke("DestroySelf", lifeTime);

        foreach (ParticleSystem p in particles)
        {
            p.Play();
        }

    }


    void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //        GameObject particle = Instantiate(_particleObject, this.transform);
    //        ParticleSystem[] particles = particle.GetComponentsInChildren<ParticleSystem>();

    //        foreach (ParticleSystem p in particles)
    //        {
    //            p.Play();
    //        }
    //    }
    //}

}
