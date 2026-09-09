using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]GameObject[] portoes = new GameObject[4];

    bool[] ativacaoPortoes = new bool[4];

    Vector2 roomCoord;

    public void Setup(bool[] portoesParaFechar, Vector2 coord)
    {
        roomCoord = coord;
        ativacaoPortoes = portoesParaFechar;
        OpenGates();
    }

    public void PlayerEntered(Vector2 coord)
    {
        if (roomCoord != coord)
        {
            return;
        }
        CloseAllGates();
    }

    public void DefeatedAllEnemies(Vector2 coord)
    {
        if (roomCoord != coord)
        {
            return;
        }
        OpenGates();
    }

    void CloseAllGates()
    {
        for(int i = 0; i < 4; i++)
        {
            portoes[i].SetActive(true);
        }
    }

    void OpenGates()
    {
        for(int i = 0; i < 4; i++)
        {
            portoes[i].SetActive(ativacaoPortoes[i]);
        }
    }
}
