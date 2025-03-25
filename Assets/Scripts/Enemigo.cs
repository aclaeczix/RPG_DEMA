using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public static int vidaEnemigo = 1;

    void Start()
    {
        vidaEnemigo = 1;
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Player")
        {
            obj.transform.GetComponentInChildren<VidasPlayer>().TomarDa�o(1);
        }
    }

    public void TomarDa�o(int da�o)
    {
        vidaEnemigo -= da�o;
        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
        }
    }

}
