using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtensionTester : MonoBehaviour
{
    [SerializeField] 
    private Transform other;

    void Start()
    {
        Color color = Color.red;
        Debug.Log(color.ToHex());

        transform.position = transform.position.With(y:0, x:1);

        Debug.Log(transform.position.DirectionTo(other.position));
    }
}
