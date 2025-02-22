using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderFumadoLevel : MonoBehaviour
{

    public float fumadoAmount;
    private float prevFumadoAmount;
    public int fumadoLevel;

    private void Update()
    {
        if (fumadoAmount != prevFumadoAmount)
        {
            if (fumadoAmount > 80)
            {
                fumadoLevel = 3;
                prevFumadoAmount = fumadoAmount;
                Debug.Log("Level 3");
                return;
            }
            else if (fumadoAmount > 40)
            {
                fumadoLevel = 2;
                prevFumadoAmount = fumadoAmount;
                Debug.Log("Level 2");
                return;
            }
            else if (fumadoAmount > 20)
            {
                fumadoLevel = 1;
                prevFumadoAmount = fumadoAmount;
                Debug.Log("Level 1");
                return;
            }
        }
    }


}
