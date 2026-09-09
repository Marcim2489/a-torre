using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RoomsGenerator : MonoBehaviour
{
    [SerializeField]int amountOfRooms = 15;
    [SerializeField]int horizontalMapSize = 6;
    [SerializeField]int verticalMapSize = 6;
    [SerializeField]Vector2 roomSize = new Vector2(21,15);
    [SerializeField]Room roomPlaceholder;
    [SerializeField]Transform bossPlaceholder;
    [SerializeField]InputAction restart;
    [SerializeField]Transform player;
    List<Vector2> existingRooms;
    List<Vector2> availableRooms;
    List<Vector2> usedRooms;

    Vector2 initialRoom;
    Vector2 bossRoom;

    bool canRestart = false;

    void Awake()
    {
        GenerateMap();
    }

    void Start()
    {
        InstantiateRooms();
        restart.started += RestartScene;
        restart.Enable();
        player.position = initialRoom * roomSize;
        bossPlaceholder.position = bossRoom * roomSize;
    }

    void RestartScene(InputAction.CallbackContext context)
    {
        if (canRestart == false)
        {
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (canRestart)
        {
            return;
        }
        if (restart.IsPressed() == false)
        {
            canRestart = true;
        }
    }

    void InstantiateRooms()
    {
        foreach(Vector2 roomCoord in usedRooms)
        {
            Room room = Instantiate(roomPlaceholder);
            room.transform.position = roomCoord * roomSize;
            bool[] r = new bool[4];
            r[0] = !usedRooms.Contains(roomCoord + Vector2.right);
            r[1] = !usedRooms.Contains(roomCoord + Vector2.left);
            r[2] = !usedRooms.Contains(roomCoord + Vector2.up);
            r[3] = !usedRooms.Contains(roomCoord + Vector2.down);
            room.Setup(r, roomCoord);
        }
    }

    void GenerateMap()
    {
        existingRooms = new List<Vector2>(horizontalMapSize * verticalMapSize);
        availableRooms = new List<Vector2>();
        usedRooms = new List<Vector2>();
        List<Vector2> possibleInitialRooms = new List<Vector2>(horizontalMapSize);
        for(int i = 0; i < horizontalMapSize; i++)
        {
            for(int j = 0; j < verticalMapSize; j++)
            {
                Vector2 coord = new Vector2(i,j);
                if(j == 0)
                {
                    possibleInitialRooms.Add(coord);
                }
                existingRooms.Add(coord);
            }
        }
        
        initialRoom = possibleInitialRooms[Random.Range(0, possibleInitialRooms.Count)];
        AddCoord(initialRoom);

        while(usedRooms.Count < amountOfRooms)
        {
            AddCoord(availableRooms[Random.Range(0, availableRooms.Count)]);
        }

        Vector2 bossCoord = new Vector2(-1,-1);
        float distanceToStart = 0f;

        foreach(Vector2 coord in availableRooms)
        {
            float dist = (coord - initialRoom).magnitude;
            if (dist > distanceToStart)
            {
                // Debug.Log($"nova boss room {coord} -- {dist}");
                distanceToStart = dist;
                bossCoord = coord;
            }
        }

        if (bossCoord == new Vector2(-1, -1))
        {
            Debug.Log("bruh");
            bossCoord = availableRooms[Random.Range(0, availableRooms.Count)];
        }

        bossRoom = bossCoord;
        // Debug.Log(bossRoom);
        AddCoord(bossRoom);
    }

    void AddCoord(Vector2 room)
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

        Vector2 salaDireita = room + Vector2.right;
        Vector2 salaEsquerda = room + Vector2.left;
        Vector2 salaCima = room + Vector2.up;
        Vector2 salaBaixo = room + Vector2.down;

        if(availableRooms.Contains(salaDireita) == false && existingRooms.Contains(salaDireita))
        {
            availableRooms.Add(salaDireita);
        }
        if(availableRooms.Contains(salaEsquerda) == false && existingRooms.Contains(salaEsquerda))
        {
            availableRooms.Add(salaEsquerda);
        }
        if(availableRooms.Contains(salaCima) == false && existingRooms.Contains(salaCima))
        {
            availableRooms.Add(salaCima);
        }
        if(availableRooms.Contains(salaBaixo) == false && existingRooms.Contains(salaBaixo))
        {
            availableRooms.Add(salaBaixo);
        }
    }

}
