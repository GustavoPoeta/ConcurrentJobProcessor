<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a id="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- ABOUT THE PROJECT -->
## About The Project

This is a demonstration of how asynchronous multi-threaded tasks work.
In my technical school's final project, I had a feature that added students using a csv file, and it was considerably slow; so I did this demo to learn and improve a better way to do it.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

This project needs:
* .NET 10
* PostgreSQL 18
* Docker

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/GustavoPoeta/ConcurrentJobProcessor.git
   ```
   
2. ```sh
   cd ConcurrentJobProcessor
   cd Backend
   ```

3. Create a .env file in backend folder and fill it with postgresql credentials and connection string (follow the .env.example)

4. In the docker compose file, change api command to sleep infinity.


5. Run the containers
   ```sh
   docker compose up
   ```
6. Open the api container's terminal:
    ```sh
   docker exec -it concurrent_job_processor bash
   ```

8. Install dotnet ef:
  ```sh
   dotnet tool install --global dotnet-ef
   export PATH="$PATH:/root/.dotnet/tools"
   ```

8. Run the migration
   ```sh
   dotnet ef database update
   ```

9. Delete the api and stop postgres container.

10. Change the docker compose file back to:
   ```sh
   dotnet run --no-launch-profile
   ```

11. Create a CSV file with these headers and populate it: name,categories,price

12. Run the containers
   ```sh
   docker compose up
   ```

13. Open the index.html inside Frontend/test.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->

[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/gustavopoeta/
