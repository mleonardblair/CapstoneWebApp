using System;

namespace EcommerceApp.Client.Helpers
{
    public class MudMethods
    {
        private static Random _random = new Random();
        public static MudBlazor.Color GetRandomColor(Dictionary<string, MudBlazor.Color> tagColors)
        {
            // Create a list of all MudBlazor colors excluding grey.
            var excludedColors = new List<MudBlazor.Color> { MudBlazor.Color.Default, MudBlazor.Color.Inherit, MudBlazor.Color.Transparent, MudBlazor.Color.Info };
            var availableColors = Enum.GetValues(typeof(MudBlazor.Color))
                                      .Cast<MudBlazor.Color>()
                                      .Where(c => !excludedColors.Contains(c))
                                      .ToList();

            // Remove the colors that have already been used.
            availableColors.RemoveAll(c => tagColors.Values.Contains(c));

            if (availableColors.Any())
            {
                // Return a random color from the remaining ones.
                return availableColors[_random.Next(availableColors.Count)];
            }
            else
            {
                // If all colors have been used, you could return a default one, re-start the cycle,
                // or handle it however you deem fit.
                return MudBlazor.Color.Default; // Or any other color you wish to default to.
            }
        }

    }
}
