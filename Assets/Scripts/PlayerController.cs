using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float rotateSensitivity = 5;

    public float respawnTime = 3;

    public GameObject hoverPrefab;

    public Text uiHealth;

    public Text uiDeaths;

    public Text uiFrags;

    private GameObject hover;

    private HoverController hoverController;

    private float respawnCountDown;

    private bool alive = false;

    private int deathCount = 0;

    private int fragCount = 0;

    // Use this for initialization
    void Start ()
    {
        uiHealth.text = "Health: 0";
        uiFrags.text = "Frags: 0";
        uiDeaths.text = "Deaths: 0";
    }
    // Update before physics
    void FixedUpdate()
    {
        if (hover != null)
        {
            ControlHover();
            alive = true;
        }
        else
        {
            Respawn();
            alive = false;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //Slow...
        uiHealth.text = "Health: " + hoverController.health.ToString();
    }

    public void addFrag()
    {
        fragCount++;
        uiFrags.text = "Frags: " + fragCount.ToString();
    }

    private void ControlHover()
    {
        if (!hoverController.IsAlive())
        {
            hover = null;
            return;
        }
        //Thrust + Strafe
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        hoverController.Move(Input.GetAxis("Mouse X") * rotateSensitivity, move.z, move.x);
        //rb.AddRelativeForce(move.x * hover.strafeForce, 0, move.z * hover.thrustForce);
        if (Input.GetButton("Fire1"))
        {
            hoverController.Shot();
        }
        //Rotate
        //transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSensitivity, 0);
        //Shot
    }

    private void Respawn()
    {
        if (alive)
        {
            deathCount++;
            uiDeaths.text = "Deaths: " + deathCount.ToString();
            respawnCountDown = respawnTime;
        }
        else
        {
            respawnCountDown -= Time.fixedDeltaTime;
            if (respawnCountDown <= 0)
            {
                GameObject[] respawnList = GameObject.FindGameObjectsWithTag("Respawn Player");
                if (respawnList.Length > 0)
                {
                    int selIndex = Random.Range(0, respawnList.Length);
                    hover = Instantiate(hoverPrefab, respawnList[selIndex].transform.position, respawnList[selIndex].transform.rotation) as GameObject;
                    hover.tag = "Player";
                    hoverController = hover.GetComponent<HoverController>();
                }
            }
        }
    }
}
