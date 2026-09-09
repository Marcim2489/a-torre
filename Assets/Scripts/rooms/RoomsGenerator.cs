using UnityEngine;
using System.Collections.Generic;

public class RoomsGenerator : MonoBehaviour
{
    [SerializeField]int amountOfRooms = 15;
    [SerializeField]int horizontalMapSize = 6;
    [SerializeField]int verticalMapSize = 6;
    [SerializeField]GameObject roomPlaceholder;
    List<Vector2> existingRooms;
    List<Vector2> availableRooms;
    List<Vector2> usedRooms;

    void Awake()
    {
        existingRooms = new List<Vector2>(horizontalMapSize * verticalMapSize);
        availableRooms = new List<Vector2>();
        usedRooms = new List<Vector2>();
        for(int i = 0; i < horizontalMapSize; i++)
        {
            for(int j = 0; j < verticalMapSize; j++)
            {
                existingRooms.Add(new Vector2(i,j));
            }
        }
        AddRoom(existingRooms[2]);

        while(usedRooms.Count < amountOfRooms)
        {
            AddRoom(availableRooms[Random.Range(0, availableRooms.Count)]);
        }
    }

    void Start()
    {
        foreach(Vector2 room in usedRooms)
        {
            GameObject p = Instantiate(roomPlaceholder);
            p.transform.position = room;
        }
    }

    void AddRoom(Vector2 room)
    {
        if (existingRooms.Contains(room) == false || usedRooms.Contains(room))
        {
            return;
        }

        if (availableRooms.Contains(room))
        {
            availableRooms.Remove(room);
        }

        usedRooms.Add(room);
        if(availableRooms.Contains(room + Vector2.left) == false)
        {
            availableRooms.Add(room + Vector2.left);
        }
        if(availableRooms.Contains(room + Vector2.right) == false)
        {
            availableRooms.Add(room + Vector2.right);
        }
        if(availableRooms.Contains(room + Vector2.down) == false)
        {
            availableRooms.Add(room + Vector2.down);
        }
        if(availableRooms.Contains(room + Vector2.up) == false)
        {
            availableRooms.Add(room + Vector2.up);
        }
    }

}
