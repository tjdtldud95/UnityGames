using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 100f * Time.deltaTime));
    }
}
