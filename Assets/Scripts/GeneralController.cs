using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralController : MonoBehaviour
{

	[SerializeField] public GameObject alco;
	[SerializeField] public GameObject energy;
	[SerializeField] public GameObject swamp;
	[SerializeField] public GameObject lava;
	[SerializeField] public GameObject steam;
	[SerializeField] public GameObject dust;
	[SerializeField] public GameObject geyser;

	public static GeneralController instance;

	private void Awake() {
		instance = this;
	}

	private void FixedUpdate() {

	}

	private void FindNewObject() {

	}

}
