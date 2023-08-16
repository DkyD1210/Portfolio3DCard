using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardManager : MonoBehaviour
{


    [SerializeField]
    private TMP_Text m_CostText;

    [SerializeField]
    private RectTransform CardLayer;

    public List<Card> m_CardList;

    [SerializeField]
    private Vector3 HandStart;

    [SerializeField]
    private Vector3 HandEnd;

    public CardSystem cardSystem;

    void Start()
    {
        m_CardList.AddRange(CardLayer.gameObject.GetComponentsInChildren<Card>());
        HandStart = new Vector3((CardLayer.rect.width / 2) * -1, 0, 0);
        HandEnd = new Vector3(CardLayer.rect.width / 2, 0, 0);
    }


    void Update()
    {
        ShowCost();
        CardHand();
    }

    private void ShowCost()
    {
        float recoverCost = cardSystem.RecoverCost * 0.01f;
        cardSystem.Cost += recoverCost * Time.deltaTime;
        cardSystem.Cost = Mathf.Clamp(cardSystem.Cost, 0, cardSystem.MaxCost);

        m_CostText.text = $"{(int)cardSystem.Cost}/{cardSystem.MaxCost}";
    }

    private void CardHand()
    {

        int count = m_CardList.Count;


        for (int i = 0; i < count; i++)
        {
            //float distance = Vector3.Distance(HandStart, HandEnd);
            float value = (float)i / (count - 1);
            Vector3 pos = Vector3.Lerp(HandStart, HandEnd, value);
            pos.y = CardLayer.position.y * (Mathf.Abs(0.5f - value) * -1);

            Card card = m_CardList[i];

            card.transform.localPosition = pos;
            card.transform.localRotation = Quaternion.Euler(0, 0, 20 * (0.5f - value));
        }
    }

}

[System.Serializable]
public class CardSystem
{
    public float Cost;

    public int MaxCost = 10;

    public int RecoverCost = 100;
}
