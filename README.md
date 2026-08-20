# 🚀 Customer Insight Engine

> An enterprise-grade, microservices-based platform for real-time, multilingual sentiment analysis on customer feedback.

![Demo](<img width="800" height="423" alt="demo" src="https://github.com/user-attachments/assets/9e1c1b7e-3172-4a9a-b789-dcb73f87a0e6" />)

## 📖 Project Overview

**Customer Insight Engine** is a full-stack AI architecture designed to process, analyze, and persist customer reviews. By decoupling the machine learning model from the core business logic and user interface, this project demonstrates a highly scalable **Microservices Architecture**. 

It accurately classifies text as `Positive` or `Negative` and provides a mathematical **Confidence Score** using a custom-trained NLP model, instantly saving the telemetry to a relational database.

## ✨ Key Features

* **🧠 Custom Machine Learning Model:** Trained on a massive 250,000-row Kaggle dataset supporting both English and Turkish languages.
* **🔗 Decoupled Microservices:** Independent operation of the Frontend (React), Backend Orchestrator (C#), and AI Engine (Python).
* **⚡ Asynchronous Communication:** Non-blocking HTTP data flow between the .NET API and the FastAPI service.
* **💾 Data Persistence:** Entity Framework Core integration for robust SQL Server data logging.
* **🎨 Modern UI/UX:** A minimalist, fast, and responsive React web interface powered by Vite.

---

## 🏗️ System Architecture & Data Flow

The platform is divided into three main operational layers that communicate seamlessly:

1. **Client Request:** The user submits a review via the **React** frontend.
2. **API Orchestration:** The **C# .NET Core 9.0 API** catches the payload, validates it, and forwards it to the AI microservice.
3. **AI Inference:** The **Python FastAPI** server processes the text through a serialized Scikit-Learn model (TF-IDF & Logistic Regression) and returns the sentiment label and confidence score.
4. **Database Commit:** The C# API maps the AI response to a domain model and commits the record to **SQL Server** using EF Core.
5. **UI Update:** The React interface immediately reflects the AI's verdict and the unique Database ID.

---

## 🛠️ Technology Stack

### 🖥️ Frontend (Client)
* **React 18** (with Vite for ultra-fast HMR)
* **CSS3** (Custom minimalist dark-mode UI)

### ⚙️ Backend API (Orchestrator)
* **C# .NET Core 9.0**
* **Entity Framework Core 9.0** (Code-First Approach)
* **RESTful Architecture** & Swagger UI

### 🤖 AI Microservice (Engine)
* **Python 3.12**
* **FastAPI** & **Uvicorn** (High-performance ASGI server)
* **Scikit-Learn** & **Pandas** (Data processing and ML)

### 🗄️ Database
* **Microsoft SQL Server**

---

## 🚀 Getting Started (Running Locally)

To run this microservices architecture on your local machine, you need to spin up all three servers simultaneously.

### 1. Start the AI Microservice (Python)
```bash
cd ai_service
uvicorn main:app --reload
```
Runs on http://127.0.0.1:8000
### 2. Start the Backend API (C#)
```bash
cd CustomerInsight.API
dotnet run
```
Runs on http://localhost:5111 (or your configured port)
### 3. Start the Frontend (React)
```bash
cd frontend
npm run dev
```
Runs on http://localhost:5173
