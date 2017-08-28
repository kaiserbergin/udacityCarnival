using System;
using UnityEngine;
using TMPro;

public class CarnivalScores : MonoBehaviour {

	[SerializeField]
	private int TeddyBearPointsMin = 2000;

	[SerializeField]
	private GameObject TeddyBear;

	[SerializeField]
	private TextMeshPro plinkoScore;
	[SerializeField]
	private TextMeshPro wheelScore;
	[SerializeField]
	private TextMeshPro coinScore;
    [SerializeField]
    private TextMeshPro shroomScore;

    public static CarnivalScores Instance;

	private int plinkoPoints;
	private int wheelPoints;
	private int coinPoints;
    private int shroomPoints;

	void Awake() {
		if (Instance == null)
			Instance = this;

		TeddyBear.SetActive(false);
	}

	void OnDestroy() {
		if (Instance = this)
			Instance = null;
	}

	// Update is called once per frame
	void Update () {
		plinkoScore.text = "Plinko: " + plinkoPoints.ToString("0000");
		wheelScore.text = "Wheel: " + wheelPoints.ToString("0000");
		coinScore.text = "Coins: " + coinPoints.ToString("0000");
        shroomScore.text = "Shrooms: " + shroomPoints.ToString("0000");

        if (plinkoPoints + wheelPoints + coinPoints + shroomPoints >= TeddyBearPointsMin) {
			TeddyBear.SetActive(true);
		}
	}

	public void IncrementPlinkoScore(float points) {
		plinkoPoints += (int) points;
	}

	public void IncrementWheelScore(float points) {
		wheelPoints += (int) points;
	}

	public void IncrementCoinScore() {
		coinPoints += 1000;
	}

    public void IncrementShroomScore(float points)
    {
        shroomPoints += (int)points;
    }
}
