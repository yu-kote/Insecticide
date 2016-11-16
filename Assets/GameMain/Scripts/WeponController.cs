using UnityEngine;
using System.Collections;

public class WeponController : MonoBehaviour
{


    public void OnTriggerExit(Collider other)
    {
        hitobject = other.gameObject;
    }

    void Start()
    {

    }
    public GameObject hitobject;

    void Update()
    {

    }
}
