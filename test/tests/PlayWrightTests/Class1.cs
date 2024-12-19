namespace PlayWrightTests;

using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;


public class Class1
{

/*[Test]
test10('has title', async({ page }) => {
			await page.goto('https://playwright.dev/');

			// Expect a title "to contain" a substring.
			await expect(page).toHaveTitle(/ Playwright /);
})*/

[Test]
public async Task HasTitleWFirstPlayWrightTest()
{
	await Page.GotoAsync("http://localhost:5273");

	await Expect(Page).toHaveTitleAsync(new Regex("Chirp!"));

}




}
