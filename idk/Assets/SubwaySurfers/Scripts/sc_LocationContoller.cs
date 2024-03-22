using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class sc_LocationContoller : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] List<Transform> locationsReserve;
    [SerializeField] float locationLenght;
    [SerializeField] int locationReserveLenght;
    [SerializeField] int locationTailCount;
    [SerializeField] Transform locationOrigin;

    private List<Transform> locations = new List<Transform>();
    private void Awake()
    {
        Transform location = locationsReserve[Random.Range(0, locationsReserve.Count)];
        location.transform.position = new Vector3(location.transform.position.z, location.transform.position.y, player.position.z);
        locations.Add(location);
        Debug.Log(locations.Count + " " + location.position);

        for (int i = 1; i < locationReserveLenght; i++)
        {
            PushLocation();
        }
    }
    private void PushLocation()
    {
        Transform location = locationsReserve[Random.Range(0, locationsReserve.Count)];
        location.transform.position = new Vector3(location.transform.position.x, location.transform.position.y, locations[locations.Count - 1].position.z + locationLenght);
        locations.Add(location);
        Debug.Log(locations.Count + " " + location.position);
    }
    private void PopLocation()
    {
        locations[0].position = locationOrigin.position;
        locations.RemoveAt(0);
    }

    void Update()
    {
        if (player.position.z > locations[locationTailCount].position.z + locationLenght / 2)
        {
            PopLocation();
            PushLocation();
        }
    }
}
