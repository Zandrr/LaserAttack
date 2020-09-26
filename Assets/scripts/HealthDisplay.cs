using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        SetHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        SetHealthText();
    }

    private void SetHealthText()
    {
        healthText.text = player.GetHealth().ToString();
    }
}
