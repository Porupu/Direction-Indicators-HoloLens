using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIndicatorRegister : MonoBehaviour
{
    [SerializeField] float destroyTimer = 10.0f;

    void Start()
    {
        Invoke("Register", 0.0f); //0 seconds delay
    }

    void Register()
    {
        //if (!DI_System.CheckIfObjectInSight(this.transform)) //check if the object is not in sight
        //{
        //    DI_System.CreateIndicator(this.transform);
        //}
        DI_System.CreateIndicator(this.transform);
        Destroy(this.gameObject, destroyTimer);
    }

}
