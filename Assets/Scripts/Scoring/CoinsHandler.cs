using TMPro;
using UnityEngine;

public class CoinsHandler : MonoBehaviour
{
    public static int Coins;
    
    [SerializeField] private TMP_Text coinsText;

    public void GrabCoin()
    {
        Coins++;
        coinsText.text = Coins.ToString();
    }
}
