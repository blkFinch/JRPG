using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleItemMenuButton : MonoBehaviour
{
    Button btn;

    private void Start() {
        setupBtn();
    }
    public void setupBtn() {
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(btnClicked);
    }

    public void btnClicked() {
        MenuManager.Instance.OpenItemMenu();
    }
}
