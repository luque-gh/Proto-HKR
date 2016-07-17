using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IPlayable
{

    public float rotateSensitivity = 5;

    public float respawnTime = 3;

    public GameObject hoverPrefab;

    public Text uiHealth;

    public Text uiDeaths;

    public Text uiFrags;

    private GameObject hover;

    private IHover hoverController;

    private int deathCount = 0;

    private int fragCount = 0;

    // Use this for initialization
    void Start ()
    {
        uiHealth.text = "Health: 0";
        uiFrags.text = "Frags: 0";
        uiDeaths.text = "Deaths: 0";
        Invoke("Respawn", 1);
    }
    // Update before physics
    void FixedUpdate()
    {
        if (hover != null)
        {
            ActionUpdate();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //Slow...
        uiHealth.text = "Health: " + hoverController.GetHealth().ToString();
    }

    public void ActionUpdate()
    {
        if (!hoverController.IsAlive())
        {
            hover = null;
            IncrementDeaths();
            Invoke("Respawn", respawnTime);
            return;
        }
        //Thrust + Strafe
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        hoverController.Move(Input.GetAxis("Mouse X") * rotateSensitivity, move.z, move.x);
        //rb.AddRelativeForce(move.x * hover.strafeForce, 0, move.z * hover.thrustForce);
        if (Input.GetButton("Fire1"))
        {
            hoverController.ShotWeapon1();
        }
        //Rotate
        //transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSensitivity, 0);
        //Shot
    }

    public void Respawn()
    {
        GameObject[] respawnList = GameObject.FindGameObjectsWithTag("Respawn Player");
        if (respawnList.Length > 0)
        {
            int selIndex = Random.Range(0, respawnList.Length);
            hover = Instantiate(hoverPrefab, respawnList[selIndex].transform.position, respawnList[selIndex].transform.rotation) as GameObject;
            hover.tag = "Player";
            hoverController = hover.GetComponent<IHover>();
        }
    }

    public void IncrementFrag()
    {
        fragCount++;
        uiFrags.text = "Frags: " + fragCount.ToString();
    }

    public void DecrementFrag()
    {
        fragCount--;
    }

    public void IncrementDeaths()
    {
        deathCount++;
        uiDeaths.text = "Deaths: " + deathCount.ToString();
    }
}
