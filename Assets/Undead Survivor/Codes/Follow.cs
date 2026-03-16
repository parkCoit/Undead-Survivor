using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    RectTransform rect;
    private void Awake() {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate() {

        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}
