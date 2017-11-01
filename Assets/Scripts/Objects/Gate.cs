using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    [SerializeField]
    float deltaY;
    [SerializeField]
    float speed;
    [SerializeField]
    int damage;

    bool isOpen;
    Vector3 difference;
    


    private void Awake()
    {
        isOpen = false;
        difference = new Vector3(0.0f, deltaY, 0.0f);
    }

    public void OpenGate()
    {
        if (!isOpen)
        {
            transform.position = transform.position + difference;
            isOpen = true;
            Debug.Log(isOpen);
        }
        //Vector3.Lerp(transform.position, transform.position - difference, speed * Time.deltaTime);
    }

    public void CloseGate()
    {
        if (isOpen)
        {
            transform.position = transform.position - difference;
            isOpen = false;
            Debug.Log(isOpen);
        }
        //Vector2.Lerp(transform.position, newPosition, speed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            col.gameObject.GetComponent<WeebPlayer>().TakeDamage(damage);
        }
    }
}
