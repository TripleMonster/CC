using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<RegionController> _regionsList;

    int mCurSelectedID = 0;

    void Start()
    {
        if (_regionsList != null) 
        {
            foreach (var item in _regionsList)
            {
                item._SelectedEvent.AddListener(OnSelectedRegionByID);
            }
        }
    }

    void Update()
    {
        
    }

    public void OnSelectedRegionByID(int id) 
    {
        int reID = id - 1;
        if (reID > _regionsList.Count || reID < 0)
            return;

        Debug.Log("当前选中的区域为 : " + id + ", 上次选中区域为 :" + mCurSelectedID);

        if (mCurSelectedID != id && mCurSelectedID > 0)
        {
            int pID = mCurSelectedID - 1;
            pID = pID < 0 ? 0 : pID;
            RegionController regionCtr = _regionsList[pID];
            regionCtr.LostSelectedState();
        }

        mCurSelectedID = id;
    }
}
