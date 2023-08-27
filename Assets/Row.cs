using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;

    public bool rowStopped;
    public string stoppedSlot;

    // Start is called before the first frame update
    void Start()
    {
        rowStopped = true;
        GameControl.HandlePulled += StartRotating;
    }

    private void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine("Rotate");
    }

    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.025f;

        // constant portion of spinning
        for (int i = 0; i < 30; i++)
        {
            // if row is at the bottom, move it to the top
            if (transform.localPosition.y <= -3.5f)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, 1.75f);
            }

            // move row down
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 0.25f);

            yield return new WaitForSeconds(timeInterval);
        }

        // get random value between 60 and 100
        randomValue = Random.Range(60, 100);

        // ensure randomValue is divisible by 3
        // there are three steps between each slot
        if (randomValue % 3 == 1)
        {
            randomValue += 2;
        }
        else if (randomValue % 3 == 2)
        {
            randomValue += 1;
        }

        // final spin, slowing down as it reaches its final destination
        for (int i = 0; i < randomValue; i++)
        {
            // if row is at the bottom, move it to the top
            if (transform.localPosition.y <= -3.5f)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, 1.75f);
            }

            // move row down
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 0.25f);

            // manipulate timeInterval to slow down spin
            // as i gets closer to randomValue, timeInterval increases
            if (i < Mathf.RoundToInt(randomValue * 0.25f))
            {
                // i is 0% to 25% of randomValue
                timeInterval = 0.05f;
            }
            else if (i < Mathf.RoundToInt(randomValue * 0.5f))
            {
                // i is 25% to 50% of randomValue
                timeInterval = 0.1f;
            }
            else if (i < Mathf.RoundToInt(randomValue * 0.75f))
            {
                // i is 50% to 75% of randomValue
                timeInterval = 0.15f;
            }
            else if (i < Mathf.RoundToInt(randomValue * 0.95f))
            {
                // i is 75% to 95% of randomValue
                timeInterval = 0.2f;
            }
            else
            {
                // i is 95% to 100% of randomValue
                timeInterval = 0.25f;
            }

            yield return new WaitForSeconds(timeInterval);
        }

        // calculate currently selected slot based on y position
        switch (transform.localPosition.y)
        {
            case -3.5f:
                stoppedSlot = "Diamonds";
                break;
            case -2.75f:
                stoppedSlot = "Crown";
                break;
            case -2f:
                stoppedSlot = "Melon";
                break;
            case -1.25f:
                stoppedSlot = "Bar";
                break;
            case -0.5f:
                stoppedSlot = "Seven";
                break;
            case 0.25f:
                stoppedSlot = "Cherry";
                break;
            case 1f:
                stoppedSlot = "Lemon";
                break;
            case 1.75f:
                stoppedSlot = "Diamond";
                break;
        }

        rowStopped = true;
    }

    private void OnDestroy()
    {
        GameControl.HandlePulled -= StartRotating;
    }
}
