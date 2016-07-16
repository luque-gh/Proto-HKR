using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject hoverCameraHelper;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        //Costy!!!
        GameObject[] hoverList = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject hover in hoverList) 
        {
            if (hover.GetComponent< HoverController >().IsAlive())
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                foreach (Transform child in hover.transform)
                {
                    GameObject childGO = child.gameObject;
                    if (child.gameObject.name == hoverCameraHelper.name)
                    {
                        transform.position = childGO.transform.position;
                        transform.rotation = childGO.transform.rotation;
                        break;
                    }
                }
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
