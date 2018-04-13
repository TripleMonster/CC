using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PlaneMnager : MonoBehaviour {

    public GameObject _Cube;
    public UnityEngine.UI.Text _TipMessage;
    public bool _Editable;
    public GameObject _Hero;

    Vector3 planeOriginPosition;
    Transform CubeParent;
    Quaternion CubeRotation;

    PathInfo outputPathInfo;
    PathInfo loadedPathInfo;

    List<CoordinateInfo> pathList;

	void Start () {
        CubeParent = _Cube.transform.parent;
        CubeRotation = _Cube.transform.rotation;
        planeOriginPosition = transform.position;
        loadedPathInfo = FileManager.Instance.LoadJson<PathInfo>("/Output/Json/PathInfoTest.json");
        outputPathInfo = new PathInfo();

        //GeneratePath();
        pathList = new List<CoordinateInfo>();
        for (int i = 0; i < 10; i++) {
            float x = i + 0.5f;
            float z = i*2 +  0.5f;
            CoordinateInfo ci = new CoordinateInfo(x, z);
            pathList.Add(ci);
        }
	}

	private void Update(){
        if (_Editable) {
            if (Input.GetMouseButton(0))
                AddCube();

            if (Input.GetMouseButton(1))
                RemoveCube();    
        }

        //if (Input.GetMouseButton(0))
            //MouseClickPointToMove();
	}

    void MouseClickPointToMove() {
        foreach (var item in pathList) {
            Debug.Log("postiion------" + new Vector3(item.x, 0, item.z));
            _Hero.transform.localPosition = Vector3.Slerp(_Hero.transform.localPosition, new Vector3(item.x, 0, item.z), Time.deltaTime);
        }

        //RaycastHit hit;
        //if (CheckIsRayCastedByMask("Plane", out hit)) {
        //    Vector3 cubeCenterPosition = CalculateCorrespondingPointInPlane(hit.point) + new Vector3(0.5f, 0, -0.5f);
        //    _Hero.transform.localPosition = cubeCenterPosition;
        //    Debug.Log("cubeCenterPosition======" + cubeCenterPosition);
        //}
    }

    bool CheckIsRayCastedByMask(string mask, out RaycastHit raycastHit){
        Vector3 mouseClickPosition = Input.mousePosition;
        Debug.Log("input=" + mouseClickPosition);
        Ray ray = Camera.main.ScreenPointToRay(mouseClickPosition);
        if (Physics.Raycast(ray, out raycastHit, 100f, LayerMask.GetMask(mask)))
            return true;
            
        return false;
    }

	void GeneratePath () {
        foreach (CoordinateInfo info in loadedPathInfo.coordinate) {
            GameObject cube = Instantiate(_Cube) as GameObject;
            cube.transform.parent = CubeParent;
            cube.transform.rotation = CubeRotation;
            cube.transform.localPosition = new Vector3(info.x, 0, info.z);
            cube.gameObject.SetActive(true);  
        }
    }

    Vector3 CalculateCorrespondingPointInPlane(Vector3 hitPoint) {
        float offsetX = hitPoint.x - planeOriginPosition.x;
        float offsetZ = hitPoint.z - planeOriginPosition.z;

        Debug.Log("offsetX=" + offsetX + "; offsetZ=" + offsetZ);
        Vector2 grideId = new Vector2(Mathf.Ceil(offsetX), Mathf.Ceil(offsetZ)); 
        Debug.Log("gridX=" + grideId.x + ";gridZ=" + grideId.y);
        Vector3 gridePosition = new Vector3(grideId.x-1.0f, 0, grideId.y);
        Debug.Log("gridePosition=" + gridePosition);

        return gridePosition;
    }

    void AddCube() {
        RaycastHit hit;
        if (CheckIsRayCastedByMask("Cube", out hit))
            return;

        if (CheckIsRayCastedByMask("Plane", out hit)) {
            if (Input.GetMouseButtonDown(0)) {
                Vector3 newPosition = CalculateCorrespondingPointInPlane(hit.point);
                GameObject cube = Instantiate<GameObject>(_Cube);
                cube.transform.parent = CubeParent;
                cube.transform.rotation = CubeRotation;
                cube.transform.localPosition = newPosition;
                cube.gameObject.SetActive(true);
            }
        }
    }

    void RemoveCube() {
        RaycastHit hit2;
        if (CheckIsRayCastedByMask("Cube", out hit2)){
            GameObject cubeObj = hit2.transform.gameObject;
            Destroy(cubeObj);
        }
    }

    void ShowTipMessageToScreen(string message) {
        _TipMessage.text = message;
    }

    public void OutputPathInfoToJson() {
        outputPathInfo.level = 1;
        outputPathInfo.cubeCount = outputPathInfo.coordinate.Count;
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("CubeObj");
        if (cubes.Length == 0) return;  
        
        foreach (var cube in cubes){
            CoordinateInfo info = new CoordinateInfo(cube.transform.localPosition.x, cube.transform.localPosition.z);
            outputPathInfo.coordinate.Add(info);
        }

        string pathInfo = JsonConvert.SerializeObject(outputPathInfo);
        FileManager.Instance.OutPutJsonFile("PathInfoTest.json", pathInfo);

        ShowTipMessageToScreen("导出Json文件成功!");
    }

    public void ClearAllCubes() {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("CubeObj");

        foreach (var cube in cubes){
            if (cube.activeSelf)
                Destroy(cube);
        }
        ShowTipMessageToScreen("已清除所有Cube!!!");
    }
}
