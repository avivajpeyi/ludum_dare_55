using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbarUI : StaticInstance<PlayerHealthbarUI> {
    private Slider _healthbar;

    private void Awake() {
        _healthbar = GetComponent<Slider>();
    }
    
    public void InitBar(float max)
    {
        _healthbar.minValue = 0f;
        _healthbar.maxValue = max;
        _healthbar.value = max;
    }
    
   public  void SetBarValue(float value) {
        _healthbar.value = value;
    }

    public void SetBarColor(Color color) {
        _healthbar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
    }
    

   public void DisableHealthbar() {
        gameObject.SetActive(false);
    }
    
    
}