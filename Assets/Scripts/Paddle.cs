using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float scrn = 16f;
    // Start is called before the first frame update

    //cached reff
    GameStatus theGameStatus;
    Ball theBall;

    void Start()
    {
        theGameStatus = FindObjectOfType<GameStatus>();
        theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        //float mousePosInUnit = Input.mousePosition.x / Screen.width * scrn;

        /* if(Input.mousePosition.x / Screen.width * scrn < 1f && Input.mousePosition.x / Screen.width * scrn > 15f)
         {
             mousePosInUnit = Input.mousePosition.x / Screen.width * scrn;
         }
         else
         {
             mousePosInUnit = transform.position.x;
         }
        */
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (theGameStatus.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * scrn;
        }
    }
}

