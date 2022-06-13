using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Cntrl : MonoBehaviour
{
    GameObject Card;
    int nCard = 0;
    public SpriteRenderer sprite;
    Sprite A_H;

    public Sprite[] carta;


    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            nCard = (nCard + 1) % carta.Length;
            sprite.sprite = carta[nCard];
        }

    }
}
