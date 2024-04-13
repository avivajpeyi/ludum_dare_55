using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {
    private Slider slider;
    public KeyCode increaseKey = KeyCode.Space;

    void Start() {
        slider = GetComponent<Slider>();
    }

    void Update() {
        if(Input.GetKeyDown(increaseKey)) {
            IncreaseScore(0.1f);
        }
    }

    public void IncreaseScore(float amount) {
        slider.value += amount;
    }
}