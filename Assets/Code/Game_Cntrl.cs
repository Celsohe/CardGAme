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
//carta[] 
//{
//       A_H,
//        //"K_H","Q_H","J_H","K_H",
//        //"A_S","K_S","Q_S","J_S","K_S",
//        //"A_D","K_D","Q_D","J_D","K_D",
//        //"A_C","K_C","Q_C","J_C","K_C",
//        //"Back",
//};


    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            nCard = (nCard + 1) % carta.Length;
            sprite.sprite = carta[nCard];
        }

    }
}
