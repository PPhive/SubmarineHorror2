using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesOpening : MonoBehaviour
{
    public float MaxCountdown;
    public Sprite[] Sprites;
    public SpriteRenderer MyRenderer;

    public int stage;

    public float timer;

    public bool IsOnUI;
    

    void Start()
    {
        MyRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        stage = Mathf.FloorToInt(timer/(MaxCountdown / (Sprites.Length)));
        if (stage < Sprites.Length)
        {
            MyRenderer.sprite = Sprites[stage];
        }

        if (IsOnUI && timer < MaxCountdown)
        {

            MyRenderer.color = new Color(1, 0, 0, timer / MaxCountdown);
            Debug.Log(MyRenderer.color.a);
        }
        else
        {
            MyRenderer.color = new Color(1, 0, 0, 1);
        }

    }
}
