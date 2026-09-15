using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerStatsUIView : MonoBehaviour
{
    [SerializeField] private Text AttackText;
    [SerializeField] private Text DefText;
    [SerializeField] private Text SpeedText;
     [SerializeField] private Text IntellectText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AttackText.text = PlayerHelper.Instance.Atack.ToString();
        DefText.text = PlayerHelper.Instance.Stamina.ToString();
        SpeedText.text = PlayerHelper.Instance.Speed.ToString();
        IntellectText.text = PlayerHelper.Instance.Intelect.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        AttackText.text = PlayerHelper.Instance.Atack.ToString();
        DefText.text = PlayerHelper.Instance.Stamina.ToString();
        SpeedText.text = PlayerHelper.Instance.Speed.ToString();
        IntellectText.text = PlayerHelper.Instance.Intelect.ToString();
    }
}
