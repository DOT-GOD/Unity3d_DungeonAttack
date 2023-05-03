using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    GameObject _particleObject = null;               //지정필요 : 파티클 프리팹

    [SerializeField]
    [Range(0.5f, 5.0f)]
    public float lifeTime = 0.8f;                    //지정필요 : 투사체 수명(사정거리제한 및 영원히 날아가는 현상 방지)

    void Start()
    {
        //화염구를 발사하고 자신의 오브젝트 파괴
        GameObject particle = Instantiate(_particleObject, this.transform);
        ParticleSystem[] particles = particle.GetComponentsInChildren<ParticleSystem>();
        Destroy(this.gameObject, lifeTime);

        foreach (ParticleSystem p in particles)
        {
            p.Play();
        }

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
