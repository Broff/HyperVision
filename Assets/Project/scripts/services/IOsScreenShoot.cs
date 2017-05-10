using UnityEngine;
using System.Collections;

//#if UNITY_IOS && !UNITY_EDITOR
   //using U3DXT.iOS.Native.Social;
   //using U3DXT.iOS.Social;
//#endif

public class IOsScreenShoot : MonoBehaviour {

    private string text = "";
    private string url = "";

    public void TakeScreen(string textShare, string url)
    {
        text = textShare;
        this.url = url;
        //StartCoroutine(Share());
    }


    IEnumerator Share()
    {
        yield return new WaitForEndOfFrame();
        //capture image
        var img = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        img.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        img.Apply();

        //share
        #if UNITY_IOS && !UNITY_EDITOR
            //SocialXT.Post(SLRequest.SLServiceTypeFacebook, text, img,url,true);
        #endif
    }
}