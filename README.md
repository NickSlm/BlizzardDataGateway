**Overview**

This project exposes RESTful endpoints that allow authorized users to retrieve specific data stored by the Worker Service.

The API acts as a controlled gateway between clients and the snapshot database.

**Architecture**

Controllers expose specific endpoints

Business logic layer handles data access rules

Entity Framework Core for database interaction

DTOs for safe data exposure

**What Users Can Do**

Depending on granted access, users can:

Retrieve snapshot metadata

Pull character data from specific snapshots

Access filtered or limited datasets

Query historical data

**Data Control**

The API:

Controls which data is exposed

Prevents direct database access

Allows extension for authentication/authorization

Can be extended with role-based permissions

**Tech Stack**

ASP.NET Core Web API

Entity Framework Core

SQL Server / Relational DB

RESTful Architecture

Dependency Injection
