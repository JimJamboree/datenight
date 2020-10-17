using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persist : MonoBehaviour
{
    [SerializeField] private GameObject attachedObject;
    // Start is called before the first frame update
    void Start()
    {
        attachedObject = gameObject;
        DontDestroyOnLoad(attachedObject);
    }
}
