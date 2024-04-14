using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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


    public float doubleClickTimeThreshold = 0.2f; // Threshold for double click
    public float longClickTimeThreshold = 1.0f; // Threshold for long click
    private float lastClickTime = 0f; // Time of the last click
    private float clickStartTime = 0f; // Time the current click started
    private bool isClicking = false; // Flag to track if currently clicking
    private bool isLongClick = false;
    private bool isDoubleClick = false;
    private bool isReleaseingClick = false;
    
    
    
    
    public static event Action OnDoubleClick;
    public void TriggerDoubleClick() => OnDoubleClick?.Invoke();

    public static event Action OnLongClickStart;
    public void TriggerLongClickStart() => OnLongClickStart?.Invoke();
    public static event Action OnLongClickEnd;
    public void TriggerLongClickEnd() => OnLongClickEnd?.Invoke();


    private void Start()
    {
        OnDoubleClick += SpawnBomb;
        OnLongClickStart += SpawnAttractor;
        OnLongClickEnd += SummonAttractor;
    }

    private void OnDestroy()
    {
        OnDoubleClick -= SpawnBomb;
        OnLongClickStart -= SpawnAttractor;
        OnLongClickEnd -= SummonAttractor;
    }


    void ClickChecker()
    {
        // Detect Double Click
        // Check for double click
        if (Input.GetMouseButtonDown(0))
        {
            clickStartTime = Time.time;
            isClicking = true;
            isLongClick = false;
            isDoubleClick = false;
            isReleaseingClick = false;
            Debug.Log("Time between clicks: " + (Time.time - lastClickTime) + " seconds");

            if (Time.time - lastClickTime < doubleClickTimeThreshold)
            {
                // Double click detected
                Debug.Log("Double Clicked!");
                isDoubleClick = true;
                TriggerDoubleClick();
                return;
            }
            

            lastClickTime = Time.time;
        }


        else if (Input.GetMouseButton(0))
        {
            if (Time.time - clickStartTime > longClickTimeThreshold && !isLongClick)
            {
                // Long click detected
                Debug.Log("Long Clicked started");
                isLongClick = true;
                TriggerLongClickStart();
                
            }
        }

        if (Input.GetMouseButtonUp(0) && isLongClick)
        {
            // Short click released
            Debug.Log("Long Click Released!");
            isClicking = false;
            isLongClick = false;
            isReleaseingClick = true;
            TriggerLongClickEnd();
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
}