using UnityEngine;
using System.Collections;

public class HoverShotController : MonoBehaviour {

    public GameObject shotGameObject;

    // Use this for initialization
    void Start()
    {

    }

    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateShot()
    {
        GameObject shot = Instantiate(shotGameObject);
        shot.transform.localPosition = this.transform.TransformPoint(Vector3.zero);
        Vector3 forward = this.transform.TransformDirection(Vector3.forward);
        shot.transform.localRotation = Quaternion.LookRotation(forward, this.transform.TransformDirection(Vector3.up));
        //Tosquisse para corrigir capsula!
        shot.transform.Rotate(-90, 0, 0);
        shot.GetComponent<ShotController>().setInitialVelocity();
    }
}
