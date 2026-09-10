using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]GameObject[] portoesBloqueadores = new GameObject[4];
    [SerializeField]Door[] portas = new Door[4];
    [SerializeField]CharacterHealthManager inimigo;

    bool[] ativacaoPortoes = new bool[4];

    Vector2 roomCoord;

    int amountOfEnemies;

    public void Setup(bool[] portoesParaFechar, Vector2 coord)
    {
        roomCoord = coord;
        foreach (Door porta in portas)
        {
            porta.RoomCoord = roomCoord;
        }
        ativacaoPortoes = portoesParaFechar;
        OpenGates();
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
            // CloseAllGates();
            RoomsManager.Instance.roomTransitionFinished += CloseAllGates;
            // foreach(Door porta in portas)
            // {
            //     porta.gameObject.SetActive(false);
            // }
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
            EnableDoors();
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
        // if (roomCoord != coord)
        // {
        //     return;
        // }
        RoomsManager.Instance.DefeatRoom(roomCoord);
        OpenGates();
        EnableDoors();
    }

    void CloseAllGates()
    {
        // Debug.Log("aa");
        RoomsManager.Instance.roomTransitionFinished -= CloseAllGates;
        for(int i = 0; i < 4; i++)
        {
            portoesBloqueadores[i].SetActive(true);
        }
    }

    void OpenGates()
    {
        for(int i = 0; i < 4; i++)
        {
            portoesBloqueadores[i].SetActive(ativacaoPortoes[i]);
        }
    }

    void DisableDoors()
    {
        foreach(Door porta in portas)
        {
            porta.gameObject.SetActive(false);
        }
    }

    void EnableDoors()
    {
        foreach(Door porta in portas)
        {
            porta.gameObject.SetActive(true);
        }
    }
}
