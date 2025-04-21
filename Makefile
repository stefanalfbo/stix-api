SHELL := /bin/bash

.PHONY: help

help:
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(firstword $(MAKEFILE_LIST)) | sort | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-15s\033[0m %s\n", $$1, $$2}'

build: ## Build the STIX API application
	dotnet build

https: ## Create a self-signed certificate for HTTPS
	dotnet dev-certs https -ep aspnetapp.pfx -p password
	dotnet dev-certs https --trust

run-api: ## Run the STIX API application
	dotnet run --launch-profile https --project src/StixApi

run-idp: ## Run the Identity Server
	dotnet run --project src/StixApi.IdentityServer

run-client: ## Run the test client
	dotnet run --project src/StixApi.Client

watch: ## Run the STIX API application with hot reload
	dotnet watch run --launch-profile https --project src/StixApi

test: ## Run the STIX API tests
	dotnet test --no-restore --verbosity normal

migrate: ## Run the database migrations
	dotnet ef database update --project src/StixApi
	@echo "Database migrations completed."

create-jwt: ## Create a new JWT token
	dotnet user-jwts create --project src/StixApi --issuer "iss" --audience "aud" 

create-jwt-admin: ## Create a new JWT token with role admin
	dotnet user-jwts create --project src/StixApi --issuer "iss" --audience "aud" --role "admin"