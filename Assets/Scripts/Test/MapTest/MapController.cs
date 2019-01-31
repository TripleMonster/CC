using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<RegionController> _regionsList;
    public List<DragButton> _ArmyButtonList;

    int mCurSelectedID = 0;

    void Start()
    {
        if (_regionsList != null) 
        {
            foreach (var region in _regionsList)
            {
                region._SelectedEvent.AddListener(OnSelectedRegionByID);
            }
        }


        if (_ArmyButtonList != null)
        {
            foreach (var armyButton in _ArmyButtonList)
            {
                armyButton.DropEvent.AddListener(OnArmyDroped);
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

    public void OnArmyDroped(int i)
    {
        
    }
}
