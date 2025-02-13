using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ResourcesWebApplication.Library.GamePlays.Models;

namespace ResourcesWebApplication.Library.GamePlays
{
    public class GameFrames
    {
        public async Task<List<GameDualPlayers>> GameFrame(string url)
        {
            GetPlays getPlays = new GetPlays();
            IEnumerable<GamePlayersDataset> plays = await getPlays.GetPlaysAsync(url);
            List<int> fthStrategy = new List<int>();
            List<int> sndStrategy = new List<int>();

            List<string> fthPreferences = new List<string>();
            List<string> sndPreferences = new List<string>();

            List<string> sum = new List<string>();
            List<string> fthPlayerID = new List<string>();
            List<string> sndPlayerID = new List<string>();
            if (plays.Count() != 3)
            {
                throw new Exception("Players Dataset must contain exactly 3 rows.");
            }
            foreach (var item in plays)
            {
                var fthStrategyItem = new {
                    fthStrategy = int.Parse(item.FthStrategy)
                };
                var sndStrategyItem = new {
                    sndStrategy = int.Parse(item.SndStrategy)
                };
                fthStrategy.Add(fthStrategyItem.fthStrategy);
                sndStrategy.Add(sndStrategyItem.sndStrategy);
                var fthPreferenceItem = new {
                    fthPreference = item.FthPreference
                };
                var sndPreferenceItem = new {
                    sndPreference = item.SndPreference
                };
                fthPreferences.Add(fthPreferenceItem.fthPreference);
                sndPreferences.Add(sndPreferenceItem.sndPreference);
                var sumItem = new {
                    sum = item.Sum
                };
                sum.Add(sumItem.sum);
                var fthPlayer = new {
                    fthPlayerID = item.FthPlayer
                };
                fthPlayerID.Add(fthPlayer.fthPlayerID);

                var sndPlayer = new {
                    sndPlayerID = item.SndPlayer
                };
                sndPlayerID.Add(sndPlayer.sndPlayerID);
            }

            List<GameDualPlayers> results = new List<GameDualPlayers>();

            int[] fthStrategies = {fthStrategy[0],fthStrategy[1],fthStrategy[2]};
            int[] sndStrategies = {sndStrategy[0],sndStrategy[1],sndStrategy[2]};
            
            Dictionary<string, int> fthPreference = new Dictionary<string, int>() {{fthPreferences[0], 3},{fthPreferences[1], 2},{fthPreferences[2], 1}};
            Dictionary<string, int> sndPreference = new Dictionary<string, int>() {{sndPreferences[0], 3},{sndPreferences[1], 2},{sndPreferences[2], 1}};

            foreach (var play in plays)
            {
                int fthStrategyItem = int.Parse(play.FthStrategy);
                int sndStrategyItem = int.Parse(play.SndStrategy);
                int total = fthStrategyItem + sndStrategyItem;

                // Determine Antonia's preference based on the sum
                string fthPreferenceItem;
                if (total <= int.Parse(sum[0]))
                {
                    fthPreferenceItem = fthPreferences[0]; // Mexican
                }
                else if (total == int.Parse(sum[1]))
                {
                    fthPreferenceItem = fthPreferences[1]; // Italian
                }
                else
                {
                    fthPreferenceItem = fthPreferences[2]; // Japanese
                }

                // Determine Bob's preference based on Antonia's preference
                string sndPreferenceItem;
                if (fthPreferenceItem == fthPreferences[0])
                {
                    sndPreferenceItem = total <= int.Parse(sum[0]) ? fthPreferences[0] : fthPreferences[1];
                } else if (fthPreferenceItem == fthPreferences[1])
                {
                    sndPreferenceItem = total <= int.Parse(sum[0]) ? sndPreferences[0] : sndPreferences[1];
                } else {
                    sndPreferenceItem = sndPreferences[2];
                }

                GameDualPlayers result = new GameDualPlayers() {
                    FthPlayer = fthPlayerID[1],
                    SndPlayer = sndPlayerID[0],
                    FthStrategy = fthStrategyItem.ToString(),
                    SndStrategy = sndStrategyItem.ToString(),
                    Sum = total.ToString(),
                    FthPreference = fthPreferenceItem,
                    SndPreference = sndPreferenceItem,
                    FthUtility = fthPreference[fthPreferenceItem].ToString(),
                    SndUtility = sndPreference[sndPreferenceItem].ToString(),
                };

                results.Add(result);
            }

            return results;
        }
    }
}