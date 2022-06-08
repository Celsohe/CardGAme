using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Cntrl : MonoBehaviour
{
    GameObject Card;
    Sprite sprite;
    public string[] carta = new string[]
{
        "A_H","K_H","Q_H","J_H","K_H",
        "A_S","K_S","Q_S","J_S","K_S",
        "A_D","K_D","Q_D","J_D","K_D",
        "A_C","K_C","Q_C","J_C","K_C",
        "Back",
};

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(carta[1]);
        sprite = Card.GetComponent<Sprite>();
        sprite.name = carta[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
