using UnityEngine;
using UnityEngine.UI;

public class PanelManage: MonoBehaviour
{
    [SerializeField] GameObject[] panels;
    [SerializeField] GameObject mask;
    [SerializeField] Button button;

    int i = 0;

    private void Awake()
    {
        for (int j = 0; j < panels.Length; j++)
        {
            panels[j].SetActive(false);
        }

        panels[i].SetActive(true);
    }

    public void Next()
    {
        panels[i++].SetActive(false);

        if (i == panels.Length)
        {
            mask.SetActive(false);

            button.gameObject.SetActive(false);

            State.Publish(Condition.READY);

            return;
        }

        if(panels[i].activeSelf == false)
        {
            panels[i].SetActive(true);
        }
    }
}
