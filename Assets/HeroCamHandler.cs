using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeroCamHandler : MonoBehaviour
{
    [SerializeField] float distanceToShow = 10f;

    [Header("Components")]
    [SerializeField] RectTransform cam = null;
    [SerializeField] RectTransform tail = null;
    [SerializeField] TextMeshProUGUI distanceText = null;

    [Header("Positions")]
    [SerializeField] RectTransform leftTransform = null;
    [SerializeField] RectTransform rightTransform = null;
    [SerializeField] float tailOffset;

    Transform player;
    Transform hero;

    private void Awake() 
    {
        player = GameObject.FindWithTag("Player").transform;
        hero = GameObject.FindWithTag("Hero").transform;   
    }

    private void Update() 
    {
        HandleCam();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            SetCamPos(true);
        }
                if(Input.GetKeyDown(KeyCode.W))
        {
            SetCamPos(false);
        }
    }

    void HandleCam()
    {
        float distanceToHero = CalculateDistanceToHero();
        if(Mathf.Abs(distanceToHero) > distanceToShow)
        {
            cam.gameObject.SetActive(true);
            bool isRight = distanceToHero > 0;
            SetCamPos(isRight);
            UpdateDistanceText(distanceToHero);
        } 
        else
        {
            cam.gameObject.SetActive(false);
        }
    }

    private void SetCamPos(bool isRight)
    {
        RectTransform transformToSet;
        if(isRight) 
        {
            transformToSet = rightTransform;
            distanceText.alignment = TextAlignmentOptions.Right;
            tail.anchoredPosition = new Vector2(tailOffset, 0f);
        }
        else 
        {
            transformToSet = leftTransform;
            distanceText.alignment = TextAlignmentOptions.Left;
            tail.anchoredPosition = new Vector2(-tailOffset, 0f);
        }

        cam.anchorMin = transformToSet.anchorMin;
        cam.anchorMax = transformToSet.anchorMax;
        cam.anchoredPosition = transformToSet.anchoredPosition;
    }

    float CalculateDistanceToHero()
    {
        return hero.position.x - player.position.x;
    }

    void UpdateDistanceText(float distanceToHero)
    {
        float distance = Mathf.Abs(distanceToHero) - distanceToShow + 1;
        distanceText.text = string.Format("{0:0}m", distance);
    }
}
