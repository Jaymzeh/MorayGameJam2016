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
            nameText.text = GameControl.instance.selectedAnt.name;
            healthText.text = GameControl.instance.selectedAnt.GetComponent<Ant>().health.ToString();
        }
        else {
            nameText.text = "";
            healthText.text = "";
        }

        allyText.text = "Allies: " + GameControl.instance.allyCount;
        enemyText.text = "Enemies: " + GameControl.instance.enemyCount;

        foodText.text = "Food: " + GameControl.instance.playerResources;
	}
}
