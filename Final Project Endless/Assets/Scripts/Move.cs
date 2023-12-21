using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float Horizontal;
    public float MaxLeft,MaxRight;
    public float MaxBack,MaxFor;
    public float Speed;
    internal Transform tr;
    float yOriginal;
    private void Awake()
    {
        tr = transform;
        yOriginal = tr.position.y;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal += Input.GetAxis("Horizontal")*Speed*Time.deltaTime;
        Horizontal=Mathf.Clamp(Horizontal, MaxLeft, MaxRight);
        tr.position=new Vector3(Horizontal, yOriginal, 0);
    }
}
