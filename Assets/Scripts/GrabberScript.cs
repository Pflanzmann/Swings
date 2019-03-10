using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberScript : MonoBehaviour
{
    Camera mainCamera;

    public BallScript currentBall;

    public static GrabberScript instance;
    float time = 0;
    bool canDrop = true;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        mainCamera = Camera.main;

        //Get First Ball
        currentBall = StorageManagerScript.instance.GetBall();
        currentBall.transform.SetParent(transform);
        SpawnerScript.instance.PopulateBall(currentBall);
        currentBall.transform.position = transform.position;
    }

    void Update()
    {
        GrabberAction();
        GrabberMovement();
        GrabberKeyMovement();

        if (time < 0.3f)
        {
            time += Time.deltaTime;
        }
        else
            canDrop = true;
    }

    private void GrabberMovement()
    {
        if (Input.GetMouseButton(0) && canDrop)
        {
            //Break if out of playground
            Vector2 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (position.x < -1 || position.x > 15 || position.y < -1.5f || position.y > 11)
                return;

            position = new Vector2(Mathf.Clamp(position.x, 0, 14), 8.5f);
            transform.position = position;
        }
    }

    private void GrabberAction()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && (Input.GetAxisRaw("Vertical") < 0 || Input.GetMouseButtonUp(0)) && canDrop && BallManagerScript.instance.Count == 0 && BallManagerScript.instance.checkFinished)
        {
            time = 0;
            canDrop = false;
            Vector2 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            //Break if out of playground
            if ((position.x < -1 || position.x > 15) && !(Input.GetAxisRaw("Vertical") < 0) || position.y < -1.5f || position.y > 11)
                return;

            //set position of grabber
            transform.position = new Vector2(Mathf.RoundToInt(transform.position.x / 2) * 2, 8.5f);

            //Check for null in Grabber and Set Ball in Field
            if (currentBall != null)
            {
                FieldScript.instance.SetBall(currentBall, (int)transform.position.x / 2);
            }

            //set current ball from spwaner and spawns new ball
            currentBall = SpawnerScript.instance.MoveDown((int)transform.position.x / 2);

            //Moves Ball to Grabber
            BallManagerScript.instance.AddBallsMovement(
                currentBall,
                new Vector2(
                    transform.position.x / 2,
                    transform.position.y));

            //Add Grabber as parent for ball
            currentBall.transform.SetParent(transform);

            //++ballCount
            StatusController.instance.AddFallenBalls();
        }
    }

    public void GrabberKeyMovement()
    {
        if (Input.anyKeyDown && Input.GetAxis("Horizontal") != 0 && Input.GetAxisRaw("Vertical") == 0 && canDrop)
        {
            Vector2 position = transform.position;
            position = new Vector2(Mathf.Clamp(transform.position.x + (2 * Input.GetAxisRaw("Horizontal")), 0, 14), 8.5f);
            transform.position = position;
        }
    }
}
