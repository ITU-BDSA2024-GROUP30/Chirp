## Build, test, release, and deployment

> "Illustrate with a UML activity diagram how your Chirp! applications are build, tested, released, and deployed. That is, illustrate the flow of activities in your respective GitHub Actions workflows.
>
> Describe the illustration briefly, i.e., how your application is built, tested, released, and deployed."


## How to run test suite locally

> "List all necessary steps that Adrian or Helge have to perform to execute your test suites. Here, you can assume that we already cloned your repository in the step above.
>
> Briefly describe what kinds of tests you have in your test suites and what they are testing."

-Run dotnet test from src path (check if this is the correct placement) OR run 'build and test' workflow on github actions, which has a manual trigger and will run through each test folder.
-We have some unit tests, that test the chirp core part of the program, such as whether our domain entities can be created correctly and be related to each other.
-We have a standard test in infrastructure, that should ALWAYS pass - this is our canary in the coalmine, if this test fails, our test suite is not functioning.
-Some API tests in chirpweb, that test if we get expected outputs from the public and private timelines.


## License

> "State which software license you chose for your application."

-MIT License, based on .NET and/or Microsoft, also based on Helge's license
