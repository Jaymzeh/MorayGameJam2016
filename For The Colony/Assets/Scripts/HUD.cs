using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

    public Text nameText, healthText, level;
    public Text allyText, enemyText, foodText;
    public Image portrait;

    public int numOfAllies,numOfEnemies;

	// Update is called once per frame
	void Update () {
        if (GameControl.instance.selectedAnt != null) {
            nameText.text = "Name: " + GameControl.instance.selectedAnt.name;
            portrait.color = new Color(1, 1, 1, 1);
            healthText.text = "Health: " + GameControl.instance.selectedAnt.GetComponent<Ant>().health.ToString();
            portrait.sprite = GameControl.instance.selectedAnt.GetComponent<Ant>().portrait;
        }
        else {
            nameText.text = "";
            healthText.text = "";
            portrait.color = new Color(1,1,1,0);
        }

        allyText.text = "Allies: " + GameControl.instance.allyCount;
        enemyText.text = "Enemies: " + GameControl.instance.enemyCount;

        foodText.text = "Food: " + GameControl.instance.playerResources;
	}
}
