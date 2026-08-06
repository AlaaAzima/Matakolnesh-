using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool isFirstupdate = true;
    private void Update()
    {
        if (isFirstupdate)
        {
            isFirstupdate = false;
            Loader.LoaderCallBack();
        }
    }
}
