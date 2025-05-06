using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SecondPlayer : MonoBehaviour
{

    public PlayerInputManager id_quantity; 

    public int jugadorID;

    public Image secondPlayerHUD;

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        jugadorID = id_quantity.playerCount;
        if(jugadorID == 2 )
        {
            secondPlayerHUD.enabled = true;
        }
        else
        {
            secondPlayerHUD.enabled = false;
        }
    }
}