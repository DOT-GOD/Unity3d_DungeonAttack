using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer _mixer;

    // 1 = master, 2 = BGM, 3 = SFX
    [SerializeField]
    [Range(1, 3)]
    public int _volumeType = 1;

    void Start()
    {
    }

    void Update()
    {
    }

    //슬라이더로 볼륨 조절
    public void SetLevel(float sliderVol)
    {
        if (_volumeType == 1)
            _mixer.SetFloat("Master", Mathf.Log10(sliderVol) * 20);
        else if (_volumeType == 2)
            _mixer.SetFloat("BGM", Mathf.Log10(sliderVol) * 20);
        else if (_volumeType == 3)
            _mixer.SetFloat("SFX", Mathf.Log10(sliderVol) * 20);
    }
}
