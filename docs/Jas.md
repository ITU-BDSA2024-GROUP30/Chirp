# Introduction
This is the official project report for the _Chirp!_ application developed as part of the Analysis, Design, and Software Architecture course at ITU. The report provides an overview of the project's design, architecture, development process, and ethical considerations.

The primary goal of this report is to document the technical and collaborative aspect of the project. The application is an X-like[^whatisx] application, that share a lot of the same functionalities. In order to create the application a number of different technologies were used, e.g. ASP.NET Core and SQLite.

 [^whatisx]:'Reminicent of the website 'X', previously known as Twitter'

# User Activities
For unauthenticated users, common actions could include browsing Cheeps on the public timeline or viewing a specific Author's timeline, which shows only Cheeps made by that Author. The user journey for this _Chirp!_ use case is illustrated in the following User Flow diagram:
\pagebreak
![User Flow Diagram for the _Chirp!_ application](../docs/images/UserFlowDiagram.png)


# Usage of Large Language Models
During the development of our project, we utilised large language models (LLMs) to varied extents to assist different aspects of our work. 

## Applications of ChatGPT and Gemini
We worked with *ChatGPT* and *Gemini* the same way, just depended on the individual person in the groups preference.

The LLMs were mainly used to resolve unclear errors, especially when other resouces like Google searches did not yeild sufficient answers. Framing questions such as "I expect to get (...), but I got (...). Why?" let us receive targeted and actionable feedback. It was also instrumental in explaining why certain pieces of code behaved in unexpected ways and clarifying frameworks or technoglogies we could not directly inspect, such as Razor pages. Along with connecting the dots regarding the theory and in practice.

ChatGPT and Gemini also helped us better understand error messages and validate our assumptions about problems. For example, we often prompted it with questions like “Can you explain…” or “I think my problem is… why?” These interactions often provided clearer explanations that improved our understanding of the issues at hand.

## Effectiveness
The used LLMs proved to be helpful for the majority of tasks we encountered. It was particularly effective for troubleshooting and error resolution, often serving as a substitute for consulting teaching assistants (when they were unavailable) or spending hours researching. The ability to quickly identify issues and gain insights into complex concepts sped up our development process.

However, there were some limitations. Responses could occasionally be overly complex or repetitive, particularly in specific areas like EF Core, where the tools sometimes struggled to provide unique or actionable suggestions. Despite these occasional inefficiencies, ChatGPT and Gemini were overall more helpful than not, acting as a useful sparring partner when tackling challenging problems. 

## Impact on Development Workflow
The use of LLMs accelerated our development process by enabling faster understanding of problems and solutions. It allowed us to spend less time searching for answers and more time implementing and refining our code. In areas where traditional resources were insufficient or time-consuming, ChatGPT and Gemini filled the gap. While there were moments where its responses fell short, its overall impact was positive, contributing to our efficiency and learning throughout the project.      
