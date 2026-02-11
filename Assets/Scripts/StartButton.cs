using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] GameObject startPanel;

    private void OnEnable()
    {
        State.Subscribe(Condition.LOBBY, ActiveTrue);
        State.Subscribe(Condition.READY, ActiveFalse);
    }

    private void OnDisable()
    {
        State.Unsubscribe(Condition.LOBBY, ActiveTrue);
        State.Unsubscribe(Condition.READY, ActiveFalse);
    }

    public void StartGame()
    {
        State.Publish(Condition.READY);
    }

    void ActiveTrue()
    {
        if (startPanel.activeSelf == false)
        {
            startPanel.SetActive(true);
        }
    }

    void ActiveFalse()
    {
        if(startPanel.activeSelf == true)
        {
            startPanel.SetActive(false);
        }
    }
}
