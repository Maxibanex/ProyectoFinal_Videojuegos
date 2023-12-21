using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Pool itemsPool;
    public Pool SafeitemsPool;

    Queue<Transform> elements;
    
    public float increment = 0.01f;
    public float Speed = 10;
    private float speed;
    public Vector3 direction;
    public Vector3 offset;
    public int Quantity = 25;
    public int SafeQuantity = 3;
    public float Displace = 15f;

    Transform tr;
    Vector3 originalPos;
    float currentDisplace;
    int moved = 0;


    private void Update()
    {
        tr.position += direction * speed * Time.deltaTime;
        currentDisplace = Mathf.Abs(Vector3.Distance(tr.position,originalPos));
        var timesToInfinite = currentDisplace / Displace;
        if (timesToInfinite > moved+2) 
            ToInfinite();
        speed += Time.deltaTime * increment;
       }


    private void Awake()
    {
        tr = transform;
        itemsPool.Initializate();
        SafeitemsPool.Initializate();
        originalPos = tr.position;
        elements = new Queue<Transform>();
        
    }


    public void Clean()
    {
        while (elements.Any())
        {
            elements.Dequeue().gameObject.SetActive(false);
        }
    }

    public void Generate()
    {
        tr.position = originalPos;
        speed = Speed;
        currentDisplace = 0;
        moved = 0;
        for (int i = 0; i < Quantity; i++)
        {

            var elementTransform = i < SafeQuantity ? SafeitemsPool.GetRandom() : itemsPool.GetRandom();
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

        var gems = elementTransform.GetComponentsInChildren<Gem>();
        foreach (var g in gems)
            g.gameObject.SetActive(true);
        
        elements.Enqueue(elementTransform);

        Scores.Instance.current.km++;
        moved++;
    }
}
