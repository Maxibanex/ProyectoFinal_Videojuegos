using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Generator : MonoBehaviour
{
    Queue<Transform> elements;
    public Pool itemsPool;
    public int Quantity = 25;
    public float Displace = 15f;
    float currentDisplace;
    public float speed;
    public float increment=0.01f;
    int moved = 0;

    Vector3 originalPos;
    public Vector3 offset;
    public Vector3 direction;
    Transform tr;

    private void Update()
    {
        tr.position += direction * speed * Time.deltaTime;
        currentDisplace = Mathf.Abs(Vector3.Distance(tr.position,originalPos));
        var timesToInfinite = currentDisplace / Displace;
        if (timesToInfinite > moved+2) 
            ToInfinite();
        speed += Time.deltaTime * increment;
       }

   

    private void OnEnable()
    {
        itemsPool.Initializate();
        tr = transform;
        originalPos = tr.position;
        elements= new Queue<Transform>();
        for (int i = 0; i < Quantity; i++)
        {
            
            var elementTransform=itemsPool.GetRandom();
            elementTransform.position = offset - direction * Displace * i;
            elementTransform.gameObject.SetActive(true);
            elements.Enqueue(elementTransform);
            
        }
    }

    public void ToInfinite()
    {
        var last = elements.LastOrDefault();
        var tel = elements.Dequeue();
        tel.gameObject.SetActive(false);

        var elementTransform = itemsPool.GetRandom();
        elementTransform.position = last.position - direction * Displace;
        elementTransform.gameObject.SetActive(true);
        elements.Enqueue(elementTransform);
        moved++;
    }
}
