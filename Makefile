SHELL := /bin/bash

.PHONY: help

help:
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(firstword $(MAKEFILE_LIST)) | sort | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-15s\033[0m %s\n", $$1, $$2}'

build: ## Build the STIX API application
	dotnet build

https: ## Create a self-signed certificate for HTTPS
	dotnet dev-certs https -ep aspnetapp.pfx -p password
	dotnet dev-certs https --trust

run: ## Run the STIX API application
	dotnet run --launch-profile https --project src/StixApi

watch: ## Run the STIX API application with hot reload
	dotnet watch run --launch-profile https --project src/StixApi