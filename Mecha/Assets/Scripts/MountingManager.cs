using UnityEngine;

public class MountingManager : MonoBehaviour
{
    private bool inZone;
    private bool inMech;

    [SerializeField] Transform pilotSeat;
    [SerializeField] Transform dropOff;
    [SerializeField] GameObject assignedMech;

    private GameObject player;

    void Start()
    {
        inZone = false;
        inMech = false;
        player = null;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;
        if (otherObject.CompareTag("Player"))
        {
            inZone = true;
            player = otherObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inZone = false;
            player = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (inZone && Input.GetKeyDown(KeyCode.E))
        {
            if (!inMech)
            {
                player.GetComponent<PilotController>().SetPilotMovement(false);
                player.transform.SetParent(pilotSeat);
                player.transform.localPosition = Vector3.zero;
                player.transform.localRotation = Quaternion.identity;
                assignedMech.GetComponent<MechaController>().enabled = true;
                assignedMech.GetComponent<MechaController>().SetMechaMovement(true);
                inMech = true;
            } else
            {
                player.GetComponent<PilotController>().SetPilotMovement(true);
                player.transform.SetParent(null);
                player.transform.localPosition = dropOff.position;
                assignedMech.GetComponent<MechaController>().enabled = false;
                assignedMech.GetComponent<MechaController>().SetMechaMovement(false);
                inMech = false;
            }
        }
    }

    private void LateUpdate()
    {
        if (inMech && player != null)
        {
            player.transform.position = pilotSeat.position;
            player.transform.rotation = pilotSeat.rotation;
        }
    }
}
