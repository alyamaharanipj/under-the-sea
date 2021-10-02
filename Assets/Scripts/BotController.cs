using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : Utilities
{
    public float speed;
    public float increase = 0.05f;
    public Transform target;
    public Transform[] moveSpots;
    public int radius;
    private int randomSpot;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        randomSpot = Random.Range(0,moveSpots.Length);
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
        }
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if(Vector2.Distance(transform.position,target.position) < radius){
        	if(target.localScale.x < transform.localScale.x){
            	transform.position = Vector2.MoveTowards(transform.position,target.position,speed * Time.deltaTime);
            }else transform.position = Vector2.MoveTowards(transform.position,target.position,-speed * Time.deltaTime);
        }
        else transform.position = Vector2.MoveTowards(transform.position,moveSpots[randomSpot].position,speed * Time.deltaTime);
        if(Vector2.Distance(transform.position,moveSpots[randomSpot].position) < 0.2f){
            randomSpot = Random.Range(0,moveSpots.Length);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
        {
            Print("Ate food", "log");
            transform.localScale += new Vector3(increase, increase, 0);
            other.GetComponent<Food>().RemoveObject();
        }

        else if (other.gameObject.tag == "Player")
        {
        	if (transform.localScale.x > other.gameObject.transform.localScale.x){
            	Print("Ate player", "log");
            	transform.localScale = transform.localScale + (other.gameObject.transform.localScale/2);
            	Destroy(other.gameObject);
            }
        }
    }
}
