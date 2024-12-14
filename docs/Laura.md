
### How to make _Chirp!_ work locally

1. Clone Project by running the following command from your terminal

    `git clone https://github.com/ITU-BDSA2024-GROUP30/Chirp.git`

2. Navigate to this project directory (Chirp/src/ChirpWeb)

3. From the ChirpWeb project folder, run the following commands in the terminal

    `dotnet user-secrets set "authentication_github_clientId" "Ov23li1DOiXTMCfh0Wxn"`

    `dotnet user-secrets set "authentication_github_clientSecret" "6afb7425e1d9b80b84c43372a2f4c5e35506b0f"`
	


4. Run the program by using the following command in the terminal (Still from ChirpWeb). 

    `dotnet run`


5. After running the command, you should see output indicating that the app is running (`Now listening on: http://localhost:5273`).    Open your browser and go to the URL provided in the terminal (e.g., `http://localhost:5273`) to access the application.


<br>
*We are aware that we should not include our ClientID and ClientSecret like this in the repository, but it is needed for someone to run the program locally. 


\*\*While running our program locally, there is a problem with OAuth and github register/login when using the Safari browser. This is not the case for Chrome or Firefox. Safari does not cause problems for our global Chirp app. 