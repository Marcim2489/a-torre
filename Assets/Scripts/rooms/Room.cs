using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]GameObject[] paredesTampadoras = new GameObject[4];
    [SerializeField]GameObject[] corredores = new GameObject[4];
    [SerializeField]Door[] portas = new Door[4];
    [SerializeField]GameObject[] bloqueadoresDePortas = new GameObject[4];
    [SerializeField]CharacterHealthManager inimigo;


    Vector2 roomCoord;

    int amountOfEnemies;

    public void Setup(bool[] portoesParaFechar, Vector2 coord)
    {
        roomCoord = coord;
        foreach (Door porta in portas)
        {
            porta.RoomCoord = roomCoord;
        }
        CreateRoomWallsAndCorridors(portoesParaFechar);
        RoomsManager.Instance.roomChanged += PlayerEntered;
        RoomsManager.Instance.roomTransitionStarted += DisableDoors;
    }

    void OnDestroy()
    {
        if (RoomsManager.Instance != null)
        {
            RoomsManager.Instance.roomTransitionStarted -= DisableDoors;
        }
    }

    public void PlayerEntered(Vector2 coord)
    {
        if (roomCoord != coord)
        {
            return;
        }
        if (roomCoord != RoomsManager.Instance.InitialRoom && RoomsManager.Instance.DefeatedRooms.Contains(roomCoord) == false)
        {
            RoomsManager.Instance.roomTransitionFinished += CloseDoors;
            for(int i = 0; i < 3; i++)
            {
                CharacterHealthManager e = Instantiate(inimigo);
                e.transform.position = transform.position + i * Vector3.right*1.5f;
                e.died += EnemyKilled;
                amountOfEnemies++;
            }
        }
        else
        {
            OpenDoors();
        }
        
    }

    void EnemyKilled()
    {
        amountOfEnemies--;
        if (amountOfEnemies <= 0)
        {
            DefeatedAllEnemies();
        }
    }

    void DefeatedAllEnemies()
    {
        RoomsManager.Instance.DefeatRoom(roomCoord);
        OpenDoors();
    }

    void CloseDoors()
    {
        // Debug.Log("aa");
        RoomsManager.Instance.roomTransitionFinished -= CloseDoors;
        for(int i = 0; i < 4; i++)
        {
            bloqueadoresDePortas[i].SetActive(true);
            portas[i].gameObject.SetActive(false);
        }
    }

    void CreateRoomWallsAndCorridors(bool[] aFechar)
    {
        for(int i = 0; i < 4; i++)
        {
            bool fechar = aFechar[i];
            paredesTampadoras[i].SetActive(fechar);
            corredores[i].SetActive(!fechar);
        }
    }

    void DisableDoors()
    {
        foreach(Door porta in portas)
        {
            porta.gameObject.SetActive(false);
        }
    }

    void OpenDoors()
    {
        for(int i = 0; i < 4; i++)
        {
            bloqueadoresDePortas[i].SetActive(false);
            portas[i].gameObject.SetActive(true);
        }
    }
}
