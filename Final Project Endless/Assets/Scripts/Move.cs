using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float Horizontal;
    public float MaxLeft,MaxRight;
    //public float MaxBack,MaxFor;
    public AnimationCurve jumpCurve;

    private bool Jumping = false;
    public float JumpScale = 5f;
    public float JumpDuration = 1f;
    public float Speed;
    
    internal Transform tr;
    float yOriginal;
    float yOffset;
    
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
        if (!Jumping && Input.GetButtonDown("Jump"))
            StartCoroutine(fly());

        Horizontal=Mathf.Clamp(Horizontal, MaxLeft, MaxRight);
        tr.position=new Vector3(Horizontal, yOriginal + yOffset, 0);
    }


    public IEnumerator fly()
    {
        Jumping = true;
        float d = 0;
        while (d < JumpDuration)
        {
            d += Time.deltaTime;
            yOffset = jumpCurve.Evaluate(d / JumpDuration) * JumpScale;
            yield return null; //yield va delante de una funcion Ienumerator que devuelva null, relacionado a Coroutine, Sirve para decir que el frame acaba en esa iteracion del bucle.
        }


        Jumping = false;
    }

}
