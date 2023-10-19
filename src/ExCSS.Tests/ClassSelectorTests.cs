using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ExCSS.Tests
{
    public class ClassSelectorTests
    {
        [Fact]
        public async Task FindAllClassSelectorsThatMatchClassName()
        {
            // Arrange
            var css =
                @".sample-class { background-color: #101010 } .sample-class[type='input'] { background-color: #121212 }";
            var sheet = await new StylesheetParser().ParseAsync(css);

            // Act
            var list = sheet.StyleRules
                .Where(x =>
                    (x.Selector is CompoundSelector selector &&
                     selector.Any(y => y is ClassSelector c2 && c2.Class == "sample-class"))
                    || (x.Selector is ClassSelector c && c.Class == "sample-class")
                );

            // Assert
            Assert.Equal(2, list.Count());
        }
    }
}