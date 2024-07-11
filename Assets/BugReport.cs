using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugReport : MonoBehaviour
{

    private string formURL = "https://forms.gle/76r2aEKEDaKqMGKC6";

    public void OpenForm()
    {
        Application.OpenURL(formURL);
    }
}
