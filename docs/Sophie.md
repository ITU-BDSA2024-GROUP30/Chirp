## Build, test, release, and deployment
### Build

![Illustration of the building process for _Chirp!_](../docs/images/BDSA-build.png)

<p>Figure XXX above illustrates the build process. By running dotnet build in the terminal, an implicit run of dotnet restore is triggered. After restoring, .dll files are created for each .csproj file. When all .dll files are created, the terminal will output a 'Build Successful!' message.</p>
<newpage></newpage>

### Test

![Illustration of the testing from the _Chirp!_ root directory.](../docs/images/BDSA-testing.png)

<p>Figure XXXX above details how our program is tested. After inputting dotnet test in the terminal from the source directory, dotnet restore will run. Hereafter, all testfiles will run, and the terminal will output Success/Failure states for each testing directory. Alternative methods to testing are described in the "How to run test suite locally" section. </p>
<newpage></newpage>

### Release

![Illustration of the release process for _Chirp!_](../docs/images/BDSA-release.png)

<p>Figure XXXXX above is an illustration of our release process. A release can be triggered on push from any branch, but requires a tag that matches the pattern '*.*.*'.
Upon pushing a tag, the release workflow will activate and create releases for 3 operating systems: Windows, MacOS and Linux. Depending on whether the sub-release targets Windows or one of the two others, the executablefile will be respectively Chirp.exe or Chirp.
Our release is created from the ChirpWeb.csproj file, zipped and uploaded to Github where anyone can download them.</p>

[MAYBE MENTION THAT RELEASE DOESN'T WORK]
<newpage></newpage>

### Deployment

![Illustration of the deployment process for _Chirp!_](../docs/images/BDSA-deployment.png)

<p>Figure XXXXXX shows how our program deploys by way of our Github workflow. Upon a push to main, the workflow will always be triggered, ensuring automatic deployment. For more control, we also have a Workflow trigger on Github where we can deploy from any branch.
The workflow takes care of building and publishing the project, as well as sending artifacts and secrets to the Azure host. When the workflow is finished, the website is updated.</p>

<newpage></newpage>

## How to run test suite locally
<b>Assuming the repository is cloned:</b>
- Open a command line tool
- Navigate to the root directory (/Chirp) OR a specific test directory (e.g /Chirp/test/Chirp.ChirpCore.Tests)
- Enter 'dotnet test' in the command line and press enter
- All tests will now run - if you're in a specific test directory, only tests for this directory run.

<b>Without cloning the repository:</b>
- Navigate to the Github website for the Chirp30 repository
- Press 'Actions' in the upper toolbar
- Choose workflow 'Build and Test .NET' in the list of workflows to the left
- Choose 'Run workflow' twice - let branch remain as 'main'
- Observe the workflow where each test directory has its own job, which states the passes and fails of the tests

In Chirp.ChirpCore.Tests we have unit tests. These test the ChirpCore part of the program by creating entities of our domain model types and testing whether they can be created correctly as well as test that Cheeps and Authors can be related to each other.
We have a test in Chirp.ChirpInfrastructure.Tests that is independent of the program code. This test should ALWAYS pass - it is our canary in the coal mine. If this test fails, our test suite is not functioning.
Up until the final few weeks we had API tests in Chirp.ChirpWeb.Tests, which tested if we got expected outputs from the public and private timelines. These tests were removed when we changed our database to run using an environment variable.


## License
>The MIT License (MIT)
>
>Copyright (c) 2024 ITU-BDSA2024-GROUP30
>
>Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
>
>The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
>
>THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
>>See our License.MD file in the root of our directory for the associated Microsoft licenses.

We have used packages from Microsoft in our program (.NET, EFCore), from which all fall under the MIT License. As the MIT License is GPL compatible[^1], it means we could have chosen a different Open Source license for our project[^2]. However this would have been more intricate, as the different license we would choose would still need to be compatible with the MIT license.
Therefore we too have decided to use the MIT license, such that all code in the program is under the same license.

[^1]:'University of Pittsburgh, "Course & Subject Guides: MIT License Compatibility"' <https://pitt.libguides.com/openlicensing/MIT#:~:text=MIT%20License%20Compatibility,project%20must%20of%20GPL%20compliant.>
[^2]:'Wikipedia: "License Compatibility: Compatibility of FOSS licenses"' <https://en.wikipedia.org/w/index.php?title=License_compatibility&section=3#Compatibility_of_FOSS_licenses>
