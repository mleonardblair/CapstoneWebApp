using System;

namespace EcommerceApp.Client.Helpers
{
    public class MudMethods
    {
        private static Random _random = new Random();
        private static List<MudBlazor.Color> availableColors = GetAvailableColors();

        private static List<MudBlazor.Color> GetAvailableColors()
        {
            // Create a list of all MudBlazor colors excluding grey.
            var excludedColors = new List<MudBlazor.Color> { MudBlazor.Color.Default, MudBlazor.Color.Inherit, MudBlazor.Color.Transparent, MudBlazor.Color.Info };
            return Enum.GetValues(typeof(MudBlazor.Color))
                       .Cast<MudBlazor.Color>()
                       .Where(c => !excludedColors.Contains(c))
                       .ToList();
        }
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
                return MudBlazor.Color.Primary; // Or any other color you wish to default to.
            }
        }
        public static MudBlazor.Color GetRandomColor2(Dictionary<string, MudBlazor.Color> tagColors)
        {
            if (availableColors.Count == 0)
            {
                // If all colors have been used, you could reset the availableColors list here,
                // or handle it however you deem fit.
                availableColors = GetAvailableColors();
            }

            // Get a random index from the availableColors list.
            int randomIndex = _random.Next(availableColors.Count);

            // Get the color at the random index.
            MudBlazor.Color randomColor = availableColors[randomIndex];

            // Remove the selected color from the availableColors list.
            availableColors.RemoveAt(randomIndex);

            // Add the selected color to the tagColors dictionary if needed.
            // You can decide whether to store this color or not in your application logic.

            return randomColor;
        }
    }
}
