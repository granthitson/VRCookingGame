using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{

    Image image;

    private bool hasClicked = false;
    private Color initialColor;
    private Color newColor;
    private Button btn;

    private IEnumerator wait;

    private void Awake()
    {
        image = GetComponent<Image>();
        initialColor = image.color;
    }

    void Start()
    {
        btn = GetComponent<Button>();
        newColor = btn.colors.selectedColor;

        wait = Wait(.7f);
        StartCoroutine(wait);
    }

    public void Clicked()
    {
        hasClicked = true;
    }

    private void ChangeColor()
    {
        if (image.color == initialColor)
        {
            image.color = newColor;

        }
        else if (image.color == newColor)
        {
            image.color = initialColor;
        }
    }

    IEnumerator Wait(float waitTime)
    {
        while (!hasClicked)
        {
            ChangeColor();
            yield return new WaitForSeconds(waitTime);
        }

        image.color = initialColor;
    }
}
