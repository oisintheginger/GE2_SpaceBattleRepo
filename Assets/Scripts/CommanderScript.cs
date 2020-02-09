using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommanderScript : MonoBehaviour
{
    Animator commanderAnimator;
    ManagerScript gameManager;
    genericMovementScript commanderMovement;
    [SerializeField] List<Transform> CommanderPositions;
    [SerializeField] int currentStage;
    [SerializeField] float timer =0;
    // Start is called before the first frame update
    private void Awake()
    {
        currentStage = 0;
        commanderMovement = this.GetComponent<genericMovementScript>();
        commanderAnimator = this.GetComponent<Animator>();
        gameManager = FindObjectOfType<ManagerScript>();

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StateChecker();
        Debug.Log("Current Stage = " + currentStage);
    }

    void StateChecker()
    {
        //Stage 1: Walking to Shout Position
        switch(currentStage)
        {
            case 0:
                commanderAnimator.SetBool("Moving", true);
                commanderMovement.target.transform.position = CommanderPositions[0].position;
                if (Vector3.Distance(this.gameObject.transform.position, commanderMovement.target.transform.position) < 1)
                {
                    commanderAnimator.SetBool("Moving", false);
                    Timer(5f);
                }
                break;

            case 1:
                Timer(5f);
                commanderAnimator.SetTrigger("Yell");
                break;

            case 2:
                commanderAnimator.SetTrigger("Salute");
                gameManager.AlertPilots = true;
                Timer(2f);
                break;

            case 3:
                commanderAnimator.SetBool("Moving", true);
                
                gameManager.camerasList[0].SetActive(false);
                gameManager.camerasList[1].SetActive(true);
                commanderMovement.target.transform.position = CommanderPositions[1].position;
                
                Timer(8f);
                
                break;

            case 4:
                gameManager.camerasList[1].SetActive(false);
                gameManager.camerasList[3].SetActive(true);
                Timer(10f);
                break;

            case 5:
                gameManager.camerasList[3].SetActive(false);
                gameManager.camerasList[2].SetActive(true);
                if(gameManager.camerasList[2].transform.parent.transform.position.y>30f)
                {
                    Timer(0.5f);
                }
                break;

            case 6:
                commanderAnimator.SetBool("Moving", false);
                
                gameManager.camerasList[2].SetActive(false);
                gameManager.camerasList[4].SetActive(true);
                Timer(2f);
                break;
            case 7: 
                commanderAnimator.SetTrigger("Salute");
                Timer(3f);
                break;

            case 8:
                SceneManager.LoadScene("Model Scene");
                break;
        }
    }

    public void AdvanceStage()
    {
        currentStage++;
    }
    

    void Timer(float timeToWait)
    {
        
        timer += Time.deltaTime;
        if (timer >= timeToWait)
        {
            timer = 0;
            AdvanceStage();
        }
        
    }
}
