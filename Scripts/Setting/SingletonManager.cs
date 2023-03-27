using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    public static SingletonManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static SingletonManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void OnClick_Plane1()
    {
    }
    public void OnClick_Plane2()
    {
    }
    public void OnClick_Plane3()
    {
    }


    public int CurrentScore { get; set; }
    public int BestScore { get; set; }
}

//"경로/Plane" + PlaneNumber;
