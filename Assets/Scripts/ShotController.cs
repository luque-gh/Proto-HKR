using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour
{

    public float velocity = 75.0f;
    public float maxLifetime = 3.0f;
    public float damage = 10.0f;
    private float lifeTime = 0;

    public void setInitialVelocity()
    {
        Rigidbody shotRb = GetComponent<Rigidbody>();
        shotRb.velocity = transform.TransformDirection(-Vector3.up) * velocity;
    }

    // Use this for initialization
    void Start()
    {
        //Não funcionar setando aqui a velocidade inicial, pois perde um update de física
    }

    void FixedUpdate()
    {
        lifeTime += Time.fixedDeltaTime;
        //Usando up por causa de capsula tosca!
        //transform.Translate(-Vector3.up * velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime > maxLifetime)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "IA" ||
            other.gameObject.tag == "Player")
        {
            HoverController hc = other.gameObject.GetComponent<HoverController>();
            if (hc != null)
            {
                hc.Damage(damage, transform.position, null);
            }
        }
        Destroy(this.gameObject);
    }
}
