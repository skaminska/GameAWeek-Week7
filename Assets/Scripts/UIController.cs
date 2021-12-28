using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject citizensMenu;
    [SerializeField] TextMeshProUGUI workerRequirements;
    private void Start()
    {
        citizensMenu.SetActive(false);
        workerRequirements.text = PlayerController.Instance.newWorkerRequireGold + "\n" + PlayerController.Instance.newWorkerRequireFood;

    }

    private void Update()
    {
        if (citizensMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
            citizensMenu.SetActive(false);
    }


    public void OnMainButtonClick()
    {
        citizensMenu.SetActive(true);
    }

    public void BuyNewWorker()
    {
        PlayerController.Instance.BuyNewWorker();
    }
}
