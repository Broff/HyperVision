using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSkinsController : MonoBehaviour {

    public GameObject skinController;
    public GameObject skinContainer;
    public float widthDelta;
    public Transform firstPos;
    public Vector3 start;
    public int count;

	void Start () {
        start = firstPos.localPosition;
        InitSkins();
	}

    private void InitSkins()
    {
        count = skinController.GetComponent<SkinsController>().skins.Count;
        Vector3 lastPos = new Vector3(0,0,0);// firstPos.transform.position;
        //int[] billingSkins = skinController.GetComponent<SkinsController>().GetBuySkins();
        int activeId = skinController.GetComponent<SkinsController>().GetActiveIndex();
        for (int i = 0; i < count; i++)
        {
            GameObject container = Instantiate(skinContainer);
            container.transform.parent = transform;
            container.transform.localPosition = lastPos;
            lastPos.x += widthDelta;            
            container.GetComponent<SkinContainerController>().InitSkin(skinController.GetComponent<SkinsController>().skins[i]);
            if(i == activeId){
                container.GetComponent<SkinContainerController>().isActive = true;
            }
            if (skinController.GetComponent<SkinsController>().CheckPurchased(i) == true)
            {
                container.GetComponent<SkinContainerController>().isBuy = true;
            }
            
        }
        FindObjectOfType<StandController>().SetFocus(transform.GetChild(activeId).gameObject);
        FindObjectOfType<StandController>().moveToFocused();
    }
}
