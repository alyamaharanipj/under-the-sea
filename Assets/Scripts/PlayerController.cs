using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : Utilities
{
    public GameObject splitMass;
    public GameObject food;
    public Joystick joystick;
 

    public Button shooting;
    public Button splitting;

    public float movementSpeed = 50.0f;
    public float maxMovementSpeed = 3.0f;
    public float massSplitMultiplier = 0.5f;
    public float increase = 0.05f;
    public Vector2 movement;
    public Vector2 mouseDistance;
    public string eatSound = "EatSound";
    public string spawnSound = "SpawnSound";
    public string mergeSound = "MergeSound";
    public Text nama;


    private Rigidbody2D rigidBody2D;
    private GameManager gameManager;
    private AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        Button btn = shooting.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        Button btn2 = splitting.GetComponent<Button>();
        btn2.onClick.AddListener(TaskOnClick2);
        
        rigidBody2D = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();

        nama.text = gameManager.user.text;

        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
        }
        if (audioManager == null)
        {
            Print("No AudioManager found!", "error");
        }
    }

    // FixedUpdate is used for physics
    private void FixedUpdate()
    {
        mouseDistance.x = (Input.mousePosition.x - Camera.main.WorldToScreenPoint(gameObject.transform.position).x) * 0.005f;
        mouseDistance.y = (Input.mousePosition.y - Camera.main.WorldToScreenPoint(gameObject.transform.position).y) * 0.005f;
        //movement.x = Input.GetAxis("Horizontal") + mouseDistance.x;
        //movement.y = Input.GetAxis("Vertical") + mouseDistance.y;
        //movement.x = Mathf.Clamp(movement.x, -maxMovementSpeed, maxMovementSpeed);
        //movement.y = Mathf.Clamp(movement.y, -maxMovementSpeed, maxMovementSpeed);
        //joystick
        movement.x = joystick.Horizontal + mouseDistance.x ;
        movement.y = joystick.Vertical + mouseDistance.y;
        movement.x = Mathf.Clamp(movement.x, -maxMovementSpeed, maxMovementSpeed);
        movement.y = Mathf.Clamp(movement.y, -maxMovementSpeed, maxMovementSpeed);
        rigidBody2D.velocity = movement * movementSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    public void Update()
    {
        /*if ((joystick2.Horizontal>0.05)||(joystick2.Vertical>0.05))
        {
            if (transform.localScale.x * massSplitMultiplier >= 1.0f)
            {
                audioManager.PlaySound(mergeSound);
                transform.localScale = transform.localScale * massSplitMultiplier;
                GameObject newSplitMass = Instantiate(splitMass, transform.position + new Vector3(-0.6f+transform.position.x, 0.8f+transform.position.y, 0), transform.rotation) as GameObject;
                newSplitMass.transform.localScale = transform.localScale;
            }

            else
            {
                Print("Can't split mass!", "log");
            }
        }
        if ((joystick3.Horizontal>0.05)||(joystick3.Vertical>0.05))
        {
            if (gameManager.currentScore >= 10)
            {
                transform.localScale -= new Vector3(increase, increase, 0) ;
                GameObject newFood = Instantiate(food, transform.position + new Vector3(( 0.2f*transform.position.x), (0.2f*transform.position.y), 0), transform.rotation) as GameObject;
                newFood.transform.localScale = transform.localScale * massSplitMultiplier;
                gameManager.ChangeScore(-10);
            }
            else
            {
                Print("Can't split mass!", "log");
            }
    }*/
}
    
    void TaskOnClick2()
    {
          if (transform.localScale.x * massSplitMultiplier >= 1.0f)
            {
                audioManager.PlaySound(mergeSound);
                transform.localScale = transform.localScale * massSplitMultiplier;
                GameObject newSplitMass = Instantiate(splitMass, transform.position + new Vector3(-0.2f*transform.position.x, 0.2f*transform.position.y, 0), transform.rotation) as GameObject;
                newSplitMass.transform.localScale = transform.localScale;

            }
            else
            {
                Print("Can't split mass!", "log");
            }
        
    }

    void TaskOnClick(){
         if (gameManager.currentScore >= 10)
            {
                transform.localScale -= new Vector3(increase, increase, 0) ;
                GameObject newFood = Instantiate(food, transform.position + new Vector3(( 0.2f*transform.position.x), (0.2f*transform.position.y), 0), transform.rotation) as GameObject;
                newFood.transform.localScale = transform.localScale * massSplitMultiplier;
                gameManager.ChangeScore(-10);
            }
            else
            {
                Print("Can't split mass!", "log");
            }
    }
    
    


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
        {
            Print("Ate food", "log");
            audioManager.PlaySound(eatSound);
            transform.localScale += new Vector3(increase, increase, 0) ;
            other.GetComponent<Food>().RemoveObject();
            gameManager.ChangeScore(10);
            gameManager.ChangeEatenFood(1);
        }
        else if (other.gameObject.tag == "SplitMass")
        {
            Print("Collided with mass", "log");
            audioManager.PlaySound(mergeSound);
            transform.localScale = transform.localScale + other.gameObject.transform.localScale;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Bot")
        {
            if (transform.localScale.x > other.gameObject.transform.localScale.x){
                Print("Ate bot", "log");
                transform.localScale = transform.localScale + (other.gameObject.transform.localScale/2);
                Destroy(other.gameObject);
            }
        }
    }

    public void skill1()
    {
            if (gameManager.currentScore >= 10)
            {
                transform.localScale -= new Vector3(increase, increase, 0) ;
                GameObject newFood = Instantiate(food, transform.position + new Vector3(( 2f), (0.8f), 0), transform.rotation) as GameObject;
                newFood.transform.localScale = transform.localScale * massSplitMultiplier;
                gameManager.ChangeScore(-10);
            }
            else
            {
                Print("Can't split mass!", "log");
            }
    }
}
