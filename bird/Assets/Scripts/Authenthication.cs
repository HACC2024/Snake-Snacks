using System;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Authenthication : MonoBehaviour
{
    [SerializeField] private TMP_InputField Username;
    [SerializeField] private TMP_InputField Password;
    [SerializeField] private TMP_InputField ReType_Password;
    [SerializeField] private TMP_InputField Screen_Name;

    [SerializeField] private GameObject Sign_Up;
    [SerializeField] private GameObject Sign_In;
    [SerializeField] private GameObject After_SignIn;
    [SerializeField] private GameObject After_SignUp;
    //async void Awake()
    //{
    //    try
    //    {
    //        await UnityServices.InitializeAsync();
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogException(e);
    //    }
    //}

    // Setup authentication event handlers if desired
    void SetupEvents()
    {
        AuthenticationService.Instance.SignedIn += () => {
            // Shows how to get a playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

            // Shows how to get an access token
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

        };

        AuthenticationService.Instance.SignInFailed += (err) => {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () => {
            Debug.Log("Player signed out.");
        };

        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log("Player session could not be refreshed and expired.");
        };
    }

    async public void SignUp()
    {
        if(Password.text.Equals(ReType_Password.text))
        {
            try
            {
                await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(Username.text, Password.text);
                Sign_Up.SetActive(false);
                After_SignUp.SetActive(true);
                Debug.Log("SignUp is successful.");
            }
            catch (AuthenticationException ex)
            {
                // Compare error code to AuthenticationErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
            catch (RequestFailedException ex)
            {
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
        }
        else
        {
            Debug.Log("Passwords don't match");
        }
        
    }

    async public void Create_Screen_Name()
    {
        try
        {
            await AuthenticationService.Instance.UpdatePlayerNameAsync(Screen_Name.text);
            Debug.Log("Name set");
            SceneManager.LoadScene("GPSTesting", LoadSceneMode.Single);
        }
        catch (AuthenticationException ex)
        {
            //Couldn't authenticate, relog
            Debug.LogException(ex);
            return;
        }
        catch (RequestFailedException ex)
        {
            //Not signed in/request failed
            Debug.LogException(ex);
            return;
        }
    }

    async  public void SignIn()
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(Username.text, Password.text);
            Debug.Log("SignIn is successful.");
            SceneManager.LoadScene("GPSTesting", LoadSceneMode.Single);
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }
}
