using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TObject {
	WATER, FIRE, AIR, DIRTY, ALCOHOL, SWAMP, LAVA, ENERGY, STEAM, DUST, GEYSER, STONE, GUNPOWDER, DIRT
}

public class ObjectController : MonoBehaviour
{

	[SerializeField] public TObject mode;

	private bool connected;
    List<newObjects> newObjects = new List<newObjects>();
	private newObjects newObj;

	private void Awake() {
		connected = false;
	}

	void FixedUpdate() {
		if (newObjects.Count > 0) {
			newObjects[0].instance.transform.position = FindCenter(newObjects[0]);
		}
	}

	private void OnTriggerEnter(Collider other) {
		try {
			if (!connected) {
				if (mode == TObject.WATER) {
					if (other.GetComponent<ObjectController>().mode == TObject.FIRE) {
						RoutineEnter(GeneralController.instance.alco, other);
					} else if (other.GetComponent<ObjectController>().mode == TObject.DIRTY) {
						RoutineEnter(GeneralController.instance.swamp, other);
					} else if (other.GetComponent<ObjectController>().mode == TObject.AIR) {
						RoutineEnter(GeneralController.instance.steam, other);
					}
				}

				if (mode == TObject.AIR) {
					if (other.GetComponent<ObjectController>().mode == TObject.FIRE) {
						RoutineEnter(GeneralController.instance.energy, other);
					} else if (other.GetComponent<ObjectController>().mode == TObject.DIRTY) {
						RoutineEnter(GeneralController.instance.dust, other);
					}
				}

				if (mode == TObject.DIRTY) {
					if (other.GetComponent<ObjectController>().mode == TObject.FIRE) {
						RoutineEnter(GeneralController.instance.lava, other);
					}
				}

				//if (mode == TObject.STEAM) {
				//	if (other.GetComponent<ObjectController>().mode == TObject.DIRTY) {
				//		RoutineEnter(GeneralController.instance.geyser, other);
				//	}
				//}
			}
		} catch { }
	}

	private void OnTriggerExit(Collider other) {
		try  {
			if (connected) { 
				if (mode == TObject.WATER) {
					if (other.GetComponent<ObjectController>().mode == TObject.FIRE ||
						other.GetComponent<ObjectController>().mode == TObject.DIRTY ||
						other.GetComponent<ObjectController>().mode == TObject.AIR) {
						RoutineExit(other);
					}
				} else if (mode == TObject.AIR) {
					if (other.GetComponent<ObjectController>().mode == TObject.FIRE ||
						other.GetComponent<ObjectController>().mode == TObject.DIRTY) {
						RoutineExit(other);
					}
				} else if (mode == TObject.DIRTY) {
					if (other.GetComponent<ObjectController>().mode == TObject.FIRE) {
						RoutineExit(other);
					}
				}
			}
		} catch { }
    }

    private Vector3 FindCenter(newObjects obj) {
        if (obj.pos3 == null) {
            return new Vector3((obj.pos1.position.x + obj.pos2.position.x) / 2f,
                               (obj.pos1.position.y + obj.pos2.position.y) / 2f,
                               (obj.pos1.position.z + obj.pos2.position.z) / 2f);
        } else {
            return new Vector3( (obj.pos1.position.x + obj.pos2.position.x + obj.pos3.position.x) / 3f,
                                           (obj.pos1.position.y + obj.pos2.position.y + obj.pos3.position.y) / 3f,
                                           (obj.pos1.position.z + obj.pos2.position.z + obj.pos3.position.z) / 3f );
		}

    }

	private void RoutineEnter(GameObject instance, Collider other) {
		newObj = new newObjects();
		newObj.instance = Instantiate(instance, transform.parent);
		newObj.pos1 = transform;
		newObj.pos2 = other.transform;
		newObjects.Add(newObj);

		transform.GetChild(0).gameObject.SetActive(false);
		other.transform.GetChild(0).gameObject.SetActive(false);
		connected = true;
	}

	private void RoutineExit(Collider other) {
		Destroy(newObj.instance);
		newObjects.Remove(newObj);

		transform.GetChild(0).gameObject.SetActive(true);
		other.transform.GetChild(0).gameObject.SetActive(true);
		connected = false;
	}
}

struct newObjects {
	public GameObject instance;
	public Transform pos1;
	public Transform pos2;
	public Transform pos3;
}
