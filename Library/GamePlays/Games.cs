using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ResourcesWebApplication.Library.GamePlays.Models;

namespace ResourcesWebApplication.Library.GamePlays
{
    public class Games
    {
        public async Task<List<GameDualPlayers>> Game(string url)
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

            foreach (var item in plays)
            {
                fthStrategy.Add(int.Parse(item.FthStrategy));
                sndStrategy.Add(int.Parse(item.SndStrategy));
                fthPreferences.Add(item.FthPreference);
                sndPreferences.Add(item.SndPreference);
                sum.Add(item.Sum);
                fthPlayerID.Add(item.FthPlayer);
                sndPlayerID.Add(item.SndPlayer);
            }

            Dictionary<string, int> fthPreference = new Dictionary<string, int>()
            {
                { fthPreferences[0], 3 },
                { fthPreferences[1], 2 },
                { fthPreferences[2], 1 }
            };

            Dictionary<string, int> sndPreference = new Dictionary<string, int>()
            {
                { sndPreferences[0], 3 },
                { sndPreferences[1], 2 },
                { sndPreferences[2], 1 }
            };

            List<GameDualPlayers> results = new List<GameDualPlayers>();
            foreach (var fthStrategyItem in fthStrategy)
            {
                foreach (var sndStrategyItem in sndStrategy)
                {
                    int total = fthStrategyItem + sndStrategyItem;
                    
                    string fthPreferenceOperation = total <= int.Parse(sum[0]) ? fthPreferences[0] :
                        total == int.Parse(sum[1]) ? fthPreferences[1] : fthPreferences[2];
                    string sndPreferenceOperation = total == int.Parse(sum[1]) ? sndPreferences[0] :
                        total <= int.Parse(sum[0]) ? sndPreferences[1] : sndPreferences[2];

                    int fthUtility = fthPreference[fthPreferenceOperation];
                    int sndUtility = sndPreference[sndPreferenceOperation];

                    GameDualPlayers result = new GameDualPlayers()
                    {
                        FthPlayer = fthPlayerID[1],
                        SndPlayer = sndPlayerID[0],
                        FthStrategy = fthStrategyItem.ToString(),
                        SndStrategy = sndStrategyItem.ToString(),
                        Sum = total.ToString(),
                        FthPreference = fthPreferenceOperation,
                        SndPreference = sndPreferenceOperation,
                        FthUtility = fthUtility.ToString(),
                        SndUtility = sndUtility.ToString(),
                    };
                    results.Add(result);
                }
            }
            return results;
        }
    }
}