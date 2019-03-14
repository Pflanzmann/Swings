using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManagerScript : MonoBehaviour
{
    [SerializeField] GameObject particlePrefab;
    [SerializeField] Transform particleStoreTransform;
    Queue<ParticleSystem> particleStorage = new Queue<ParticleSystem>();

    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform ballStoreTransform;
    Queue<BallScript> ballStorage = new Queue<BallScript>();

    public Transform balanceStoreTransform;
    public Transform numberStoreTransform;

    Vector2 storePosition = new Vector2(10, 20);

    public static StorageManagerScript instance;
    [SerializeField] Sprite standartSprite;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    public BallScript GetBall()
    {
        if (ballStorage.Count == 0)
        {
            return Instantiate(ballPrefab, ballStoreTransform).GetComponent<BallScript>();
        }

        BallScript ball = ballStorage.Dequeue();
        ball.gameObject.SetActive(true);
        return ball;
    }

    public void SetBall(BallScript ball)
    {
        if (ballStorage.Contains(ball))
            return;

        ball.gameObject.SetActive(false);

        ball.activated = false;
        ball.standby = false;
        ball.done = false;
        ball.ability = null;

        ball.sprite.sprite = standartSprite;
        ball.transform.position = storePosition;
        ball.transform.SetParent(ballStoreTransform);
        ballStorage.Enqueue(ball);
    }

    public ParticleSystem GetParticle()
    {
        if (particleStorage.Count == 0)
        {
            return Instantiate(particlePrefab, particleStoreTransform).GetComponent<ParticleSystem>();
        }

        ParticleSystem ps = particleStorage.Dequeue();
        ps.gameObject.SetActive(true);
        return ps;
    }

    public void SetParticle(ParticleSystem ps)
    {
        if (particleStorage.Contains(ps))
            return;


        ps.gameObject.SetActive(false);
        particleStorage.Enqueue(ps);
    }
}
