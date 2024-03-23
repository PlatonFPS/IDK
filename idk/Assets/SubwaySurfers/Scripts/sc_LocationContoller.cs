using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class sc_LocationContoller : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] List<Transform> locationsReserve;
    [SerializeField] float locationLenght;
    [SerializeField] int locationReserveLenght;
    [SerializeField] int locationTailCount;
    [SerializeField] Transform locationOrigin;

    private List<Transform> locations = new List<Transform>();
    private sc_SubwayMovement sc_SubwayMovement;
    private void Awake()
    {
        sc_SubwayMovement = player.GetComponent<sc_SubwayMovement>();

        int index = Random.Range(0, locationsReserve.Count);
        Transform location = locationsReserve[index];
        locationsReserve.RemoveAt(index);
        location.transform.position = new Vector3(location.transform.position.z, location.transform.position.y, player.position.z + locationLenght * locationTailCount);
        locations.Add(location);

        for (int i = 1; i < locationReserveLenght; i++)
        {
            PushLocation();
        }
    }
    private void PushLocation()
    {
        int index = Random.Range(0, locationsReserve.Count);
        Transform location = locationsReserve[index];
        locationsReserve.RemoveAt(index);
        location.transform.position = new Vector3(location.transform.position.x, location.transform.position.y, locations[locations.Count - 1].position.z + locationLenght);
        locations.Add(location);
    }
    [SerializeField] int winCount;
    [SerializeField] TextMeshProUGUI text;
    private float currentWinCount = 0;

    private void UpdateWinCount()
    {
        currentWinCount += 1;
        if (currentWinCount >= winCount)
        {
            sc_SubwayMovement.Win();
            currentWinCount = winCount;
        }
        text.text = (Mathf.RoundToInt(currentWinCount / winCount * 100)).ToString() + "%";
    }

    private void PopLocation()
    {
        if(locations.Count > locationReserveLenght + locationTailCount)
        {
            locations[0].position = locationOrigin.position;
            locationsReserve.Add(locations[0]);
            locations.RemoveAt(0);
            UpdateWinCount();
        }
    }

    void Update()
    {
        if (player.position.z > locations[locationTailCount].position.z + locationLenght/ 2)
        {
            PopLocation();
            PushLocation();
        }
    }
}
