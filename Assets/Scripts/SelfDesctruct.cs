using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDesctruct : MonoBehaviour
{
    [SerializeField] float timeTillDestroy = 3f;
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, timeTillDestroy);
    }
}
