using UnityEngine;
using System.Collections;

public class HoverController : MonoBehaviour {

    public float groundDistance = 1.5f;
    public float levitateJitter = 0.5f;
    public float levitateMaxVelocity = 1.0f;
    public float levitateForce = 30.0f;
    public float thrustForce = 30.0f;
    public float strafeForce = 25.0f;
    public float shotMinInterval = 0.25f;
    public float health = 100;

    private float lastShotElapsedTime = 1;

    //Rigid Body
    private Rigidbody rb;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update before physics
    void FixedUpdate()
    {
        if (!IsAlive())
        {
            return;
        }
        lastShotElapsedTime += Time.deltaTime;
        RaycastHit hit;
        if ((rb.velocity.y < levitateMaxVelocity) && Physics.Raycast(transform.position, -Vector3.up, out hit, groundDistance - levitateJitter))
        {
            float yForce = groundDistance * groundDistance - hit.distance * hit.distance;
            rb.AddForce(0, yForce * levitateForce, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(float turn, float thrust, float strafe)
    {
        rb.AddRelativeForce(strafe * strafeForce, 0, thrust * thrustForce);
        transform.Rotate(0, turn, 0);
    }

    public void Shot()
    {
        if (lastShotElapsedTime >= shotMinInterval)
        {
            lastShotElapsedTime = 0;
            HoverShotController[] scList = this.GetComponentsInChildren<HoverShotController>();
            for (int i = 0; i < scList.Length; i++)
            {
                scList[i].CreateShot();
            }
        }
    }

    public void Damage(float value, Vector3 contactPos)
    {
        health -= value;
        if (health <= 0)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddExplosionForce(1000, contactPos, 5, 2);
            rb.angularDrag = 0;
            rb.drag = 0;
            DestroyObject(gameObject, 15.0f);
        }
    }

    public bool IsAlive()
    {
        return health > 0;
    }
}
