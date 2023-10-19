using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ExCSS.Tests
{
    public class SelectorsTests
    {
        [Fact]
        public async Task FindAllStyleRulesForAnElement()
        {
            // Arrange
            var sheet = await ParseBootstrapAsync();

            // Act
            var list = sheet.StyleRules
                .Where(r =>
                    (r.Selector is TypeSelector t && t.Text == "input")
                || (r.Selector is CompoundSelector selector && selector.First() is TypeSelector t2 && t2.Text == "input")
            );

            // Assert
            Assert.Equal(6, list.Count());
        }

        [Fact]
        public async Task FindAllStyleRulesElementsWithMoreThanTwoCompoundSelectors()
        {
            // Arrange
            var sheet = await ParseBootstrapAsync();

            // Act
            var list = sheet.StyleRules
                .Where(r => (r.Selector as CompoundSelector)?.Length > 2);

            // Assert
            Assert.Equal(2, list.Count());
        }

        [Fact]
        public async Task FindAllStyleRulesWithCompoundSelector()
        {
            // Arrange
            var sheet = await ParseBootstrapAsync();

            // Act
            var list = sheet.StyleRules
                .Where(r => r.Selector is CompoundSelector selector && selector.Last().Text.StartsWith("["));

            // Assert
            Assert.Equal(7, list.Count());
        }

        private async Task<Stylesheet> ParseBootstrapAsync()
        {
            //await using only with C# 8+ and .NET Core 3+/.NET 5+
            /*await*/ using (var stream = GetStream("bootstrap.css"))
            {
                var parser = new StylesheetParser();
                return await parser.ParseAsync(stream);
            }
        }

        private Stream GetStream(string fileName)
        {
            var fullyQualifiedName = $"{GetType().Namespace}.{fileName}";
            return GetType().Assembly.GetManifestResourceStream(fullyQualifiedName);
        }
    }
}