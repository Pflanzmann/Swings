using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class FieldScript : MonoBehaviour
{
    public List<CollumeScript> collums = new List<CollumeScript>();
    [SerializeField] GameObject balancePrefab;

    public static FieldScript instance;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        SpawnBalances();
        SetBalanceNumberText();
    }

    private void SetBalanceNumberText()
    {
        GameObject temp;

        for (int i = 0; i < 8; i++)
        {
            collums[i].Index = i;
            collums[i].GetComponent<SpriteRenderer>().sprite = SpriteHolder.instance.balanceSprites[i];
            collums[i].SetBalancePosition(1, 0);

            temp = collums[i].GetComponentInChildren<TextMeshPro>().gameObject;
            temp.transform.SetParent(StorageManagerScript.instance.numberStoreTransform);
            temp.transform.position = new Vector3(i * 2 - .5f, -1.5f, -1);
            collums[i].SetWeightDisplay(temp);
        }
    }

    public void SpawnBalances()
    {
        for (int i = 0; i < 8; i++)
        {
            collums.Add(Instantiate(balancePrefab, StorageManagerScript.instance.balanceStoreTransform).GetComponent<CollumeScript>());
        }
    }

    public void SetBall(BallScript ball, int collume)
    {
        collums[collume].AddBall(ball);
    }

    public void SetBalances()
    {
        for (int i = 0, j = 7; i < 4; i++, j--)
        {
            collums[i].CalculateValue();
            collums[j].CalculateValue();

            if (collums[i].Value < collums[j].Value)
            {
                int momentum = collums[j].Value - collums[i].Value;
                collums[i].SetBalancePosition(2, momentum);
                collums[j].SetBalancePosition(0, 0);
            }
            else if (collums[i].Value == collums[j].Value)
            {
                collums[i].SetBalancePosition(1, 0);
                collums[j].SetBalancePosition(1, 0);
            }
            else
            {
                int momentum = collums[i].Value - collums[j].Value;
                collums[i].SetBalancePosition(0, 0);
                collums[j].SetBalancePosition(2, momentum);
            }
        }

        CheckWeight();
    }

    public void CheckTrippel()
    {
        int tempOffset;
        int tempColor1;
        int tempColor2;
        int tempColor3;

        for (int collume = 0; collume < 6; collume++)
        {
            for (int row = 0; row < collums[collume].Lenght; row++)
            {
                tempOffset = collums[collume].Offset;
                tempColor1 = collums[collume + 0].ReturnColor(row, tempOffset);
                tempColor2 = collums[collume + 1].ReturnColor(row, tempOffset);
                tempColor3 = collums[collume + 2].ReturnColor(row, tempOffset);

                if (tempColor1 != 0 && tempColor2 != 0 && tempColor3 != 0 &&
                    ((tempColor1 == tempColor2 || tempColor2 == -1) &&
                    (tempColor1 == tempColor3 || tempColor3 == -1) ||
                    ((tempColor2 == tempColor1 || tempColor1 == -1) &&
                    (tempColor2 == tempColor3 || tempColor3 == -1)) ||
                    ((tempColor3 == tempColor1 || tempColor1 == -1) &&
                    (tempColor3 == tempColor2 || tempColor2 == -1))))
                {
                    if (tempColor1 == -1)
                    {
                        tempColor1 = tempColor2;
                        collume = collume + 1;
                    }
                    if (tempColor1 == -1 && tempColor2 == -1)
                    {
                        tempColor1 = tempColor3;
                        collume = collume + 1;
                    }

                    FindConnected(collume, row, tempColor1, tempOffset);
                    return;
                }
            }
        }
    }

    public void FindConnected(int collume, int row, int color, int offset)
    {
        Queue<Vector3Int> qBallsToDelete = new Queue<Vector3Int>();
        List<Vector3Int> isTestedVector3 = new List<Vector3Int>();

        qBallsToDelete.Enqueue(new Vector3Int(collume, row, offset));

        //iterate queue
        do
        {
            Vector3Int vec = qBallsToDelete.Dequeue();

            if (!isTestedVector3.Contains(vec))
            {
                int tempColor = collums[vec.x].ReturnColor(vec.y, vec.z);

                //Check before
                if (vec.x > 0)
                {
                    if (collums[vec.x - 1].ReturnColor(vec.y, vec.z) == color || collums[vec.x - 1].ReturnColor(vec.y, vec.z) == -1)
                    {
                        qBallsToDelete.Enqueue(new Vector3Int(vec.x - 1, vec.y, vec.z));
                    }
                }

                //Check after
                if (vec.x < 7)
                {
                    if (collums[vec.x + 1].ReturnColor(vec.y, vec.z) == color || collums[vec.x + 1].ReturnColor(vec.y, vec.z) == -1)
                    {
                        qBallsToDelete.Enqueue(new Vector3Int(vec.x + 1, vec.y, vec.z));
                    }
                }

                //Check up
                if (vec.y < collums[vec.x].Lenght + 1)
                {
                    if (collums[vec.x].ReturnColor(vec.y + 1, vec.z) == color || collums[vec.x].ReturnColor(vec.y + 1, vec.z) == -1)
                    {
                        qBallsToDelete.Enqueue(new Vector3Int(vec.x, vec.y + 1, vec.z));
                    }
                }

                //Check down
                if (vec.y + vec.x > -1)
                {
                    if (collums[vec.x].ReturnColor(vec.y - 1, vec.z) == color || collums[vec.x].ReturnColor(vec.y - 1, vec.z) == -1)
                    {
                        qBallsToDelete.Enqueue(new Vector3Int(vec.x, vec.y - 1, vec.z));
                    }
                }

                isTestedVector3.Add(vec);
            }
        }
        while (qBallsToDelete.Count > 0);

        isTestedVector3 = isTestedVector3.OrderBy(v => v.y).ToList();
        isTestedVector3.Reverse();

        foreach (Vector3Int item in isTestedVector3)
        {
            collums[item.x].DeleteBallInstant(item.y, item.z);
        }

        BallManagerScript.instance.CheckZeroMovement();
    }

    public void CheckWeight()
    {
        int weight = GrabberScript.instance.currentBall.Value;

        for (int i = 0; i < 4; i++)
        {
            int counterCollume = 7 - i;

            if ((collums[i].Offset > 0 && collums[i].Value + weight >= collums[counterCollume].Value))
            {
                collums[i].shakeAnimation.wrapMode = WrapMode.Loop;
                collums[i].shakeAnimation.Play();
            }
            else
            {
                collums[i].shakeAnimation.wrapMode = WrapMode.Once;
            }
            if (collums[counterCollume].Offset > 0 && collums[counterCollume].Value + weight >= collums[i].Value)
            {
                collums[counterCollume].shakeAnimation.wrapMode = WrapMode.Loop;
                collums[counterCollume].shakeAnimation.Play();
            }
            else
            {
                collums[counterCollume].shakeAnimation.wrapMode = WrapMode.Once;
            }
        }
    }

    public Vector3Int FindBallInField(BallScript ball)
    {
        Vector3Int pos;

        for (int i = 0; i < collums.Count; i++)
        {
            pos = collums[i].GetBallIndex(ball);

            if (pos.y != -1)
            {
                pos.x = i;
                return pos;
            }
        }

        return new Vector3Int(-1, -1, -1);
    }
}
