using System.Collections.Generic;
using TwitchSDK;
using TwitchSDK.Interop;
using UnityEngine;
public class TwitchChannelPointManager : MonoBehaviour
{
	public void SetSampleRewards()
	{
		CustomRewardDefinition ColorRed = new CustomRewardDefinition();
		ColorRed.Title = "Red!";
		ColorRed.Cost = 10;
		CustomRewardDefinition ColorBlue = new CustomRewardDefinition();
		ColorBlue.Title = "Blue!";
		ColorBlue.Cost = 25;
		CustomRewardDefinition ColorOctarine = new CustomRewardDefinition();
		ColorOctarine.Title = "Octarine!";
		ColorOctarine.Cost = 750;

		List<CustomRewardDefinition> cDefinitionList = new List<CustomRewardDefinition>();
		cDefinitionList.Add(ColorRed);
		cDefinitionList.Add(ColorBlue);
		cDefinitionList.Add(ColorOctarine);

		Twitch.API.ReplaceCustomRewards(cDefinitionList.ToArray());
	}

	public void ClearRewards()
	{
		CustomRewardDefinition Cleared = new CustomRewardDefinition();
		Cleared.Title = "";
		Cleared.Cost = 0;
		Cleared.IsEnabled = false;
		Twitch.API.ReplaceCustomRewards(Cleared);
	}
}