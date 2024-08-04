using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpawner : MonoBehaviour
{
    [SerializeField] private Transform buttonParent;
    [SerializeField] private Transform cubeParent;
    [SerializeField] private SelectUIManager selectUIManager;

    private int[] ids = { 1001, 1005, 1006 };

    void Start()
    {
        for (int i = 0; i < ids.Length; i++)
        {
            CreateButtons();
        }
    }

    private void CreateButtons()
    {
        GameObject buttonPrefab = Resources.Load<GameObject>("Prefabs/Button/ButtonPrefab");
        GameObject buttonObject = Instantiate(buttonPrefab);
        Button button = buttonObject.GetComponent<Button>();
        buttonObject.transform.SetParent(buttonParent, false);

        if (button != null)
        {
            ButtonID buttonID = buttonObject.GetComponent<ButtonID>();
            if (buttonID != null)
            {
                buttonID.id = ids[Random.Range(0, ids.Length)];
            }

            button.onClick.AddListener(() => selectUIManager.SelectAnimatronics(button));

        }
        PositionCubeInRange(CreateCube(buttonObject));
    }
    private GameObject CreateCube(GameObject buttonObject)
    {
        GameObject cubePrefab = Resources.Load<GameObject>("Prefabs/Button/CubePrefab");
        GameObject cube = Instantiate(cubePrefab);

        cube.transform.SetParent(cubeParent, true);

        TraceMapObject traceMapObject = cube.GetComponent<TraceMapObject>();
        if(traceMapObject != null)
        {
            traceMapObject.traceButton = buttonObject;
        }

        return cube;
    }

    private void PositionCubeInRange(GameObject cube)
    {
        Transform cubeTransform = cube.transform;

        float xRange = 0.05f - (-0.07f);
        float yRange = 0.3f - (-0.17f);

        float xPosition = Random.Range(-0.07f, 0.05f);
        float yPosition = Random.Range(-0.17f, 0.3f);

        cubeTransform.localPosition = new Vector3(xPosition, yPosition, 0);
    }

    public void AddSpawnButton()
    {
        CreateButtons();
    }

    IEnumerator WaitSpawnButton()
    {
        yield return new WaitForSeconds(5);
        CreateButtons();
    }

}
