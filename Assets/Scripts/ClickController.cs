using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class ClickController : Singleton<ClickController>
{
    [SerializeField] private GameObject BombPrefab;
    [SerializeField] private GameObject AttractorPrefab;


    private bool isMouseDown = false;
    private Attractor _currentAttractor;
    private int spwnIndex = 0;

    bool startGrowing = false;


    Vector3 mousePos
    {
        get
        {
            Vector3 pos = Helpers.Camera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            return pos;
        }
    }


    void FixedUpdate()
    {
        // If the mouse button is held down, increase the size of the object
        if (isMouseDown)
        {
            _currentAttractor.Grow();
        }
    }


    public float longClickTimeThreshold = 1.0f; // Threshold for long click
    private float clickStartTime = 0f; // Time the current click started


    public static event Action OnQuickClick;
    public void TriggerQuickClick() => OnQuickClick?.Invoke();

    public static event Action OnLongClickStart;
    public void TriggerLongClickStart() => OnLongClickStart?.Invoke();
    public static event Action OnLongClickEnd;
    public void TriggerLongClickEnd() => OnLongClickEnd?.Invoke();


    private void Start()
    {
        OnQuickClick += SpawnBomb;
        OnLongClickStart += SpawnAttractor;
        OnLongClickEnd += SummonAttractor;
        if (Player.Instance != null)
            Player.OnGameOver += Disable;
    }

    private void OnDestroy()
    {
        OnQuickClick -= SpawnBomb;
        OnLongClickStart -= SpawnAttractor;
        OnLongClickEnd -= SummonAttractor;
        if (Player.Instance != null)
            Player.OnGameOver -= Disable;
    }

    private bool longClickStarted = false;

    void ClickChecker()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickStartTime = Time.time;
        }

        float timeSinceClickStartTime = Time.time - clickStartTime;

        // If itt has been held down for more than the threshold, trigger StartLongClick
        if (Input.GetMouseButton(0))
        {
            if (timeSinceClickStartTime > longClickTimeThreshold && !longClickStarted)
            {
                Debug.Log("Long click started!");
                longClickStarted = true;
                TriggerLongClickStart();
            }
                
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            if (timeSinceClickStartTime < longClickTimeThreshold)
            {
                
                TriggerQuickClick();
            }

            else
            {
                
                TriggerLongClickEnd();
                longClickStarted = false;
            }
                
        }
    }

    void Update()
    {
        ClickChecker();
        if (startGrowing)
        {
            _currentAttractor.Grow();
        }
    }

    void SpawnAttractor()
    {
        _currentAttractor = Instantiate(AttractorPrefab, mousePos, Quaternion.identity)
            .GetComponent<Attractor>();
        startGrowing = true;
    }

    void SummonAttractor()
    {
        _currentAttractor.Summon();
        startGrowing = false;
    }

    void SpawnBomb()
    {
        Bomb b = Instantiate(BombPrefab, mousePos, Quaternion.identity)
            .GetComponent<Bomb>();
        b.Summon();
    }


    void Disable()
    {
        if (this!=null)
            this.enabled = false;
    }
}