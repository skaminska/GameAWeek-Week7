using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject citizensMenu;
    [SerializeField] TextMeshProUGUI workerRequirements;
    [SerializeField] List<TextMeshProUGUI> infos;

    private void Start()
    {
        citizensMenu.SetActive(false);
        workerRequirements.text = PlayerController.Instance.newWorkerRequireGold + "\n" + PlayerController.Instance.newWorkerRequireFood;
        InvokeRepeating("UpdateUIInfo", 0, 0.5f);
    }

    private void Update()
    {
        if (citizensMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
            citizensMenu.SetActive(false);


    }
    
    void UpdateUIInfo()
    {
        var updatedInfo = PlayerController.Instance.UpdateInfo();
        for(int i =0; i<infos.Count; i++)
        {
            infos[i].text = updatedInfo[i];
        }
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
