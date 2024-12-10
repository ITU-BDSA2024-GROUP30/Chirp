## Build, test, release, and deployment

> "Illustrate with a UML activity diagram how your Chirp! applications are build, tested, released, and deployed. That is, illustrate the flow of activities in your respective GitHub Actions workflows.
>
> Describe the illustration briefly, i.e., how your application is built, tested, released, and deployed."


## How to run test suite locally

> "List all necessary steps that Adrian or Helge have to perform to execute your test suites. Here, you can assume that we already cloned your repository in the step above.
>
> Briefly describe what kinds of tests you have in your test suites and what they are testing."
-
-

Assuming the repository is cloned:
- Open a command line tool
- Navigate to the root directory (/Chirp)
- Enter 'dotnet test' in the command line and press enter
- All tests will now run

Alternatively, testing from each testing directory:
- Open a command line tool
- Navigate to the specific test directory (e.g /Chirp/test/Chirp.ChirpCore.Tests)
- Enter 'dotnet test' in the command line and press enter
- Tests in the chosen test directory will now run

Without cloning the repository:
- Navigate to the website for the Chirp30 repository
- Press 'Actions' in the upper toolbar
- Choose workflow 'Build and Test .NET' in the list of workflows to the left
- Choose 'Run workflow' twice - let branch remain as 'main'
- Observe the workflow where each test directory has its own job, which states the passes and fails of the tests


- We have some unit tests, that test the chirp core part of the program, such as whether our domain entities can be created correctly and be related to each other.
- We have a standard test in infrastructure, that should ALWAYS pass - this is our canary in the coalmine, if this test fails, our test suite is not functioning.
- Some API tests in chirpweb, that test if we get expected outputs from the public and private timelines.


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
>>See our License.MD file in the root of our directory for associated Microsoft licenses.

We have used packages from Microsoft in our program (.NET, EFCore), from which all fall under the MIT License. As the MIT License is [GPL compatible][1], it means we could have chosen a different Open Source license for our [project][2]. However this would have been more intricate, as the different license we would choose would need to be compatible with the MIT license.
Therefore we too have decided to use the MIT license, such that all code in the program is under the same license.

Helge's code under the MIT license? Only got this through verbal communication.

[1]: <https://pitt.libguides.com/openlicensing/MIT#:~:text=MIT%20License%20Compatibility,project%20must%20of%20GPL%20compliant.>'Name for footnote here'
[2]:<https://en.wikipedia.org/w/index.php?title=License_compatibility&section=3#Compatibility_of_FOSS_licenses>'NAme for footnote on license compat.'
