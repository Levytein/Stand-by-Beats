using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class itemDisplay : MonoBehaviour
{
    // Start is called before the first frame update



    public static Image itemImage;

    public static CanvasGroup itemCanvas;
    public static TextMeshProUGUI ItemName;
    public static TextMeshProUGUI EffectOne;

    public Image refitemImage;

    public TextMeshProUGUI refItemName;
    public TextMeshProUGUI refEffectOne;


    void Awake()
    {
        ItemName = refItemName;
        EffectOne = refEffectOne;
        itemImage = refitemImage;
        itemCanvas = GetComponent<CanvasGroup>();


    }

   public static void OpenItemPanel()
    {
        itemCanvas.alpha = 1;
        itemCanvas.interactable = true;
        itemCanvas.blocksRaycasts = true;
    }

    public static void CloseItemPanel()
    {
        itemCanvas.alpha = 0;
        itemCanvas.interactable = false;
        itemCanvas.blocksRaycasts = false;
    }
}
