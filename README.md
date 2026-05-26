
# Concurrent Job Processor

<!-- Improved compatibility of back to top link -->
<a id="readme-top"></a>

[![LinkedIn][linkedin-shield]][linkedin-url]

<div align="center">

<h3 align="center">Concurrent Job Processor</h3>

<p align="center">
A demonstration project for asynchronous and multi-threaded task processing using .NET, PostgreSQL, Docker, and SignalR.
Focused on improving performance when importing large CSV datasets.
</p>

</div>

---

## About The Project

This project demonstrates how asynchronous multi-threaded processing works in practice.

The idea came from a feature developed during my technical school's final project, where importing students from a CSV file became considerably slow as the dataset increased.

To better understand concurrency, background jobs, and scalable processing, I built this demo application that processes CSV imports more efficiently using workers and asynchronous job execution.

### Technologies Used

- .NET 10
- ASP.NET Core
- PostgreSQL 18
- Docker
- SignalR
- Entity Framework Core

---

## Getting Started

### Prerequisites

Make sure you have the following installed:

- .NET 10 SDK
- PostgreSQL 18
- Docker

---

## Installation

### 1. Clone the repository

git clone https://github.com/GustavoPoeta/ConcurrentJobProcessor.git

### 2. Navigate to the backend folder

cd ConcurrentJobProcessor
cd Backend

### 3. Create the `.env` file

Create a `.env` file inside the `Backend` folder and configure it using the `.env.example` file as reference.

---

### 4. Temporarily change the Docker Compose API command

In the `docker-compose.yml`, change the API command to:

command: sleep infinity

This allows entering the container before starting the application.

---

### 5. Start the containers

docker compose up

---

### 6. Access the API container terminal

docker exec -it concurrent_job_processor bash

---

### 7. Install Entity Framework CLI tools

dotnet tool install --global dotnet-ef
export PATH="$PATH:/root/.dotnet/tools"

---

### 8. Run database migrations

dotnet ef database update

---

### 9. Stop and remove the API container

After the migration finishes:

- Stop the containers
- Remove the API container

---

### 10. Restore the original Docker Compose command

Change the API command back to:

command: dotnet run --no-launch-profile

---

### 11. Prepare the CSV file

Create a CSV file using the following headers:

name,categories,price

Example:

Coffee Maker,Kitchen,120.50
Gaming Mouse,Electronics,89.90
Notebook,Office,15.00

---

### 12. Start the containers again

docker compose up

---

### 13. Open the frontend test page

Open the following file in your browser:

Frontend/test/index.html

---

## Features

- CSV product import
- Concurrent background job processing
- Worker-based architecture
- SignalR real-time updates
- PostgreSQL persistence
- Dockerized environment

---

## Motivation

This project exists mainly as a learning exercise around:

- Concurrency
- Background processing
- Worker patterns
- Scalability
- Async architecture in .NET

A common mistake in these systems is trying to solve performance issues only with faster queries or better hardware. In many cases, the bottleneck is architectural — especially when processing large sequential operations synchronously.

This project explores a different approach.

---

## Author

### Gustavo Poeta

- LinkedIn: https://www.linkedin.com/in/gustavopoeta/

---

<!-- MARKDOWN LINKS -->

[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/gustavopoeta/
```
