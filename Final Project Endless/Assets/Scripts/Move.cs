using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float Horizontal;
    public float MaxLeft,MaxRight;
    //public float MaxBack,MaxFor;
    public Transform Pivot;
    
    public AnimationCurve slideCurve;
    private bool Sliding = false;
    public float SlideUpDownDuration = 1f;
    public float SlideDuration = 1f;
    public float SlideScale = -90f;


    public AnimationCurve jumpCurve;
    private bool Jumping = false;
    public float JumpScale = 5f;
    public float JumpDuration = 1f;
    public float Speed;
    
    internal Transform tr;
    float yOriginal;
    float yOffset;
    float xRotation;

    
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
        if (!Jumping && !Sliding && Input.GetButtonDown("Jump"))
            StartCoroutine(fly());

        if (!Jumping && !Sliding && Input.GetKeyDown(KeyCode.DownArrow))
            StartCoroutine(slide());

        Horizontal =Mathf.Clamp(Horizontal, MaxLeft, MaxRight);
        tr.position=new Vector3(Horizontal, yOriginal + yOffset, 0);
        Pivot.rotation = Quaternion.Euler(xRotation, 0, 0);
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


    public IEnumerator slide()
    {
        Sliding = true;
        float d = 0;
        while (d < SlideUpDownDuration)
        {
            d += Time.deltaTime;
            xRotation = slideCurve.Evaluate(d / JumpDuration) * SlideScale;
            yield return null;
        }
        yield return new WaitForSeconds(SlideDuration);
        while (d > 0)
        {
            d -= Time.deltaTime;
            xRotation = slideCurve.Evaluate(d / JumpDuration) * SlideScale;
            yield return null;
        }

        Sliding = false;
    }

}
