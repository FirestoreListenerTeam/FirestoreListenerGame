using UnityEngine.UI;
using UnityEngine;

public class spriteManagerTurnTheCrank : MonoBehaviour {

    public Sprite first, second;

    public Image sprite;

    float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 0.5f)
        {
            if (sprite.sprite == first)
                sprite.sprite = second;
            else
                sprite.sprite = first;

            timer = 0.0f;
        }
    }
}
