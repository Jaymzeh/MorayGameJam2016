using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AntProfile : MonoBehaviour {

    string[] name = { "Olivia", "Ezra", "Amelia", "Asher", "Charlotte",
        "Atticus", "Ava", "Declan", "Isla", "Oliver",
        "Arabella", "SilasAurora", "Levi", "Adeline", "Milo",
        "Eleanor", "Jack", "Penelope", "Jasper", "Isabella",
        "Elijah", "Astrid,	Leo", "Mia", "Henry",
        "Violet,	Wyatt", "Aria", "Ethan", "Rose",
        "Liam", "Thea", "Caleb", "Cora", "Eli",
        "Alice", "Sebastian", "Claire", "Theodore", "Emma",
        "Benjamin", "Hazel", "Oscar", "Nora", "Austin",
        "Imogen", "Felix", "Elizabeth", "William",
        "Lucy", "Luke", "Esme", "Miles", "Scarlett",
        "Axel", "Grace", "Thomas", "Ella", "Andrew",
        "Mila", "James", "Sadie", "Alexander",
        "Evangeline", "Zachary", "Genevieve", "Finn", "Audrey",
        "Jacob", "Chloe", "Bodhi", "Emily", "Aryan",
        "Maeve", "Isaac", "Evelyn", "Lucas", "Luna",
        "Soren", "Nova", "Julian", "Adelaide", "Xavier",
        "Ivy", "John", "Caroline", "Matthew,Elise",
        "Samuel,Aurelia,	Jude", "Stella", "Roman",
        "Eloise",	"Kai", "Anouk", "Owen", "Ophelia",
        "Nathaniel"
    };

    public Image[] portrait;

	public string GetName() {
        return name[Random.Range(0, name.Length)];
    }
    public void GetPortait() {
        
    }
}
