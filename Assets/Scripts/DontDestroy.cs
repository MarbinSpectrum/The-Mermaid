using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy<T> : MonoBehaviour where T : DontDestroy<T>
{ 
    private static T Instance;

    public static T instance
    {
        get
        {
            if (Instance == null)
            {
                GameObject[] temp = Resources.LoadAll<GameObject>("Manager");

                foreach (GameObject obj in temp)
                    if (obj.GetComponent<T>())
                    {
                        Instance = Instantiate(obj).GetComponent<T>();
                        break;
                    }
                DontDestroyOnLoad(Instance.gameObject);
            }
            return Instance;
        }
    }
}
