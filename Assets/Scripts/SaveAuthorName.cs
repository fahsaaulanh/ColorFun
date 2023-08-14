using TMPro;
using UnityEngine;

public class SaveAuthorName : MonoBehaviour
{
    [SerializeField] GameObject InputPopupName;
    [SerializeField] GameObject SelectTemplateDesainPopup;
    [SerializeField] private GameData gameData;
    [SerializeField] private TMP_InputField nameField;

    public void Save()
    {
        if (nameField.text != "")
        {
            InputPopupName.SetActive(false);
            SelectTemplateDesainPopup.SetActive(true);
            gameData.currentAuthor = nameField.text;
        }
    }
}
