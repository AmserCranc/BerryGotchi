// using TwitchSDK;
// using TwitchSDK.Interop;
// using UnityEngine;
// public class AuthManager : MonoBehaviour
// {
// 	GameTask<AuthenticationInfo> AuthInfoTask;
// 	GameTask<AuthState> curAuthState;


//     public TwitchOAuthScope[] RequiredScopes;

//     void Start()
// 	{
//         RequiredScopes = new TwitchOAuthScope[1];
//         RequiredScopes[0] = TwitchOAuthScope.Channel.ManageRedemptions;
//         AuthInfoTask = Twitch.API.GetAuthenticationInfo(RequiredScopes);

// 		UpdateAuthState();
// 	}

// 	void FixedUpdate()
//     {
        
//     }
	
// 	public void UpdateAuthState()
// 	{
// 		curAuthState = Twitch.API.GetAuthState();
//         Debug.Log(curAuthState.MaybeResult.Status);
// 		if(curAuthState.MaybeResult.Status == AuthStatus.LoggedIn)
// 		{
// 			// user is logged in
            
// 		}
// 		if (curAuthState.MaybeResult.Status == AuthStatus.LoggedOut)
// 		{
// 			// user is logged out, do something
// 			// In this example you could also call GetAuthInformation() to retrigger login
// 		}
// 		if (curAuthState.MaybeResult.Status == AuthStatus.WaitingForCode)
// 		{
// 			// Waiting for code
// 			var UserAuthInfo = Twitch.API.GetAuthenticationInfo(RequiredScopes).MaybeResult;
// 			if(UserAuthInfo == null)
// 			{
// 				// User is still loading
// 			}
// 			// We have reached the state where we can ask the user to login
// 			Application.OpenURL($"{UserAuthInfo.Uri}{UserAuthInfo.UserCode}");
// 		}
// 	}

// 	// Triggered by something external, like a login button on a options menu screen
// 	public void GetAuthInformation()
// 	{
// 	  // Check to see if the user is currently logged in or not.
// 		if (AuthInfoTask == null)
// 		{
// 			// This example uses all scopes, we suggest you only request the scopes you actively need.
// 			var scopes = TwitchOAuthScope.Bits.Read.Scope + " " + TwitchOAuthScope.Channel.ManageBroadcast.Scope + " " + TwitchOAuthScope.Channel.ManagePolls.Scope + " " + TwitchOAuthScope.Channel.ManagePredictions.Scope + " " + TwitchOAuthScope.Channel.ManageRedemptions.Scope + " " + TwitchOAuthScope.Channel.ReadHypeTrain.Scope + " " + TwitchOAuthScope.Clips.Edit.Scope + " " + TwitchOAuthScope.User.ReadSubscriptions.Scope;
// 			TwitchOAuthScope tscopes = new TwitchOAuthScope(scopes);
// 			AuthInfoTask = Twitch.API.GetAuthenticationInfo(tscopes);
// 		}
// 	}
// }